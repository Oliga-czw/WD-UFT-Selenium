using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        [TestCaseID(42801)]
        [Title("manage orders/materials in campaign")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_42801()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. create a campaign with order test1");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Thread.Sleep(3000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            driver.FindElement("//td[text()='test1']/../td[1]/span[@class='gwt-CheckBox']/input").Click();
            driver.FindElement("//a[text()='Create Campaign']").Click();
            driver.Wait();
            driver.FindElement("//input[@class='WD_TextBox']").SendKeys("test001");
            driver.FindElement("//button[text()='OK']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//button[text()='OK']").Click();
            LogStep(@"2. create a campaign with order test2");
            driver.FindElement("//td[text()='test2']/../td[1]/span[@class='gwt-CheckBox']/input").Click();
            driver.FindElement("//a[text()='Create Campaign']").Click();
            driver.Wait();
            driver.FindElement("//input[@class='WD_TextBox']").SendKeys("test002");
            driver.FindElement("//button[text()='OK']").Click();
            Thread.Sleep(2000);
            driver.FindElement("//button[text()='OK']").Click();
            driver.FindElement("//div[text()='Campaigns']").Click();
            Thread.Sleep(3000);
            string start_status = driver.FindElement("//td[text()='test001']/../td[5]").Text;
            Base_Assert.AreEqual(start_status,"Planned");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Planned_campaign.PNG");
            LogStep(@"3. Active a campaign");
            driver.FindElement("//td[text()='test001']/../td[1]").Click();
            driver.FindElement("//a[text()='Activate Orders for Selected Campaigns']").Click();
            Thread.Sleep(3000);
            string final_status = driver.FindElement("//td[text()='test001']/../td[5]").Text;
            Base_Assert.AreEqual(final_status, "Active");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Active_campaign.PNG");
            LogStep(@"4. delete a campaign");
            int count1 = driver.FindElements("//td[text()='test001']/../../tr").Count;
            driver.FindElement("//td[text()='test002']/../td[10]/img").Click();
            Thread.Sleep(2000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "delete_campaign.PNG");
            driver.FindElement("//button[@class='gwt-Button OkStyle']").Click();
            Thread.Sleep(3000);
            Base_Assert.AreEqual(driver.FindElements("//td[text()='test001']/../../tr").Count,count1-1);
            LogStep(@"5. Assign selected material(s) to Campaign");
            driver.FindElement("//td[text()='test001']/../td[9]/img").Click();
            Thread.Sleep(1000);
            driver.FindElement("//td[text()='X0125']/../td[1]").Click();
            driver.FindElement("//td[text()='M801890']/../td[1]").Click();
            driver.FindElement("//a[text()='Assign to Campaign']").Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "material(s)_to_Campaign.PNG");
            driver.FindElement("//button[text()='Apply']").Click();
            Application.LaunchWDAndLogin();
            Thread.Sleep(5000);
            WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
            Thread.Sleep(2000);
            WD.mainWindow.GetSnapshot(Resultpath + "order_is_displayed.PNG");
            var order_name = WD.mainWindow.DispensingInternalFrame.orderTable.GetCell(0,"Order").Value;
            Base_Assert.AreEqual(order_name,"test1");
            WD.mainWindow.DispensingInternalFrame.HomeButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.HomeInternalFrame.CampaignDispense.Click();
            Thread.Sleep(2000);
            WD.mainWindow.CampaignSelectionInternalFrame.CampaignsTable.SelectRows(0);
            WD.mainWindow.CampaignSelectionInternalFrame.nextButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.GetSnapshot(Resultpath + "Materials_displayed.PNG");
            var material_name1 = WD.mainWindow.MaterialInternalFrame.materialTable.GetCell(0, "Material ID").Value;
            var material_name2 = WD.mainWindow.MaterialInternalFrame.materialTable.GetCell(1, "Material ID").Value;
            Base_Assert.AreEqual(material_name1, "X0125");
            Base_Assert.AreEqual(material_name2, "M801890");
            WD.mainWindow.MaterialInternalFrame.cancel.Click();
            Thread.Sleep(2000);
            WD.mainWindow.CampaignSelectionInternalFrame.homeButton.Click();
            Thread.Sleep(2000);
            LogStep(@"6.Assign selected material(s) to Booth");
            driver.FindElement("//td[text()='test001']/../td[9]/img").Click();
            Thread.Sleep(1000);
            driver.FindElement("//td[text()='M801890']/../td[1]").Click();
            driver.FindElement("//a[text()='Assign to Booth']").Click();
            driver.FindElement("//select[@class='gwt-ListBox']/option[text()='booth2']").Click();
            driver.FindElement("//button[@class='gwt-Button OkStyle']").Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "material(s)_to_Booth.PNG");
            driver.FindElement("//button[text()='Apply']").Click();
            WD.mainWindow.HomeInternalFrame.CampaignDispense.Click();
            Thread.Sleep(2000);
            WD.mainWindow.CampaignSelectionInternalFrame.CampaignsTable.SelectRows(0);
            WD.mainWindow.CampaignSelectionInternalFrame.nextButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.GetSnapshot(Resultpath + "Materials_booth_displayed.PNG");
            var material_name = WD.mainWindow.MaterialInternalFrame.materialTable.GetCell(0, "Material ID").Value;
            Base_Assert.AreEqual(material_name, "X0125");
            Base_Assert.AreEqual(WD.mainWindow.MaterialInternalFrame.materialTable.Rowscount(),1);
            LogStep(@"7.export campaign list to CSV format");
            Base_File.ClearFolder("C:\\Users\\qaone1\\Downloads");
            driver.FindElement("//a[text()='CSV']").Click();
            Thread.Sleep(3000);
            List<string> tempFileNames = new List<string>();
            DirectoryInfo tempFolder = new DirectoryInfo("C:\\Users\\qaone1\\Downloads");
            foreach (FileInfo file in tempFolder.GetFiles())
            {
                tempFileNames.Add(file.FullName);
            }
            string fileName = tempFileNames[0];
            Console.WriteLine(fileName);
            
            List<string> DataList = Base_File.ReadCsv(fileName);
            Base_Assert.AreEqual(DataList.Count(),1);
            foreach (var coloumn2 in DataList)
            {
                Base_Assert.IsTrue(coloumn2.Contains("test001"));

            }


        }

       
    }
}