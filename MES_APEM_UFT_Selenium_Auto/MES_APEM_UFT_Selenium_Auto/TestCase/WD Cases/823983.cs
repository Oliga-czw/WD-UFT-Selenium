using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.Windows.Forms;
using System.Drawing;
using HP.LFT.SDK;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(823983)]
        [Title("UC815412_W&D enhancement for Source as Target_APRM if DO not set SOURCE_TARGET_REQUIRE_TARGET_TARE key or set SOURCE_TARGET_REQUIRE_TARGET_TARE = 0")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_823983()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";
            string method = WDMethod.SourceAsTarget;
            string barcode = "X0125001";
            string source_left = "300";
            string source_start = "320";
            string scale = "simulator";
            string Configpath = Base_Directory.ConfigDirx86 + "config.m2r_cfg";
            string ConfigKey = "SOURCE_TARGET_REQUIRE_TARGET_TARE = 0";

            try
            {
                LogStep(@"1. Set key in config file");
                Base_Function.AddConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_allx86);
                LogStep(@"2. config APRM admin and apem admin");
                APRM_Fuction.InitailAPRMWD();
                LogStep(@"3. Active orders");
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Web_Fuction.gotoWDWeb(driver);
                driver.Wait();
                Web_Fuction.login();
                driver.Wait();
                Web_Fuction.gotoTab(WDWebTab.order);
                Web_Fuction.active_order(order);
                driver.Close();
                LogStep(@"5. Open WD client");
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
                WD.mainWindow.GetSnapshot(Resultpath + "weight_1.PNG");
                Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.WeightNumber._UFT_Label.Text, "220.0");
                //input remove weight
                WD.SimulatorWindow.weight.SetText(source_left);
                WD.SimulatorWindow.OK.Click();
                Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.WeightNumber._UFT_Label.Text, "200.0");
                WD.mainWindow.GetSnapshot(Resultpath + "weight_removal.PNG");
                //Check the "Initial gross", "Finial gross", and "Difference" quantity on the right panel 
                var Initial_gross = WD.mainWindow.ScaleWeightInternalFrame.Data_InitailGross.AttachedText;
                var Finial_gross = WD.mainWindow.ScaleWeightInternalFrame.Data_FinalGross.AttachedText;
                var Difference = WD.mainWindow.ScaleWeightInternalFrame.Data_Diffenence.AttachedText;
                Base_Assert.AreEqual(Initial_gross, "320.0");
                Base_Assert.AreEqual(Finial_gross, "300.0");
                Base_Assert.AreEqual(Difference, "220.0");
                //click accept dispense
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
                //cancel weighing
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
                //select method and input barcode
                WD_Fuction.SelectMehod(method, "M801890001");
                if (WD.ConfirmationDialog.IsExist())
                {
                    WD.ConfirmationDialog.YesButton.Click();
                }
                if (WD.MessageDialog.IsExist())
                {
                    WD.MessageDialog.OKButton.Click();
                }
                WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
                Thread.Sleep(3000);
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
                WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
                //reset weighing   1072
                WD.mainWindow.Material_SelectionInternalFrame.materialTable.Row("1072").Click();
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
                //select method and input barcode
                WD_Fuction.SelectMehod(method, "1072003");
                if (WD.ConfirmationDialog.IsExist())
                {
                    WD.ConfirmationDialog.YesButton.Click();
                }
                if (WD.MessageDialog.IsExist())
                {
                    WD.MessageDialog.OKButton.Click();
                }
                WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
                Thread.Sleep(3000);
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
                WD.mainWindow.ScaleWeightInternalFrame.reset.Click();
                Thread.Sleep(5000);
                //LogStep(@"6. Open Batch query tool ");
                Application.LaunchBatchQueryTool();
                Thread.Sleep(3000);
                //open new query
                BatchQueryTool.NewQuery();
                //open batch detail display
                BatchQueryTool.BatchQueryToolWindow.ListView._STD_ListView.ActivateItem(order);
                //wait for loading
                Thread.Sleep(15000);
                //X0125:Begin_Source_Gross is 320 and End_Source_Gross is 300
                APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
                APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1]").Expand();
                APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1]").Expand();
                APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1]").Expand();
                APRM.BatchMainWindow.TreeView.Select("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1];Container [1]");
                //wait for loading
                Thread.Sleep(5000);
                APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM Batch detail(Accept).PNG");
                APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("End Source Gross");
                Base_Assert.AreEqual("300", APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text, "End Source Gross");
                APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
                APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("Begin Source Gross");
                Base_Assert.AreEqual("320", APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text, "Begin Source Gross");
                APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
                //M801890:Cancel the weigh.The Quantity should be reduced for End source.
                APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
                APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1]").Expand();
                APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1]").Expand();
                APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [2]").Expand();
                APRM.BatchMainWindow.TreeView.Select("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [2];Action [1]");
                //wait for loading
                Thread.Sleep(5000);
                APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM Batch detail(Cancel).PNG");
                APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("End Source Gross");
                string Cancel_End_source_Gross = APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text;
                APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
                APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("Begin Source Gross");
                string Cancel_Begin_source_Gross = APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text;
                APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
                Base_Assert.IsTrue(int.Parse(Cancel_End_source_Gross) < int.Parse(Cancel_Begin_source_Gross), "The Quantity should be reduced for End source.");
                //1072:Reset the weigh.The Quantity should be the same.
                APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
                APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1]").Expand();
                APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1]").Expand();
                APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [3]").Expand();
                APRM.BatchMainWindow.TreeView.Select("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [3];Action [1]");
                //wait for loading
                Thread.Sleep(5000);
                APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM Batch detail(Reset).PNG");
                APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("End Source Gross");
                string Reset_End_source_Gross = APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text;
                APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
                APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("Begin Source Gross");
                string Reset_Begin_source_Gross = APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text;
                APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
                Base_Assert.AreEqual(Reset_End_source_Gross, Reset_Begin_source_Gross, "The Quantity should be the same.");
                APRM.BatchMainWindow.Close();
                BatchQueryTool.BatchQueryToolWindow.Close();
                BatchQueryTool.BatchQueryToolWindow.Save_Dialog.NO.Click();

            }
            finally
            {
                LogStep(@"4.delete config key ");
                Base_Function.DeleteConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_allx86);
            }


        }

    }
}
