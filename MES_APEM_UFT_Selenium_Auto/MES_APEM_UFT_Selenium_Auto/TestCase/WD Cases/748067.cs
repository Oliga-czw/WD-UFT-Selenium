using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(748067)]
        [Title("UC693790_W&D_Enhance Net Removal: record gross weight when cancel the weighing")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(1000000)]

        [TestMethod]
        public void VSTS_748067()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string order = "test1";
            string material = WDMaterial.X0125;
            string method = WDMethod.Netremoval;
            string barcode = "X0125001";
            string source_left = "556";
            string tare = "15";
            string source_start = "1000";
            string scale = "simulator";
            string beginsource = "1000";
            string endsource = "556";

            string Configpath = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string ConfigKey1 = "NET_REMOVAL_REQUIRE_TARGET_TARE =0";
            //APRM 
            APRM_Fuction.InitailAPRMWD();
            try
            {
                LogStep(@"1. Set key in config file");
                Base_Function.AddConfigKey(Configpath, ConfigKey1);
                //codify all 
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                Thread.Sleep(13000);
                LogStep(@"1. Open WD web");
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Web_Fuction.gotoWDWeb(driver);
                driver.Wait();
                Web_Fuction.login();
                driver.Wait();
                LogStep(@"2. Active order");
                Web_Fuction.gotoTab(WDWebTab.order);
                Web_Fuction.active_order(order);
                LogStep(@"3. Open WD client and reset net removal");
                Application.LaunchWDAndLogin();
                WD_Fuction.SelectOrderandMaterial(order, material);
                WD_Fuction.SelectMehod(method, barcode);
                //select simulator
                if (WD.MessageDialog.IsExist())
                {
                    WD.MessageDialog.OKButton.Click();
                }
                WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
                //zeor
                WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
                //no tare
                //WD.mainWindow.ScaleWeightInternalFrame.tare_editor.SetText(tare, true);
                //start weight
                WD.SimulatorWindow.weight.SetText(source_start);
                WD.SimulatorWindow.OK.Click();
                WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
                //input remove weight
                WD.SimulatorWindow.weight.SetText(source_left);
                WD.SimulatorWindow.OK.Click();
                //check reset 
                WD.mainWindow.GetSnapshot(Resultpath + "Net removal Cancel.PNG");
                WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
                //exit wd client
                Base_Assert.IsTrue(WD.mainWindow.Material_SelectionInternalFrame.IsExist() || WD.mainWindow.MaterialInternalFrame.IsExist(), "Exit Dispense");
                WD_Fuction.Close();

                LogStep(@"4. Check begin/end source in WEB");
                //Check weight report
                Web_Fuction.gotoTab(WDWebTab.report);
                Web.Report_Page.Weighing.Click();
                Thread.Sleep(5000);
                Web.Report_Page.Generate_Report.Click();
                Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Weight Report.PNG");

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
                driver.Close();
                LogStep(@"5. Check begin/end source in DB");
                //check table EBR_WD_WEIGH_HISTORY
                SqlHelper helper = new SqlHelper();
                string SQL = $"SELECT BEGIN_SOURCE_GROSS,END_SOURCE_GROSS FROM EBR_WD_WEIGH_HISTORY";
                List<List<string>> Source = helper.Execute(SQL);
                var Begin_Source = Source[0][0];
                var End_Source = Source[0][1];
                Base_Assert.AreEqual(Begin_Source, "1000.0");
                Base_Assert.AreEqual(End_Source, "556.0");
                LogStep(@"6. Check begin/end sourcein batch");
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
            finally
            {
                LogStep(@"4.delete config key ");
                Base_Function.DeleteConfigKey(Configpath, ConfigKey1);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);

            }
        }
    }
}
