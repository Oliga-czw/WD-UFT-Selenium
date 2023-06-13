using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(39772)]
        [Title("V8.8.4-Partial button is delt with OK when need partial weighing")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Low)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_39772()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test2";
            string material = WDMaterial.X0125;
            string method = WDMethod.Netremoval;
            string barcode = "X0125001";
            string tare = "15";
            string source_left = "800";
            string source_start = "1000";

            LogStep(@"1. Active orders");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            LogStep(@"2. Open WD client and partial weight");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            //net removal 
            WD_Fuction.SelectMehod(method, barcode);
            //zeor
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.mainWindow.ScaleWeightInternalFrame.tare_editor.SetText(tare);
            //start weight
            WD.SimulatorWindow.weight.SetText(source_start);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //input remove weight
            WD.SimulatorWindow.weight.SetText(source_left);
            WD.SimulatorWindow.OK.Click();
            //check patial dispense 
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.Partial.IsEnabled);
            WD.mainWindow.GetSnapshot(Resultpath + "Net removal partial.PNG");
            WD.mainWindow.ScaleWeightInternalFrame.Partial.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            //check stay in weight screen
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.IsExist(), "Stay in weight screen");
            //check net removal method is selected
            Base_Assert.AreEqual(method, WD.mainWindow.ScaleWeightInternalFrame.dispense_method.SelectedItems, "Net removal method is selected");



            driver.Close();
        }
    }
}
