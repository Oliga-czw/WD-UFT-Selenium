using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections;
using HP.LFT.SDK.Java;
using HP.LFT.Verifications;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(41964)]
        [Title("scale check-cancel when not all weights are checked.")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_41964()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. Login WD execution, click 'Scale check'");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.ScaleChecking.Click();
            Thread.Sleep(3000);
            var scaleList = WD.mainWindow.ScaleCheckInternalFrame.ScaleList;
            scaleList.SelectItems("simulator");
            var standardizationStatusTable = WD.mainWindow.ScaleCheckInternalFrame.Standardization_type;
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.Click();
            Thread.Sleep(2000);

            LogStep(@"2.with plate empty, click Zero button");
            WD.mainWindow.CheckWeightInternalFrame.zero.Click();
            
            LogStep(@"3.the weight is out of Precision range(weight<95 or weight >105)");
            WD.SimulatorWindow.weight.SetText("400");
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "WD_Scalcheck.PNG");
            //var now_time = DateTime.Now.ToString("yyyy/M/d tth:mm:ss");
            WD.mainWindow.CheckWeightInternalFrame.readScale.ClickSignle();
            var now_time = DateTime.Now.ToString("yyyy/M/d tth:mm:ss");
            // no message
            Base_Assert.Equals(WD.mainWindow.ScaleCheckInternalFrame.IsEnabled, true);
            LogStep(@"4.the status is changed in web");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.equipment);
            Thread.Sleep(2000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Web_status.PNG");
            var simulator_status = driver.FindElement("//td[text()='simulator']/../td[4]").Text;
            Assert.AreEqual(simulator_status, "Maintain");
            LogStep(@"5.report");
            Web_Fuction.gotoTab(WDWebTab.report);
            Thread.Sleep(2000);
            //Scale Check
            driver.FindElement("//div[text()='Scale Check']").Click();

            Thread.Sleep(2000);
            driver.FindElement("//*[@value='simulator']").Click();
            driver.FindElement("//option[text()='qaone1(qaone1)']").Click();
            driver.FindElement("//*[@value='booth1']").Click();
            driver.FindElement("//*[text()='Generate Report']").Click();
            Thread.Sleep(5000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Report.PNG");
            //System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", now_time);
            var result = driver.FindElement("//table[@class='Report_Paper_Border_Shading']/tbody/tr[4]/td/table/tbody/tr[2]/td[5]").Text;
            //var result = driver.FindElement("//td[(text()='" + now_time + "']/../td[5]").Text;

            Base_Assert.AreEqual(result, "Failure");

        }


    }
}