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
        [TestCaseID(912651)]
        [Title("UC859545_APEM Mobile: BP execution in Mobile")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        //defect 1323665
        //[TestMethod]
        public void VSTS_912651()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Application.LaunchMocAndLogin();
            MOC_TemplatesFunction.Importtemplates("912651.zip");
            APEM.MocmainWindow.Config_moudle.Click();
            Thread.Sleep(5000);
            APEM.MOCConfigWindow.Export.ClickSignle();
            Thread.Sleep(2000);
            APEM.MOCConfigWindow.ConfigExportDialog.HomeButton.ClickSignle();
            APEM.MOCConfigWindow.ConfigExportDialog.FileName.SetText("EN912651");
            APEM.MOCConfigWindow.ConfigExportDialog.ExportToFileButton.ClickSignle();
            string filePath = "C:\\Users\\qaone1\\Desktop\\EN912651.ini";
            string newData = "# Executable BPs in Mobile\r\nWEB_EXECUTABLE_5 = BPL912651.CREATE\r\nWEB_EXECUTABLE_7 = BPL912651.IMPORT\r\nWEB_EXECUTABLE_6 = BPL912651.IMPORT2\r\nWEB_EXECUTABLE_8 = BPL912651.HAHAHHAH";
            string newData1 = "WEB_EXECUTABLE_5 = BPL912651.CREATE\r\nWEB_EXECUTABLE_7 = BPL912651.IMPORT\r\nWEB_EXECUTABLE_6 = BPL912651.IMPORT2\r\nWEB_EXECUTABLE_8 = BPL912651.HAHAHHAH";
            string iniContent = File.ReadAllText(filePath);
            string searchString = "# Executable BPs in Mobile\r\n";
            bool contains = iniContent.Contains(searchString);
            using (StreamWriter sw = new StreamWriter(filePath, true))

            {
                if (contains == false)
                {
                    sw.WriteLine(newData);
                }
                else
                {
                    if (iniContent.Contains("WEB_EXECUTABLE_5") == false)
                    {
                        sw.WriteLine(newData1);
                    }
                }

            }
            Console.WriteLine("数据已成功写入.ini文件末尾。");
            APEM.MOCConfigWindow.Import_ReplaceMerge.ClickSignle();
            APEM.MOCConfigWindow.ConfigImportDialog.FileName.SendKeys(filePath);
            MOC_Fuction.ConfigClose();
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            Thread.Sleep(5000);
            Mobile.Main_Page.BPList.Click();
            Thread.Sleep(3000);
            Mobile.BPList_Page.BPSearch.SendKeys("BPL912651");
            Thread.Sleep(3000);
            //Select one ready BP and execute it
            Mobile.BPList_Page.BPListTableRows[0].FindElement(By.TagName("mat-icon")).Click();
            Thread.Sleep(5000);
            //Return to the BP table during executing BP
            Mobile.Main_Page.BPList.Click();
            Thread.Sleep(3000);
            Mobile.BPList_Page.BPSearch.SendKeys("BPL912651");
            Thread.Sleep(3000);
            var BP_Name = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[1].Text;
            var state = Mobile.BPList_Page.BPListTableRows[0].FindElement(By.TagName("mat-icon")).GetAttribute("data-mat-icon-name");
            Console.WriteLine(state);
            Assert.AreEqual(state, "phase_state_executing");
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "BP_Executing.PNG");
            Mobile.BPList_Page.BPQueueButton.Click();
            var QueueBpname = Mobile.BPList_Page.BPQueuebpName._Selenium_WebElement.Text;
            Assert.AreEqual(BP_Name, QueueBpname);
            //Finish executing the BP and check BP table
            Mobile.BPList_Page.BPQueueExecut.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.OKButton.Click();
            Thread.Sleep(5000);
            Mobile.BPList_Page.BPSearch.SendKeys("BPL912651");
            Thread.Sleep(3000);
            var state1 = Mobile.BPList_Page.BPListTableRows[0].FindElement(By.TagName("mat-icon")).GetAttribute("data-mat-icon-name");
            Console.WriteLine(state1);
            Assert.AreEqual(state1, "phase_state_enabled");
            Assert.AreEqual(Mobile.BPList_Page.BPQueueButton.GetAttribute("disabled"), "true");
            //When in dark mode
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOn_mode(1);
            Mobile.Main_Page.BPList.Click();
            Thread.Sleep(3000);
            Mobile.BPList_Page.BPSearch.SendKeys("BPL912651");
            Thread.Sleep(3000);
            //Select one ready BP and execute it
            Mobile.BPList_Page.BPListTableRows[0].FindElement(By.TagName("mat-icon")).Click();
            Thread.Sleep(5000);
            //Return to the BP table during executing BP
            Mobile.Main_Page.BPList.Click();
            Thread.Sleep(3000);
            Mobile.BPList_Page.BPSearch.SendKeys("BPL912651");
            Thread.Sleep(3000);
            var BP_Name2 = Mobile.BPList_Page.BPListTableRows[0].FindElements(By.TagName("td"))[1].Text;
            var state2 = Mobile.BPList_Page.BPListTableRows[0].FindElement(By.TagName("mat-icon")).GetAttribute("data-mat-icon-name");
            Console.WriteLine(state2);
            Assert.AreEqual(state2, "phase_state_executing");
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "dark_BP_Executing.PNG");
            Mobile.BPList_Page.BPQueueButton.Click();
            var QueueBpname2 = Mobile.BPList_Page.BPQueuebpName._Selenium_WebElement.Text;
            Assert.AreEqual(BP_Name2, QueueBpname2);
            //Finish executing the BP and check BP table
            Mobile.BPList_Page.BPQueueExecut.Click();
            Thread.Sleep(2000);
            Mobile.OrderExecution_Page.OKButton.Click();
            Thread.Sleep(5000);
            Mobile.BPList_Page.BPSearch.SendKeys("BPL912651");
            Thread.Sleep(3000);
            var state3 = Mobile.BPList_Page.BPListTableRows[0].FindElement(By.TagName("mat-icon")).GetAttribute("data-mat-icon-name");
            Console.WriteLine(state3);
            Assert.AreEqual(state3, "phase_state_enabled");
            Assert.AreEqual(Mobile.BPList_Page.BPQueueButton.GetAttribute("disabled"), "true");
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOff_mode(1);
            driver.Close();
        }
    }
}