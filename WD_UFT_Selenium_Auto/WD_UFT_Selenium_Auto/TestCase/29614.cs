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
        [TestCaseID(29614)]
        [Title("scale check-cancel after all weights are checked")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_29614()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. Open Wd client and login");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.ScaleChecking.Click();
            Thread.Sleep(3000);
            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();

            var orderName = WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.GetCell(0, "Order").Value.ToString();
            LogStep(@"2. select an order and click 'Start Kitting'");
            WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.SelectRows(0);
            WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
            Thread.Sleep(2000);
            Base_Assert.AreEqual(orderName, WD.mainWindow.SelectAnOrderToKittingFrame.selectedOrder._UFT_Label.Text);
            //test10000000000000000017
            var barcodeEditor = WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor;
            barcodeEditor.SetText("test\n");
            LogStep(@"3.scan container that already scanned");
            //barcodeEditor.Activate();
            barcodeEditor.SetText("test10000000000000000017\n");
            WD.mainWindow.GetSnapshot(Resultpath + "re-scan.PNG");
            Base_Assert.IsTrue(WD.MessageDialog.Lable.AttachedText.Contains("Please re-scan."));
            WD.MessageDialog.OKButton.Click();
            Base_Assert.IsTrue(barcodeEditor._UFT_Editor.IsEnabled);
        }
    }
}