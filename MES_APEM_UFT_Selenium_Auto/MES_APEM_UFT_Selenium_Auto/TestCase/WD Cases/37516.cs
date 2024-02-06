using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections;
using HP.LFT.SDK.Java;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(37516)]
        [Title("V10Enh-Verify calibration expired in minutes should be blocked in execution.")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_37516()
        {

            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. Login WD execution, click 'Scale check'");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.ScaleChecking.Click();
            Thread.Sleep(3000);
            var scaleList = WD.mainWindow.ScaleCheckInternalFrame.ScaleList;
            scaleList.SelectItems("simulator");
            var standardizationStatusTable = WD.mainWindow.ScaleCheckInternalFrame.Standardization_type;
            //System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", standardizationStatusTable._UFT_Table.Rows.Count.ToString());
            Thread.Sleep(2000);
            ArrayList arrayList = new ArrayList();
            var headers = standardizationStatusTable._UFT_Table.ColumnHeaders;
            for (int a = 0; a < headers.Count; a = a + 1) {
                arrayList.Add(headers[a]);
            }
            WD.mainWindow.GetSnapshot(Resultpath + "ScaleCheck.PNG");
            Base_Assert.IsFalse(arrayList.Contains("Last Calibration date"));
        }

        
    }
}