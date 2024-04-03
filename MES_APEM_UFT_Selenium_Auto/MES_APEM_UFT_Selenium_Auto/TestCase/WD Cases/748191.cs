using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.IO;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(748191)]
        [Title("UC693793_Check \"Begin source gross\" and \"End source gross\" records generated for double check integrated into ARPM when cancel the weight")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(1000000)]

        [TestMethod]
        public void VSTS_748191()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";
            string material = WDMaterial.X0125;
            string method = WDMethod.Doublecheck;
            string scale = "simulator";
            string barcode = "X0125001";
            string tare = "15";
            string beginsource = "1000";
            string net = "459";
            string endsource = "556";

            //APRM 
            APRM_Fuction.InitailAPRMWD();
            LogStep(@"1. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            LogStep(@"2. Active order");
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            LogStep(@"3. Open WD client and select order");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            //select simulator
            if (WD.MessageDialog.IsExist())
            {
                WD.MessageDialog.OKButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
            LogStep(@"4. Cancel the dispense");
            //cancel before weighting
            WD.mainWindow.GetSnapshot(Resultpath + "Double check Cancel before weighting.PNG");
            WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
            //check no record
            Web_Fuction.gotoTab(WDWebTab.report);
            Web.Report_Page.Weighing.Click();
            Thread.Sleep(5000);
            Web.Report_Page.Generate_Report.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Weight Report no record cancel.PNG");
            string js = "return document.evaluate(\"//td[text()='X0125001']\", document).iterateNext()";
            Base_Assert.IsTrue(driver.execute_script_return(js) == null, "Weight report is null");
            //cancel after weighting
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            //select simulator
            if (WD.MessageDialog.IsExist())
            {
                WD.MessageDialog.OKButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
            //begin
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.SimulatorWindow.weight.SetText(beginsource);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //tare
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.SimulatorWindow.weight.SetText(tare);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //weight and reset
            WD.SimulatorWindow.weight.SetText(net);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "Double check Cancel after weighting.PNG");
            WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
            WD_Fuction.Close();
            LogStep(@"4. Check gross weight");
            //Check weight report
            Web_Fuction.gotoTab(WDWebTab.report);
            Web.Report_Page.Weighing.Click();
            Thread.Sleep(5000);
            Web.Report_Page.Generate_Report.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Weight Report Cancel.PNG");
            var columns = new List<string>() { "Begin Source", "End Source" };
            var datatexts = new List<string>() { "1,000.0", "556.0" };

            var data_list = new List<List<string>>();
            var head_list = new List<string>();
            //get head list
            foreach (var h in Web.Report_Page.Report_Heads)
            {
                head_list.Add(h.Text);
            }

            var row = Web.Report_Page.Report_Table_Rows;
            //get table data
            for (int j = 0; j < row.Count; j++)
            {
                var single_row_text = new List<string>();
                var cells = row[j].FindElements(By.CssSelector("td.Inner_Column_Left"));
                foreach (var cell in cells)
                {
                    single_row_text.Add(cell.Text);

                }
                data_list.Add(single_row_text);
            }
            //check begin/end source .
            for (int i = 0; i < columns.Count; i++)
            {
                int number = head_list.IndexOf(columns[i]);
                string datatext = datatexts[i];
                for (int m = 0; m < data_list.Count; m++)
                {
                    Base_Assert.AreEqual(datatext, data_list[m][number]);
                }
            }
            //check inventory 
            Web_Fuction.gotoTab(WDWebTab.inventory);
            Thread.Sleep(5000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Iventory.PNG");
            string Nominal = Web_Fuction.get_inventory_data(barcode, "Nominal");
            string Actual = Web_Fuction.get_inventory_data(barcode, "Actual");
            Base_Assert.AreEqual(Nominal, "1,000", "inventory is same.");
            Base_Assert.AreEqual(Actual, "556.0", "inventory is same.");
            //check APRM batch
            Application.LaunchBatchDetailDisplay();
            Batch_Fuction.findBatch(order);
            //wait for loading
            Thread.Sleep(40000);
            //X0125Accept :Begin_Source_Gross is 1000 and End_Source_Gross is 1000
            APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1]").Expand();
            APRM.BatchMainWindow.TreeView.Select("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1];Action [1]");
            //wait for loading
            Thread.Sleep(5000);
            APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM Batch detail(Cancel).PNG");
            APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("End Source Gross");
            Base_Assert.AreEqual(endsource, APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text, "End Source Gross");
            APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
            APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("Begin Source Gross");
            Base_Assert.AreEqual(beginsource, APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text, "Begin Source Gross");
            APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
            APRM.BatchMainWindow.Close();

        }


     
    }
}
