using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(41180)]
        [Title("material weighing start: weighing can be conducted and material information correctly displayed")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_41180()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;

            //active
            string order1 = "test1";    
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Thread.Sleep(3000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            Web_Fuction.active_order(order1);
            LogStep(@"1. Open Wd client and login");
            Application.LaunchWDAndLogin();
            //Base_File.ClearFolder("C:\\ProgramData\\AspenTech\\AeBRS\\WDUpload");
            LogStep(@"2. Open Material Dispening");
            WD.mainWindow.HomeInternalFrame.MaterialDispensing.Click();
            Thread.Sleep(2000);
            WD.mainWindow.Material_SelectionInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.Material_SelectionInternalFrame.next.Click();
            if (WD.mainWindow.BoothCleanInternalFrame.IsExist())
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            }
            Thread.Sleep(3000);
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("X0125001");
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("444");

            WD.SimulatorWindow.OK.Click();

            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            LogStep(@"3. select a material and click next");
            WD.mainWindow.Material_SelectionInternalFrame.materialTable.Row("1072").Click();
            //WD.mainWindow.Material_SelectionInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.Material_SelectionInternalFrame.next.Click();
            Thread.Sleep(3000);
            if (WD.mainWindow.BoothCleanInternalFrame.IsExist())
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            }
            if (WD.mainWindow.HandleInformationInterFrame.IsExist())
            {
                WD.mainWindow.HandleInformationInterFrame.Acknowledge.ClickSignle();
            }
            Thread.Sleep(3000);
            LogStep(@"4. Assert the first/default method is presented");
            var selectedMethod = WD.mainWindow.ScaleWeightInternalFrame.dispense_method.SelectedItems[0].Text;
            var FirstMethod = WD.mainWindow.ScaleWeightInternalFrame.dispense_method.Items[0].Text;
            Base_Assert.AreEqual(selectedMethod, FirstMethod);
            
            Console.Write(selectedMethod);
            //LogStep(@"5. enter the barcode of source container");
            var DisployMaterial = WD.mainWindow.ScaleWeightInternalFrame.disploylMaeterial._UFT_Label.Text;
            //System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", DisployMaterial.ToString(), Encoding.Default);
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("1072007");
            Thread.Sleep(1000);
            if (WD.ConfirmationDialog._UFT_Dialog.Exists())
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.AvailQty._UFT_Label.Text, "800.0 G");
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.Lot._UFT_Label.Text, "A124");
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.Potency._UFT_Label.Text, "");
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.Expiration._UFT_Label.Text, "11/25/25, 5:25:00 PM");
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.Status._UFT_Label.Text, "Approved");
            WD.mainWindow.GetSnapshot(Resultpath + "correct_barcode.PNG");
            WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
            Thread.Sleep(5000);
            //3.1 enter Incorrect material.
            WD.mainWindow.Material_SelectionInternalFrame.materialTable.Row("1072").Click();
            WD.mainWindow.Material_SelectionInternalFrame.next.Click();
            Thread.Sleep(3000);
            if (WD.mainWindow.HandleInformationInterFrame.IsExist())
            {
                WD.mainWindow.HandleInformationInterFrame.Acknowledge.ClickSignle();
            }
            Thread.Sleep(3000);
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("445254");
            Base_Assert.AreEqual(WD.MessageDialog.Lable.Text, "Container barcode is not recognized. Scan another container.");
            WD.mainWindow.GetSnapshot(Resultpath + "Incorrect_barcode.PNG");
            WD.MessageDialog.OKButton.Click();
            Thread.Sleep(3000);
            //enter not matching one (optionally) downloaded from ERP: user is warned, and depending on configuration, he is allowed to proceed or not, creating a deviation.
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("X0125001");
            Base_Assert.AreEqual(WD.MessageDialog.Lable.Text, "The scanned container is not the required material. Please scan the correct container.");
            WD.mainWindow.GetSnapshot(Resultpath + "not_match_barcode.PNG");
            WD.MessageDialog.OKButton.Click();
            Thread.Sleep(3000);
            //enter quarantined (except if specifically allowed), or expired lot (from ERP stock).
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("1072006");
            Base_Assert.AreEqual(WD.MessageDialog.Lable.Text, "Quarantined lot is not allowed.");
            WD.mainWindow.GetSnapshot(Resultpath + "quarantined_barcode.PNG");
            WD.MessageDialog.OKButton.Click();
            //enter Non-FEFO lot:
            //WD.mainWindow.ScaleWeightInternalFrame.barcode.SetText("445254\n");
            //Base_Assert.AreEqual(WD.MessageDialog.Lable.Text, "Container barcode is not recognized. Scan another container.");
            //enter Non-released material
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("1072001");
            Base_Assert.AreEqual(WD.MessageDialog.Lable.Text, "Only approved and allowed quarantined HU can be used.");
            WD.MessageDialog.OKButton.Click();
            Thread.Sleep(3000);
            Base_File.ClearFolder("C:\\ProgramData\\AspenTech\\AeBRS\\WDUpload");
            LogStep(@"6. start to weigh using the selecte weighing method and exit the dispenstion after weighing is done.");
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("1072007");
            Thread.Sleep(3000);
            if (WD.ConfirmationDialog._UFT_Dialog.Exists())
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            Thread.Sleep(3000);
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("200");
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "weight_accept.PNG");
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            Thread.Sleep(3000);
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }

            LogStep(@"7. check the Label for target container and consumption xml, adjustment xml");
            string path = @"C:\ProgramData\AspenTech\AeBRS\WDUpload\";
            string xmlData = Base_File.ReadXml(path,0);
            Console.WriteLine(xmlData);
            Base_Assert.IsTrue(xmlData.Contains("<MaterialUse>Consumed</MaterialUse><Quantity><QuantityString>200.0</QuantityString>"));

        }


    }
}