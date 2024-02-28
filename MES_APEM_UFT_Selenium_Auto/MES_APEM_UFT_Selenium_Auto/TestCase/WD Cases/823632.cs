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
        [TestCaseID(823632)]
        [Title("UC813649_'Source as Target' method should work as new style if DO NOT set SOURCE_TARGET_REQUIRE_TARGET_TARE key")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_823632()
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
            driver.Close();
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
            LogStep(@"4. click accept button");
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
            //check accept button
            WD.mainWindow.ScaleWeightInternalFrame.accept.ClickSignle();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }

            //WD_Fuction.Close();
            SqlHelper helper = new SqlHelper();
            string SQL = $"SELECT TOP(1) TARGET_TARE,BEGIN_SOURCE_GROSS,END_SOURCE_GROSS FROM EBR_WD_WEIGH_HISTORY ORDER BY WEIGH_ID DESC;";
            List<List<string>> Source = helper.Execute(SQL);
            var Source_Container_Tare = Source[0][0];
            var InitailGross = Source[0][1];
            var FinalGross = Source[0][2];
            Base_Assert.AreEqual(Source_Container_Tare, "100.0");
            Base_Assert.AreEqual(InitailGross, "320.0");
            Base_Assert.AreEqual(FinalGross, "300.0");
            LogStep(@"5. click partial dispense button"); 
            WD.mainWindow.Material_SelectionInternalFrame.materialTable.Row("M801890").Click();
            WD.mainWindow.Material_SelectionInternalFrame.next.Click();
            Thread.Sleep(3000);
            if (WD.mainWindow.BoothCleanInternalFrame.IsExist())
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            }
            if (WD.mainWindow.HandleInformationInterFrame.IsExist())
            {
                WD.mainWindow.HandleInformationInterFrame.Acknowledge.ClickSignle();
            }
            Thread.Sleep(3000);
            WD_Fuction.SelectMehod(method, "M801890001");
            //select simulator
            if (WD.ConfirmationDialog.IsExist())
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            if (WD.MessageDialog.IsExist())
            {
                WD.MessageDialog.OKButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
            //zeor
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            Thread.Sleep(2000);
            WD.mainWindow.ScaleWeightInternalFrame.SourceTare.SendKeys("50");
            //start weight
            WD.SimulatorWindow.weight.SetText("120");
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //input remove weight
            WD.SimulatorWindow.weight.SetText("100");
            WD.SimulatorWindow.OK.Click();
            //check partial dispense
            WD.mainWindow.ScaleWeightInternalFrame.Partial.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            SqlHelper helper1 = new SqlHelper();
            string SQL1 = $"SELECT TOP(1) TARGET_TARE,BEGIN_SOURCE_GROSS,END_SOURCE_GROSS FROM EBR_WD_WEIGH_HISTORY ORDER BY WEIGH_ID DESC;";
            List<List<string>> Source1 = helper1.Execute(SQL1);
            var Source_Container_Tare1 = Source1[0][0];
            var InitailGross1 = Source1[0][1];
            var FinalGross1 = Source1[0][2];
            Base_Assert.AreEqual(Source_Container_Tare1, "50.0");
            Base_Assert.AreEqual(InitailGross1, "120.0");
            Base_Assert.AreEqual(FinalGross1, "100.0");
        }

    }
}
