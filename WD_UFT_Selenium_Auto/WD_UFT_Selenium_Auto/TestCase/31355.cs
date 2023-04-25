using System;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(31355)]
        [Title("Scale Checking- Pass and accept")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_31355()
        {
           
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            LogStep(@"2. Go and admin and General");
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.General.Click();
            LogStep(@"3.check Log on required for Execution system, and click 'Apply' button.");

            if (Web.Administration_Page.log_on_required_chx.GetAttribute("checked") is null)
            {
                Web.Administration_Page.log_on_required_chx.Click();
                Web_Fuction.administration_Apply("Configuration successfully saved");
            }
            
            Assert.AreEqual(driver.FindElement("//div[text()='Welcome']/../../td[2]/div").Text, "qae\\qaone1");

            //Material
            Web_Fuction.gotoTab(WDWebTab.material);
            Assert.AreEqual(driver.FindElement("//div[@class='WD_Page_Title_Style']").Text, "Material");
            //Equipment Overview
            Web_Fuction.gotoTab(WDWebTab.equipment);
            Assert.AreEqual(driver.FindElement("//div[@class='WD_Page_Title_Style Left_Margin_5px']").Text, "Equipment Overview");
            //Inventory
            Web_Fuction.gotoTab(WDWebTab.inventory);
            Assert.AreEqual(driver.FindElement("//div[@class='WD_Page_Title_Style']").Text, "Inventory");
            //Orders
            Web_Fuction.gotoTab(WDWebTab.order);
            Assert.AreEqual(driver.FindElement("//div[@class='WD_Page_Title_Style']").Text, "Orders");
            //Report
            Web_Fuction.gotoTab(WDWebTab.report);
            Assert.AreEqual(driver.FindElement("//div[@class='WD_Page_Title_Style']").Text, "Cleaning Report");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "WD_WebLogin.PNG");
            driver.FindElement("//div[text()='Logoff']").Click();
            Thread.Sleep(3000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "WD_WebLogoff.PNG");
            //back to logon page
            Assert.IsNotNull(driver.FindElement("//div[text()='Log on']"));
            driver.Close();
            LogStep(@"4. Open Wd client");
            Thread.Sleep(2000);
            Base_Test.LaunchApp(Base_Directory.WDDir);
            WD.mainWindow.GetSnapshot(Resultpath + "WD_EXlogin.PNG");
            Base_Assert.IsTrue(WD.mainWindow.HomeInternalFrame.IsEnabled);
            Base_Assert.Equals(WD.mainWindow.HomeInternalFrame.weightBooth._UFT_Label.Text, "booth1");
            Base_Assert.Equals(WD.mainWindow.HomeInternalFrame.operatorName._UFT_Label.Text, "qaone1");
            WD.mainWindow.HomeInternalFrame.LogOff.Click();
            Thread.Sleep(2000);
            Base_Assert.IsTrue(WD.mainWindow.LogonInternalFrame.IsEnabled);

            WD.mainWindow.LogonInternalFrame.userNameEditor.SetText("qae\\qaone1");
            WD.mainWindow.LogonInternalFrame.passwordEditor.SetSecure("Aspen111");
            WD.mainWindow.LogonInternalFrame.loginbutton.Click();
            Thread.Sleep(3000);
            //scale checking
            
            WD.mainWindow.HomeInternalFrame.ScaleChecking.Click();
            Thread.Sleep(3000);
            Base_Assert.IsTrue(WD.mainWindow.ScaleCheckInternalFrame.IsEnabled);
            WD.mainWindow.ScaleCheckInternalFrame.homeButton.Click();
            Thread.Sleep(2000);
            //Order Dispensewindow 
            WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            Thread.Sleep(3000);
            Base_Assert.IsTrue(WD.mainWindow.DispensingInternalFrame.IsEnabled);
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            Thread.Sleep(2000);
            //booth cleaning
            WD.mainWindow.HomeInternalFrame.BoothCleaning.Click();
            Thread.Sleep(3000);
            Base_Assert.IsTrue(WD.mainWindow.BoothCleanInternalFrame.IsEnabled);
            WD.mainWindow.BoothCleanInternalFrame.HomeButton.Click();
            Thread.Sleep(2000);
            //Material Dispense
            WD.mainWindow.HomeInternalFrame.MaterialDispensing.Click();
            Thread.Sleep(3000);
            Base_Assert.IsTrue(WD.mainWindow.Material_SelectionInternalFrame.IsEnabled);
            WD.mainWindow.Material_SelectionInternalFrame.HomeButton.Click();
            Thread.Sleep(2000);
            //Order Kitting
            WD.mainWindow.HomeInternalFrame.OrderKitting.Click();
            Thread.Sleep(3000);
            Base_Assert.IsTrue(WD.mainWindow.SelectAnOrderToKittingFrame.IsEnabled);
            WD.mainWindow.SelectAnOrderToKittingFrame.HomeButton.Click();
            Thread.Sleep(2000);
            //Campaign Dispense
            WD.mainWindow.HomeInternalFrame.CampaignDispense.Click();
            Thread.Sleep(3000);
            Base_Assert.IsTrue(WD.mainWindow.CampaignSelectionInternalFrame.IsEnabled);
            WD.mainWindow.CampaignSelectionInternalFrame.homeButton.Click();
            Thread.Sleep(2000);
            //exit
            WD.mainWindow.HomeInternalFrame.Exit.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "WD_EXlogoff.PNG");
            WD.ConfirmationDialog.YesButton.Click();
            Base_Assert.IsFalse(WD.mainWindow._UFT_Window.IsEnabled);
        }

       
    }
}