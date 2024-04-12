using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(34735)]
        [Title("CQ00689708_Login to WD administration using wrong userid/password fail and event will log into audit trail")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Low)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        //defect audit
        [TestMethod]
        public void VSTS_34735()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string message = @"Invalid username or password. Please re-enter domain\username and password.";

            LogStep(@"1. Open WD web");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            LogStep(@"2. input wrong userid/password");
            Web.Login_Page.username.SendKeys(@"qae\qaon8");
            Web.Login_Page.password.SendKeys("Assss889");
            Web.Login_Page.login.Click();
            Thread.Sleep(20000);
            LogStep(@"3. Can not login,error message will pop up");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Login error.PNG");
            Base_Assert.AreEqual(message, Web.Login_Page.error._Selenium_WebElement.Text, "error on web");
            driver.Close();
            LogStep(@"4. open Audit to check error");
            Application.LaunchMocAndLogin();
            APEM.MocmainWindow.Audit_moudle.ClickSignle();
            APEM.MOCAuditWindow.Users_Failures.ClickSignle();
            Thread.Sleep(2000);

            var a = APEM.MOCAuditWindow.LoginFailureInterFrame.auditTable.Rowscount();
            APEM.MOCAuditWindow.LoginFailureInterFrame.auditTable.SelectRows(a - 1);
            Thread.Sleep(2000);
            APEM.MOCAuditWindow.GetSnapshot(Resultpath + "Audit result.PNG");
            var b = APEM.MOCAuditWindow.LoginFailureInterFrame.auditTable.GetCell(a - 1, "Module").Value;
            var c = APEM.MOCAuditWindow.LoginFailureInterFrame.auditTable.GetCell(a - 1, "Reason").Value;
            Base_Assert.IsTrue("WDServer" == b.ToString() ||"Aspen WD Web Service" == b.ToString(), "in audit");
            Base_Assert.AreEqual(message, c, "in audit");
            MOC_Fuction.AuditClose();
            MOC_Fuction.MocClose();
        }
    }
}
