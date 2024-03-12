using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using OpenQA.Selenium;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;
using System.Linq;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class Mobile_TestCase
    {
        [TestCaseID(825087)]
        [Title("UC730129_the order phase table status auto refresh when status of previous phase changes")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]



        [TestMethod]
        public void VSTS_825087()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string ordername = "ORDER825087";
            string RPLName = "RPL825087";
            Application.LaunchMocAndLogin();
            Thread.Sleep(3000);
            //check bpl exit
            APEM.MocmainWindow.RPLDesign.Click();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE825087.zip");
            }
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.CheckRowSelection();
            MOC_Fuction.PlanFromRPL(RPLName, ordername);
            Selenium_Driver Edge_driver = new Selenium_Driver(Browser.edge);
            Mobile_Fuction.gotoApemMobile(Edge_driver);
            Mobile_Fuction.login(UserName.qaone2, PassWord.qaone2);
            Thread.Sleep(5000);
            Edge_driver.Minimize();
            Selenium_Driver chrome_driver = new Selenium_Driver(Browser.chrome);                                          
            Mobile_Fuction.gotoApemMobile(chrome_driver);
            Mobile_Fuction.login();    
            Thread.Sleep(5000);
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(ordername);
            Thread.Sleep(5000);
            //go to tracking page
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(1000);
            int no = 0;
            int index = 0;
            int i = 0;
            foreach (IWebElement head in Mobile.OrderTracking_Page.OrderPhaseTableHeads)
            {
                if (head.Text == "Status")
                {
                    no = i;
                }
                if (head.Text == "Name")
                {
                    index = i;
                }
                i++;
            }
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "first_Ready.PNG");
            Assert.IsTrue(Mobile.OrderTracking_Page.OrderPhaseTableRows[0].FindElements(By.TagName("td"))[no].Text.Contains("Ready"));
            Assert.IsTrue(Mobile.OrderTracking_Page.OrderPhaseTableRows[1].FindElements(By.TagName("td"))[no].Text.Contains("Not ready"));
            Assert.IsTrue(Mobile.OrderTracking_Page.OrderPhaseTableRows[2].FindElements(By.TagName("td"))[no].Text.Contains("Not ready"));
            Assert.IsTrue(Mobile.OrderTracking_Page.OrderPhaseTableRows[3].FindElements(By.TagName("td"))[no].Text.Contains("Not ready"));
            chrome_driver.SwitchToEdge();
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOn_mode(2);
            Mobile.Main_Page.Consolidated.Click();
            Thread.Sleep(3000);
            Mobile.Consolidated_Page.OrderSearch.SendKeys(ordername);
            Thread.Sleep(2000);
            int no_Edge = 0;
            int index_Edge = 0;
            int i_Edge = 0;
            foreach (IWebElement head in Mobile.Consolidated_Page.OrderPhaseTableHeads)
            {
                if (head.Text == "Status")
                {
                    no_Edge = i_Edge;
                }
                if (head.Text == "Name")
                {
                    index_Edge = i_Edge;
                }
                i_Edge++;
            }
            Assert.IsTrue(Mobile.Consolidated_Page.OrderPhaseTableRows[0].FindElements(By.TagName("td"))[no_Edge].Text.Contains("Ready"));
            Assert.IsTrue(Mobile.Consolidated_Page.OrderPhaseTableRows[1].FindElements(By.TagName("td"))[no_Edge].Text.Contains("Not Ready"));
            Assert.IsTrue(Mobile.Consolidated_Page.OrderPhaseTableRows[2].FindElements(By.TagName("td"))[no_Edge].Text.Contains("Not Ready"));
            Assert.IsTrue(Mobile.Consolidated_Page.OrderPhaseTableRows[3].FindElements(By.TagName("td"))[no_Edge].Text.Contains("Not Ready"));

            LogStep(@"Execute in moc");
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(3000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(ordername);
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(5000);
            //finish order
            APEM.PhaseExecWindow.ExecutionInternalFrame.OK_Button.Click();
            Edge_driver.SwitchToChrome();
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "finished_first.PNG");
            Assert.IsTrue(Mobile.OrderTracking_Page.OrderPhaseTableRows[0].FindElements(By.TagName("td"))[no].Text.Contains("Finished"));
            Assert.IsTrue(Mobile.OrderTracking_Page.OrderPhaseTableRows[1].FindElements(By.TagName("td"))[no].Text.Contains("Ready"));
            Assert.IsTrue(Mobile.OrderTracking_Page.OrderPhaseTableRows[2].FindElements(By.TagName("td"))[no].Text.Contains("Ready"));
            Assert.IsTrue(Mobile.OrderTracking_Page.OrderPhaseTableRows[3].FindElements(By.TagName("td"))[no].Text.Contains("Not ready"));
            chrome_driver.SwitchToEdge();
            Assert.IsTrue(Mobile.Consolidated_Page.OrderPhaseTableRows[0].FindElements(By.TagName("td"))[no_Edge].Text.Contains("Ready"));
            Assert.IsTrue(Mobile.Consolidated_Page.OrderPhaseTableRows[1].FindElements(By.TagName("td"))[no_Edge].Text.Contains("Ready"));
            Assert.IsTrue(Mobile.Consolidated_Page.OrderPhaseTableRows[2].FindElements(By.TagName("td"))[no_Edge].Text.Contains("Not Ready"));
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(5000);
            //finish order
            APEM.PhaseExecWindow.ExecutionInternalFrame.OK_Button.Click();
            Thread.Sleep(10000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(5000);
            //finish order
            APEM.PhaseExecWindow.ExecutionInternalFrame.OK_Button.Click();
            Thread.Sleep(10000);
            Edge_driver.SwitchToChrome();
            
            Assert.IsTrue(Mobile.OrderTracking_Page.OrderPhaseTableRows[0].FindElements(By.TagName("td"))[no].Text.Contains("Finished"));
            Assert.IsTrue(Mobile.OrderTracking_Page.OrderPhaseTableRows[1].FindElements(By.TagName("td"))[no].Text.Contains("Finished"));
            Assert.IsTrue(Mobile.OrderTracking_Page.OrderPhaseTableRows[2].FindElements(By.TagName("td"))[no].Text.Contains("Finished"));
            Assert.IsTrue(Mobile.OrderTracking_Page.OrderPhaseTableRows[3].FindElements(By.TagName("td"))[no].Text.Contains("Ready"));
            chrome_driver.SwitchToEdge();
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "finished_serial.PNG");
            Assert.IsTrue(Mobile.Consolidated_Page.OrderPhaseTableRows[0].FindElements(By.TagName("td"))[no_Edge].Text.Contains("Ready"));
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOff_mode(2);
        }
    }
}