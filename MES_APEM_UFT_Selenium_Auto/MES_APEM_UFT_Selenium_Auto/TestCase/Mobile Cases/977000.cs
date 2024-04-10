﻿using System.Collections;
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
        [TestCaseID(977000)]
        [Title("UC967898_Cancel executing phase function should work on Session Manager if user have administrator role in AFW security Manager.")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_977000()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName = "Order977000";
            string RPLName = "RPL977000";

            Application.LaunchMocAndLogin();
            LogStep(@"1. import bpl");//import bpl
            //check bpl exit
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE977000.zip");
            }
            LogStep(@"2. create order");
            MOC_Fuction.PlanFromRPL(RPLName, OrderName);
            APEM.ExitApplication();
            LogStep(@"3. login mobile");
            //execute order with qaone3
            Selenium_Driver edge = new Selenium_Driver(Browser.edge);
            Mobile_Fuction.gotoApemMobile(edge);
            Mobile_Fuction.login(UserName.qaone3,PassWord.qaone3);
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(OrderName);
            Thread.Sleep(5000);
            //go to tracking page,execute the phase
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(5000);
            Mobile.OrderTracking_Page.ReadyPhase.Click();
            Thread.Sleep(5000);

            edge.Minimize();

            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            LogStep(@"4. check Cancel executing phase function");
            //off dark or consolidated mode if on
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOff_mode(1);
            Mobile.Setting_Page.turnOff_mode(2);
            Thread.Sleep(2000);
            try
            {
                SessionManagerCancel(driver, Resultpath);
                Thread.Sleep(5000);
                //check executing the phase using qae\qaone3 in Edge.
                edge.SwitchToEdge();
                //switched to PFC page/order tracking page
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "After cancel phase.PNG");
                Base_Assert.IsTrue(edge.GetUrl().Contains("tracking"), "in tracking page");
                Base_Assert.IsTrue(Mobile.SessionManager_Page.TableRows[0].FindElement(By.TagName("mat-icon")).GetAttribute("data-mat-icon-name").Contains("phase_state_enabled"), "Phase is Ready to execute");
                //execute phase again
                Mobile.OrderTracking_Page.ReadyPhase.Click();
                Thread.Sleep(5000);
                LogStep(@"5. Change to dark mode");
                driver.SwitchToChrome();
                Mobile.Main_Page.Setting.Click();
                Mobile.Setting_Page.turnOn_mode(1);
                LogStep(@"6. Check Session manager in dark mode");
                SessionManagerCancel(driver, Resultpath, "dark");
                //check executing the phase using qae\qaone3 in Edge.
                edge.SwitchToEdge();
                //switched to PFC page/order tracking page
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "After cancel phase in dark.PNG");
                Base_Assert.IsTrue(edge.GetUrl().Contains("tracking"), "in tracking page");
                Base_Assert.IsTrue(Mobile.SessionManager_Page.TableRows[0].FindElement(By.TagName("mat-icon")).GetAttribute("data-mat-icon-name").Contains("phase_state_enabled"), "Phase is Ready to execute");
            }
            finally
            {
                //restore data
                driver.SwitchToChrome();
                Mobile.Main_Page.Setting.Click();
                Mobile.Setting_Page.turnOff_mode(1);
                Mobile.Setting_Page.turnOff_mode(2);
                //cancel order
                Mobile.Main_Page.ManageModule.Click();
                Thread.Sleep(3000);
                Mobile.SessionManager_Page.Search.SendKeys(UserName.qaone3);
                Thread.Sleep(2000);
                Mobile.SessionManager_Page.CloseSession.Click();
                Thread.Sleep(3000);
                Mobile.SessionManager_Page.Dialog_Yes.Click();
                Thread.Sleep(3000);
                Mobile.Main_Page.Setting.Click();
                //cancel chrome and logout
                Mobile_Fuction.CancelAllExecutingPhase();
                edge.SwitchToEdge();
                edge.Close();

            }
        }
        private void SessionManagerCancel(Selenium_Driver driver, string Resultpath, string mode = "")
        {
            //go to Session manager
            Mobile.Main_Page.ManageModule.Click();
            Thread.Sleep(2000);
            Mobile.SessionManager_Page.Search.SendKeys("ORDER977000");
            Thread.Sleep(2000);
            int Pcount = Mobile.SessionManager_Page.TableRows.Count;
            Mobile.SessionManager_Page.CancePhase.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + " " + mode + " " + "Cancel executing phase dialog .PNG");
            //click no
            Mobile.SessionManager_Page.Dialog_No.Click();
            Thread.Sleep(2000);
            Base_Assert.IsFalse(driver.is_element_exist("//mat-dialog-container"),"Dialog closed");
            //get number
            int Mcount = Mobile.SessionManager_Page.TableRows.Count;
            //check data count
            Base_Assert.IsTrue(Pcount == Mcount, "No phase cancel");
            //click yes
            Mobile.SessionManager_Page.CancePhase.Click();
            Thread.Sleep(2000);
            Mobile.SessionManager_Page.Dialog_Yes.Click();
            //get cancel time
            var cancel_time = DateTime.Now;
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + " " + mode + " " + "Cancel one executing phase.PNG");
            Base_Assert.IsFalse(driver.is_element_exist("//table/tbody/tr"), "phase cancel");
            //check eventlog
            Mobile.Main_Page.Event.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + " " + mode + " " + "Eventlog.PNG");
            //check result
            int l = 0;
            foreach (var head in Mobile.EventLog_Page.EventLogTableHeads)
            {
                //check result is right
                if (head.Text == "Description")
                {
                    Base_Assert.AreEqual(Mobile.EventLog_Page.EventLogTableRows[0].FindElements(By.TagName("td"))[l].Text, "The phase execution was cancelled from Session Manager.", "Error Description");
                }
                if (head.Text == "Code")
                {//SSERROR/ORDER977000/17/PHASE17
                    Base_Assert.IsTrue(Mobile.EventLog_Page.EventLogTableRows[0].FindElements(By.TagName("td"))[l].Text.Contains("SSERROR/ORDER977000"), "Error Description");
                }
                if (head.Text == "Date")
                {
                    //get lastest event time
                    var time = Mobile.EventLog_Page.EventLogTableRows[0].FindElements(By.TagName("td"))[l].Text;
                    DateTime event_time = Convert.ToDateTime(time);
                    Base_Assert.IsTrue(Math.Abs(event_time.Subtract(cancel_time).TotalSeconds) < 5, "new event add");
                }
                l++;
            }
        }

    }
}