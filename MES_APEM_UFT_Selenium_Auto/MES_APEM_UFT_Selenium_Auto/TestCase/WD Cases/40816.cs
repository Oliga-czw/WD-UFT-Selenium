using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.IO;
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;
using System.Linq;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(40816)]
        [Title("Administration Integration: config APRM data source")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_40816()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string order = "Case40816";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string tare = "10";
            string net = "454.4";

            LogStep(@"1. config APRM admin and apem admin");
            APRM_Fuction.FirstInitailAPRM();
            LogStep(@"2. Execute order ");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            //active order
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            //finish dispense
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            WD_Fuction.FinishNetDiapense(tare, net);
            WD_Fuction.Close();
            LogStep(@"3. Open Batch query tool ");
            Application.LaunchBatchQueryTool();
            //open new query
            BatchQueryTool.BatchQueryToolWindow.SetActive();
            Keyboard.KeyDown(Keyboard.Keys.Control);
            Keyboard.PressKey(Keyboard.Keys.N);
            Keyboard.KeyUp(Keyboard.Keys.Control);
            //dialog message
            if (BatchQueryTool.BatchQueryToolWindow.NoDefaultData_Dialog.IsExist())
            {
                BatchQueryTool.BatchQueryToolWindow.NoDefaultData_Dialog.OK.Click();
            }
            //click advance
            var point = BatchQueryTool.BatchQueryToolWindow.ConfigQueryWindow._STD_Window.Location;
            Console.WriteLine(point.X + "," + point.Y);
            point.X += 400;
            point.Y += 40;
            Mouse.Click(point);
            //send range
            BatchQueryTool.BatchQueryToolWindow.ConfigQueryWindow.Start.SendKeys("1");
            BatchQueryTool.BatchQueryToolWindow.ConfigQueryWindow.End.SendKeys("10000");
            BatchQueryTool.BatchQueryToolWindow.ConfigQueryWindow.OK.Click();
            //Execute
            Keyboard.PressKey(Keyboard.Keys.F9);
            //check record from aprm
            Thread.Sleep(8000);
            BatchQueryTool.BatchQueryToolWindow.GetSnapshot(Resultpath + "APRM record.PNG");
            //open batch detail display
            BatchQueryTool.BatchQueryToolWindow.ListView._STD_ListView.ActivateItem(order);
            //wait for loading
            Thread.Sleep(15000);
            APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1]").Expand();
            APRM.BatchMainWindow.TreeView.Select("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1];Container [1]");
            //wait for loading
            Thread.Sleep(5000);
            APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM Batch detail.PNG");
            //Net:depend on tare and net
            APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("Net");
            Base_Assert.AreEqual("444.4", APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text, "Net");
            APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
            //Tare
            APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("Tare");
            Base_Assert.AreEqual(tare, APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text, "Tare");
            APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
            //Type:Depend on method
            APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("Weigh Type");
            Base_Assert.AreEqual("Net", APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text, "Type");
            APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();

            APRM.BatchMainWindow.Close();
            BatchQueryTool.BatchQueryToolWindow.Close();
            //dialog message
            if (BatchQueryTool.BatchQueryToolWindow.Save_Dialog.IsExist())
            {
                BatchQueryTool.BatchQueryToolWindow.Save_Dialog.NO.Click();
            }

        }

    }
}