using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(922837)]
        [Title("Inspired by customer defect 864958 -- Anomaly in APRM for Sequence number 10-1 becomes just 1 at Material level")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_922837()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";

            string order = "test1";
            string material = WDMaterial.M801890;

            string barcode = "M801890001";
            string tare = "15";
            string net = "65";

            //APRM 
            APRM_Fuction.InitailAPRMWD();
            LogStep(@"1. add material and active order");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            //active order
            Web_Fuction.active_order(order);
            Thread.Sleep(2000);
            //add material
            Web_Fuction.edit_order(order);
            Thread.Sleep(2000);
            Web.Order_Page.AddMaterial.Click();
            Thread.Sleep(2000);
            Web.Order_Page.Material.select_option("2 M801890 - M801890 Description");
            Web.Order_Page.Quantity.SendKeys("50");
            string sequence1 = Web.Order_Page.Sequence.GetAttribute("value");
            Web.Order_Page.Add_Material.Click();
            Thread.Sleep(2000);
            Web.Order_Page.Order_Apply.Click();
            Thread.Sleep(2000);
            Web_Fuction.edit_order(order);
            Thread.Sleep(2000);
            Web.Order_Page.AddMaterial.Click();
            Thread.Sleep(2000);
            Web.Order_Page.Material.select_option("2 M801890 - M801890 Description");
            Web.Order_Page.Quantity.SendKeys("50");
            string sequence2 = Web.Order_Page.Sequence.GetAttribute("value");
            Web.Order_Page.Add_Material.Click();
            Thread.Sleep(2000);
            Web.Order_Page.Order_Apply.Click();
            Thread.Sleep(2000);
            //add booth
            Web_Fuction.edit_order(order);
            Web.Order_Page.AllMaterialCheckBox.Click();
            Web.Order_Page.AssigntoBooth.Click();
            Web.Order_Page.OK.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "add material.PNG");
            Thread.Sleep(2000);
            Web.Order_Page.Order_Apply.Click();
            Thread.Sleep(2000);
            driver.Close();

            LogStep(@"2. open wd client and dispense");
            Application.LaunchWDAndLogin();
            //Finish order
            //X0125
            WD_Fuction.SelectOrderandMaterial(order, WDMaterial.X0125);
            WD_Fuction.SelectMehod(WDMethod.Net, "X0125001");
            WD_Fuction.FinishNetDiapense("15", "459");
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            //M801890
            WD_Fuction.SelectOrderandMaterial(order, WDMaterial.M801890);
            WD_Fuction.SelectMehod(WDMethod.Net, "M801890001");
            WD_Fuction.FinishNetDiapense("15", "215");
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            //1072
            WD_Fuction.SelectOrderandMaterial(order, WDMaterial.x1072);
            WD_Fuction.SelectMehod(WDMethod.Net, "1072003");
            WD_Fuction.FinishNetDiapense("15", "215");
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            //M801890-004
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(WDMethod.Net, barcode);
            WD_Fuction.FinishNetDiapense(tare, net);
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            //M801890-005
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(WDMethod.Net, barcode);
            WD_Fuction.FinishNetDiapense(tare, net);
            WD_Fuction.Close();
            LogStep(@"3. check APRM Batch");
            //check APRM batch
            Application.LaunchBatchDetailDisplay();
            Batch_Fuction.findBatch(order);
            //wait for loading
            Thread.Sleep(40000);
            //X0125Accept :Begin_Source_Gross is 1000 and End_Source_Gross is 1000
            APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1]").Expand();
            APRM.BatchMainWindow.TreeView.Select("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [2]");
            //wait for loading
            Thread.Sleep(5000);
            var items = APRM.BatchMainWindow.ListView.Items();
            int i = 0;
            foreach (var item in items)
            {
                if (item.Text == "Sequence")
                {
                    if (item.GetSubItemText(3) == "2")
                    {
                        i++;
                        Base_Assert.AreEqual(sequence1, item.GetSubItemText(2));
                    }
                    if (item.GetSubItemText(3) == "3")
                    {
                        i++;
                        APRM.BatchMainWindow.ListView._STD_ListView.Select(item);
                        Thread.Sleep(2000);
                        APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM Batch detail(Sequence).PNG");
                        Base_Assert.AreEqual(sequence2, item.GetSubItemText(2));
                    }
                }
            }
            Base_Assert.IsTrue(i==2,"sequence count");

            APRM.BatchMainWindow.Close();



        }
    }
}
