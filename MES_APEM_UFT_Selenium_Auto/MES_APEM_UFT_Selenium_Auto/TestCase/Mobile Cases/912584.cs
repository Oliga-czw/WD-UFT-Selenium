using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using OpenQA.Selenium;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;
using System.Linq;
using System.IO;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class Mobile_TestCase
    {
        [TestCaseID(912584)]
        [Title("UC859545_APEM Mobile: Add BPs and check BP table in Mobile")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]
        //defect 1323665
        //[TestMethod]
        public void VSTS_912584()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            Thread.Sleep(5000);
            Base_Assert.IsTrue(Mobile.Main_Page.BPList._Selenium_WebElement.Displayed);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "BPListDisplayed.PNG");
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOn_mode(2);
            Base_Assert.IsTrue(Mobile.Main_Page.BPList._Selenium_WebElement.Displayed);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "BPListDisplayed_Consolidated.PNG");
            Mobile.Setting_Page.turnOff_mode(2);
            Mobile.Main_Page.BPList.Click();
            Thread.Sleep(3000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "BPList_NoData.PNG");
            Base_Assert.AreEqual(Mobile.BPList_Page.BPListTable._Selenium_WebElement.Size.Height, 0);
            Application.LaunchMocAndLogin();
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("BPL912584_1").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP912584.zip");
            }
            APEM.MocmainWindow.Config_moudle.Click();
            Thread.Sleep(5000);
            APEM.MOCConfigWindow.Export.ClickSignle();
            Thread.Sleep(2000);
            APEM.MOCConfigWindow.ConfigExportDialog.HomeButton.ClickSignle();
            APEM.MOCConfigWindow.ConfigExportDialog.FileName.SetText("EN912584");
            APEM.MOCConfigWindow.ConfigExportDialog.ExportToFileButton.ClickSignle();
            //if exit file
            if (APEM.ConfirmFileReplaceDialog.IsExist())
            {
                APEM.ConfirmFileReplaceDialog.YesButton.Click();
            }
            string filePath = "C:\\Users\\qaone1\\Desktop\\EN912584.ini";
            string newData = "#Executable BPs in Mobile\r\nWEB_EXECUTABLE_3 = BPL912584_1.CREATE\r\nWEB_EXECUTABLE_1 = BPL912584_2.BP_NONWEB\r\nWEB_EXECUTABLE_4 = BPL912584_3.BP_NOCERTIFY\r\nWEB_EXECUTABLE_2 = BPL912584_3.BP002";
            string newData1 = "WEB_EXECUTABLE_3 = BPL912584_1.CREATE\r\nWEB_EXECUTABLE_1 = BPL912584_2.BP_NONWEB\r\nWEB_EXECUTABLE_4 = BPL912584_3.BP_NOCERTIFY\r\nWEB_EXECUTABLE_2 = BPL912584_3.BP002";
            string iniContent = File.ReadAllText(filePath);
            string searchString = "#Executable BPs in Mobile\r\n";
            bool contains = iniContent.Contains(searchString);
            if (contains)
            {
                var lines = File.ReadAllLines(filePath);
                // check last line blank 
                if (string.IsNullOrWhiteSpace(lines.Last()))
                {
                    // delete last line 
                    lines = lines.Take(lines.Length - 1).ToArray();
                    // rewrite  
                    File.WriteAllLines(filePath, lines);
                }
            }
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                if (contains == false)
                {
                    sw.WriteLine(newData);
                }
                else
                {
                    if (iniContent.Contains("WEB_EXECUTABLE_1") == false)
                    {
                        sw.WriteLine(newData1);
                    }
                }

            }
            Console.WriteLine("数据已成功写入.ini文件末尾。");
            APEM.MOCConfigWindow.Import_ReplaceMerge.ClickSignle();
            APEM.MOCConfigWindow.ConfigImportDialog.FileName.SendKeys(filePath);
            MOC_Fuction.ConfigClose();
            Thread.Sleep(2000);
            Mobile.BPList_Page.BPSearch.SendKeys("BPL912584");
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "ExecutableBPs.PNG");
            var BPLName = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[0].Text;
            var BP_Name = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[1].Text;
            var Version = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[2].Text;
            var Description = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[3].Text;
            Base_Assert.AreEqual(BPLName, "BPL912584_1");
            Base_Assert.AreEqual(BP_Name, "CREATE");
            Base_Assert.AreEqual(Version, "2");
            Base_Assert.AreEqual(Description, "test");
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOn_mode(1);
            Mobile.Main_Page.BPList.Click();
            Thread.Sleep(3000);
            Mobile.BPList_Page.BPSearch.SendKeys("BPL912584");
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Dark_ExecutableBPs.PNG");
            var BPLName1 = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[0].Text;
            var BP_Name1 = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[1].Text;
            var Version1 = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[2].Text;
            var Description1 = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[3].Text;
            Base_Assert.AreEqual(BPLName1, "BPL912584_1");
            Base_Assert.AreEqual(BP_Name1, "CREATE");
            Base_Assert.AreEqual(Version1, "2");
            Base_Assert.AreEqual(Description1, "test");
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOff_mode(1);
            driver.Close();
        }
    }
}