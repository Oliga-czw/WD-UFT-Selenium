using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(45750)]
        [Title("scale check-The scale information are disabled on Test Scale Connectivity page")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_45750()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Thread.Sleep(2000);
            Web.Administration_Page.Permissions.Click();
            driver.FindElement("//label[text()='Add+modify']/../../../td[2]/span/input").Click();
            Web.Administration_Page.Apply.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "without_permission.PNG");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.ScaleChecking.Click();
            Thread.Sleep(2000);
            WD.mainWindow.ScaleCheckInternalFrame.testScale.Click();
            Thread.Sleep(2000);
            WD.mainWindow.TestScaleInternalFrame.RangeMin.SendKeys("2");
            WD.mainWindow.GetSnapshot(Resultpath + "Apply_disabled.PNG");
            Base_Assert.IsFalse(WD.mainWindow.TestScaleInternalFrame.Apply.IsEnabled);
        }

       
    }
}