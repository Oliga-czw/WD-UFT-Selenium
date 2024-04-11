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
using OpenQA.Selenium;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(733335)]
        [Title("Inspired from customer defect 721363 - Discrepancy in weight measure")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_733335()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
            string order = "test1";
            string material1 = WDMaterial.X0125;
            string material2 = WDMaterial.M801890;
            string method = WDMethod.Net;
            string barcode1 = "X0125001";
            string barcode2 = "M801890001";
            string scale1 = "simulator";
            string scale2 = "simulator001";
            string tare = "10";
            string net1 = "454.4";
            string net2 = "98.88";

            Base_File.ClearFolder(Base_Directory.WDUploadDir);
            LogStep(@"1. Active Order");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            //change scale 
            Web_Fuction.gotoTab(WDWebTab.equipment);
            Web_Fuction.edit_scale(scale2);
            Web.Equipment_Page.simultor_Resolution.Clear();
            Web.Equipment_Page.simultor_Resolution.SendKeys("0.01");
            Web.Equipment_Page.simultor_name.Click();
            Web.Equipment_Page.Apply.Click();
            Thread.Sleep(2000);
            LogStep(@"2. Open Wd client and login");
            Application.LaunchWDAndLogin();
            LogStep(@"3. Open Material Dispening");
            WD_Fuction.SelectOrderandMaterial(order, material1);
            WD_Fuction.SelectMehod(method, barcode1);
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.SimulatorWindow.weight.SetText(tare);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //weight
            WD.SimulatorWindow.weight.SetText(net1);
            WD.SimulatorWindow.OK.Click();

            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            WD.mainWindow.GetSnapshot(Resultpath + "X0125-Accept.PNG");
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.WeightNumber._UFT_Label.Text, "444.4");
            LogStep(@"4. select a material and click next");
            WD.mainWindow.Material_SelectionInternalFrame.materialTable.Row(material2).Click();
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
            WD_Fuction.SelectMehod(method, barcode2);
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale2);
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.SimulatorWindow.weight.SetText(tare);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            //weight
            WD.SimulatorWindow.weight.SetText(net2);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "M801890-Accept.PNG");
            Base_Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.WeightNumber._UFT_Label.Text, "88.88");
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            WD_Fuction.Close();
            LogStep(@"5. Check the label and order report");
            Web_Fuction.gotoTab(WDWebTab.order);
            Thread.Sleep(2000);
            Web.Order_Page.SearchInput.SendKeys("test1");
            Thread.Sleep(5000);
            Web.Order_Page.orderCheckbox.Click();
            Thread.Sleep(2000);
            //reprint label
            Web.Order_Page.ReprintLable.Click();
            Thread.Sleep(2000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Reprint label Pallets.PNG");
            var row = Web.Order_Page.Labeltablerows;
            for (int i = 1; i < row.Count(); i++)
            {
                Base_Assert.IsTrue(row.getElement(i).FindElements(By.TagName("td"))[1].Text != null, "pallet shows");
            }
            Web.Order_Page.ReprintLableClose.Click();
            //order report
            Web.Order_Page.PrintReport.Click();
            Thread.Sleep(10000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "order report Pallets.PNG");
            var urlll = Selenium_Driver._Selenium_Driver.FindElement(By.XPath("//iframe[@class='gwt-Frame']")).GetAttribute("src");
            string[] parts = urlll.Split('/');
            string ReportFileName = parts[parts.Length - 1];
            string ReportText = Web_Fuction.OrderPrint(ReportFileName);
            Base_Assert.IsTrue(ReportText.Contains(order + "0"), "order report pallets");
            driver.Close();

            LogStep(@"6. check the Label for target container and consumption xml, adjustment xml");
            string path = @"C:\ProgramData\AspenTech\AeBRS\WDUpload\";
            string xmlData = Base_File.ReadXml(path,0);
            Console.WriteLine(xmlData);
            Base_Assert.IsTrue(xmlData.Contains("<MaterialUse>Consumed</MaterialUse><Quantity><QuantityString>200.0</QuantityString>"));

        }


    }
}