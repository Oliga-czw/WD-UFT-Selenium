using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(43507)]
        [Title("Order: Reprint label(container  and Pallet)")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_43507()

        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";

            //active order
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            Thread.Sleep(2000);
            WD.mainWindow.DispensingInternalFrame.orderTable.SelectRows(0);
            WD.mainWindow.DispensingInternalFrame.next.Click();
            Thread.Sleep(2000);
            WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.MaterialInternalFrame.next.Click();
            if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled)
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("X0125001");
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("444");

            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(2000);
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD_Fuction.Close();
            Thread.Sleep(3000);
            driver.FindElement("//a[text()='Reprint Label']").Click();
            driver.Wait();
            Base_Assert.IsTrue(driver.FindElement("//div[text()='APEM - Aspen Weigh and Dispense Management - Reprint Label']").Displayed);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "reprint_label_display.PNG");
            driver.FindElement("//td[text()='X0125']/../td[1]/span/input").Click();
            driver.FindElement("//button[text()='Reprint Pallet Label']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//button[@class='gwt-Button OkStyle']").Click();
            //Reprint Container Label
            driver.FindElement("//button[text()='Reprint Container Label']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//button[@class='gwt-Button OkStyle']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//button[@class='gwt-Button OkStyle']").Click();
            driver.FindElement("//button[text()='Close']").Click();
            Thread.Sleep(2000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "reprint_label_close.PNG");
        }
    }
}