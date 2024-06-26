using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections;
using HP.LFT.SDK.Java;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(29844)]
        [Title("W&D ENH: show a message when the bar code does not exist in the inventory table")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]


        //defect1365210
        //[TestMethod]
        public void VSTS_29844()
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
            LogStep(@"1. conduct a weighing");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            WD.mainWindow.DispensingInternalFrame.orderTable.SelectRows(0);
            WD.mainWindow.DispensingInternalFrame.next.Click();
            Thread.Sleep(2000);
            WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.MaterialInternalFrame.next.Click();
            Thread.Sleep(2000);
            if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled is true)
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
                Thread.Sleep(2000);
            }
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("test001");
            Thread.Sleep(2000);
            WD.mainWindow.GetSnapshot(Resultpath + "WrongContainer.PNG");
            Base_Assert.AreEqual(WD.MessageDialog.Lable.Text, "Container barcode is not recognized. Scan another container.");
            WD.MessageDialog.OKButton.Click();
            Thread.Sleep(2000);
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.barcode.Text,"");
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("1072001");
            Thread.Sleep(2000);
            WD.mainWindow.GetSnapshot(Resultpath + "DoNotMatchContainer.PNG");
            Base_Assert.AreEqual(WD.MessageDialog.Lable.Text, "The scanned container is not the required material. Please scan the correct container.");
            WD.MessageDialog.OKButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("X0125001");
            Thread.Sleep(2000);
            WD.mainWindow.GetSnapshot(Resultpath + "CorrectContainer.PNG");
            Base_Assert.IsTrue(WD.SimulatorWindow._UFT_Window.IsEnabled);
        }

       
    }
}