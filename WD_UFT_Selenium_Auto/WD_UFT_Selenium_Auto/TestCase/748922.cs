using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(748922)]
        [Title("UC693794_W&D weighing report_Select other weighing method except Net Removal and Double Check")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_748922()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string barcode2 = "X0125002";
            string tare = "10";
            string net = "454.4";
            //defect 875266--new source


            LogStep(@"1. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            LogStep(@"2. Active order");
            Web_Fuction.gotoTab(WDWebTab.order);
            //Web_Fuction.active_order(order);
            //LogStep(@"3. Open WD client and select order");
            //Application.LaunchWDAndLogin();
            //WD_Fuction.SelectOrderandMaterial(order, material);
            //WD_Fuction.SelectMehod(method, barcode);
            //LogStep(@"4. do Reset,Cancel,New Source, Partial dispense and Accept");
            ////click reset
            //set_weight("10", "100");
            //WD.mainWindow.ScaleWeightInternalFrame.reset.Click();
            ////click cancel
            //WD_Fuction.SelectMehod(method, barcode);
            //set_weight("15", "165");
            //WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
            //Thread.Sleep(2000);
            //WD.mainWindow.MaterialInternalFrame.materialTable.Row(material).Click();
            //WD.mainWindow.MaterialInternalFrame.next.Click();
            ////click new source
            //WD_Fuction.SelectMehod(method, barcode);
            //set_weight("20", "120");
            //WD.mainWindow.ScaleWeightInternalFrame.NewSource.Click();
            ////click partial dispense
            //WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys(barcode2);
            //WD.SimulatorWindow.weight.SetText("200");
            //WD.SimulatorWindow.OK.Click();
            //WD.mainWindow.ScaleWeightInternalFrame.Partial.Click();
            //if (WD.ErrorDialog.IsExist())
            //{
            //    WD.ErrorDialog.OKButton.Click();
            //}
            ////click accept
            //set_weight("10", "254.4");
            //WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            //if (WD.ErrorDialog.IsExist())
            //{
            //    WD.ErrorDialog.OKButton.Click();
            //}
            ////check Finish Dispense
            //Base_Assert.IsTrue(WD.mainWindow.Material_SelectionInternalFrame.IsExist() || WD.mainWindow.MaterialInternalFrame.IsExist(), "Finish Dispense");
            //WD_Fuction.Close();
            LogStep(@"4. Check weight report");
            Web_Fuction.gotoTab(WDWebTab.report);
            Web.Report_Page.Weighing.Click();
            Web.Report_Page.Generate_Report.Click();

            var columns = new List<string>() { "Begin Source", "End Source" };
            var datatexts = new List<string>() { "", "" };

            var data_list = new List<List<string>>();
            var head_list = new List<string>();
            var head = Web.Report_Page.Report_Table._Selenium_WebElement.FindElements(By.XPath("//table[@class='Report_Paper_Border_Shading']/tbody/tr[4]/td/table/tbody/tr/td/div//a[@class='Report_Head_Style']"));
            //get head list
            foreach (var h in head)
            {
                head_list.Add(h.Text);
            }
            
            //check Begin Source and End Source should be next to the  Source HU field
            int no = head_list.IndexOf("Source HU");
            int begin = head_list.IndexOf(columns[0]);
            int end = head_list.IndexOf(columns[1]);
            Base_Assert.IsTrue((no == begin - 1) && (begin == end-1), "Begin Source and End Source are next to the  Source HU field");

            var row = Web.Report_Page.Report_Table._Selenium_WebElement.FindElements(By.XPath("//table[@class='Report_Paper_Border_Shading']/tbody/tr[4]/td/table/tbody/tr/td[@class='Inner_Column_Left']/.."));
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
            //check action is right
            int ac = head_list.IndexOf("Action");
            var action = new List<string>() { "Reset", "Cancel", "New Source", "Partial Dispense", "Accept"};
            for (int m = 0; m < data_list.Count; m++)
            {
                Console.WriteLine(data_list[m][ac]);
                //Base_Assert.AreEqual(action, data_list[m][ac]);
            }
            //check begin/end source should be empty.
            for (int i = 0; i < columns.Count; i++)
            {
                int number = head_list.IndexOf(columns[i]);
                string datatext = datatexts[i];
                for (int m = 0; m < data_list.Count; m++)
                {
                    Base_Assert.AreEqual(datatext, data_list[m][number]);
                }
            }

            driver.Close();

        }


        public void set_weight(string tare,string net)
        {
            //zeor
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.SimulatorWindow.weight.SetText(tare);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //weight
            WD.SimulatorWindow.weight.SetText(net);
            WD.SimulatorWindow.OK.Click();
        }
    }
}
