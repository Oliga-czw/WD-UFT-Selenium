using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(37386)]
        [Title("CQ00613330_WD execution time out automatically")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Low)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_37386()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string Path = Base_Directory.ConfigDir + "path.m2r_cfg";

            string ConfigKey1 = @"INACTIVITY_PERIOD = 60";
            string ConfigKey2 = @"INACTIVITY_PERIOD = 300";

            LogStep(@"1. change timeout time");
            Base_Function.EditConfigKey(Path, ConfigKey1);
            //codify all
            Base_Test.LaunchApp(Base_Directory.Codify_all);
            Thread.Sleep(30000);
            try { 
                LogStep(@"2. Open Wd client");
                Application.LaunchWDAndLogin();
                LogStep(@"5.wait for time out");
                Thread.Sleep(120000);
                WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
                Thread.Sleep(1000);
                WD.mainWindow.GetSnapshot(Resultpath + "tiem out.PNG");
                Base_Assert.AreEqual("Your session has timed out. Please log in again to resume.", WD.SessionTimedOutDialog.Lable.Text,"Time out");
                WD.SessionTimedOutDialog.OKButton.Click();
                Thread.Sleep(1000);
                WD.mainWindow.Dialog.Cancel.Click();
                Thread.Sleep(1000);
                WD.CloseDialog.YesButton.Click();
            }
            finally
            {
                LogStep(@"3. restone data");
                Base_Function.EditConfigKey(Path, ConfigKey2);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                Thread.Sleep(30000);
            }
        }

    }
}