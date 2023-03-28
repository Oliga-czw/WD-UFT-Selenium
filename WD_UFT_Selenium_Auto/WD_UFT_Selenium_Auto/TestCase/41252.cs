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
            //Application.LaunchMocAndLogin();
            //MOC.MocmainWindow.Audit_moudle.ClickSignle();
            //MOC.MOCAuditWindow.Users_Failures.ClickSignle();
            var a = MOC.MOCAuditWindow.LoginFailureInterFrame.auditTable.Rowscount();
            Console.WriteLine(a);
            MOC.MOCAuditWindow.LoginFailureInterFrame.auditTable.SelectRows(a - 1);
            var b = MOC.MOCAuditWindow.LoginFailureInterFrame.auditTable.GetCell(a - 1, "Module").Value;
            var c = MOC.MOCAuditWindow.LoginFailureInterFrame.auditTable.GetCell(a - 1, "Reason").Value;
            Console.WriteLine(b);
            Console.WriteLine(c);

        }
    }
}
