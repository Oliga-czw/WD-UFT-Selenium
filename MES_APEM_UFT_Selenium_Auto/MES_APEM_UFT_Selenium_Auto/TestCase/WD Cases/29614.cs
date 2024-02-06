using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(29614)]
        [Title("scale check-cancel after all weights are checked")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_29614()
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
            if (WD.ConfirmationDialog.IsExist())
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("444");

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

            }
            WD.mainWindow.HandingInternalFrame.AcknowledgeButton.ClickSignle();
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("M801890001");
            if (WD.ConfirmationDialog.IsExist())
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("180");

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

            }
            WD.mainWindow.HandingInternalFrame.AcknowledgeButton.ClickSignle();
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("1072003");
            if (WD.ConfirmationDialog.IsExist())
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("180");

            WD.SimulatorWindow.OK.Click();

            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            ////order Kitting
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            LogStep(@"1. Open Wd client and login");
            //WD.mainWindow.HomeInternalFrame.ScaleChecking.Click();
            //Thread.Sleep(3000);
            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();

            var orderName = WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.GetCell(0, "Order").Value.ToString();
            LogStep(@"2. select an order and click 'Start Kitting'");
            WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.SelectRows(0);
            WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
            Thread.Sleep(2000);
            Base_Assert.AreEqual(orderName, WD.mainWindow.SelectAnOrderToKittingFrame.selectedOrder._UFT_Label.Text);
            //test10000000000000000017
            var barcodeEditor = WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor;
            barcodeEditor.SendKeys("test");
            LogStep(@"3.scan container that already scanned");
            //barcodeEditor.Activate();
            barcodeEditor.SendKeys("test10000000000000000017");
            WD.mainWindow.GetSnapshot(Resultpath + "re-scan.PNG");
            Base_Assert.IsTrue(WD.MessageDialog.Lable.AttachedText.Contains("Please re-scan."));
            WD.MessageDialog.OKButton.Click();
            Base_Assert.IsTrue(barcodeEditor._UFT_Editor.IsEnabled);
        }
    }
}