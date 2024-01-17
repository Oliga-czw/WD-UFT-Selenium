using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(40978)]
        [Title("booth cleaning:campaign dispensing_Campaign Dispensing")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Low)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_40978()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
            string order = "test2";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string tare = "15";
            string net = "459.4";
            string campagin = "40978campagin";


            LogStep(@"1. Check clean rules");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.CleaningRules.Click();
            Web.Administration_Page.Types.Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Clean rules-Types-Setting.PNG");
            LogStep(@"2. Active order and create campaign");
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            //create Campaign
            Web.Order_Page.CreateCampaign.Click();
            //campagin name
            driver.FindElement("//input[@class='WD_TextBox']").SendKeys(campagin);
            driver.FindElement("//button[text()='OK']").Click();
            Base_Assert.AreEqual("The campaign was successfully created.", Web.Web_Page.Message._Selenium_WebElement.FindElement(By.XPath("//div[@class='gwt-Label Alert_Label']")).Text, "create campagin message");
            Web.Web_Page.Message._Selenium_WebElement.FindElement(By.XPath("//button[text()='OK']")).Click();
            //add material to campagin
            Web.Order_Page.Campaigns.Click();
            Thread.Sleep(3000);
            //edit campagin
            driver.FindElements($"//td[text()='{campagin}']/..//img")[0].Click();
            //select all material 
            Thread.Sleep(3000);
            driver.FindElements("//table[@class='Order_Table_body_Style_Collapse']/tbody/tr[1]//input")[1].Click();
            Web.Order_Page.AssigntoCampaign.Click();
            Web.Order_Page.Apply.Click();
            LogStep(@"3. Open WD client and do booth clean");
            Application.LaunchWDAndLogin();
            WD.mainWindow.HomeInternalFrame.BoothCleaning.Click();
            WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            //get execute time
            DateTime execute_time = DateTime.Now;
            //check go to home page
            Base_Assert.IsTrue(WD.mainWindow.HomeInternalFrame.IsEnabled);
            LogStep(@"4. do a Campaign Dispensing dispensing");
            //select campagin and material
            WD.mainWindow.HomeInternalFrame.CampaignDispense.Click();
            WD.mainWindow.CampaignSelectionInternalFrame.CampaignsTable.Row(campagin).Click();
            WD.mainWindow.CampaignSelectionInternalFrame.nextButton.Click();
            WD.mainWindow.MaterialInternalFrame.materialTable.Row(material).Click();
            WD.mainWindow.MaterialInternalFrame.next.Click();
            WD_Fuction.SelectMehod(method, barcode);
            WD_Fuction.FinishNetDiapense(tare,net);
            LogStep(@"5. launch BoothCleaning again,check the Current Status and Last dispensed fields");
            //back to home page
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            WD.mainWindow.CampaignSelectionInternalFrame.homeButton.Click();
            WD.mainWindow.HomeInternalFrame.BoothCleaning.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "Update information with last time entered.PNG");
            string Status1 = WD.mainWindow.BoothCleanInternalFrame.Status._UFT_Label.Text;
            string PreviousClean1 = WD.mainWindow.BoothCleanInternalFrame.PreviousClean._UFT_Label.Text;
            string Material1 = WD.mainWindow.BoothCleanInternalFrame.Material._UFT_Label.Text;
            string Order1 = WD.mainWindow.BoothCleanInternalFrame.Order._UFT_Label.Text;
            string Product1 = WD.mainWindow.BoothCleanInternalFrame.Product._UFT_Label.Text;

            Base_Assert.AreEqual("Clean for X0125", Status1, "Update information with last time entered");
            Base_Assert.AreEqual("X0125   X0125 Description", Material1, "Update information with last time entered");
            Base_Assert.AreEqual(order, Order1, "Update information with last time entered");
            Base_Assert.AreEqual("1902 25mM HEPS, 100mM NaCI, pH 8.00", Product1, "Update information with last time entered");
            //select a cleaning type again, but click Home
            WD.mainWindow.BoothCleanInternalFrame.HomeButton.Click();
            //check go to home page
            Base_Assert.IsTrue(WD.mainWindow.HomeInternalFrame.IsEnabled);
            LogStep(@"6. launch BoothCleaning again,check the Current Status and Last dispensed fields");
            WD.mainWindow.HomeInternalFrame.BoothCleaning.Click();
            WD.mainWindow.GetSnapshot(Resultpath + "no information updated.PNG");
            string Status2 = WD.mainWindow.BoothCleanInternalFrame.Status._UFT_Label.Text;
            string PreviousClean2 = WD.mainWindow.BoothCleanInternalFrame.PreviousClean._UFT_Label.Text;
            string Material2 = WD.mainWindow.BoothCleanInternalFrame.Material._UFT_Label.Text;
            string Order2 = WD.mainWindow.BoothCleanInternalFrame.Order._UFT_Label.Text;
            string Product2 = WD.mainWindow.BoothCleanInternalFrame.Product._UFT_Label.Text;
            Base_Assert.AreEqual(Status2, Status1, "Update information with last time entered");
            Base_Assert.AreEqual(Material2, Material1, "Update information with last time entered");
            Base_Assert.AreEqual(Order2, Order1, "Update information with last time entered");
            Base_Assert.AreEqual(Product2, Product1, "Update information with last time entered");
            Base_Assert.AreEqual(PreviousClean2, PreviousClean1, "Update information with last time entered");
            WD_Fuction.Close();
            //delete campagin
            driver.FindElements($"//td[text()='{campagin}']/..//img")[1].Click();
            string message = Web.Web_Page.Confirm._Selenium_WebElement.FindElement(By.XPath("//div[@class='gwt-Label Alert_Label']")).Text;
            Base_Assert.AreEqual($"Are you sure you want to delete campaign \"{campagin}\"?" , message, "Delete campagin message");
            Web.Web_Page.Confirm._Selenium_WebElement.FindElement(By.XPath("//button[text()='OK']")).Click();
            LogStep(@"7. go to booth clean report");
            Web_Fuction.gotoTab(WDWebTab.report);
            Web.Report_Page.Cleaning.Click();
            //set criteria
            var Booth = driver.FindElements("//select")[0];
            var Type = driver.FindElements("//select")[1];
            var Operator = driver.FindElements("//select")[2];
            Web.Report_Page.Start_Time.Click();
            driver.FindElement("//button[text()='Zero']").Click();
            driver.Wait();
            Booth.FindElement(By.XPath("//option[text()='booth1']")).Click();
            Type.FindElement(By.XPath("//option[text()='Full Clean']")).Click();
            Operator.FindElement(By.XPath($"//option[text()='{userNameforReport.qaone1}']")).Click();
            Web.Report_Page.End_Time.Click();
            driver.FindElement("//button[text()='Now']").Click();
            driver.Wait();
            Web.Report_Page.Generate_Report.Click();
            //5.check report
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Clean Report.PNG");
            var column = new List<string>() { "Booth", "Type", "Operator" };
            var datatext = new List<string>() { "booth1", "Full Clean", userNameforReport.qaone1 };
            //check date
            Web_Fuction.check_report_date(execute_time);
            //check data
            Web_Fuction.Check_report(column, datatext);

            driver.Close();
            
        }
    }
}
