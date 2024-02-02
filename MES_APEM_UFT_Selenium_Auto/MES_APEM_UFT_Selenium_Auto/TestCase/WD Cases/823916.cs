using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.Linq;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(823916)]
        [Title("UC813649_'Reset' and 'Cancel' button for 'Source as Target' method should work if SOURCE_TARGET_REQUIRE_TARGET_TARE = 0 is set on flags.m2r_cfg on server side")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
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
                WD.mainWindow.ScaleWeightInternalFrame.dispense_method.SelectItems(method);
                //check The Weighing Info
                WD.mainWindow.GetSnapshot(Resultpath + "Weighing Info.PNG");
                string initial_actual = WD.mainWindow.ScaleWeightInternalFrame.InitailGross.AttachedText;
                string final_actual = WD.mainWindow.ScaleWeightInternalFrame.FinalGross.AttachedText;
                string difference_actual = WD.mainWindow.ScaleWeightInternalFrame.Diffenence.AttachedText;
                string[] a = { initial_actual, final_actual, difference_actual };
                string[] e = { "Initial Gross:", "Final Gross:", "Difference:" };
                Base_Assert.IsTrue(e.SequenceEqual(a), "Weighing Info");

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
                //check record in db

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
                Base_Function.DeleteConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
            }
            

        }

    }
}
