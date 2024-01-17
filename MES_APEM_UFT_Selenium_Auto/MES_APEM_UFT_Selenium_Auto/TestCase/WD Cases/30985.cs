using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Text;
using OpenQA.Selenium;
using System.Linq;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(30985)]
        [Title("V8.8.4-Test Print Label in Open Weighing screen.")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_30985()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. Open Wd client and login");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            LogStep(@"2.Click Open Weighing button in the bottom left corner of the screen");
            WD.mainWindow.HomeInternalFrame.OpenWeigh.Click();
            Thread.Sleep(5000);
            LogStep(@"3. Select a Ready scale configured for the workstation.");
            WD.mainWindow.OpenWeighInternalFrame.Scale_select._UFT_IList.Select("simulator");

            LogStep(@"4. Click Zero button");
            WD.mainWindow.OpenWeighInternalFrame.zero.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "Zero.PNG");
            LogStep(@"5. Input a tare to platform and click Tare.");
            WD.SimulatorWindow.weight.SetText("100");
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.OpenWeighInternalFrame.tare._UFT_Button.Click();
            Thread.Sleep(5000);
            var TarestLabel = WD.mainWindow.OpenWeighInternalFrame.TarestLabel;
            var NetstLabel = WD.mainWindow.OpenWeighInternalFrame.NetstLabel;
            var gStLabel = WD.mainWindow.OpenWeighInternalFrame.ScaleReading;
            var GrossstLabel = WD.mainWindow.OpenWeighInternalFrame.GrossstLabel;

            Assert.AreEqual(gStLabel._UFT_Label.Text, "0.0 G");
            Assert.AreEqual(TarestLabel._UFT_Label.Text, "100.0");
            Assert.AreEqual(NetstLabel._UFT_Label.Text, "0.0");
            Assert.AreEqual(GrossstLabel._UFT_Label.Text, "100.0");
            WD.mainWindow.GetSnapshot(Resultpath + "Tare.PNG");
            LogStep(@"6.Input sample material to platform");
            WD.SimulatorWindow.weight.SetText("300");
            WD.SimulatorWindow.OK.Click();

            Assert.AreEqual(gStLabel._UFT_Label.Text, "200.0 G");
            Assert.AreEqual(TarestLabel._UFT_Label.Text, "100.0");
            Assert.AreEqual(NetstLabel._UFT_Label.Text, "200.0");
            Assert.AreEqual(GrossstLabel._UFT_Label.Text, "300.0");
            WD.mainWindow.GetSnapshot(Resultpath + "weighing.PNG");
            WD.mainWindow.OpenWeighInternalFrame.PrintLabelButton.Click();
            Thread.Sleep(3000);
        }

        
    }
}