using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using HP.LFT.SDK;
using System.Data;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(42875)]
        [Title("deviation Report")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]
        
        [TestMethod]
        public void VSTS_42875()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;

            string xml = "14 aspen wd deviation_42875 bulk load.xml";
            string order = "test1";
            string material = WDMaterial.X0125;
            string product = "1902";
            string lot = "B50877";
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string tare = "500";
            string net = "944.4";

            LogStep(@"1. import deviation xml and Active orders");
            WD_Fuction.Bulkload(xml);
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(6000);
            Web_Fuction.active_order(order);
            LogStep(@"2. Open WD client,create deviation and finish weight");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            //input large tare
            //WD_Fuction.FinishNetDiapense(tare, net);
            //zero
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.SimulatorWindow.weight.SetText(tare);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            Thread.Sleep(2000);
            //devition dialog
            if (WD.mainWindow.Dialog.IsExist())
            {
                WD.mainWindow.Dialog.Password.SetSecure(PassWord.qaone1);
                WD.mainWindow.Dialog.OK.Click();
                Thread.Sleep(2000);
            }
            //weight
            WD.SimulatorWindow.weight.SetText(net);
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(1000);
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            Thread.Sleep(1000);
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            //check Finish Dispense
            Base_Assert.IsTrue(WD.mainWindow.Material_SelectionInternalFrame.IsExist() || WD.mainWindow.MaterialInternalFrame.IsExist() || WD.mainWindow.DispensingInternalFrame.IsExist(), "Finish Dispense");
            WD_Fuction.Close();

            LogStep(@"3. go to deviation report");
            Web_Fuction.gotoTab(WDWebTab.report);
            Web.Report_Page.Deviation.Click();
            LogStep(@"4. set criteria ");
            var Type = driver.FindElements("//select")[1];
            var Operator = driver.FindElements("//select")[0];
            var Product = driver.FindElements("//input[@class='WD_TextBox']")[0];
            var Material = driver.FindElements("//input[@class='WD_TextBox']")[1];
            var Order = driver.FindElements("//input[@class='WD_TextBox']")[2];
            var Lot = driver.FindElements("//input[@class='WD_TextBox']")[3];
            
            Web.Report_Page.Start_Time.Click();
            driver.FindElement("//button[text()='Zero']").Click();
            driver.Wait();
            Type.FindElement(By.XPath("//option[text()='Event']")).Click();
            Console.Write($"//option[text()='{userNameforReport.qaone1}']");
            Operator.FindElement(By.XPath($"//option[text()='{userNameforReport.qaone1}']")).Click();
            Web.Report_Page.End_Time.Click();
            driver.FindElement("//button[text()='Now']").Click();
            driver.Wait();
            Product.SendKeys(product);
            Lot.SendKeys(lot);
            Material.SendKeys(material);
            Order.SendKeys(order);
            Web.Report_Page.Generate_Report.Click();
            Thread.Sleep(2000);
            LogStep(@"5.check report");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Deviation Report.PNG");
            var column = new List<string>() { "Material", "Type", "Operator","Order", "Description", "Lot" , "Product" };
            var datatext = new List<string>() { material, "Event", userNameforReport.qaone1, order, "Tare limits exceeded", lot, product };
           
            //check data
            Web_Fuction.Check_report(column, datatext);
            LogStep(@"6.save pdf and print ");
            Thread.Sleep(2000);
            Web.Report_Page.SaveAs.Click();
            Thread.Sleep(2000);
            Web.Report_Page.Print.Click();
            //wait for screenshot and download
            Thread.Sleep(10000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Print Report.PNG");
            driver.FindElement("//button[text()='Close']").Click();
            //The searchList can't contain ' '
            var searchList = new List<string>() { "Event", userNameforReport.qaone1, order,material,lot,product };
            Web_Fuction.Check_PDF(Base_Directory.DownloadFileDir, "*DEVIATION*", searchList);
            driver.Close();

            
        }
    }
}
