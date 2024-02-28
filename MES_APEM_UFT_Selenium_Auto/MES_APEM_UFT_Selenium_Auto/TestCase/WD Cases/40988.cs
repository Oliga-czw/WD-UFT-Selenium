using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections;
using HP.LFT.SDK.Java;
using HP.LFT.Verifications;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(40988)]
        [Title("scale check-cancel when not all weights are checked.")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_40988()
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
            System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", Selectedstandardization);
            var selectedlastcheckdate = standardizationStatusTable.GetCell(1, "Last Check Date").Value.ToString();
            LogStep(@"2. do a scale check,go back to scale check again, check the Standardization Status");
            standardizationStatusTable.SelectRows(0);
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.Click();
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.DoubleClick();
            Thread.Sleep(3000);
            var standardizationlabel = WD.mainWindow.CheckWeightInternalFrame.Standardization_label;
            System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", standardizationlabel._UFT_Label.Text);
            Base_Assert.AreEqual(standardizationlabel._UFT_Label.Text, Selectedstandardization);
            WD.mainWindow.CheckWeightInternalFrame.cancelButton.Click();
            Thread.Sleep(2000);
            Assert.IsTrue(WD.mainWindow.ScaleCheckInternalFrame.IsEnabled);
            LogStep(@"3.select one standardization type, click start check");
            standardizationStatusTable.SelectRows(1);
            WD.mainWindow.ScaleCheckInternalFrame.StandardizationList.SelectItems("STD-monthly");
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.ClickSignle();
            Thread.Sleep(3000);
            LogStep(@"4. with plate empty, click Zero button");
            WD.mainWindow.CheckWeightInternalFrame.zero.Click();
            Base_Assert.AreEqual(WD.mainWindow.CheckWeightInternalFrame.ScaleResult_Label._UFT_Label.Text, "0.0 G");
            LogStep(@"5.put the weight in the plate shown in Check Weight list, Click Read Scale");
            WD.SimulatorWindow.weight.SetText("100");
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.CheckWeightInternalFrame.readScale.ClickSignle();
            var expirationDateTable = WD.mainWindow.CheckWeightInternalFrame.checkTable;

            Base_Assert.AreEqual(expirationDateTable.GetCell(0, "Actual").Value.ToString(), "100.0 G");
            Base_Assert.IsTrue(WD.mainWindow.CheckWeightInternalFrame.CheckResult._UFT_Label.Text.Contains("click Read Scale"));
            LogStep(@"6.Click Cancel when NOT all weights are checked");
            WD.mainWindow.CheckWeightInternalFrame.cancelButton.Click();
            Thread.Sleep(2000);
            Base_Assert.IsTrue(WD.mainWindow.ScaleCheckInternalFrame.IsEnabled);

            Base_Assert.AreEqual(selectedlastcheckdate, standardizationStatusTable.GetCell(1, "Last Check Date").Value.ToString());
        }

    }
}