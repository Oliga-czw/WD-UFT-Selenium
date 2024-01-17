using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(37819)]
        [Title("V8.8.4-Test scale connection successful for Opening Weighing screen.")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_37819()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. open the WD and open 'Open Weighing");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.OpenWeigh.Click();
            Thread.Sleep(2000);
            var scaleSelectBox = WD.mainWindow.OpenWeighInternalFrame.Scale_select;
            scaleSelectBox.SelectItems("simulator");
            Base_Assert.IsTrue(WD.mainWindow.OpenWeighInternalFrame.RangeMinLabel._UFT_Label.IsEnabled);
            Base_Assert.IsTrue(WD.mainWindow.OpenWeighInternalFrame.RangeMaxLabel._UFT_Label.IsEnabled);
            Base_Assert.IsTrue(WD.mainWindow.OpenWeighInternalFrame.ResolutionLabel._UFT_Label.IsEnabled);
            WD.SimulatorWindow.weight.SetText("100");
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "ScaleInformation.PNG");
            Base_Assert.AreEqual(WD.mainWindow.OpenWeighInternalFrame.GrossstLabel._UFT_Label.Text, "100.0");
            WD_Fuction.Close();
        }

    }   
}