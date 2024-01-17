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
        [TestCaseID(41767)]
        [Title("V8.8.4-Weight a material with New Source button used and signature configured")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_41767()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
            string xml = "10 aspen wd signautres_41767 bulk load.xml";
            string order = "test4";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string tare = "15";
            string net = "215";
            string comment = "signature";

            LogStep(@"1. import signature xml");
            WD_Fuction.Bulkload(xml);
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Signatures.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "New Source Signature-Setting.PNG");
            LogStep(@"2. Active order");
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            LogStep(@"3. Open WD client and partial dispense");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            //zeor
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.SimulatorWindow.weight.SetText(tare);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //weight
            WD.SimulatorWindow.weight.SetText(net);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.NewSource.Click();
            LogStep(@"4. Check new source signature");
            //signature1
            WD.mainWindow.Dialog.UserID.SetText(UserName.qaone1);
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            WD.mainWindow.Dialog.OK.Click();
            //need comment 
            string message = WD.MessageDialog.Lable.Text;
            Base_Assert.AreEqual("Please create comment first.", message, "need comment");
            WD.mainWindow.GetSnapshot(Resultpath + "New Source signature1.PNG");
            WD.MessageDialog.OKButton.Click();
            //send comment
            WD.mainWindow.Dialog.Comment.SetText(comment);
            WD.mainWindow.Dialog.OK.Click();
            //signature2 
            WD.mainWindow.Dialog.UserID.SetText(UserName.qaone2);
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone2);
            WD.mainWindow.Dialog.Comment.SetText(comment);
            WD.mainWindow.GetSnapshot(Resultpath + "New Source signature2.PNG");
            WD.mainWindow.Dialog.OK.Click();
            //exit wd client
            WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
            Base_Assert.IsTrue(WD.mainWindow.Material_SelectionInternalFrame.IsExist() || WD.mainWindow.MaterialInternalFrame.IsExist(), "Exit Dispense");
            WD_Fuction.Close();
            driver.Close();
            LogStep(@"5. Check signature in batch");
            Application.LaunchBatchDetailDisplay();
            Batch_Fuction.findBatch(order);
            //wait for loading
            Thread.Sleep(40000);
            APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1]").Expand();
            APRM.BatchMainWindow.TreeView.Select("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1];Action [1]");
            //wait for loading
            Thread.Sleep(5000);
            APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("Signer_1");
            Base_Assert.AreEqual("qaone1 (qaone1)", APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text, "Signature1");
            APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
            APRM.BatchMainWindow.GetSnapshot(Resultpath + "Batch Signature.PNG");
            APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("Signer_2");
            Base_Assert.AreEqual("qaone2 (qaone2)", APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text, "Signature2");
            APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
            APRM.BatchMainWindow.Close();
        }
    }
}
