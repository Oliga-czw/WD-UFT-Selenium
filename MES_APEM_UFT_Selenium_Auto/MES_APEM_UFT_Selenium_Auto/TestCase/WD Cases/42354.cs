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

        [TestCaseID(42354)]
        [Title("Order: Management options, order redispense.")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_42354()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order1 = "test1";
            string order2 = "test2";
            string order3 = "test3";
            string xml1 = "10 aspen wd signautres_42354 bulk load.xml";
            string xml2 = "14 aspen wd deviation_42354 bulk load.xml";
            WD_Fuction.Bulkload(xml1);
            LogStep(@"1. edit planned, active,  archived or cancel order");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order1);
            Web_Fuction.cancel_order(order2);
            Thread.Sleep(3000);
            Web_Fuction.cancel_order(order3);
            Thread.Sleep(3000);
            Web_Fuction.Archive_order(order3);
            Thread.Sleep(2000);

            driver.Wait();
            var order_list = driver.FindElements("//table[@class='Order_Table_body_Style_Collapse']/tbody/tr");
            for (int i = 2; i <= order_list.Count; i++)
            {
                var tr_xpath = "//table[@class='Order_Table_body_Style_Collapse']/tbody/tr[" + i.ToString() + "]/td[3]/img";
                Console.WriteLine(tr_xpath);
                driver.FindElement(tr_xpath).Click();
                var Redispense = driver.FindElement("//a[text()='Redispense a Material']");
                Base_Assert.IsTrue(Redispense.GetAttribute("class").Contains("Disable"));
            }
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "redispense_disable.PNG");
            //Order Dispense
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            Thread.Sleep(2000);
            WD.mainWindow.DispensingInternalFrame.orderTable.SelectRows(0);
            WD.mainWindow.DispensingInternalFrame.next.Click();
            Thread.Sleep(2000);
            WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.MaterialInternalFrame.next.Click();
            if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled)
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            }
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
            Web_Fuction.gotoTab(WDWebTab.order);
            Thread.Sleep(3000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver.FindElement("//td[text()='test1']/../td[7]").Text, "Started");
            driver.FindElement("//td[text()='test1']/../td[3]/img").Click();
            Base_Assert.IsFalse(driver.FindElement("//a[text()='Redispense a Material']").GetAttribute("class").Contains("Disable"));
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "redispense_enable.PNG");
            WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.MaterialInternalFrame.next.Click();
            if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled)
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();

            }
            WD.mainWindow.HandingInternalFrame.AcknowledgeButton.ClickSignle();
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("M801890001");
            if (WD.ConfirmationDialog._UFT_Dialog.IsEnabled)
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("180");

            WD.SimulatorWindow.OK.Click();

            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }


            WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.MaterialInternalFrame.next.Click();
            if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled)
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();

            }
            WD.mainWindow.HandingInternalFrame.AcknowledgeButton.ClickSignle();
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("1072003");
            if (WD.ConfirmationDialog._UFT_Dialog.IsEnabled)
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("180");

            WD.SimulatorWindow.OK.Click();

            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            WD_Fuction.Close();
            Web_Fuction.gotoTab(WDWebTab.order);
            Thread.Sleep(3000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver.FindElement("//td[text()='test1']/../td[7]").Text, "Completed");
            driver.FindElement("//td[text()='test1']/../td[3]/img").Click();
            Base_Assert.IsFalse(driver.FindElement("//a[text()='Redispense a Material']").GetAttribute("class").Contains("Disable"));
            driver.Close();
            //deviation
            WD_Fuction.Bulkload(xml2);
            Selenium_Driver driver1 = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver1);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Thread.Sleep(3000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver1.Wait();
            LogStep(@"select a container to redispense");
            driver1.FindElement("//td[text()='test1']/../td[3]/img").Click();
            driver1.FindElement("//a[text()='Redispense a Material']").Click();
            Thread.Sleep(2000);
            var materials_list1 = driver1.FindElements("//div[text()='Select a material to redispense.']/../../..//table[@class='Order_Table_body_Style_Collapse']/tbody/tr");
            driver1.FindElement("//td[text()='Non-FEFO']/../td[1]/span/input").Click();
            driver1.FindElement("//button[text()='Redispense Material']").Click();
            Thread.Sleep(2000);
            Base_Assert.IsTrue(driver1.FindElement("//div[@class='DialogTitleName']").Displayed);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "signature_pop_up.PNG");
            driver1.FindElement("//input[@type='password']").SendKeys(PassWord.qaone1);
            driver1.FindElement(("//button[@id='Dialogbox_Bottom_OK_Button_Id']")).Click();
            Thread.Sleep(2000);
            driver1.FindElement("//a[text()='Redispense a Material']").Click();
            Thread.Sleep(2000);
            var materials_list2 = driver1.FindElements("//div[text()='Select a material to redispense.']/../../..//table[@class='Order_Table_body_Style_Collapse']/tbody/tr");
            Base_Assert.AreEqual(materials_list1.Count, materials_list2.Count + 1);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "materials_list_changed.PNG");
            driver1.Close();
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            Thread.Sleep(2000);
            WD.mainWindow.DispensingInternalFrame.orderTable.SelectRows(0);
            WD.mainWindow.DispensingInternalFrame.next.Click();
            Thread.Sleep(2000);
            WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
            WD.mainWindow.MaterialInternalFrame.next.Click();
            //if (WD.mainWindow.BoothCleanInternalFrame.IsEnabled)
            //{
            //    WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();

            //}
            WD.mainWindow.HandingInternalFrame.AcknowledgeButton.ClickSignle();
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("1072003");
            if (WD.ConfirmationDialog._UFT_Dialog.IsEnabled)
            {
                WD.ConfirmationDialog.YesButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            Thread.Sleep(2000);
            WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
            WD.mainWindow.Dialog.OK.Click();
            Thread.Sleep(2000);
            WD.SimulatorWindow.weight.SetText("180");

            WD.SimulatorWindow.OK.Click();

            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
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
            Selenium_Driver driver2 = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver2);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Thread.Sleep(3000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver2.FindElement("//td[text()='test1']/../td[7]").Text, "Deviation Pending");
            driver2.FindElement("//td[text()='test1']/../td[3]/img").Click();
            Base_Assert.IsFalse(driver2.FindElement("//a[text()='Redispense a Material']").GetAttribute("class").Contains("Disable"));
            driver2.FindElement("//div[text()='Deviation Management']").Click();
            driver2.Wait();
            driver2.FindElement("//a[text()='Deviation Pending']").Click();
            driver2.FindElement("//a[text()='Ack/Comment']").Click();
            driver2.FindElement("//textarea[@class='gwt-TextArea Comment_TextArea']").SendKeys("test");
            driver2.FindElement("//label[text()='I acknowledge this deviation.']/../input").Click();
            driver2.FindElement("//div[text()='Username:']/../../td[2]/input").SendKeys(UserName.qaone1);
            driver2.FindElement("//div[text()='Password:']/../../td[2]/input").SendKeys(PassWord.qaone1);
            driver2.FindElement("//button[text()='OK']").Click();
            //Finished the order
            Thread.Sleep(3000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(5000);
            driver2.FindElement("//a[text()='Accept/Comment']").Click();
            Thread.Sleep(5000);
            driver2.FindElement("//textarea[@class='gwt-TextArea Comment_TextArea']").SendKeys("test");
            driver2.FindElement("//label[text()='I accept all product deviations in this order.']/../input").Click();
            driver2.FindElement("//div[text()='Username:']/../../td[2]/input").SendKeys(UserName.qaone1);
            driver2.FindElement("//div[text()='Password:']/../../td[2]/input").SendKeys(PassWord.qaone1);
            driver2.FindElement("//button[text()='OK']").Click();
            Thread.Sleep(3000);
            driver2.FindElement("//div[text()='Orders' and  @class='Tab_Label']").Click();
            Thread.Sleep(3000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver2.FindElement("//td[text()='test1']/../td[7]").Text, "Finished");
            driver2.FindElement("//td[text()='test1']/../td[3]/img").Click();
            Base_Assert.IsFalse(driver2.FindElement("//a[text()='Redispense a Material']").GetAttribute("class").Contains("Disable"));
        }


    }
}