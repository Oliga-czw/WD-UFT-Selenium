using System;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(31169)]
        [Title("scale check-cancel after all weights are checked")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_31169()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. Open Wd client and login");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.ScaleChecking.Click();
            Thread.Sleep(3000);

            var scaleList = WD.mainWindow.ScaleCheckInternalFrame.ScaleList;
            Assert.IsNotNull(scaleList._UFT_IList.GetItem("simulator"));
            Assert.IsNotNull(scaleList._UFT_IList.GetItem("simulator001"));

            scaleList.SelectItems("simulator");
            LogStep(@"2. do a scale check,go back to scale check again, check the Standardization Status");
            var standardizationStatusTable = WD.mainWindow.ScaleCheckInternalFrame.Standardization_type;
            System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", standardizationStatusTable._UFT_Table.Rows.Count.ToString());

            WD.mainWindow.ScaleCheckInternalFrame.startcheck.Click();
            Thread.Sleep(2000);
            WD.mainWindow.CheckWeightInternalFrame.cancelButton.Click();
            Thread.Sleep(2000);
            // standardization types are show correctly

            scaleList.SelectItems("simulator");
            WD.mainWindow.GetSnapshot(Resultpath + "standardizationShow.PNG");
            // var StandardTable = standardizationStatusTable.Rows;
            //Verify.Contains(@"STD-daily", standardizationStatusTable.Rows);
            //Verify.Contains(@"STD-monthly", standardizationStatusTable.Rows);
            //Verify.Contains(@"STD-weekly", standardizationStatusTable.Rows);
            LogStep(@"3.select one standardization type, click start check");
            WD.mainWindow.ScaleCheckInternalFrame.startcheck.Click();
            Thread.Sleep(2000);
            // scale information should be same as that defined in web
            WD.mainWindow.GetSnapshot(Resultpath + "scale_information.PNG");
            var StandardizationLabel = WD.mainWindow.CheckWeightInternalFrame.Standardization_label._UFT_Label.Text;
            var LastCheckdateLabel = WD.mainWindow.CheckWeightInternalFrame.LastCheckdate_label._UFT_Label.Text;
            var ExpirationDateLabel = WD.mainWindow.CheckWeightInternalFrame.ExpirationDate_label._UFT_Label.Text;
            var ExpirationPeriodLabel = WD.mainWindow.CheckWeightInternalFrame.ExpirationPeriod_label._UFT_Label.Text;
            Base_Assert.AreEqual(StandardizationLabel, "STD-daily");
            Base_Assert.AreEqual(ExpirationDateLabel, "5/22/25, 12:00:00 PM");
            Base_Assert.AreEqual(LastCheckdateLabel, "4/6/23, 12:00:00 PM");

            Base_Assert.AreEqual(ExpirationPeriodLabel, "777");
            LogStep(@"4.with plate empty, click Zero button");
            WD.mainWindow.CheckWeightInternalFrame.zero.Click();
            //it should read as 0 after click Zero, Zero button disabled? Read Scale is enabled
            WD.mainWindow.GetSnapshot(Resultpath + "after_ClickZero.PNG");
            Base_Assert.AreEqual(WD.mainWindow.CheckWeightInternalFrame.zero.IsEnabled, false);
            var btnReadScale = WD.mainWindow.CheckWeightInternalFrame.readScale;
            Base_Assert.AreEqual(btnReadScale.IsEnabled, true);
            LogStep(@"5. put the weight in the plate shown in Check Weight list, Click Read Scale");
            WD.SimulatorWindow.weight.SetText("100");
            WD.SimulatorWindow.OK._UFT_Button.Click();
            Thread.Sleep(2000);
            btnReadScale._UFT_Button.Click();
            Thread.Sleep(3000);
            //Base_Assert.AreEqual(WD.mainWindow.ScaleCheckInternalFrame.Standardization_type._UFT_Table.IsEnabled, true);
            //it should show green check mark if it is in allowed Precision range. 
            var checkMark = WD.mainWindow.CheckWeightInternalFrame.checkTable.GetCell(0, "Pass");
            //Base_Assert.AreEqual(checkMark.Value.GetType().Attributes, "");
            Base_Assert.AreEqual(WD.mainWindow.CheckWeightInternalFrame.CheckResult._UFT_Label.Text, "All scale checks passed.");
            WD.mainWindow.GetSnapshot(Resultpath + "checkPass.PNG");
            WD.mainWindow.CheckWeightInternalFrame.cancelButton.ClickSignle();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(WD.mainWindow.ScaleCheckInternalFrame.IsEnabled, true);
            Base_Assert.AreEqual(ExpirationDateLabel, "5/22/25, 12:00:00 PM");
            Base_Assert.AreEqual(LastCheckdateLabel, "4/6/23, 12:00:00 PM");
        }

        
    }
}