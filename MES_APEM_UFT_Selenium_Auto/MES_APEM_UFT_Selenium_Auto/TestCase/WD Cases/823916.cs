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
        [TestCaseID(823916)]
        [Title("UC813649_'Reset' and 'Cancel' button for 'Source as Target' method should work if SOURCE_TARGET_REQUIRE_TARGET_TARE = 0 is set on flags.m2r_cfg on server side")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_823916()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test2";
            string material = WDMaterial.X0125;
            string method = WDMethod.SourceAsTarget;
            string barcode = "X0125001";
            string source_left = "300";
            string source_start = "320";
            string source_tare = "100";
            string scale = "simulator";
            string Configpath = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string ConfigKey = "SOURCE_TARGET_REQUIRE_TARGET_TARE = 0";

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
                driver.Close();
                LogStep(@"3. Open WD client");
                Application.LaunchWDAndLogin();
                WD_Fuction.SelectOrderandMaterial(order, material);
                LogStep(@"4.select net removal ");
                //Source as target
                WD_Fuction.SelectMehod(method, barcode);
                if (WD.MessageDialog.IsExist())
                {
                    WD.MessageDialog.OKButton.Click();
                }
                //check The Weighing Info
                
                Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.InitailGross._UFT_Label.IsEnabled);
                Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.FinalGross._UFT_Label.IsEnabled);
                Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.Diffenence._UFT_Label.IsEnabled);
                //select simulator

                WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
                //zeor
                WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
                WD.mainWindow.GetSnapshot(Resultpath + "Weighing Info.PNG");
                //input source tare
                WD.mainWindow.ScaleWeightInternalFrame.SourceTare.SendKeys(source_tare);
                //start weight
                WD.SimulatorWindow.weight.SetText(source_start);
                WD.SimulatorWindow.OK.Click();
                WD.mainWindow.ScaleWeightInternalFrame.start.Click();
                //check weight shows 220g
                WD.mainWindow.GetSnapshot(Resultpath + "weight1.PNG");
                Base_Assert.AreEqual("220.0", WD.mainWindow.ScaleWeightInternalFrame.WeightNumber.AttachedText, "weight1");
                //input 300 weight
                WD.SimulatorWindow.weight.SetText(source_left);
                WD.SimulatorWindow.OK.Click();
                //check weight shows 200g
                WD.mainWindow.GetSnapshot(Resultpath + "weight2.PNG");
                Base_Assert.AreEqual("200.0", WD.mainWindow.ScaleWeightInternalFrame.WeightNumber.AttachedText, "weight2");
                //check Weighing Info vaule
                WD.mainWindow.GetSnapshot(Resultpath + "Weighing Info Value.PNG");

                //check reset
                WD.mainWindow.ScaleWeightInternalFrame.reset.Click();
                if (WD.ErrorDialog.IsExist())
                {
                    WD.ErrorDialog.OKButton.Click();
                }
                //check record in db
                SqlHelper helper1 = new SqlHelper();
                string SQL1 = $"SELECT TOP(1) TARGET_TARE,BEGIN_SOURCE_GROSS,END_SOURCE_GROSS FROM EBR_WD_WEIGH_HISTORY ORDER BY WEIGH_ID DESC;";
                List<List<string>> Source1 = helper1.Execute(SQL1);
                var Source_Container_Tare1 = Source1[0][0];
                var InitailGross1 = Source1[0][1];
                var FinalGross1 = Source1[0][2];
                Base_Assert.AreEqual(Source_Container_Tare1, "100.0");
                Base_Assert.AreEqual(InitailGross1, FinalGross1);
                //check cancel
                WD_Fuction.SelectMehod(method, barcode);
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
                WD.mainWindow.ScaleWeightInternalFrame.start.Click();
                WD.SimulatorWindow.weight.SetText(source_left);
                WD.SimulatorWindow.OK.Click();
                //check cancel
                WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
                if (WD.ErrorDialog.IsExist())
                {
                    WD.ErrorDialog.OKButton.Click();
                }
                //check recode in DB
                SqlHelper helper = new SqlHelper();
                string SQL = $"SELECT TOP(1) TARGET_TARE,BEGIN_SOURCE_GROSS,END_SOURCE_GROSS FROM EBR_WD_WEIGH_HISTORY ORDER BY WEIGH_ID DESC;";
                List<List<string>> Source = helper.Execute(SQL);
                var Source_Container_Tare = Source[0][0];
                var InitailGross = Source[0][1];
                var FinalGross = Source[0][2];
                Base_Assert.AreEqual(Source_Container_Tare, "100.0");
                Base_Assert.IsTrue(float.Parse(InitailGross) > float.Parse(FinalGross));
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
