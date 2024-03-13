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
        [TestCaseID(976946)]
        [Title("UC967898_Users have Administrator role in AFW security can vie session manager page")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(1200000)]
        //defect 1338983
        [TestMethod]
        public void VSTS_976946()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName1 = "Order976946_1";
            string OrderName2 = "Order976946_2";
            string RPLName = "RPL976946";

            Application.LaunchMocAndLogin();
            LogStep(@"1. import bpl");//import bpl
            //check bpl exit
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE976946.zip");
            }
            LogStep(@"2. create order");
            MOC_Fuction.PlanFromRPL(RPLName, OrderName1);
            MOC_Fuction.PlanFromRPL(RPLName, OrderName2);
            APEM.ExitApplication();
            LogStep(@"3. login mobile");
            //execute order with qaone1 and qaone2
            Selenium_Driver edge = new Selenium_Driver(Browser.edge);
            Mobile_Fuction.gotoApemMobile(edge);
            Mobile.Login_Page.username.SendKeys(UserName.qaone2);
            Mobile.Login_Page.password.SendKeys(PassWord.qaone2);
            Mobile.Login_Page.login.Click();
            Thread.Sleep(5000);
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(OrderName2);
            Thread.Sleep(5000);
            //go to tracking page,execute the phase
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(5000);
            for(int i = 0; i < 7; i++)
            {
                Mobile.OrderTracking_Page.ReadyPhase.Click();
                Thread.Sleep(5000);
                Mobile.Main_Page.Tracking.Click();
                Thread.Sleep(5000);
            }
            edge.Minimize();

            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(OrderName1);
            Thread.Sleep(5000);
            //go to tracking page,execute the phase
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(5000);
            for (int i = 0; i < 7; i++)
            {
                Mobile.OrderTracking_Page.ReadyPhase.Click();
                Thread.Sleep(5000);
                Mobile.Main_Page.Tracking.Click();
                Thread.Sleep(5000);
            }
            LogStep(@"4. check Manage module icon");
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Manage module icon.PNG");
            Base_Assert.IsTrue(driver.is_element_exist(Mobile.Main_Page.ManageModule), "Manage module icon");
            driver.Wait();
            LogStep(@"5. check Manage module columns");
            //off dark or consolidated mode if on
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOff_mode(1);
            Mobile.Setting_Page.turnOff_mode(2);
            Thread.Sleep(2000);
            try
            {
                SessionManagerData(driver, Resultpath);
            LogStep(@"6. Change to dark mode");
                Mobile.Main_Page.Setting.Click();
                Mobile.Setting_Page.turnOn_mode(1);
                LogStep(@"7. Check Session manager in dark mode");
                SessionManagerData(driver, Resultpath, "dark");
                LogStep(@"8. Change to consolidated");
                Mobile.Main_Page.Setting.Click();
                Mobile.Setting_Page.turnOn_mode(2);
                LogStep(@"9. Check Session manager inconsolidated");
                SessionManagerData(driver, Resultpath, "consolidated");
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
                edge.SwitchToEdge();
                edge.Maxsize();
                Mobile.Main_Page.ManageModule.Click();
                Thread.Sleep(3000);
                Mobile.SessionManager_Page.CloseSession.Click();
                Thread.Sleep(3000);
                Mobile.SessionManager_Page.Dialog_Yes.Click();
                Thread.Sleep(3000);
                edge.Close();
                
            }
        }
        private void SessionManagerData(Selenium_Driver driver, string Resultpath, string mode = "")
        {
            int Ocount = 14;//phase count
            //go to Session manager
            Mobile.Main_Page.ManageModule.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath +" " + mode + " "+ "Manage module columns.PNG");
            List<string> Expect = new List<string> { "User", "Order / Executing Phase", "Workstation", "Operation" };
            List<string> headname = new List<string> { };
            foreach (IWebElement head in Mobile.SessionManager_Page.TableHeads)
            {
                headname.Add(head.Text);
            }
            Base_Assert.IsTrue(Expect.SequenceEqual(headname), "Manage module columns");
            LogStep(@".1 check Manage module data");
            //get event number
            int Mcount = Mobile.SessionManager_Page.TableRows.Count;
            //check data if same
            Console.WriteLine(Mcount);
            Base_Assert.IsTrue(Ocount==Mcount, "All data displays.");//defect 1338983,now need close all session manually
            //check User ascending ordered
            List<string> col = new List<string> { "User" };
            List<string> UserNameList = new List<string> { };
            int no = 0;
            int i = 1;
            foreach (IWebElement head in Mobile.OrderProcess_Page.OrderPhaseTableHeads)
            {
                if (col.Contains(head.Text))
                {
                    no = i;
                    break;
                }
                i++;
            }
            var UserName = driver.FindElements($"//table/tbody/tr/td[{no}]");
            foreach (var des in UserName)
            {
                UserNameList.Add(des.Text);
            }
            //sort
            List<string> UserNameListSort = UserNameList.ToList();
            UserNameListSort.Sort();
            bool same = UserNameList.SequenceEqual(UserNameListSort);
            Base_Assert.IsTrue(same, "Session manager ascending by User name by default.");

            LogStep(@".2 Scroll the scrollbar.");
            driver.execute_script("document.getElementsByClassName('table-content scroll-bar full show-navigation desktop-mode')[0].scrollTop = 100000");
            Thread.Sleep(5000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath  +" " + mode + " " + "Scroll down scrollbar.PNG");
            //check scroll bar at bottom
            var ScrollHeightDown = driver.execute_script_return("return document.getElementsByClassName('table-content scroll-bar full show-navigation desktop-mode')[0].scrollTop");
            Base_Assert.IsTrue(int.Parse(ScrollHeightDown.ToString()) > 0, "Scroll bar down.");
            LogStep(@".3 Click Refresh icon");
            Mobile.SessionManager_Page.RefreshButton.Click();
            Thread.Sleep(5000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + " " + mode + " " + "Refresh.PNG");
            //check scroll bar at top
            var ScrollHeightTop = driver.execute_script_return("return document.getElementsByClassName('table-content scroll-bar full show-navigation desktop-mode')[0].scrollTop");
            Base_Assert.AreEqual(ScrollHeightTop.ToString(), "0", "Scroll bar at top.");
            //check data ascending ordered
            List<string> UserNameListRefresh = new List<string> { };
            no = 0;
            i = 1;
            foreach (IWebElement head in Mobile.OrderProcess_Page.OrderPhaseTableHeads)
            {
                if (col.Contains(head.Text))
                {
                    no = i;
                    break;
                }
                i++;
            }
            UserName = driver.FindElements($"//table/tbody/tr/td[{no}]");
            foreach (var des in UserName)
            {
                UserNameListRefresh.Add(des.Text);
            }
            //sort
            List<string> UserNameListRefreshSort = UserNameListRefresh.ToList();
            UserNameListRefreshSort.Sort();
            same = UserNameListRefresh.SequenceEqual(UserNameListRefreshSort);
            Base_Assert.IsTrue(same, "Refresh Session manager ascending by User name by default.");
            LogStep(@".4 filter by columns");
            List<string> filter_name = new List<string> { "User", "Order / Executing Phase" };
            List<string> delete_col = new List<string> { @"qae\qaone1", "ORDER976946_2 / PHASE17", "ORDER976946_2 / PHASE29" };
            string user = @"qae\qaone2";
            //set filter
            foreach (var head in Mobile.SessionManager_Page.TableHeads)
            {
                if (filter_name.Contains(head.Text))
                {
                    var filter = head.FindElement(By.TagName("mat-icon"));
                    driver.execute_script("arguments[0].click();", filter);
                    var checkboxs = driver.FindElements("//div[@class='mat-list-text']");

                    foreach (IWebElement checkbox in checkboxs)
                    {
                        if (delete_col.Contains(checkbox.Text.Trim()))
                        {
                            checkbox.Click();
                            Thread.Sleep(1000);
                        }
                        Thread.Sleep(1000);
                    }
                    driver.action_move_to_element_click(filter);
                    Thread.Sleep(2000);

                }
            }
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + " " + mode +  " filter.PNG");
            //check result
            int l = 0;
            foreach (var head in Mobile.SessionManager_Page.TableHeads)
            {
                //check result is right
                if (head.Text == "User")
                {
                    foreach (var tr in Mobile.SessionManager_Page.TableRows)
                    {
                        var td = tr.FindElements(By.TagName("td"))[l];
                        Console.WriteLine(td.Text);
                        Base_Assert.IsTrue(td.Text.Equals(user, StringComparison.OrdinalIgnoreCase));

                    }
                }
                l++;
            }
            Base_Assert.IsTrue(Mobile.SessionManager_Page.TableRows.Count == 5, "filter row count");
            //restore data
            foreach (var head in Mobile.SessionManager_Page.TableHeads)
            {
                if (filter_name.Contains(head.Text))
                {
                    var filter = head.FindElement(By.TagName("mat-icon"));
                    driver.execute_script("arguments[0].click();", filter);
                    var checkboxs = driver.FindElements("//div[@class='mat-list-text']");

                    foreach (IWebElement checkbox in checkboxs)
                    {
                        if (delete_col.Contains(checkbox.Text.Trim()))
                        {
                            checkbox.Click();
                            Thread.Sleep(1000);
                        }
                        Thread.Sleep(1000);
                    }
                    driver.action_move_to_element_click(filter);
                    Thread.Sleep(2000);
                }
            }
        }
        
    }
}