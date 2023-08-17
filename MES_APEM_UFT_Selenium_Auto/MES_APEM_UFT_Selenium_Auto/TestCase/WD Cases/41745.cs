using HP.LFT.SDK;
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
            string message = @"Username or password is incorrect.";

            LogStep(@"1. Open WD client");
            Base_Test.LaunchApp(Base_Directory.WDDir);
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            LogStep(@"2. input wrong userid/password");
            Base_Test.Login(@"qae\qaon8", "Assss889");
            Thread.Sleep(20000);
            LogStep(@"3. Can not login,error message will pop up");
            WD.mainWindow.GetSnapshot(Resultpath + "error.PNG");
            Base_Assert.AreEqual(message, WD.MessageDialog.Lable.Text, "error in wd client");
            WD.MessageDialog.OKButton.Click();
            WD_Fuction.Close();
            LogStep(@"4. open Audit to check error");
            Application.LaunchMocAndLogin();
            APEM.MocmainWindow.Audit_moudle.ClickSignle();
            APEM.MOCAuditWindow.Users_Failures.ClickSignle();
            var a = APEM.MOCAuditWindow.LoginFailureInterFrame.auditTable.Rowscount();
            APEM.MOCAuditWindow.LoginFailureInterFrame.auditTable.SelectRows(a - 1);
            Thread.Sleep(2000);
            APEM.MOCAuditWindow.GetSnapshot(Resultpath + "Audit result.PNG");
            var b = APEM.MOCAuditWindow.LoginFailureInterFrame.auditTable.GetCell(a - 1, "Module").Value;
            var c = APEM.MOCAuditWindow.LoginFailureInterFrame.auditTable.GetCell(a - 1, "Reason").Value;
            Base_Assert.AreEqual("WDWorkstation", b, "in audit");
            Base_Assert.AreEqual(message, c, "in audit");
            MOC_Fuction.AuditClose();
            MOC_Fuction.MocClose();
        }
    }
}
