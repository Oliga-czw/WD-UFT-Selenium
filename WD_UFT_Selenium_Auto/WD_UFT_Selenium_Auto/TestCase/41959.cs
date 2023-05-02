using System;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections;
using HP.LFT.SDK.Java;
using HP.LFT.Verifications;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(41959)]
        [Title("scale check-cancel when not all weights are checked.")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_41959()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. Login WD execution, click 'Scale check'");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.ScaleChecking.Click();
            Thread.Sleep(3000);

            var scaleList = WD.mainWindow.ScaleCheckInternalFrame.ScaleList;
            var standardizationStatusTable = WD.mainWindow.ScaleCheckInternalFrame.Standardization_type;
            var Selectedstandardization = standardizationStatusTable.GetCell(0, "ID").Value.ToString();
            var selectedlastcheckdate = standardizationStatusTable.GetCell(0, "Last Check Date").Value.ToString();
            //2. do a scale check,go back to scale check again, check the Standardization Status(test - cancel before clicking zero )
            //3. select one standardization type, click start check
            standardizationStatusTable.SelectRows(0);
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.Click();
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.DoubleClick();
            Thread.Sleep(3000);

            var standardizationlabel = WD.mainWindow.CheckWeightInternalFrame.Standardization_label;

            Assert.AreEqual(standardizationlabel._UFT_Label.Text, Selectedstandardization);
            //Click cancel
            WD.mainWindow.CheckWeightInternalFrame.cancelButton.Click();
            Assert.IsTrue(WD.mainWindow.ScaleCheckInternalFrame.IsEnabled);
           // Click scale check(test - move back to home after selecting standardization type)
            standardizationStatusTable.SelectRows(0);
            WD.mainWindow.ScaleCheckInternalFrame.homeButton.Click();
            Assert.IsTrue(WD.mainWindow.HomeInternalFrame.IsEnabled);
            WD.mainWindow.HomeInternalFrame.ScaleChecking.Click();
            Thread.Sleep(3000);
            //Click "Scale check"(test - cancel before read scale
            standardizationStatusTable.SelectRows(0);
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.Click();
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.DoubleClick();
            Thread.Sleep(3000);
            //with plate empty, click Zero button
            WD.mainWindow.CheckWeightInternalFrame.zero.Click();
            
            Assert.AreEqual(WD.mainWindow.CheckWeightInternalFrame.ScaleResult_Label._UFT_Label.Text, "0.0 G");
            Assert.IsFalse(WD.mainWindow.CheckWeightInternalFrame.zero.IsEnabled);
            
            Assert.IsTrue(WD.mainWindow.CheckWeightInternalFrame.readScale.IsEnabled);
            //Click Cancel
            WD.mainWindow.CheckWeightInternalFrame.cancelButton.Click();
            Assert.IsTrue(WD.mainWindow.ScaleCheckInternalFrame.IsEnabled);
            Assert.AreEqual(selectedlastcheckdate, standardizationStatusTable.GetCell(0, "Last Check Date").Value.ToString());
            WD_Fuction.Close();
            
        }

    }
}