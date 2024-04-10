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
        [TestCaseID(977002)]
        [Title("UC967898_The phase will change to Ready to execute(Set CANCEL_PENDDING_WORKSTATION_OPER=0) when end user session function should work on Session Manager if user have administrator role in AFW security Manager.")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_977002()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName1 = "Order977002";
            string RPLName = "RPL977000";
            string Configpath = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string ConfigKey1 = @"CANCEL_PENDDING_WORKSTATION_OPER = 0";
            string ConfigKey2 = @"CANCEL_PENDDING_WORKSTATION_OPER = 1";
            
            //set config in flag
            Base_Function.EditConfigKey(Configpath, ConfigKey1);
            //codify all
            Base_Test.LaunchApp(Base_Directory.Codify_all);
            //restart tomcat
            Base_Test.KillProcess("tomcat10");
            Thread.Sleep(30000);
            Base_Function.ResartServices(ServiceName.Tomcat);
            //wait for tomcat start completely.
            Thread.Sleep(120000);
            try
            {
                Application.LaunchMocAndLogin();
                LogStep(@"1. import bpl");//import bpl
                //check bpl exit
                APEM.MocmainWindow.RPLDesign.ClickSignle();
                if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Existing)
                {
                    MOC_TemplatesFunction.Importtemplates("CASE977000.zip");
                }
                LogStep(@"2. create order");
                MOC_Fuction.PlanFromRPL(RPLName, OrderName1);
                APEM.ExitApplication();
                LogStep(@"3. login mobile");
                //execute order with qaone3
                Selenium_Driver edge = new Selenium_Driver(Browser.edge);
                Mobile_Fuction.gotoApemMobile(edge);
                Mobile_Fuction.login(UserName.qaone3, PassWord.qaone3);
                Mobile.OrderProcess_Page.OrderSearch.SendKeys(OrderName1);
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
                LogStep(@"4. check close user session function");
                //off dark or consolidated mode if on
                Mobile.Main_Page.Setting.Click();
                Mobile.Setting_Page.turnOff_mode(1);
                Mobile.Setting_Page.turnOff_mode(2);
                Thread.Sleep(2000);
                try
                {
                    SessionManagerClose(driver, Resultpath);
                    Thread.Sleep(5000);
                    //check executing the phase using qae\qaone3 in Edge.
                    edge.SwitchToEdge();
                    //logout page
                    Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Logout page.PNG");
                    Base_Assert.IsTrue(edge.GetUrl().Contains("logout"), "Logout page");
                    //relogin check phase state
                    Mobile.Logout_Page.login.Click();
                    Thread.Sleep(5000);
                    Mobile_Fuction.login(UserName.qaone3, PassWord.qaone3);
                    //switched to PFC page/order tracking page
                    Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Phase is ready.PNG");
                    Base_Assert.IsTrue(edge.GetUrl().Contains("tracking"), "in tracking page");
                    Base_Assert.IsTrue(Mobile.SessionManager_Page.TableRows[0].FindElement(By.TagName("mat-icon")).GetAttribute("data-mat-icon-name").Contains("phase_state_enabled"), "Phase is ready");
                    //execute another phase for dark mode
                    Mobile.Main_Page.ProcessOrder.Click();
                    Thread.Sleep(5000);
                    Mobile.OrderProcess_Page.OrderSearch.SendKeys(OrderName1);
                    Thread.Sleep(5000);
                    Mobile.OrderProcess_Page.GotoTracking.Click();
                    Thread.Sleep(5000);
                    Mobile.OrderTracking_Page.ReadyPhase.Click();
                    Thread.Sleep(5000);
                    LogStep(@"5. Change to dark mode");
                    driver.SwitchToChrome();
                    Mobile.Main_Page.Setting.Click();
                    Mobile.Setting_Page.turnOn_mode(1);
                    LogStep(@"6. Check Session manager in dark mode");
                    SessionManagerClose(driver, Resultpath, "dark");
                    //check executing the phase using qae\qaone3 in Edge.
                    edge.SwitchToEdge();
                    //logout page
                    Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Logout page dark mode.PNG");
                    Base_Assert.IsTrue(edge.GetUrl().Contains("logout"), "Logout page");
                    //relogin check phase state
                    Mobile.Logout_Page.login.Click();
                    Thread.Sleep(5000);
                    Mobile_Fuction.login(UserName.qaone3, PassWord.qaone3);
                    //switched to PFC page/order tracking page
                    Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Phase is ready dark mode.PNG");
                    Base_Assert.IsTrue(edge.GetUrl().Contains("tracking"), "in tracking page");
                    Base_Assert.IsTrue(Mobile.SessionManager_Page.TableRows[0].FindElement(By.TagName("mat-icon")).GetAttribute("data-mat-icon-name").Contains("phase_state_enabled"), "Phase is ready");
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
            finally
            {
                //set config in flag
                Base_Function.EditConfigKey(Configpath, ConfigKey2);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                //restart tomcat
                Base_Test.KillProcess("tomcat10");
                Thread.Sleep(30000);
                Base_Function.ResartServices(ServiceName.Tomcat);
                //wait for tomcat start completely.
                Thread.Sleep(120000);
            }
        }
        //private void SessionManagerClose(Selenium_Driver driver, string Resultpath, string mode = "")
        //{
        //    //go to Session manager
        //    Mobile.Main_Page.ManageModule.Click();
        //    Thread.Sleep(2000);
        //    Mobile.SessionManager_Page.Search.SendKeys(UserName.qaone3);
        //    Thread.Sleep(2000);
        //    int Pcount = Mobile.SessionManager_Page.TableRows.Count;
        //    Mobile.SessionManager_Page.CloseSession.Click();
        //    Thread.Sleep(2000);
        //    Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + " " + mode + " " + "end the user session dialog.PNG");
        //    //click no
        //    Mobile.SessionManager_Page.Dialog_No.Click();
        //    Thread.Sleep(2000);
        //    Base_Assert.IsFalse(driver.is_element_exist("//mat-dialog-container"),"Dialog closed");
        //    //get number
        //    int Mcount = Mobile.SessionManager_Page.TableRows.Count;
        //    //check data count
        //    Base_Assert.IsTrue(Pcount == Mcount, "No session cancel");
        //    //click yes
        //    Mobile.SessionManager_Page.CloseSession.Click();
        //    Thread.Sleep(2000);
        //    Mobile.SessionManager_Page.Dialog_Yes.Click();
        //    Thread.Sleep(2000);
        //    Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + " " + mode + " " + "end the user session.PNG");
        //}

    }
}