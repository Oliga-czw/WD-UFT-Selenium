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
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(29461)]
        [Title("Administration General: Execution System setting")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_29461()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;

            LogStep(@"1. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            LogStep(@"2. Go and admin and General");
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.General.Click();
            LogStep(@"3.check Log on required for Execution system, and click 'Apply' button.");

            if (Web.Administration_Page.log_on_required_chx.GetAttribute("checked") is null)
            {
                Web.Administration_Page.log_on_required_chx.Click();
                Web_Fuction.administration_Apply("Configuration successfully saved");
            }
            LogStep(@"4. Open Wd client");
            Thread.Sleep(2000);
            Base_Test.LaunchApp(Base_Directory.WDDir);
            WD.mainWindow.GetSnapshot(Resultpath + "logon_required.PNG");
            Base_Assert.IsTrue(WD.mainWindow.LogonInternalFrame.IsEnabled);
            LogStep(@"5.close the WD client");
            WD.ExitApplication();
            LogStep(@"6.do not check Log on required for Execution system, and click 'Apply' button.");
            Web.Administration_Page.log_on_required_chx.Click();
            Web_Fuction.administration_Apply("Configuration successfully saved");
            LogStep(@"7. Open Wd client");
            Base_Test.LaunchApp(Base_Directory.WDDir);
            Thread.Sleep(2000);
            WD.mainWindow.GetSnapshot(Resultpath + "logon_without_username.PNG");
            Base_Assert.IsTrue(WD.mainWindow.HomeInternalFrame.IsEnabled);
            WD_Fuction.Close();
            LogStep(@"8. restone data");
            Web.Administration_Page.log_on_required_chx.Click();
            Web_Fuction.administration_Apply("Configuration successfully saved");
            driver.Close();
        }

    }
}