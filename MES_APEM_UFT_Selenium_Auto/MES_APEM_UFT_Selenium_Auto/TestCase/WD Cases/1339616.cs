using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using OpenQA.Selenium;
using System.IO;
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(1339616)]
        [Title("UC1332305_Manual Weighing with UOM as EA should work as expected when weighing")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_1339616()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "testEA";
            string material = WDMaterial.X0125;
            string barcode = "X012501";
            string net = "23";
            APRM_Fuction.FirstInitailAPRMWD();
            //active order 
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            Thread.Sleep(5000);
            Web_Fuction.login();
            Thread.Sleep(5000);
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            LogStep(@"1. Open Wd client and login");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            LogStep(@"2. click into order_Dispensing");
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD.mainWindow.GetSnapshot(Resultpath + "method_Disabled.PNG");
            Base_Assert.IsFalse(WD.mainWindow.ScaleWeightInternalFrame.dispense_method._UFT_IList.IsEnabled);
            Base_Assert.IsFalse(WD.mainWindow.ScaleWeightInternalFrame.scale._UFT_IList.IsEnabled);
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.barcode.IsEnabled);
            //input barcode
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys(barcode);
            if (WD.ConfirmationDialog.IsExist())
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.CheckResult.AttachedText.Contains("Input target container net"));
            WD.mainWindow.ScaleWeightInternalFrame.net_editor.SendKeys(net);
            Thread.Sleep(3000);
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            ////weighing report
            Web_Fuction.gotoTab(WDWebTab.report);
            Thread.Sleep(3000);
            Web.Report_Page.Weighing.Click();
            Thread.Sleep(3000);
            Web.Report_Page.Generate_Report.Click();
            Thread.Sleep(3000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "WeighReport.PNG");
            var heads = Web.Report_Page.Report_Heads;
            int UOM_headindex = 0;
            int i = 0;
            foreach (var h in heads)
            {
                if (h.Text == "UOM")
                {
                    UOM_headindex = i;
                }
                i++;
            }
            var table_datas = Web.Report_Page.Report_Table_Rows;
            string weigh1_UOM = table_datas[0].FindElements(By.TagName("td"))[UOM_headindex].Text;
            Console.WriteLine(weigh1_UOM);
            Base_Assert.AreEqual(weigh1_UOM, "EA");
            ////order report
            Web_Fuction.gotoTab(WDWebTab.order);
            Thread.Sleep(3000);
            Web.Order_Page.SearchInput.SendKeys(order);
            Thread.Sleep(2000);
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
            Console.Write(ReportText);
            int lineCount = 0;
            using (StringReader reader = new StringReader(ReportText))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineCount++;
                    if (lineCount == 16) // 跳转到指定行号  
                    {
                        Base_Assert.IsTrue(line.Contains("X012501"));
                        Base_Assert.IsTrue(line.Contains("23 EA"));
                    }

                }
            }
            driver.Close();
            ////APRM record
            LogStep(@"Open Batch query tool ");
            Application.LaunchBatchQueryTool();
            Thread.Sleep(3000);
            //open new query
            BatchQueryTool.NewQuery();
            //open batch detail display
            BatchQueryTool.BatchQueryToolWindow.ListView._STD_ListView.ActivateItem(order);
            //wait for loading
            Thread.Sleep(15000);
            //X0125Partial :Begin_Source_Gross is 1000 and End_Source_Gross is 600
            APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1]").Expand();
            APRM.BatchMainWindow.TreeView.Select("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1]");
            //APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1]").Expand();
            //APRM.BatchMainWindow.TreeView.Select("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1];Container [1]");
            //wait for loading
            Thread.Sleep(5000);
            APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM_RECORD.PNG");
            APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("UOM");
            Base_Assert.AreEqual("EA", APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text);
        }

    }
}