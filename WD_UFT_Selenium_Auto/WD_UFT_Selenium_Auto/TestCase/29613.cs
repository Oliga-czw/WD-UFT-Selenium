using System;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(29613)]
        [Title("Order kitting :reprint last label")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_29613()
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
            LogStep(@"1. Open Wd client and login");
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
            WD.SimulatorWindow.weight.SetText("400");

            WD.SimulatorWindow.OK.Click();

            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }

            //BarcodeEditor.SetText("M801890001");

            WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.MaterialInternalFrame.next.Click();
            if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled)
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
                WD.mainWindow.HandingInternalFrame.AcknowledgeButton.ClickSignle();
            }
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("M801890001");
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("200");

            WD.SimulatorWindow.OK.Click();

            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }


            WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.MaterialInternalFrame.next.Click();
            if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled)
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
                WD.mainWindow.HandingInternalFrame.AcknowledgeButton.ClickSignle();
            }
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("1072003");
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("200");

            WD.SimulatorWindow.OK.Click();

            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            ////order Kitting
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();

            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();
            LogStep(@"2.Without select any order and click 'Start Kitting' button.");
            WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
            Base_Assert.AreEqual(WD.MessageDialog.Lable.AttachedText, "Please select an order to kit.");
            WD.mainWindow.GetSnapshot(Resultpath + "no_order_startkitting.PNG");
            WD.MessageDialog.OKButton.Click();
            Thread.Sleep(2000);
            WD.MessageDialog.OKButton.Click();

            //web report
            Web_Fuction.gotoTab(WDWebTab.report);
            driver.FindElement("//div[text()='Label Reprint']").Click();
            driver.FindElement("//button[text()='Generate Report']").Click();
            int labelReport = driver.FindElements("//td[text()='test1000000000000024']/..").Count;
            LogStep(@"3.click reprint last label.");
            WD.mainWindow.SelectAnOrderToKittingFrame.printButton.Click();
            WD.MessageDialog.OKButton.Click();
            WD_Fuction.Close();
            driver.FindElement("//button[text()='Generate Report']").Click();
            Base_Assert.AreEqual(driver.FindElements("//td[text()='test1000000000000024']/..").Count, labelReport+1);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "report.PNG");

        }


    }
}