//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using OpenQA.Selenium;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Threading;
//using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
//using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
//using MES_APEM_UFT_Selenium_Auto.Product.WD;
//using MES_APEM_UFT_Selenium_Auto.Product.APRM;

//namespace MES_APEM_UFT_Selenium_Auto.TestCase
//{
//    public partial class WD_TestCase
//    {
//        [TestCaseID(748053)]
//        [Title("UC693790_W&D_Enhance Net Removal: record gross weight when reset the weighing")]
//        [TestCategory(ProductArea.WD)]
//        [Priority(CasePriority.Medium)]
//        [TestCategory(CaseState.Created)]
//        [TestCategory(AutomationTool.UFT_Selenium)]
//        [Owner(AutomationEngineer.Ziwei)]
//        [Timeout(600000)]

//        [TestMethod]
//        public void VSTS_748053()
//        {
//            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
//            string order = "test4";
//            string material = WDMaterial.X0125;
//            string method = WDMethod.Netremoval;
//            string barcode = "X0125001";
//            string source_left = "800";
//            string tare = "15";
//            string source_start = "1000";
//            string scale = "simulator";

//            LogStep(@"1. Open WD web");
//            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
//            Web_Fuction.gotoWDWeb(driver);
//            driver.Wait();
//            Web_Fuction.login();
//            driver.Wait();
//            //LogStep(@"2. Active order");
//            //Web_Fuction.gotoTab(WDWebTab.order);
//            //Web_Fuction.active_order(order);
//            //LogStep(@"3. Open WD client and reset net removal");
//            //Application.LaunchWDAndLogin();
//            //WD_Fuction.SelectOrderandMaterial(order, material);
//            //WD_Fuction.SelectMehod(method, barcode);
//            ////select simulator
//            //if (WD.MessageDialog.IsExist())
//            //{
//            //    WD.MessageDialog.OKButton.Click();
//            //}
//            //WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
//            ////zeor
//            //WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
//            ////tare
//            //WD.mainWindow.ScaleWeightInternalFrame.tare_editor.SetText(tare, true);
//            ////start weight
//            //WD.SimulatorWindow.weight.SetText(source_start);
//            //WD.SimulatorWindow.OK.Click();
//            //WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
//            ////input remove weight
//            //WD.SimulatorWindow.weight.SetText(source_left);
//            //WD.SimulatorWindow.OK.Click();
//            ////check reset 
//            //WD.mainWindow.GetSnapshot(Resultpath + "Net removal Reset.PNG");
//            //WD.mainWindow.ScaleWeightInternalFrame.reset.Click();
//            ////exit wd client
//            //WD.mainWindow.ScaleWeightInternalFrame.cancel.ClickSignle();
//            //Base_Assert.IsTrue(WD.mainWindow.Material_SelectionInternalFrame.IsExist() || WD.mainWindow.MaterialInternalFrame.IsExist(), "Exit Dispense");
//            //WD_Fuction.Close();
            
//            LogStep(@"4. Check begin/end source in WEB");
//            Web_Fuction.gotoTab(WDWebTab.inventory);

//            var heads = driver.FindElements("//th[@colspan='1']/p");
//            //var SourceData = driver.FindElements("//div[text()='X0125001']/../../../td");
//            string data = "";
//            for (int i = 0;i<heads.Count;i++)
//            {
//                if (heads[i].Text == "Nominal")
//                {
//                    data = driver.FindElement($"//div[text()='X0125001']/../../../td[{i + 1}]/div/div").Text;
//                    break;
//                }
//            }
//            string data2 = "";
//            for (int i = 0; i < heads.Count; i++)
//            {
//                if (heads[i].Text == "Actual")
//                {
//                    data2 = driver.FindElement($"//div[text()='X0125001']/../../../td[{i + 1}]/div/div").Text;
//                    break;
//                }
//            }
//            Console.WriteLine(data);
//            Console.WriteLine(data2);



//            driver.Close();
//            //LogStep(@"5. Check begin/end source in DB");
//            //var data = WD_Fuction.GetBeginEndSource();
//            //Base_Assert.AreEqual("1000.0", data[0][0]);
//            //Base_Assert.AreEqual("1000.0", data[0][1]);
//            LogStep(@"6. Check begin/end sourcein batch");
//            Application.LaunchBatchDetailDisplay();
//            //Batch_Fuction.findBatch(order);
//            APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
//            //WD.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1]").Expand();
//            //WD.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1]").Expand();
//            //WD.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1]").Expand();
//            //WD.BatchMainWindow.TreeView.Selete("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1];Action [1]");
//            ////wait for loading
//            //Thread.Sleep(5000);
//            //WD.BatchMainWindow.ListView._STD_ListView.ActivateItem("Signer_1");
//            //Base_Assert.AreEqual("qaone1 (qaone1)", WD.BatchMainWindow.BatchCharacteristicDialog.Value.Text, "Signature1");
//            //WD.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
//            //WD.BatchMainWindow.GetSnapshot(Resultpath + "Batch Signature.PNG");
//            //WD.BatchMainWindow.ListView._STD_ListView.ActivateItem("Signer_2");
//            //Base_Assert.AreEqual("qaone2 (qaone2)", WD.BatchMainWindow.BatchCharacteristicDialog.Value.Text, "Signature2");
//            //WD.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
//            //WD.BatchMainWindow.Close();
//        }
//    }
//}
