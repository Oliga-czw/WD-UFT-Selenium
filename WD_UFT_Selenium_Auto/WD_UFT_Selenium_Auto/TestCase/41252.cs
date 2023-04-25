using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(41252)]
        [Title("audit permissions report")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Low)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_41252()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string xml = "10 aspen wd signautres_41252 bulk load.xml";
            string change_reason = "permission report test";



            LogStep(@"1. import signature xml");
            WD_Fuction.Bulkload(xml);
            WD_Fuction.WDSign();
            LogStep(@"2. change the permission");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();

            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Permissions.Click();
            //change 'permission' 
            driver.FindElement("//div[text()='User Exits ']/../..//input").Click();
            Web.Administration_Page.Apply.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "permission signature.PNG");
            //input passwords
            var inputs = Web.Equipment_Page.FindElements("//input[@class='Input_TextBox_Style']");
            inputs[1].SendKeys(PassWord.qaone1);
            Thread.Sleep(2000);
            //input reason
            Web.Equipment_Page.FindElement("//textarea[@class='DialogTextArea']").SendKeys(change_reason);
            Web.Equipment_Page.FindElement("//button[text()='OK']").Click();
            //get execute time
            DateTime execute_time = DateTime.Now;
            //check apply successfully
            string message = driver.FindElement("//div[@class='gwt-Label Alert_Label']").Text;
            Base_Assert.AreEqual("Apply Permission Successful", message, "apply successfully");
            driver.Wait();
            driver.FindElement("//button[text()='OK']").Click();
            LogStep(@"3. Go to permission report");
            Web_Fuction.gotoTab(WDWebTab.report);
            Thread.Sleep(3000);
            //check audit expanded or not
            if (Web.Report_Page.Permissions._Selenium_WebElement.Displayed == false)
            {
                driver.FindElement("//div[text()='Audits']").Click();
            }
            Web.Report_Page.Permissions.Click();
            LogStep(@"4. set criteria ");
            var User = driver.FindElements("//select")[2];
            var Permission = driver.FindElements("//select")[0];
            var Role = driver.FindElements("//select")[1];
            Web.Report_Page.Start_Time.Click();
            driver.FindElement("//button[text()='Zero']").Click();
            driver.Wait();
            User.FindElement(By.XPath("//option[text()='qaone1(qaone1)']")).Click();
            Permission.FindElement(By.XPath("//option[text()='Administration']")).Click();
            Role.FindElement(By.XPath("//option[text()='Production Execution Administrator']")).Click();
            Web.Report_Page.End_Time.Click();
            driver.FindElement("//button[text()='Now']").Click();
            driver.Wait();
            //click romove input
            driver.FindElement("//label[text()='Remove from Role']/../input").Click();
            Web.Report_Page.Generate_Audit.Click();
            LogStep(@"5.check report");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "permission Report.PNG");
            //check date
            Web_Fuction.check_audit_date(execute_time);
            var column = new List<string>() { "User", "Reason for change" };
            var datatext = new List<string>() { "qaone1(qaone1)", change_reason };
            Web_Fuction.Check_audit(column, datatext);
            Web.Report_Page.difference.Click();
            var column2 = new List<string>() { "Permission", "Action", "Role" };
            var datatext2 = new List<string>() { "User Exits -Modify", "Remove from Role", "Production Execution Administrator" };
            Web_Fuction.Check_audit_difference(column2, datatext2);
            Thread.Sleep(2000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "permission difference Report.PNG");
            // print not write,check it manual
            LogStep(@"6.delete user and check report");
            //delete user qaone1
            Application.LaunchAFW();
            string role = AFWRole.Admin;
            string accout = UserName.qaone1;
            AFW_Fuction.removeRole(role, accout);
            AFW_Fuction.closeAFW();
            //check report
            Web.Report_Page.ScaleCheck.Click();
            Web.Report_Page.Permissions.Click();
            var User2 = driver.FindElements("//select")[2];
            User2.FindElement(By.XPath("//option[text()='qaone1(qaone1)']")).Click();
            Web.Report_Page.Generate_Audit.Click();
            var row = Web.Report_Page.body._Selenium_WebElement.FindElements(By.XPath("//td[@class='Inner_Column_Left']/.."));
            Base_Assert.IsTrue(row.Count > 0, "records still display with the user");
            //add user qaone1
            Application.LaunchAFW();
            AFW_Fuction.addRole(role, accout);
            AFW_Fuction.closeAFW();
            driver.Close();
        }
    }
}
