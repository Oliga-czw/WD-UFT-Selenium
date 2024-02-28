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
        [TestCaseID(823976)]
        [Title("UC814915_W&D enhancement for Source as Target_Weighing report if DO not set SOURCE_TARGET_REQUIRE_TARGET_TARE key or set SOURCE_TARGET_REQUIRE_TARGET_TARE = 0")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_823976()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";
            string method = WDMethod.SourceAsTarget;
            string barcode = "X0125001";
            string source_left = "300";
            string source_start = "320";
            string scale = "simulator";
            LogStep(@"1. Active orders");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            //driver.Close();
            LogStep(@"2. Open WD client");
            Application.LaunchWDAndLogin();
            WD.mainWindow.HomeInternalFrame.MaterialDispensing.Click();
            WD.mainWindow.Material_SelectionInternalFrame.materialTable.Row("X0125").Click();
            var Required_value = WD.mainWindow.Material_SelectionInternalFrame.materialTable._UFT_Table.GetCell(0, "Clean Required").Value;

            WD.mainWindow.Material_SelectionInternalFrame.next.Click();
            if (Required_value.ToString() == "Yes")
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            }
            //select method and input barcode
            WD_Fuction.SelectMehod(method, barcode);

            //select simulator
            if (WD.MessageDialog.IsExist())
            {
                WD.MessageDialog.OKButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.InitailGross._UFT_Label.IsEnabled);
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.FinalGross._UFT_Label.IsEnabled);
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.Diffenence._UFT_Label.IsEnabled);
            //zeor
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "SourceAsTarget.PNG");
            Thread.Sleep(2000);
            WD.mainWindow.ScaleWeightInternalFrame.SourceTare.SendKeys("100");
            //start weight
            WD.SimulatorWindow.weight.SetText(source_start);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //assert the weight
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.WeightNumber._UFT_Label.Text, "220.0");
            //input remove weight
            WD.SimulatorWindow.weight.SetText(source_left);
            WD.SimulatorWindow.OK.Click();
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.WeightNumber._UFT_Label.Text, "200.0");
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.Data_InitailGross._UFT_Label.Text, "320.0");
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.Data_FinalGross._UFT_Label.Text, "300.0");
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.Data_Diffenence._UFT_Label.Text, "220.0");
            WD.mainWindow.GetSnapshot(Resultpath + "weight_1.PNG");
            LogStep(@"3. click reset button"); 
            WD.mainWindow.ScaleWeightInternalFrame.reset.Click();
            //Check the "Begin Source" and "End Source" in Weighing report: The Quantity should be the same.
            Web_Fuction.gotoTab(WDWebTab.report);
            Thread.Sleep(2000);
            Web.Report_Page.Weighing.Click();
            Console.WriteLine(Web_Fuction.check_weighing_report_source("Reset"));
            var reset_data_source = Web_Fuction.check_weighing_report_source("Reset");
            var reset_begin_source = reset_data_source.Item1;
            var reset_end_source = reset_data_source.Item2;
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Reset.PNG");
            Base_Assert.AreEqual(reset_begin_source, reset_end_source);
            LogStep(@"4. click cancel button");
            WD_Fuction.SelectMehod(method, barcode);
            //select simulator
            if (WD.MessageDialog.IsExist())
            {
                WD.MessageDialog.OKButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
            //zeor
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //start weight
            WD.SimulatorWindow.weight.SetText(source_start);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //input remove weight
            WD.SimulatorWindow.weight.SetText(source_left);
            WD.SimulatorWindow.OK.Click();
            //check cancel
            WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
            //Check the "Begin Source" and "End Source" in Weighing report: The "Begin source" and "End Source" Quantity should be reduced for End source.
            Web.Report_Page.Weighing.Click();
            Console.WriteLine(Web_Fuction.check_weighing_report_source("Cancel"));
            var cancel_data_source = Web_Fuction.check_weighing_report_source("Cancel");
            var cancel_begin_source = cancel_data_source.Item1;
            var cancel_end_source = cancel_data_source.Item2;
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Cancel.PNG");
            Base_Assert.IsTrue(float.Parse(cancel_begin_source) > float.Parse(cancel_end_source));

            LogStep(@"5. click accept button");
            WD.mainWindow.Material_SelectionInternalFrame.materialTable.Row("X0125").Click();
            WD.mainWindow.Material_SelectionInternalFrame.next.Click();
            WD_Fuction.SelectMehod(method, barcode);
            //select simulator
            if (WD.MessageDialog.IsExist())
            {
                WD.MessageDialog.OKButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
            //zeor
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            Thread.Sleep(2000);
            WD.mainWindow.ScaleWeightInternalFrame.SourceTare.SendKeys("100");
            //start weight
            WD.SimulatorWindow.weight.SetText(source_start);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //input remove weight
            WD.SimulatorWindow.weight.SetText(source_left);
            WD.SimulatorWindow.OK.Click();
            //check accept dispense
            WD.mainWindow.ScaleWeightInternalFrame.accept.ClickSignle();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(3000);
            WD_Fuction.Close();
            Web.Report_Page.Weighing.Click();
            Console.WriteLine(Web_Fuction.check_weighing_report_source("Accept"));
            var accept_data_source = Web_Fuction.check_weighing_report_source("Accept");
            var accept_begin_source = accept_data_source.Item1;
            var accept_end_source = accept_data_source.Item2;
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Accept.PNG");
            Base_Assert.AreEqual("320.0", accept_begin_source);
            Base_Assert.AreEqual("300.0", accept_end_source);

        }

    }
}
