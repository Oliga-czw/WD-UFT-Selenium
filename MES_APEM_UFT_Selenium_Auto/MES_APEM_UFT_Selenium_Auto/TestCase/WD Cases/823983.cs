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
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

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
                //Base_Function.AddConfigKey(Configpath,ConfigKey);
                //codify all
                //Base_Test.LaunchApp(Base_Directory.Codify_allx86);
                LogStep(@"2. Active orders");
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Web_Fuction.gotoWDWeb(driver);
                driver.Wait();
                Web_Fuction.login();
                driver.Wait();
                Web_Fuction.gotoTab(WDWebTab.order);
                Web_Fuction.active_order(order);
                driver.Close();
                LogStep(@"3. Open WD client");
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
                Base_Assert.AreEqual(Finial_gross,"300.0");
                Base_Assert.AreEqual(Difference,"220.0");
                WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
                LogStep(@"4. Open Batch query tool ");
                Application.LaunchBatchQueryTool();
                

                ////click accept dispense
                //WD.mainWindow.ScaleWeightInternalFrame.accept.ClickSignle();
                //if (WD.ErrorDialog.IsExist())
                //{
                //    WD.ErrorDialog.OKButton.Click();
                //}
                //if (WD.ErrorDialog.IsExist())
                //{
                //    WD.ErrorDialog.OKButton.Click();
                //}
                //Thread.Sleep(3000);
                //WD_Fuction.Close();


            }
            finally
            {
                LogStep(@"4.delete config key ");
                //Base_Function.DeleteConfigKey(Configpath, ConfigKey);
                ////codify all
                //Base_Test.LaunchApp(Base_Directory.Codify_allx86);
            }
            

        }

    }
}
