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
        [TestCaseID(823978)]
        [Title("UC814915_W&D enhancement for Source as Target_Weighing report if set SOURCE_TARGET_REQUIRE_TARGET_TARE = 1")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_823978()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test2";
            string method = WDMethod.SourceAsTarget;
            string barcode = "X0125001";
            string source_left = "300";
            string source_start = "320";
            string scale = "simulator";
            string Configpath = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string ConfigKey = "SOURCE_TARGET_REQUIRE_TARGET_TARE = 1";
            try
            {
                LogStep(@"1. Set key in config file");
                Base_Function.AddConfigKey(Configpath,ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                LogStep(@"2. Active orders");
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Web_Fuction.gotoWDWeb(driver);
                driver.Wait();
                Web_Fuction.login();
                driver.Wait();
                Web_Fuction.gotoTab(WDWebTab.order);
                Web_Fuction.active_order(order);
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
                WD_Fuction.Close();
                Web_Fuction.gotoTab(WDWebTab.report);
                Thread.Sleep(2000);
                Web.Report_Page.Weighing.Click();
                Console.WriteLine(Web_Fuction.check_weighing_report_source("Accept"));
                var accept_data_source = Web_Fuction.check_weighing_report_source("Accept");
                var accept_begin_source = accept_data_source.Item1;
                var accept_end_source = accept_data_source.Item2;
                Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Accept.PNG");
                Base_Assert.AreEqual("", accept_begin_source);
                Base_Assert.AreEqual("", accept_end_source);
            }
            finally
            {
                LogStep(@"4.delete config key ");
                Base_Function.DeleteConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
            }
            

        }

    }
}
