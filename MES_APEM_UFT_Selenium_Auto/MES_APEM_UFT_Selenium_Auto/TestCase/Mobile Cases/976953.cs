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
        [TestCaseID(976953)]
        [Title("UC967898_Search function should work on Session Manager page if users have administrator role in AFW security Manager.")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        //defect 1338983
        [TestMethod]
        public void VSTS_976953()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName = "Order976953";
            string RPLName = "RPL976953";

            Application.LaunchMocAndLogin();
            LogStep(@"1. import bpl");//import bpl
            //check bpl exit
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE976953.zip");
            }
            LogStep(@"2. create order");
            MOC_Fuction.PlanFromRPL(RPLName, OrderName);
            APEM.ExitApplication();
            LogStep(@"3. login mobile");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(OrderName);
            Thread.Sleep(5000);
            //go to tracking page,execute the phase
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(5000);
            for (int i = 0; i < 3; i++)
            {
                Mobile.OrderTracking_Page.ReadyPhase.Click();
                Thread.Sleep(5000);
                Mobile.Main_Page.Tracking.Click();
                Thread.Sleep(5000);
            }
            LogStep(@"4. check Search function");
            //off dark or consolidated mode if on
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOff_mode(1);
            Mobile.Setting_Page.turnOff_mode(2);
            Thread.Sleep(2000);
            try
            {
                SessionManagerSearch(driver, Resultpath);
            LogStep(@"5. Change to dark mode");
                Mobile.Main_Page.Setting.Click();
                Mobile.Setting_Page.turnOn_mode(1);
                LogStep(@"6. Check Session manager in dark mode");
                SessionManagerSearch(driver, Resultpath, "dark");
            }
            finally
            {
                //restore data
                Mobile.Main_Page.Setting.Click();
                Mobile.Setting_Page.turnOff_mode(1);
                Mobile.Setting_Page.turnOff_mode(2);
                //cancel order
                Mobile.Main_Page.ManageModule.Click();
                Thread.Sleep(3000);
                Mobile.SessionManager_Page.CloseSession.Click();
                Thread.Sleep(3000);
                Mobile.SessionManager_Page.Dialog_Yes.Click();
                Thread.Sleep(3000);
                driver.Close();    
                
            }
        }
        private void SessionManagerSearch(Selenium_Driver driver, string Resultpath, string mode = "")
        {
            int Ocount = 2;//phase count
            string SearchWord = "TEST";
            //go to Session manager
            Mobile.Main_Page.ManageModule.Click();
            Thread.Sleep(2000);
            Mobile.SessionManager_Page.Search.SendKeys(SearchWord);
            Thread.Sleep(5000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath +" " + mode + " "+ "Search.PNG");

            LogStep(@".1 check Manage module data");
            //get number
            int Mcount = Mobile.SessionManager_Page.TableRows.Count;
            //check data count
            Console.WriteLine(Mcount);
            Base_Assert.IsTrue(Ocount==Mcount, "All data displays.");//defect 1338983,now need close all session manually
            //check result
            int l = 0;
            foreach (var head in Mobile.SessionManager_Page.TableHeads)
            {
                //check result is right
                if (head.Text == "Order / Executing Phase")
                {
                    foreach (var tr in Mobile.SessionManager_Page.TableRows)
                    {
                        var td = tr.FindElements(By.TagName("td"))[l];
                        Console.WriteLine(td.Text);
                        Base_Assert.IsTrue(td.Text.Contains(SearchWord));
                    }
                }
                l++;
            }
            //check search word is bold and yellow
            var strongs = driver.FindElements("//strong");
            foreach (var strong in strongs)
            {
                string color = strong.GetAttribute("style");
                Console.WriteLine(color);
                if (mode == "")
                {
                    Base_Assert.IsTrue(color.Contains("yellow"), "strong color");
                }
                //dark mode
                else
                {
                    Base_Assert.IsTrue(color.Contains("rgb(255, 0, 144)"), "strong color");
                }
                Base_Assert.IsTrue(strong.Text.Equals(SearchWord, StringComparison.OrdinalIgnoreCase), " strong text");
            }

        }
        
    }
}