using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.Windows.Forms;
using System.Drawing;
using HP.LFT.SDK;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;
using System.IO;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(746113)]
        [Title("UC745042_W&D_APRM integration should have the signer characteristic value in the format UserID(UserFullName)")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_746113()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string xml = "14 aspen wd deviation_746113 bulk load.xml";
            string order = "test1";
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string source = "200";
            string scale = "simulator";
            LogStep(@"1. config APRM admin and apem admin");
            APRM_Fuction.InitailAPRMWD();
            LogStep(@"2. Active orders");
            WD_Fuction.Bulkload(xml);
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            driver.Close();
            LogStep(@"3. Open WD client");
            Application.LaunchWDAndLogin();
            WD.mainWindow.HomeInternalFrame.MaterialDispensing.Click();
            WD.mainWindow.Material_SelectionInternalFrame.materialTable.Row("X0125").Click();
            var Required_value = WD.mainWindow.Material_SelectionInternalFrame.materialTable._UFT_Table.GetCell(0, "Clean Required").Value;

            WD.mainWindow.Material_SelectionInternalFrame.next.Click();
            if (Required_value.ToString() == "Yes")
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            }
            //select method and input barcode
            WD_Fuction.SelectMehod(method, barcode);

            //select simulator
            if (WD.MessageDialog.IsExist())
            {
                WD.MessageDialog.OKButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
            //zeor
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            WD.mainWindow.Dialog.OK.Click();
            WD.SimulatorWindow.weight.SetText(source);
            WD.SimulatorWindow.OK.Click();
           
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }

            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            } 
            WD_Fuction.Close();
            LogStep(@"4. Open Batch query tool ");
            Application.LaunchBatchQueryTool();
            Thread.Sleep(3000);
            //open new query
            BatchQueryTool.NewQuery();
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
            APRM.BatchMainWindow.GetSnapshot(Resultpath + "signer(Container).PNG");
            APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("Signer_1");
            Console.WriteLine(APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text);
            Base_Assert.AreEqual("qaone1 (qaone1)", APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text);
            APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
            ////Action Node
            APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;Actions [1]").Expand();
            APRM.BatchMainWindow.TreeView.Select("Batch;Actions [1];Action [1]");
            //wait for loading
            Thread.Sleep(5000);
            APRM.BatchMainWindow.GetSnapshot(Resultpath + "User(Action).PNG");
            APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("User");
            Console.WriteLine(APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text);
            Base_Assert.AreEqual("qaone1 (qaone1)", APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text);
            APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
            //Deviation Node
            APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;DEVIATION_MANAGEMENT [1]").Expand();
            APRM.BatchMainWindow.TreeView.Select("Batch;DEVIATION_MANAGEMENT [1];DEVIATION [1]");
            //wait for loading
            Thread.Sleep(5000);
            APRM.BatchMainWindow.GetSnapshot(Resultpath + "signer(Container).PNG");
            APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("Signer_1");
            Base_Assert.AreEqual("qaone1 (qaone1)", APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text);
            Console.WriteLine(APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text);
            APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
            APRM.BatchMainWindow.Close();
            BatchQueryTool.BatchQueryToolWindow.Close();
            BatchQueryTool.BatchQueryToolWindow.Save_Dialog.NO.Click();
        }

    }
}
