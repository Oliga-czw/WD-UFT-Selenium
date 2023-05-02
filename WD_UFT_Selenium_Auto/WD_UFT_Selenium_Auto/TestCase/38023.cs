using System;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Text;
using OpenQA.Selenium;
using System.Linq;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(38023)]
        [Title("Search filter work for WD client")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_38023()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";

            //active order how much?
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            LogStep(@"1. Open Wd client and login");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            // open the WD and open 'Material Dispense'
            LogStep("2.Click Material Dispensing,input value to Search field and select left order, click Next to dispensing.");
            WD.mainWindow.HomeInternalFrame.MaterialDispensing.Click();
            var searchEditor = WD.mainWindow.Material_SelectionInternalFrame.Search;

            searchEditor.SetText("X0125");

            WD.mainWindow.Material_SelectionInternalFrame.SearchButton.Click();

            var materialsTable = WD.mainWindow.Material_SelectionInternalFrame.materialTable;
            var materialRowsCount = materialsTable._UFT_Table.Rows.Count;
            if (materialRowsCount > 0) {
                //System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", materialsTable._UFT_Table.GetVisibleText());
                Base_Assert.AreEqual(materialsTable._UFT_Table.Rows.Count,2);
                //for (int a = 0; a < materialRowsCount; a = a + 1)
                //{
                //    Base_Assert.AreEqual(materialsTable._UFT_Table.GetCell(a, "a3").Value.ToString(),"X0125");
                //}
            }
            WD.mainWindow.GetSnapshot(Resultpath + "Material.PNG");
            WD.mainWindow.Material_SelectionInternalFrame.HomeButton.Click();
            Thread.Sleep(2000);
            LogStep(@"3.Click Order Dispensing,input value to Search field and select left order, click Next to dispensing.");
            // Order Dispensing
            WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            var orderListtable = WD.mainWindow.DispensingInternalFrame.orderTable;
            WD.mainWindow.DispensingInternalFrame.Search.SetText("test3");

            WD.mainWindow.DispensingInternalFrame.SearchButton.Click();

            var OrderRowsCount = orderListtable._UFT_Table.Rows.Count;
            //ArrayList arrayList = new ArrayList();
            if (OrderRowsCount > 0)
            {
                Base_Assert.AreEqual(orderListtable._UFT_Table.Rows.Count, 1);
                //for (int a = 0; a < OrderRowsCount; a = a + 1)
                //{
                //    Base_Assert.IsTrue(orderListtable._UFT_Table.GetCell(a, "order").Value.ToString().Contains("test3"));
                //}
            }
            WD.mainWindow.GetSnapshot(Resultpath + "Order.PNG");
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            Thread.Sleep(2000);
            LogStep(@"4.Click Campaign Dispensing,input value to Search field and select left order, click Next to dispensing.");
            //open cam dispensing
            WD.mainWindow.HomeInternalFrame.CampaignDispense.Click();
            WD.mainWindow.CampaignSelectionInternalFrame.Search.SetText("test");
            WD.mainWindow.CampaignSelectionInternalFrame.SearchButton.Click();

            var campaignTable = WD.mainWindow.CampaignSelectionInternalFrame.CampaignsTable;
            var campaignRowsCount = campaignTable._UFT_Table.Rows.Count;
            if (campaignRowsCount > 0)
            {
                Base_Assert.AreEqual(campaignTable._UFT_Table.Rows.Count, 1);
                //for (int a = 0; a < OrderRowsCount; a = a + 1)
                //{
                //    Base_Assert.IsTrue(campaignTable._UFT_Table.GetCell(a, "CampaignID").Value.ToString().Contains("test"));
                //}
            }
            WD.mainWindow.GetSnapshot(Resultpath + "Campaign.PNG");
            Thread.Sleep(2000);
            WD.mainWindow.CampaignSelectionInternalFrame.homeButton.Click();
            Thread.Sleep(2000);
            WD_Fuction.Close();
        }

        
    }
}