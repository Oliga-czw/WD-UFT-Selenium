﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        [TestCaseID(748225)]
        [Title("UC730650_Check tare input box exists using Net Removal method when set config key=1 in config.m2r_cfg and flag.m2r_cfg  on current workstation ")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_748225()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test2";
            string material = WDMaterial.X0125;
            string method = WDMethod.Netremoval;
            string barcode = "X0125001";
            string source_left = "555.6";
            string tare = "15";
            string source_start = "1000";
            string scale = "simulator";
            string Configpath1 = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string Configpath2 = Base_Directory.ConfigDirx86 + "config.m2r_cfg";
            string ConfigKey = "NET_REMOVAL_REQUIRE_TARGET_TARE = 1";

            try
            {
                LogStep(@"1. Set key in config file");
                Base_Function.AddConfigKey(Configpath1,ConfigKey);
                Base_Function.AddConfigKey(Configpath2, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                Thread.Sleep(5000);
                Base_Test.LaunchApp(Base_Directory.Codify_allx86);
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
                WD_Fuction.SelectOrderandMaterial(order, material);
                LogStep(@"4.select net removal ");
                //net removal 
                WD.mainWindow.ScaleWeightInternalFrame.dispense_method.SelectItems(method);
                //check The target container tare input box disappears.
                WD.mainWindow.GetSnapshot(Resultpath + "The target container tare input box exists.PNG");
                Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.tareLable._UFT_Label.Exists(), "The target container tare input box exists.");
                //input barcode
                WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys(barcode);
                //select simulator
                if (WD.MessageDialog.IsExist())
                {
                    WD.MessageDialog.OKButton.Click();
                }
                WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
                //zeor
                WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
                //tare
                WD.mainWindow.ScaleWeightInternalFrame.tare_editor.SetText(tare, true);
                //start weight
                WD.SimulatorWindow.weight.SetText(source_start);
                WD.SimulatorWindow.OK.Click();
                WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
                //input remove weight
                WD.SimulatorWindow.weight.SetText(source_left);
                WD.SimulatorWindow.OK.Click();
                //check accept dispense 
                WD.mainWindow.GetSnapshot(Resultpath + "Net removal Accept.PNG");
                WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
                if (WD.ErrorDialog.IsExist())
                {
                    WD.ErrorDialog.OKButton.Click();
                }
                Base_Assert.IsTrue(WD.mainWindow.Material_SelectionInternalFrame.IsExist() || WD.mainWindow.MaterialInternalFrame.IsExist(), "Exit Dispense");
                WD_Fuction.Close();
            }
            finally
            {
                LogStep(@"4.delete config key ");
                Base_Function.DeleteConfigKey(Configpath1, ConfigKey);
                Base_Function.DeleteConfigKey(Configpath2, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                Thread.Sleep(5000);
                Base_Test.LaunchApp(Base_Directory.Codify_allx86);
            }
            

        }

    }
}
