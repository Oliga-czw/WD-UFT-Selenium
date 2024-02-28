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

        [TestCaseID(39755)]
        [Title("V8.8.4-Test Zero/Tare buttons in Open Weighing screen.")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_39755()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. Open Wd client and login");
            Application.LaunchWDAndLogin();
            LogStep(@"2. click into openWeigh ");
            WD.mainWindow.HomeInternalFrame.OpenWeigh.Click();
            Thread.Sleep(5000);
            LogStep(@"3. select a scale and and click Zero");
            WD.mainWindow.OpenWeighInternalFrame.Scale_select.SelectItems("simulator");
            WD.mainWindow.OpenWeighInternalFrame.zero.Click();
            var gStLabel = WD.mainWindow.OpenWeighInternalFrame.ScaleReading;
            WD.mainWindow.GetSnapshot(Resultpath + "Zeroclick.PNG");
            Base_Assert.AreEqual(gStLabel._UFT_Label.Text, "0.0 G");
            LogStep(@"4. Input a tare to platform and click Tare.");
            var inputPlatform = WD.SimulatorWindow.weight;
            inputPlatform.SetText("100");
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(2000);
            WD.mainWindow.SetActive();
            WD.mainWindow.OpenWeighInternalFrame.tare._UFT_Button.Click();
            Thread.Sleep(2000);
            WD.mainWindow.GetSnapshot(Resultpath + "Tareclick.PNG");
            var Tarest = WD.mainWindow.OpenWeighInternalFrame.TarestLabel;
            var Netst = WD.mainWindow.OpenWeighInternalFrame.NetstLabel;
            var Grossst = WD.mainWindow.OpenWeighInternalFrame.GrossstLabel;
            Base_Assert.AreEqual(gStLabel._UFT_Label.Text, "0.0 G");
            Base_Assert.AreEqual(Tarest._UFT_Label.Text, "100.0");
            Base_Assert.AreEqual(Netst._UFT_Label.Text, "0.0");
            Base_Assert.AreEqual(Grossst._UFT_Label.Text, "100.0");
            LogStep(@"5.Input sample material to platform");
            WD.mainWindow.GetSnapshot(Resultpath + "WeighMaterial.PNG");
            WD.SimulatorWindow.SetActive();
            WD.SimulatorWindow.weight.SetText("300");
            WD.SimulatorWindow.OK.Click();
            Base_Assert.AreEqual(gStLabel._UFT_Label.Text, "200.0 G");
            Base_Assert.AreEqual(Tarest._UFT_Label.Text, "100.0");
            Base_Assert.AreEqual(Netst._UFT_Label.Text, "200.0");
            Base_Assert.AreEqual(Grossst._UFT_Label.Text, "300.0");
            WD_Fuction.Close();
        }


    }
}