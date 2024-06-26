using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(43325)]
        [Title("cleaning report")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]
        [Defect("1363239")]
        //[TestMethod]

        public void VSTS_43325()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string tare = "10";
            string net = "454.4";
            string xml = "14 aspen wd deviation_43325 bulk load.xml";

            LogStep(@"1. Open WD client and do a booth clean");
            Application.LaunchWDAndLogin();
            WD.mainWindow.HomeInternalFrame.BoothCleaning.Click();
            WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            //get execute time
            DateTime execute_time = DateTime.Now;
            LogStep(@"2. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            LogStep(@"3. Go to clean report");
            Web_Fuction.gotoTab(WDWebTab.report);
            Web.Report_Page.Cleaning.Click();
            LogStep(@"4. set criteria ");
            var Booth = driver.FindElements("//select")[0];
            var Type = driver.FindElements("//select")[1];
            var Operator = driver.FindElements("//select")[2];
            Web.Report_Page.Start_Time.Click();
            driver.FindElement("//button[text()='Zero']").Click();
            driver.Wait();
            Booth.FindElement(By.XPath("//option[text()='booth1']")).Click();
            Type.FindElement(By.XPath("//option[text()='Full Clean']")).Click();
            Operator.FindElement(By.XPath("//option[text()='qaone1(qaone1)']")).Click();
            Web.Report_Page.End_Time.Click();
            driver.FindElement("//button[text()='Now']").Click();
            driver.Wait();
            Web.Report_Page.Generate_Report.Click();
            LogStep(@"5.check report");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Clean Report.PNG");
            var column = new List<string>() { "Booth", "Type", "Operator" };
            var datatext = new List<string>() { "booth1", "Full Clean", "qaone1(qaone1)" };
            //check date
            Web_Fuction.check_report_date(execute_time);
            //check data
            Web_Fuction.Check_report(column, datatext);
            LogStep(@"6.save pdf and print ");
            Thread.Sleep(2000);
            Web.Report_Page.SaveAs.Click();
            Thread.Sleep(2000);
            Web.Report_Page.Print.Click();
            //wait for screenshot and download
            Thread.Sleep(10000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Print Report.PNG");
            driver.FindElement("//button[text()='Close']").Click();
            //The searchList can't contain ' '
            var searchList = new List<string>() { "booth1", "Full", "qaone1(qaone1)" };
            Web_Fuction.Check_PDF(Base_Directory.DownloadFileDir, "*CLEANING*", searchList);
            LogStep(@"7.set booth Unavailable");
            Web_Fuction.gotoTab(WDWebTab.equipment);
            Web_Fuction.edit_booth("booth1");
            Web.Equipment_Page.booth_status.select_option("Unavailable");
            Web.Equipment_Page.Apply.Click();
            LogStep(@"8.active order and start weight,error shows");
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            //select order and material
            WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            Thread.Sleep(2000);
            WD.mainWindow.DispensingInternalFrame.orderTable.Row(order).Click();
            WD.mainWindow.DispensingInternalFrame.next.Click();
            WD.mainWindow.MaterialInternalFrame.materialTable.Row(material).Click();
            WD.mainWindow.MaterialInternalFrame.next.Click();
            Thread.Sleep(5000);
            //WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            //check error
            WD.mainWindow.GetSnapshot(Resultpath + "clean booth error.PNG");
            Base_Assert.AreEqual(WD.MessageDialog.Lable.Text, "Run booth clean rule engine failed. The reason is: Unavailable");
            WD.MessageDialog.OKButton.Click();
            LogStep(@"9.Change deviation");
            //import xml
            WD_Fuction.Bulkload(xml);
            WD_Fuction.WDSign();
            LogStep(@"10.check deviation and finish dispense");
            //WD.mainWindow.BoothCleanInternalFrame.HomeButton.Click();
            //WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            //WD.mainWindow.DispensingInternalFrame.orderTable.Row(order).Click();
            // WD.mainWindow.DispensingInternalFrame.next.Click();
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.orderTable.Row(order).Click();
            WD.mainWindow.DispensingInternalFrame.next.Click();
            WD.mainWindow.MaterialInternalFrame.materialTable.Row(material).Click();
            WD.mainWindow.MaterialInternalFrame.next.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "WDDeviation-1.PNG");
            WD.DeviationDialog.Password.SetText(PassWord.qaone1);
            WD.DeviationDialog.OK.Click();
            //finish dispense
            WD_Fuction.SelectMehod(method, barcode);
            WD_Fuction.FinishNetDiapense(tare, net);
            driver.Close();
            WD_Fuction.Close();
        }
    }
}
