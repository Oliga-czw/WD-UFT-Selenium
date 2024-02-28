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
        [TestCaseID(935165)]
        [Title("UC859545_APEM Mobile: Check search function on BP table in APEM Mobile")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_935165()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";

            string BPName = "import";

            LogStep(@"1. import bpl");//import bpl
            Application.LaunchMocAndLogin();
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("BPL912651").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("912651.zip");
            }
            LogStep(@"2. add bp in environment");
            APEM.MocmainWindow.Config_moudle.Click();
            Thread.Sleep(5000);
            APEM.MOCConfigWindow.Export.ClickSignle();
            Thread.Sleep(2000);
            APEM.MOCConfigWindow.ConfigExportDialog.HomeButton.ClickSignle();
            APEM.MOCConfigWindow.ConfigExportDialog.FileName.SetText("EN935165");
            APEM.MOCConfigWindow.ConfigExportDialog.ExportToFileButton.ClickSignle();
            //if exit file
            if (APEM.ConfirmFileReplaceDialog.IsExist())
            {
                APEM.ConfirmFileReplaceDialog.YesButton.Click();
            }
            string filePath = "C:\\Users\\qaone1\\Desktop\\EN935165.ini";
            string newData = "# Executable BPs in Mobile\r\nWEB_EXECUTABLE_5 = BPL912651.CREATE\r\nWEB_EXECUTABLE_7 = BPL912651.IMPORT\r\nWEB_EXECUTABLE_6 = BPL912651.IMPORT2\r\nWEB_EXECUTABLE_8 = BPL912651.HAHAHHAH";
            string newData1 = "WEB_EXECUTABLE_5 = BPL912651.CREATE\r\nWEB_EXECUTABLE_7 = BPL912651.IMPORT\r\nWEB_EXECUTABLE_6 = BPL912651.IMPORT2\r\nWEB_EXECUTABLE_8 = BPL912651.HAHAHHAH";
            string iniContent = File.ReadAllText(filePath);
            string searchString = "# Executable BPs in Mobile\r\n";
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
            APEM.ExitApplication();
            LogStep(@"3. Login mobile");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            Thread.Sleep(5000);
            Mobile.Main_Page.BPList.Click();
            Thread.Sleep(3000);
            //check bp search 
            Mobile.BPList_Page.BPSearch.SendKeys(BPName);
            Thread.Sleep(3000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "BP_Search.PNG");
            //count
            Base_Assert.IsTrue(Mobile.BPList_Page.BPListTableRows.Count==2,"search result");
            //text
            int l = 0;
            List<string> head_name = new List<string> { "BP name" };
            foreach (var head in Mobile.BPList_Page.BPListTableHeads)
            {
                //check result is right
                if (head_name.Contains(head.Text))
                {
                    foreach (var tr in Mobile.BPList_Page.BPListTableRows)
                    {
                        var td = tr.FindElements(By.TagName("td"))[l];
                        Console.WriteLine(td.Text);
                        Assert.IsTrue(td.Text.ToLower().Contains(BPName));
                    }
                }
                l++;
            }
            //check search word is bold and yellow
            var strongs = driver.FindElements("//strong");
            foreach (var strong in strongs)
            {
                string color = strong.GetAttribute("style");
                Base_Assert.IsTrue(color.Contains("yellow"), "strong color");
                Base_Assert.IsTrue(strong.Text.ToLower().Equals(BPName, StringComparison.OrdinalIgnoreCase), " strong text");
            }

            driver.Close();

        }

    }
}