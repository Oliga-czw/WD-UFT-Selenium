using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(29626)]
        [Title("Order: order material with their status")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_29626()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string xml1 = "10 aspen wd signautres_42354 bulk load.xml";
            string xml2 = "14 aspen wd deviation_42354 bulk load.xml";
            WD_Fuction.Bulkload(xml1);
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Thread.Sleep(3000);
            Web_Fuction.active_order("test1");
            Thread.Sleep(3000);
            driver.FindElement("//td[text()='test1']/../td[3]/img").Click();
            var X0125_status1 = driver.FindElement("//td[text()='X0125']/../td[6]").Text;
            Base_Assert.AreEqual(X0125_status1, "Pending");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Pending.PNG");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.X0125);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            driver.FindElement("//td[text()='test1']/../td[3]/img").Click();
            var X0125_status2 = driver.FindElement("//td[text()='X0125']/../td[6]").Text;
            Base_Assert.AreEqual(X0125_status2, "Started");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Started.PNG");
            WD_Fuction.SelectMehod(WDMethod.Net, "X0125001");
            Thread.Sleep(3000);
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            Thread.Sleep(2000);
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            Thread.Sleep(2000);
            //weight
            WD.SimulatorWindow.weight.SetText("34");
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(1000);
            WD.mainWindow.ScaleWeightInternalFrame.Partial.Click();
            Thread.Sleep(3000);
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(1000);
            WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            driver.FindElement("//td[text()='test1']/../td[3]/img").Click();
            var X0125_status3 = driver.FindElement("//td[text()='X0125']/../td[6]").Text;
            Base_Assert.AreEqual(X0125_status3, "Partially Dispensed");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "PartiallyDispensed.PNG");
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.X0125);
            WD_Fuction.SelectMehod(WDMethod.Net, "X0125001");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "125");
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            driver.FindElement("//td[text()='test1']/../td[3]/img").Click();
            var X0125_status4 = driver.FindElement("//td[text()='X0125']/../td[6]").Text;
            Base_Assert.AreEqual(X0125_status4, "Completed");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Completed.PNG");
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.M801890);
            WD_Fuction.SelectMehod(WDMethod.Net, "M801890001");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "100");

            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.x1072);
            WD_Fuction.SelectMehod(WDMethod.Net, "1072003");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "100");
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();
            WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.SelectRows(0);
            WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
            Thread.Sleep(4000);
            string test01 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(0, "Container").Value.ToString();
            string test02 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(1, "Container").Value.ToString();
            string test03 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(2, "Container").Value.ToString();
            string test04 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(3, "Container").Value.ToString();
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys("test1");
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test01);
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test02);
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test03);
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test04);
            WD.mainWindow.SelectAnOrderToKittingFrame.accept.ClickSignle();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            WD_Fuction.Close();
            WD_Fuction.Bulkload(xml2);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            driver.FindElement("//td[text()='test1']/../td[3]/img").Click();
            driver.FindElement("//a[text()='Redispense a Material']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//td[text()='34.0']/../td[1]/span/input").Click();
            driver.FindElement("//button[text()='Redispense Material']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//input[@type='password']").SendKeys(PassWord.qaone1);
            driver.FindElement(("//button[@id='Dialogbox_Bottom_OK_Button_Id']")).Click();
            Thread.Sleep(2000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            driver.FindElement("//td[text()='test1']/../td[3]/img").Click();
            var X0125_status5 = driver.FindElement("//td[text()='X0125']/../td[6]").Text;
            var Request_QTY1 = driver.FindElement("//td[text()='X0125']/../td[7]").Text;
            var remaining_QTY1 = driver.FindElement("//td[text()='X0125']/../td[8]").Text;
            Base_Assert.AreNotEqual(double.Parse(Request_QTY1), double.Parse(remaining_QTY1));
            Base_Assert.AreEqual(X0125_status5, "Partially Dispensed");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "redispense_PartiallyDispensed.PNG");
            driver.FindElement("//a[text()='Redispense a Material']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//td[text()='124.0']/../td[1]/span/input").Click();
            driver.FindElement("//button[text()='Redispense Material']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//input[@type='password']").SendKeys(PassWord.qaone1);
            driver.FindElement(("//button[@id='Dialogbox_Bottom_OK_Button_Id']")).Click();
            Thread.Sleep(2000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(8000);
            driver.FindElement("//td[text()='test1']/../td[3]/img").Click();
            var X0125_status6 = driver.FindElement("//td[text()='X0125']/../td[6]").Text;
            var Request_QTY2 = driver.FindElement("//td[text()='X0125']/../td[7]").Text;
            var remaining_QTY2 = driver.FindElement("//td[text()='X0125']/../td[8]").Text;
            Base_Assert.AreEqual(double.Parse(Request_QTY2), double.Parse(remaining_QTY2));
            Base_Assert.AreEqual(X0125_status6, "Pending");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "redispense_Pending.PNG");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.X0125);
            WD_Fuction.SelectMehod(WDMethod.Net, "X0125001");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "125");
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();
            WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.SelectRows(0);
            WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
            Thread.Sleep(4000);
            string test0001 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(0, "Container").Value.ToString();
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys("test1");
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test0001);
            WD.mainWindow.SelectAnOrderToKittingFrame.accept.ClickSignle();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(5000);
            WD_Fuction.Close();


        }
    }
}