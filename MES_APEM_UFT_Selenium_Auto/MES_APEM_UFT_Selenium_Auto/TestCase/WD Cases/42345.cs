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
        [TestCaseID(42345)]
        [Title("label reprint report")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]
        //defect 1324998
        //[TestMethod]
        public void VSTS_42345()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test2";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string tare = "15";
            string net = "459.4";

            LogStep(@"1. Active orders");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(6000);
            Web_Fuction.active_order(order);
            LogStep(@"2. Open WD client and finish weight");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            WD_Fuction.FinishNetDiapense(tare, net);
            WD_Fuction.Close();
            LogStep(@"3. select a label to reprint");
            Web.Order_Page.ReprintLable.Click();
            //select a label
            driver.FindElements("//tr[@class='dialogMiddle']//input")[0].Click();
            Web.Order_Page.ReprintContainerLabel.Click();
            driver.FindElement("//div[text()='Microsoft Print to PDF']").Click();
            driver.FindElement("//button[text()='OK']").Click();
            //get execute time
            DateTime execute_time = DateTime.Now;
            Console.WriteLine(execute_time);
            //check reprint successfully
            string message = Web.Order_Page.Message._Selenium_WebElement.FindElement(By.XPath("//div[@class='gwt-Label Alert_Label']")).Text;
            Base_Assert.AreEqual("Label was sent for printing successfully!", message, "reprint successfully");
            Web.Order_Page.Message._Selenium_WebElement.FindElement(By.XPath("//button[text()='OK']")).Click();
            //close reprint window
            Web.Order_Page.ReprintLableClose.Click();
            LogStep(@"4. go to reprint label report");
            Web_Fuction.gotoTab(WDWebTab.report);
            Web.Report_Page.LabelReprint.Click();
            LogStep(@"4. set criteria ");
            var Type = driver.FindElements("//select")[0];
            var Operator = driver.FindElements("//select")[1];
            var Material = driver.FindElements("//input[@class='WD_TextBox']")[0];
            var Order = driver.FindElements("//input[@class='WD_TextBox']")[1];
            Web.Report_Page.Start_Time.Click();
            driver.FindElement("//button[text()='Zero']").Click();
            driver.Wait();
            Type.FindElement(By.XPath("//option[text()='Container']")).Click();
            Operator.FindElement(By.XPath($"//option[text()='{userNameforReport.qaone1})']")).Click();
            Web.Report_Page.End_Time.Click();
            driver.FindElement("//button[text()='Now']").Click();
            driver.Wait();
            Material.SendKeys(material);
            Order.SendKeys(order);
            Web.Report_Page.Generate_Report.Click();
            LogStep(@"5.check report");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Reprint Label Report.PNG");
            var column = new List<string>() { "MaterialID", "Type", "Operator","Order" };
            var datatext = new List<string>() { "X0125", "Container", userNameforReport.qaone1, "test2" };
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
            var searchList = new List<string>() { "Container", userNameforReport.qaone1, "test2" };
            Web_Fuction.Check_PDF(Base_Directory.DownloadFileDir, "*LABELREPRINT*", searchList);
            driver.Close();
        }
    }
}
