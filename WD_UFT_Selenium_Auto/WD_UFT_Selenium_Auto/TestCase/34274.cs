using System;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(34274)]
        [Title("WD: should detect the javaw.exe process, if there's already one should pop up warning message (from  CQ00548238)")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_34274()
        {

            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. Open Wd client and login");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            LogStep(@"2. ReOpen Wd client");
            Base_Test.LaunchApp(Base_Directory.WDDir);
            var WarningMessage = WD.reopenMessageDialog.Lable.Text;
            WD.reopenMessageDialog.GetSnapshot(Resultpath + "Reopen.PNG");
            //Base_Assert.IsTrue(WarningMessage.Contains("Module is currently executed by qae\qaone1"));
            Base_Assert.AreEqual(WarningMessage,"Module is currently executed by qae\\qaone1");
            WD.reopenMessageDialog.OKButton.Click();
            WD_Fuction.Close();
        }


    }
}