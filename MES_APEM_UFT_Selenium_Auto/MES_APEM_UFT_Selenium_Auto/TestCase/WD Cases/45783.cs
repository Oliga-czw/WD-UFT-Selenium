using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(45783)]
        [Title("Booth Task View")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_45783()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. Open Wd client and login");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            Base_Assert.IsTrue(WD.mainWindow.HomeInternalFrame.IsEnabled);
            LogStep(@"2. click Booth Cleaning button");
            WD.mainWindow.HomeInternalFrame.BoothCleaning.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "booth cleaning.PNG");
            Base_Assert.IsTrue(WD.mainWindow.BoothCleanInternalFrame.IsEnabled);
            Thread.Sleep(5000);
            WD.mainWindow.BoothCleanInternalFrame.HomeButton.Click();
            LogStep(@"3. click Scale Checking button");
            WD.mainWindow.HomeInternalFrame.ScaleChecking.Click();
            Base_Assert.IsTrue(WD.mainWindow.ScaleCheckInternalFrame.IsEnabled);
            Thread.Sleep(5000);
            WD.mainWindow.ScaleCheckInternalFrame.homeButton.Click();
            LogStep(@"4. click Material Dispensing button");
            WD.mainWindow.HomeInternalFrame.MaterialDispensing.Click();
            Base_Assert.IsTrue(WD.mainWindow.Material_SelectionInternalFrame.IsEnabled);
            Thread.Sleep(5000);
            WD.mainWindow.Material_SelectionInternalFrame.HomeButton.Click();
            LogStep(@"5. click Order Dispensing button");
            WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            Base_Assert.IsTrue(WD.mainWindow.DispensingInternalFrame.IsEnabled);
            Thread.Sleep(5000);
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            LogStep(@"6. click Order Kitting button");
            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "OrderKitting.PNG");
            Base_Assert.IsTrue(WD.mainWindow.SelectAnOrderToKittingFrame.IsEnabled);
            Thread.Sleep(5000);
            WD.mainWindow.SelectAnOrderToKittingFrame.HomeButton.Click();
            LogStep(@"7. click Open Weighing button");
            WD.mainWindow.HomeInternalFrame.OpenWeigh.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "Open Weighing.PNG");
            Base_Assert.IsTrue(WD.mainWindow.OpenWeighInternalFrame.IsEnabled);
            WD_Fuction.Close();
        }

    }
}