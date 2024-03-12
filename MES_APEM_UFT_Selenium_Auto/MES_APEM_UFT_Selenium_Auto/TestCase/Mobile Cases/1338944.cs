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
        [TestCaseID(1338944)]
        [Title("UC730129_set 'CANCEL_PENDDING_WORKSTATION_OPER = 1'and the order phase table status auto refresh when order turns invalid")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_1338944()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string Configpath = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string ConfigKey = "CANCEL_PENDDING_WORKSTATION_OPER = 1";
            string ordername = "ORDER1338944";
            try
            {
                LogStep(@"1. Set key in config file");
                Base_Function.EditConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                
                Application.LaunchMocAndLogin();
                MOC_Fuction.VerifyRPL("FOR_STATUS");
                MOC_Fuction.CertifyRPL("FOR_STATUS");
                Thread.Sleep(3000);
                APEM.MocmainWindow.Orders.ClickSignle();
                Thread.Sleep(2000);
                MOC_Fuction.CheckRowSelection();
                MOC_Fuction.PlanFromRPL("FOR_STATUS", ordername);
                APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Phases");
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
                Assert.IsTrue(Mobile.OrderTracking_Page.OrderPhaseTableRows[0].FindElements(By.TagName("td"))[no].Text.Contains("Ready"));
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
                int PhaseCount = Mobile.Consolidated_Page.OrderPhaseTableRows.Count;
                LogStep(@"Execute in moc");
                APEM.MocmainWindow.WorkstationBP.ClickSignle();
                MOC_Fuction.CheckRowSelection();
                Thread.Sleep(3000);
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(ordername);
                APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("PHASE55", "Name").Click();
                APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
                Thread.Sleep(15000);
                //finish order
                Edge_driver.SwitchToChrome();
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Executing.PNG");
                Assert.IsTrue(Mobile.OrderTracking_Page.OrderPhaseTableRows[0].FindElements(By.TagName("td"))[no].Text.Contains("Executing"));
                chrome_driver.SwitchToEdge();
                Assert.AreEqual(Mobile.Consolidated_Page.OrderPhaseTableRows.Count, PhaseCount - 1);
                LogStep(@"Kill the moc while there are phase executing");
                Base_Test.KillProcess("javaw");
                Thread.Sleep(5000);
                Application.LaunchMocAndLogin();
                Thread.Sleep(10000);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "killMOC_ready.PNG");
                Base_Assert.AreEqual(Mobile.Consolidated_Page.OrderPhaseTable._Selenium_WebElement.Size.Height, 0);
                Mobile.Main_Page.Setting.Click();
                Mobile.Setting_Page.turnOff_mode(2);
                Edge_driver.SwitchToChrome();
                Assert.IsTrue(Mobile.OrderTracking_Page.OrderPhaseTableRows[0].FindElements(By.TagName("td"))[no].Text.Contains("Cancelled"));
               

            }
            finally
            {
                LogStep(@"6.Restone config key ");
                Base_Function.DeleteConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
            }


        }

    }
}
