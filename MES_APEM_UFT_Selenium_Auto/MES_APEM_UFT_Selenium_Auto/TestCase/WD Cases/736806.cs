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
        [TestCaseID(736806)]
        [Title("UC693791_W&D_Enhance Double Check: record gross weight before and after weighing")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(1000000)]

        [TestMethod]
        public void VSTS_736806()
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
            LogStep(@"4. Accept the dispense");

            //click accept
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
            //weight
            WD.SimulatorWindow.weight.SetText(net);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "Double check accept.PNG");
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            //double check
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.SimulatorWindow.weight.SetText(endsource);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            //check Finish Dispense
            Base_Assert.IsTrue(WD.mainWindow.Material_SelectionInternalFrame.IsExist() || WD.mainWindow.MaterialInternalFrame.IsExist(), "Finish Dispense");
            WD_Fuction.Close();

            LogStep(@"4. Check gross weight");
            //check table EBR_WD_WEIGH_HISTORY
            SqlHelper helper = new SqlHelper();
            string SQL = $"SELECT BEGIN_SOURCE_GROSS,END_SOURCE_GROSS FROM EBR_WD_WEIGH_HISTORY";
            List<List<string>> Source = helper.Execute(SQL);
            var Begin_Source = Source[0][0];
            var End_Source = Source[0][1];
            Base_Assert.AreEqual(Begin_Source, "1000.0");
            Base_Assert.AreEqual(End_Source, "556.0");
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
            //check Begin Source and End Source should be next to the  Source HU field
            int no = head_list.IndexOf("Source HU");
            int begin = head_list.IndexOf(columns[0]);
            int end = head_list.IndexOf(columns[1]);
            Base_Assert.IsTrue((no == begin - 1) && (begin == end-1), "Begin Source and End Source are next to the  Source HU field");

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
            //check order print
            Web_Fuction.gotoTab(WDWebTab.order);
            Thread.Sleep(5000);
            Web.Order_Page.SearchInput.SendKeys("test1");
            Thread.Sleep(5000);
            Web.Order_Page.orderCheckbox.Click();
            Thread.Sleep(2000);
            Web.Order_Page.PrintReport.Click();
            Thread.Sleep(10000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "orderPrint.PNG");
            var urlll = Selenium_Driver._Selenium_Driver.FindElement(By.XPath("//iframe[@class='gwt-Frame']")).GetAttribute("src");
            string[] parts = urlll.Split('/');
            string ReportFileName = parts[parts.Length - 1];
            Console.WriteLine(ReportFileName);
            string ReportText = Web_Fuction.OrderPrint(ReportFileName);
            int lineCount = 0;
            using (StringReader reader = new StringReader(ReportText))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineCount++;
                    if (lineCount == 16) // 跳转到指定行号  
                    {
                        Base_Assert.IsTrue(line.Contains("X0125001"));
                    }
                    else if (lineCount == 19)
                    {
                        Base_Assert.IsTrue(line.Contains("Begin Source         1,000.0 G"));
                    }
                    else if (lineCount == 20)
                    {
                        Base_Assert.IsTrue(line.Contains("End Source           556.0 G"));
                    }

                }
            }
            Web.Order_Page.printreportDialogCloseButton.Click();
            driver.Close();
            //check APRM batch
            Application.LaunchBatchDetailDisplay();
            Batch_Fuction.findBatch(order);
            //wait for loading
            Thread.Sleep(40000);
            //X0125Accept :Begin_Source_Gross is 1000 and End_Source_Gross is 556
            APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1]").Expand();
            APRM.BatchMainWindow.TreeView.Select("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1];Container [1]");
            //wait for loading
            Thread.Sleep(5000);
            APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM Batch detail(Accept).PNG");
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
