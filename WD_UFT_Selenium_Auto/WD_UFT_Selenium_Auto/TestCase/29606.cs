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

        [TestCaseID(29606)]
        [Title("Order: Once all participant orders are ?Completed?, campaign is considered completed and it is removed from list.")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_29606()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order1 = "test1";     
            LogStep(@"1. create a campaign with orders");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Thread.Sleep(3000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver.FindElement("//td[text()='"+ order1+"']/../td[1]/span[@class='gwt-CheckBox']/input").Click();
            driver.FindElement("//a[text()='Create Campaign']").Click();
            driver.Wait();
            driver.FindElement("//input[@class='WD_TextBox']").SendKeys("test001");
            driver.FindElement("//button[text()='OK']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//button[text()='OK']").Click();
            Web_Fuction.active_order(order1);
            //Campaigns
            driver.FindElement("//div[text()='Campaigns']").Click();
            Thread.Sleep(3000);
            Base_Assert.IsTrue(driver.FindElement("//td[text()='test001']").Displayed);
            driver.Wait();      
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "campaign_created.PNG");
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
            LogStep(@"check campaign");
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver.FindElements("//table[@class='Order_Table_body_Style_Collapse']/tbody/tr").Count,1);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "campaign_removed.PNG");
            driver.FindElement("//div[text()='Orders' and  @class='Tab_Label']").Click();
            Thread.Sleep(3000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver.FindElement("//td[text()='test1']/../td[7]").Text, "Completed");
        }

       
    }
}