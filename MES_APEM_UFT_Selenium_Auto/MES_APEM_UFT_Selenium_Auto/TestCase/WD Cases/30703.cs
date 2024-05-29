using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(30703)]
        [Title("V8.8.3_CQ00677141:Allow Multiple Lots = No per order allows only a single lot in an order")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
  
        public void VSTS_30703()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
           
            string order = "test1";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string barcode2 = "X0125005";
            string barcode3 = "X0125002";
            string tare = "10";
            string partial = "110";
            string net = "454.4";

            LogStep(@"1. active order");
            //active order
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            //active order
            Web_Fuction.gotoTab(WDWebTab.order);
            driver.Wait();
            Web_Fuction.active_order(order);
            driver.Wait();
            driver.Close();
            LogStep(@"2. Open WD client and weigh");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            //partial dispense
            //zero
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.SimulatorWindow.weight.SetText(tare);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            Thread.Sleep(2000);
            if (WD.mainWindow.Dialog.IsExist())
            {
                WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
                WD.mainWindow.Dialog.OK.Click();
                Thread.Sleep(2000);
            }
            //weight
            WD.SimulatorWindow.weight.SetText(partial);
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(1000);
            WD.mainWindow.ScaleWeightInternalFrame.Partial.Click();
            if (WD.ConfirmationDialog.IsExist())
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(1000);
            LogStep(@"3. partial different lot");
            //Reset and input different lot
            WD.mainWindow.ScaleWeightInternalFrame.reset.Click();
            WD.mainWindow.ScaleWeightInternalFrame.dispense_method.SelectItems(method);
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys(barcode2);
            Thread.Sleep(1000);
            //warning
            WD.mainWindow.GetSnapshot(Resultpath + "partial different lot warning.PNG");
            string message = "Inventory from a different lot is not allowed.";
            Base_Assert.AreEqual(message, WD.MessageDialog.Lable.Text, "different lot message");
            WD.MessageDialog.OKButton.Click();
            LogStep(@"4. partial same lot");
            //Use same lot
            WD.mainWindow.ScaleWeightInternalFrame.dispense_method.SelectItems(method);
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys(barcode3);
            Thread.Sleep(1000);
            WD.mainWindow.GetSnapshot(Resultpath + "partial same lot.PNG");
            //zero
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.SimulatorWindow.weight.SetText(tare);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            Thread.Sleep(2000);
            if (WD.mainWindow.Dialog.IsExist())
            {
                WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
                WD.mainWindow.Dialog.OK.Click();
                Thread.Sleep(2000);
            }
            //weight
            WD.SimulatorWindow.weight.SetText(partial);
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(1000);
            LogStep(@"5. new source different lot");
            WD.mainWindow.ScaleWeightInternalFrame.NewSource.Click();
            Thread.Sleep(1000);
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys(barcode2);
            Thread.Sleep(1000);
            //warning
            WD.mainWindow.GetSnapshot(Resultpath + "new source different lot warning.PNG");
            Base_Assert.AreEqual(message, WD.MessageDialog.Lable.Text, "different lot message");
            WD.MessageDialog.OKButton.Click();
            LogStep(@"6. new source same lot");
            //Use same lot
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys(barcode);
            Thread.Sleep(1000);
            WD.mainWindow.GetSnapshot(Resultpath + "new source same lot.PNG");
            //weight
            WD.SimulatorWindow.weight.SetText(partial);
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(1000);
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ConfirmationDialog.IsExist())
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(1000);
            //check Finish Dispense
            Base_Assert.IsTrue(WD.mainWindow.Material_SelectionInternalFrame.IsExist() || WD.mainWindow.MaterialInternalFrame.IsExist() || WD.mainWindow.DispensingInternalFrame.IsExist(), "Finish Dispense");


            WD_Fuction.Close();
        }
    }
}
