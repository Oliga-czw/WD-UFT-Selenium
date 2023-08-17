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
            string method1 = WDMethod.SourceAsTarget;
            string method2 = WDMethod.Netremoval;
            string barcode = "X0125001";
            string tare = "15";
            string source_start1 = "1015";
            string source_weight1 = "215";
            string source_left2 = "600";
            string source_start2 = "800";
            string scale = "simulator";

            LogStep(@"1. Active orders");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            driver.Close();
            LogStep(@"2. Open WD client and partial weight");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            LogStep(@"3. Source As target ");
            //Source As target 
            WD_Fuction.SelectMehod(method1, barcode);
            //select scale
            if (WD.MessageDialog.IsExist())
            {
                WD.MessageDialog.OKButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
            //zeor
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.mainWindow.ScaleWeightInternalFrame.tare_editor.SetText(tare);
            //start weight
            WD.SimulatorWindow.weight.SetText(source_start1);
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(1000);
            WD.mainWindow.SetActive();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.ClickSignle();
            //input source weight
            WD.SimulatorWindow.weight.SetText(source_weight1);
            WD.SimulatorWindow.OK.Click();
            //check patial dispense 
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.Partial.IsEnabled);
            WD.mainWindow.GetSnapshot(Resultpath + "Source As target partial.PNG");
            WD.mainWindow.ScaleWeightInternalFrame.Partial.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            //check stay in weight screen
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.IsExist(), "Stay in weight screen");
            WD.mainWindow.GetSnapshot(Resultpath + "After Source As target partial.PNG");
            //check Source As target  method is enable
            var selectedMethod = WD.mainWindow.ScaleWeightInternalFrame.dispense_method.SelectedItems[0].Text;
            Base_Assert.AreEqual(method1, selectedMethod, "Source As target method is selected");
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.dispense_method._UFT_IList.IsEnabled, "method is ensable");
            LogStep(@"4. net removal ");
            //net removal 
            WD_Fuction.SelectMehod(method2, barcode);
            //select simulator
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
            //zeor
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.mainWindow.ScaleWeightInternalFrame.tare_editor.SetText(tare,true);
            //start weight
            WD.SimulatorWindow.weight.SetText(source_start2);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //input remove weight
            WD.SimulatorWindow.weight.SetText(source_left2);
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
            WD.mainWindow.GetSnapshot(Resultpath + "After Net removal partial.PNG");
            //check net removal method can not again
            //disable object can not get data
            //var selectedMethod2 = WD.mainWindow.ScaleWeightInternalFrame.dispense_method.SelectedItems[0].Text;
            //Base_Assert.AreEqual(method2, selectedMethod2, "Net removal method is selected");
            Base_Assert.IsFalse(WD.mainWindow.ScaleWeightInternalFrame.dispense_method._UFT_IList.IsEnabled, "method is disable");
            //Click cancel
            WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
            Base_Assert.IsTrue(WD.mainWindow.Material_SelectionInternalFrame.IsExist() || WD.mainWindow.MaterialInternalFrame.IsExist(), "Exit Dispense");
            
            WD_Fuction.Close();


        }
    }
}
