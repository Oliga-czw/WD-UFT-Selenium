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

        [TestCaseID(42955)]
        [Title("Administration User Exits: add new tag for define logic.")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_42955()
        {
            string order1 = "test1";
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. create a campaign with order test1");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Thread.Sleep(3000);
            driver.FindElement("//div[text()='User Exits']/../../td[1]/img").Click();
            driver.Wait();
            //ERP Upload Messages
            driver.FindElement("//div[text()='ERP Upload Messages']/../../td[1]/img").Click();
            driver.Wait();
            //Material Consumption Reporting
            driver.FindElement("//div[text()='Material Consumption Reporting']").Click();
            List<string> CodeList = new List<string>();
            var codes = driver.FindElements("//table[@class='List_Table_Border_Style']/tbody/tr[@class != 'List_Background_Color']/td[2]/input");
            string OrderTag = "false";
            foreach (var code in codes)
            {
                if (code.GetAttribute("value").Contains("OrderTag") is true)
                {
                    OrderTag = "true";
                }          
            }
            if (OrderTag == "false")
            {
                string code_statement= "xmlStr:=xmlStr+\" < ProductionRequestID > \"+Data.OrderTag+\" </ ProductionRequestID > ";
                driver.FindElements("//table[@class='List_Table_Border_Style']/tbody/tr[@class != 'List_Background_Color']/td[2]/input")[-1].SendKeys(code_statement);
                driver.FindElement("//button[@title='Up']").Click();
                driver.FindElement("//button[@title='Up']").Click();
                driver.FindElement("//button[@title='Up']").Click();
                driver.FindElement("//button[@title='Up']").Click();
                driver.FindElement("//button[text()='Commit User Exit']").Click();
                Thread.Sleep(2000);
                driver.FindElement("//button[@class='gwt-Button OkStyle']").Click();

            }
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Material_Consumption_Reporting.PNG");
            Web_Fuction.gotoTab(WDWebTab.order);
            Thread.Sleep(3000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            Web_Fuction.active_order(order1);
            Base_File.ClearFolder("C:\\ProgramData\\AspenTech\\AeBRS\\WDUpload");

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
            string xmlString = Base_File.ReadXml("C:\\ProgramData\\AspenTech\\AeBRS\\WDUpload",1);
            Base_Assert.IsTrue(xmlString.Contains("<ProductionRequestID>test1</ProductionRequestID>"));


        }

       
    }
}