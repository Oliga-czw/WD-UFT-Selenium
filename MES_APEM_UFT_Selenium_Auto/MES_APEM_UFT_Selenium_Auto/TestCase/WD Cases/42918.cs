using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(42918)]
        [Title("weighing report")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]
        
        [TestMethod]
        public void VSTS_42918()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";
            string material = WDMaterial.X0125;
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string tare = "15";
            string net1 = "215";
            string net2 = "459.4";
            string booth = "booth1";
            string scale = "simulator";

            LogStep(@"1. Active orders");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(6000);
            Web_Fuction.active_order(order);
            LogStep(@"2. Open WD client and finish weight");
            Application.LaunchWDAndLogin();
            WD_Fuction.SelectOrderandMaterial(order, material);
            WD_Fuction.SelectMehod(method, barcode);
            //zero
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            //tare
            WD.SimulatorWindow.weight.SetText(tare);
            WD.SimulatorWindow.OK.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            Thread.Sleep(2000);
            //weight
            WD.SimulatorWindow.weight.SetText(net1);
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(1000);
            //new source
            WD.mainWindow.ScaleWeightInternalFrame.NewSource.Click();
            WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys(barcode);
            //weight
            WD.SimulatorWindow.weight.SetText(net2);
            WD.SimulatorWindow.OK.Click();
            Thread.Sleep(1000);
            WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            Thread.Sleep(1000);
            //check Finish Dispense
            Base_Assert.IsTrue(WD.mainWindow.Material_SelectionInternalFrame.IsExist() || WD.mainWindow.MaterialInternalFrame.IsExist() || WD.mainWindow.DispensingInternalFrame.IsExist(), "Finish Dispense");
            WD_Fuction.Close();
            
            LogStep(@"3. go to order report");
            Web_Fuction.gotoTab(WDWebTab.report);
            Web.Report_Page.Weighing.Click();
            LogStep(@"4. set criteria ");
            var Booth = driver.FindElements("//select")[0];
            var Scale = driver.FindElements("//select")[1];
            var Type = driver.FindElements("//select")[2];
            var Operator = driver.FindElements("//select")[3];

            var Material = driver.FindElements("//input[@class='WD_TextBox']")[1];
            var Order = driver.FindElements("//input[@class='WD_TextBox']")[0];
            var Lot = driver.FindElements("//input[@class='WD_TextBox']")[2];

            Web.Report_Page.Start_Time.Click();
            driver.FindElement("//button[text()='Zero']").Click();
            driver.Wait();
            Booth.FindElement(By.XPath($"//option[text()='{booth}']")).Click();
            Scale.FindElement(By.XPath($"//option[text()='{scale}']")).Click();
            string type = "Net";
            Type.FindElement(By.XPath($"//option[text()='{type}']")).Click();
            Operator.FindElement(By.XPath($"//option[text()='{userNameforReport.qaone1}']")).Click();
            Web.Report_Page.End_Time.Click();
            driver.FindElement("//button[text()='Now']").Click();
            driver.Wait();
            string lot = "B50877";
            Material.SendKeys(material);
            Order.SendKeys(order);
            Lot.SendKeys(lot);
            Web.Report_Page.Generate_Report.Click();
            Thread.Sleep(3000);
            LogStep(@"5.check report");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "order Report.PNG");
            var column = new List<string>() { "MaterialID", "Type", "Operator","Order", "FEFO", "Booth", "Scale" };
            var datatext = new List<string>() { material, type, userNameforReport.qaone1, order,"Yes",booth,scale };
            //check data
            Web_Fuction.Check_report(column, datatext);
            //check action ,Lot,Source
            var heads = Web.Report_Page.Report_Heads;
            var table_datas = Web.Report_Page.Report_Table_Rows;
            int i = 0;
            foreach (var h in heads)
            {
                if (h.Text == "Action")
                {
                    
                    foreach (var data in table_datas)
                    {
                        string action = data.FindElements(By.TagName("td"))[i].Text;
                        List<string> actions = new List<string>() { "Accept", "New Source" };
                        Base_Assert.IsTrue(actions.Contains(action),"Action");
                    }
                }
                if (h.Text == "Source HU")
                {
                    foreach (var data in table_datas)
                    {
                        string action = data.FindElements(By.TagName("td"))[i].Text;
                        List<string> actions = new List<string>() { "X0125001", "X0125001, X0125001" };
                        Base_Assert.IsTrue(actions.Contains(action), "Source HU");
                    }
                }
                if (h.Text == "Lot")
                {
                    foreach (var data in table_datas)
                    {
                        string action = data.FindElements(By.TagName("td"))[i].Text;
                        List<string> actions = new List<string>() { "B50877", "B50877, B50877" };
                        Base_Assert.IsTrue(actions.Contains(action), "Lot");
                    }
                }
                i++;
            }
            LogStep(@"6.save pdf and print ");
            Thread.Sleep(2000);
            Web.Report_Page.SaveAs.Click();
            Thread.Sleep(2000);
            Web.Report_Page.Print.Click();
            //wait for screenshot and download
            Thread.Sleep(15000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Print Report.PNG");
            driver.FindElement("//button[text()='Close']").Click();
            //The searchList can't contain ' '
            var searchList = new List<string>() { type,material,booth,scale, userNameforReport.qaone1, order };
            Web_Fuction.Check_PDF(Base_Directory.DownloadFileDir, "*WEIGHING*", searchList);
            driver.Close();
        }
    }
}
