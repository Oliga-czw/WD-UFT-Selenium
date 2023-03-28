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
        [TestCaseID(41745)]
        [Title("CQ00689708_Login to WD execution using wrong userid/password fail and event will log into audit trail")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Low)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_41745()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string message = @"Invalid username or password. Please re-enter domain\username and password.";

            LogStep(@"1. Open WD web");
           
            LogStep(@"2. input wrong userid/password");
           
            
            LogStep(@"3. Can not login,error message will pop up");
            
            LogStep(@"4. open Audit to check error");
            Application.LaunchMocAndLogin();
            MOC.MocmainWindow.Audit_moudle.ClickSignle();
            MOC.MOCAuditWindow.Users_Failures.ClickSignle();
            var a = MOC.MOCAuditWindow.LoginFailureInterFrame.auditTable.Rowscount();
            MOC.MOCAuditWindow.LoginFailureInterFrame.auditTable.SelectRows(a - 1);
            Thread.Sleep(2000);
            MOC.MOCAuditWindow.GetSnapshot(Resultpath + "Audit result.PNG");
            var b = MOC.MOCAuditWindow.LoginFailureInterFrame.auditTable.GetCell(a - 1, "Module").Value;
            var c = MOC.MOCAuditWindow.LoginFailureInterFrame.auditTable.GetCell(a - 1, "Reason").Value;
            Base_Assert.AreEqual("WDServer", b, "in audit");
            Base_Assert.AreEqual(message, c, "in audit");
            MOC_Fuction.AuditClose();
            MOC_Fuction.MocClose();
        }
    }
}
