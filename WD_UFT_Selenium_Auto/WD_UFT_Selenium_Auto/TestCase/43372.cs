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
        [TestCaseID(43372)]
        [Title("Order kitting :scan refused if scan container or pallet not belong to the order")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_43372()

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
            WD.SimulatorWindow.weight.SetText("444");

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
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("M801890001");
            if (WD.ConfirmationDialog._UFT_Dialog.IsEnabled)
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
            if (WD.ConfirmationDialog._UFT_Dialog.IsEnabled)
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
            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();    
            WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.SelectRows(0);
            WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
            Thread.Sleep(4000);
            string test01 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(0, "Container").Value.ToString();
            string test02 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(1, "Container").Value.ToString();
            string test03 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(2, "Container").Value.ToString();
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys("test1");
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test01);
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test02);
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys("test10000000030000002253");
            WD.mainWindow.GetSnapshot(Resultpath + "not_belong_the_order.PNG");
            Base_Assert.AreEqual(WD.MessageDialog.Lable.Text, "There is no such container --test10000000030000002253. Please re-scan.");
            

        }


    }
}