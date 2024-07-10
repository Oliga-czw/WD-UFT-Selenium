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
        [TestCaseID(40713)]
        [Title("audit signatures report")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]
        
        [TestMethod]
        public void VSTS_40713()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string operater = "Booth cleaning";
            

            string xml = "10 aspen wd signautres_40713 bulk load.xml";
            LogStep(@"1. import Signature xml");
            WD_Fuction.Bulkload(xml);
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            LogStep(@"2. go to signatures report");
            Web_Fuction.gotoTab(WDWebTab.report);
            //check audit expanded or not
            if (Web.Report_Page.Signatures._Selenium_WebElement.Displayed == false)
            {
                driver.FindElement("//div[text()='Audits']").Click();
            }
            Web.Report_Page.Signatures.Click();
            LogStep(@"3. set criteria ");
            var User = driver.FindElements("//select")[1];
            var Operator = driver.FindElements("//select")[0];

            Web.Report_Page.Start_Time.Click();
            driver.FindElement("//button[text()='Zero']").Click();
            driver.Wait();
            Operator.FindElement(By.XPath($"//option[text()='{operater}']")).Click();
            Thread.Sleep(2000);
            User.FindElement(By.XPath($"//option[text()='{userNameforReport.qaone1}']")).Click();
            Web.Report_Page.End_Time.Click();
            driver.FindElement("//button[text()='Now']").Click();
            driver.Wait();

            Web.Report_Page.Generate_Audit.Click();
            LogStep(@"4.check report");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "signatures Report.PNG");
            var column = new List<string>() { "User" };
            var datatext = new List<string>() { userNameforReport.qaone1 };
            Web_Fuction.Check_audit(column, datatext);
            //difference
            Web.Report_Page.difference.Click();
            var column2 = new List<string>() { "Operation", "Old", "New" };
            var datatext2 = new List<string>() { "Booth cleaning-Signature Setting", "No Signature", "Single Signature" };
            Web_Fuction.Check_audit_difference(column2, datatext2);
            Thread.Sleep(2000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "signatures difference Report.PNG");
            // print not write,check it manual
            driver.Close();
        }
    }
}
