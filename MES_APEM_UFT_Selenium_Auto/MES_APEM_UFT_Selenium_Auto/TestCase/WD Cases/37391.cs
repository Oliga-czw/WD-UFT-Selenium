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
        [TestCaseID(31095)]
        [Title("V8.8.4-Test quite Opening Weighing screen")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_37391()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. open the WD and open 'Open Weighing");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.OpenWeigh.Click();
            Thread.Sleep(2000);
            WD.mainWindow.GetSnapshot(Resultpath + "OpenWeigh.PNG");
            LogStep(@"2. Select a Ready scale configured for the workstation");
            var chkComboBoxList = WD.mainWindow.OpenWeighInternalFrame.Scale_select;
            chkComboBoxList.SelectItems("simulator");
            WD.mainWindow.Close();
            WD.CloseDialog.NoButton.Click();
            Base_Assert.IsTrue(WD.mainWindow.OpenWeighInternalFrame.IsEnabled);
            WD.mainWindow.OpenWeighInternalFrame.HomeButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.GetSnapshot(Resultpath + "OpenWeigh_BC_home.PNG");
            Base_Assert.IsTrue(WD.mainWindow.HomeInternalFrame.IsEnabled);
            WD.mainWindow.HomeInternalFrame.OpenWeigh.Click();
            chkComboBoxList.SelectItems("simulator");
            WD.mainWindow.Close();
            WD.CloseDialog.YesButton.Click();
            
        }

        
    }
}