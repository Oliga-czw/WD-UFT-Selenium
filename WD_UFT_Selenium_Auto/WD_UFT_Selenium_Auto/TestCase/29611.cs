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
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(29611)]
        [Title("Order status transfer")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_29611()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order1 = "test1";
            string order2 = "test2";
            string order3 = "test3";
            string xml1 = "10 aspen wd signautres_42354 bulk load.xml";
            string xml2 = "14 aspen wd deviation_42354 bulk load.xml";
            WD_Fuction.Bulkload(xml1);
            LogStep(@"1. edit planned to active  and cancel order");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Thread.Sleep(3000);
            Web_Fuction.active_order(order1);
            Thread.Sleep(3000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_Active.PNG");
            Web_Fuction.cancel_order(order2);
            Thread.Sleep(3000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_Cancelled.PNG");
            LogStep(@"1. edit canceled order to Archive");
            Web_Fuction.cancel_order(order3);
            Thread.Sleep(3000);
            Web_Fuction.Archive_order(order3);
            Thread.Sleep(2000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_Archive.PNG");
            driver.Wait();
            //Order Dispense
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.X0125);
            WD_Fuction.SelectMehod(WDMethod.Net, "X0125001");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "125");
            Web_Fuction.gotoTab(WDWebTab.order);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver.FindElement("//td[text()='test1']/../td[7]").Text, "Started");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_Started.PNG");
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.M801890);

            WD_Fuction.SelectMehod(WDMethod.Net, "M801890001");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "125");

            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.x1072);
            WD_Fuction.SelectMehod(WDMethod.Net, "1072003");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "125");
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD_Fuction.Close();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver.FindElement("//td[text()='test1']/../td[7]").Text, "Completed");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_Completed.PNG");
            Web_Fuction.Redispense_order(order1);
            Thread.Sleep(2000);
            Base_Assert.AreEqual(driver.FindElement("//td[text()='test1']/../td[7]").Text, "Started");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_Completed_Started.PNG");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.x1072);
            WD_Fuction.SelectMehod(WDMethod.Net, "1072003");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "25");
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();
            WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.SelectRows(0);
            WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
            //ArrayList KitBarcodeList = new ArrayList();
            Thread.Sleep(4000);
            string test01 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(0, "Container").Value.ToString();
            string test02 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(1, "Container").Value.ToString();
            string test03 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(2, "Container").Value.ToString();
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys("test1");
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test01);
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test02);
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test03);
            WD.mainWindow.SelectAnOrderToKittingFrame.accept.ClickSignle();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(5000);
            WD_Fuction.Close();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver.FindElement("//td[text()='test1']/../td[7]").Text, "Finished");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_Finished(m4).PNG");
            driver.Close();

            Selenium_Driver driver1 = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver1);
            driver1.Wait();
            Web_Fuction.login();
            driver1.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver1.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            Web_Fuction.Redispense_order(order1);
            Base_Assert.AreEqual(driver1.FindElement("//td[text()='test1']/../td[7]").Text, "Started");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_Finished_Started.PNG");
            //driver1.Close();
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.x1072);
            WD_Fuction.SelectMehod(WDMethod.Net, "1072003");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "25");
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();
            WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.SelectRows(0);
            WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
            //ArrayList KitBarcodeList = new ArrayList();
            Thread.Sleep(4000);
            string test001 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(0, "Container").Value.ToString();
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys("test1");
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test001);
            WD.mainWindow.SelectAnOrderToKittingFrame.accept.ClickSignle();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            WD_Fuction.Close();
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver1.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            Web_Fuction.Archive_order(order1);
            Thread.Sleep(5000);
            Base_Assert.AreEqual(driver1.FindElement("//td[text()='test1']/../td[7]").Text, "Archived");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_finished_Archived.PNG");
            driver1.Close();

            WD_Fuction.CleanOrdersData();
            WD_Fuction.Bulkload("07 aspen wd orders bulk load.xml");
            Selenium_Driver driver2 = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver2);
            driver2.Wait();
            Web_Fuction.login();
            driver2.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order1);
            Web_Fuction.active_order(order2);
            Web_Fuction.active_order(order3);
            Thread.Sleep(2000);
            Web_Fuction.cancel_order(order2);
            Thread.Sleep(2000);
            Base_Assert.AreEqual(driver2.FindElement("//td[text()='test2']/../td[7]").Text, "Cancelled");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_Active_Cancelled.PNG");
            Web_Fuction.Finish_order(order3);
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver2.FindElement("//td[text()='test3']/../td[7]").Text, "Finished");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_Active_Finished.PNG");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.X0125);
            WD_Fuction.SelectMehod(WDMethod.Net, "X0125001");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "125");
            WD_Fuction.Close();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver2.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            Web_Fuction.Finish_order(order1);
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver2.FindElement("//td[text()='test1']/../td[7]").Text, "Finished");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_Started_Finished.PNG");
            driver2.Close();





            WD_Fuction.CleanOrdersData();
            WD_Fuction.Bulkload("07 aspen wd orders bulk load.xml");
            Selenium_Driver driver3 = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver3);
            driver3.Wait();
            Web_Fuction.login();
            driver3.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order1);
            Web_Fuction.active_order(order2);
            Web_Fuction.active_order(order3);
            Thread.Sleep(2000);
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD_Fuction.SelectOrderandMaterial("test2", WDMaterial.X0125);
            WD_Fuction.SelectMehod(WDMethod.Net, "X0125001");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "125");
            WD_Fuction.Close();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver3.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            Web_Fuction.cancel_order(order2);
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver3.FindElement("//td[text()='test2']/../td[7]").Text, "Cancelled");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_Started_Cancelled.PNG");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.X0125);
            WD_Fuction.SelectMehod(WDMethod.Net, "X0125001");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "125");
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.M801890);
            WD_Fuction.SelectMehod(WDMethod.Net, "M801890001");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "125");

            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.x1072);
            WD_Fuction.SelectMehod(WDMethod.Net, "1072003");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "125");
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD_Fuction.Close();
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver3.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            Web_Fuction.Finish_order(order1);
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver3.FindElement("//td[text()='test1']/../td[7]").Text, "Finished");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_complete_Finished(w2).PNG");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD_Fuction.SelectOrderandMaterial("test3", WDMaterial.X0125);
            WD_Fuction.SelectMehod(WDMethod.Net, "X0125001");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "55");
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver3.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            WD_Fuction.SelectOrderandMaterial("test3", WDMaterial.M801890);
            WD_Fuction.SelectMehod(WDMethod.Net, "M801890001");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "55");
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver3.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            WD_Fuction.SelectOrderandMaterial("test3", WDMaterial.x1072);
            WD_Fuction.SelectMehod(WDMethod.Net, "1072003");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "55");
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD_Fuction.Close();
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver3.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            Web_Fuction.cancel_order(order3);
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver3.FindElement("//td[text()='test3']/../td[7]").Text, "Cancelled");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_complete_Cancelled.PNG");
            driver3.Close();

            WD_Fuction.CleanOrdersData();
            WD_Fuction.Bulkload("07 aspen wd orders bulk load.xml");
            WD_Fuction.Bulkload(xml2);
            Selenium_Driver driver5 = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver5);
            driver5.Wait();
            Web_Fuction.login();
            driver5.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            driver5.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            Web_Fuction.active_order(order1);
            Thread.Sleep(2000);
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.X0125);
            WD_Fuction.SelectMehod(WDMethod.Net, "X0125001");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "125");
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.M801890);
            WD_Fuction.SelectMehod(WDMethod.Net, "M801890001");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "125");

            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.x1072);
            WD_Fuction.SelectMehod(WDMethod.Net, "1072003");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("1", "125");
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();
            WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.SelectRows(0);
            WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
            Thread.Sleep(4000);
            string test011 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(0, "Container").Value.ToString();
            string test022 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(1, "Container").Value.ToString();
            string test033 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(2, "Container").Value.ToString();
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys("test1");
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test011);
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test022);
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test033);
            WD.mainWindow.SelectAnOrderToKittingFrame.accept.ClickSignle();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            WD_Fuction.Close();
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver5.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            Web_Fuction.Redispense_order(order1);
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.x1072);
            WD_Fuction.SelectMehod(WDMethod.Net, "1072003");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("0", "25");
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
            Web_Fuction.gotoTab(WDWebTab.order);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver5.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver5.FindElement("//td[text()='test1']/../td[7]").Text, "Deviation Pending");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_complete_DevPending.PNG");
            Web_Fuction.Redispense_order(order1);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver5.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver5.FindElement("//td[text()='test1']/../td[7]").Text, "Started");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Orde_DevPending_started.PNG");
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.x1072);
            WD_Fuction.SelectMehod(WDMethod.Net, "1072003");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("0", "25");
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();
            WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.SelectRows(0);
            WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
            Thread.Sleep(4000);
            string test0011 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(0, "Container").Value.ToString();
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys("test1");
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test0011);
            WD.mainWindow.SelectAnOrderToKittingFrame.accept.ClickSignle();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(5000);
            WD_Fuction.Close();
            driver5.FindElement("//div[text()='Deviation Management']").Click();
            driver5.Wait();
            driver5.FindElement("//a[text()='Deviation Pending']").Click();
            driver5.FindElements("//a[text()='Ack/Comment']")[0].Click();
            driver5.FindElement("//textarea[@class='gwt-TextArea Comment_TextArea']").SendKeys("test");
            driver5.FindElement("//label[text()='I acknowledge this deviation.']/../input").Click();
            driver5.FindElement("//div[text()='Username:']/../../td[2]/input").SendKeys(UserName.qaone1);
            driver5.FindElement("//div[text()='Password:']/../../td[2]/input").SendKeys(PassWord.qaone1);
            driver5.FindElement("//button[text()='OK']").Click();
            driver5.FindElement("//a[text()='Deviation Pending']").Click();
            driver5.FindElement("//a[text()='Ack/Comment']").Click();
            driver5.FindElement("//textarea[@class='gwt-TextArea Comment_TextArea']").SendKeys("test");
            driver5.FindElement("//label[text()='I acknowledge this deviation.']/../input").Click();
            driver5.FindElement("//div[text()='Username:']/../../td[2]/input").SendKeys(UserName.qaone1);
            driver5.FindElement("//div[text()='Password:']/../../td[2]/input").SendKeys(PassWord.qaone1);
            driver5.FindElement("//button[text()='OK']").Click();
            //Finished the order
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver5.FindElement("//a[text()='Accept/Comment']").Click();
            driver5.FindElement("//textarea[@class='gwt-TextArea Comment_TextArea']").SendKeys("test");
            driver5.FindElement("//label[text()='I accept all product deviations in this order.']/../input").Click();
            driver5.FindElement("//div[text()='Username:']/../../td[2]/input").SendKeys(UserName.qaone1);
            driver5.FindElement("//div[text()='Password:']/../../td[2]/input").SendKeys(PassWord.qaone1);
            driver5.FindElement("//button[text()='OK']").Click();
            Thread.Sleep(3000);
            driver5.FindElement("//div[text()='Orders' and  @class='Tab_Label']").Click();
            Thread.Sleep(3000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver5.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver5.FindElement("//td[text()='test1']/../td[7]").Text, "Finished");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Orde_DevPending_Finished.PNG");
            Web_Fuction.Redispense_order(order1);
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD_Fuction.SelectOrderandMaterial("test1", WDMaterial.x1072);
            WD_Fuction.SelectMehod(WDMethod.Net, "1072003");
            Thread.Sleep(3000);
            WD_Fuction.FinishNetDiapense("0", "25");
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();
            WD.mainWindow.SelectAnOrderToKittingFrame.orderTable.SelectRows(0);
            WD.mainWindow.SelectAnOrderToKittingFrame.StartKitButton.Click();
            Thread.Sleep(4000);
            string test010 = WD.mainWindow.SelectAnOrderToKittingFrame.KitTable.GetCell(0, "Container").Value.ToString();
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys("test1");
            WD.mainWindow.SelectAnOrderToKittingFrame.barcodeEditor.SendKeys(test010);
            WD.mainWindow.SelectAnOrderToKittingFrame.accept.ClickSignle();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(5000);
            WD_Fuction.Close();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver5.FindElement("//a[text()='Order']").Click();
            Thread.Sleep(3000);
            Web_Fuction.cancel_order(order1);
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver5.FindElement("//td[text()='test1']/../td[7]").Text, "Cancelled");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Order_DevPending_Cancelled.PNG");
        }


    }
}