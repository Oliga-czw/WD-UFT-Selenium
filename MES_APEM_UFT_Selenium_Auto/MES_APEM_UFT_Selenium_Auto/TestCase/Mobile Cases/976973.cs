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
        [TestCaseID(976973)]
        [Title("UC967898_Sort function should work as expected if user have administrator role in AFW security Manager.")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]


        [TestMethod]
        public void VSTS_976973()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName1 = "Order976973_1";
            string OrderName2 = "Order976973_2";
            string RPLName = "RPL976973";

            Application.LaunchMocAndLogin();
            LogStep(@"1. import bpl");//import bpl
            //check bpl exit
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE976973.zip");
            }
            LogStep(@"2. create order");
            MOC_Fuction.PlanFromRPL(RPLName, OrderName1);
            MOC_Fuction.PlanFromRPL(RPLName, OrderName2);
            APEM.ExitApplication();
            LogStep(@"3. login mobile");
            //execute order with qaone1 and qaone2
            Selenium_Driver edge = new Selenium_Driver(Browser.edge);
            Mobile_Fuction.gotoApemMobile(edge);
            Mobile_Fuction.login(UserName.qaone2,PassWord.qaone2);
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(OrderName2);
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
            edge.Minimize();

            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            Mobile.OrderProcess_Page.OrderSearch.SendKeys(OrderName1);
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
            LogStep(@"4. check Sort function");
            //off dark or consolidated mode if on
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOff_mode(1);
            Mobile.Setting_Page.turnOff_mode(2);
            Thread.Sleep(2000);
            try
            {
                SessionManagerSort(driver, Resultpath);
                LogStep(@"5. Change to dark mode");
                Mobile.Main_Page.Setting.Click();
                Mobile.Setting_Page.turnOn_mode(1);
                LogStep(@"6. Check Session manager in dark mode");
                SessionManagerSort(driver, Resultpath, "dark");
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
                //cancel edge
                Mobile.SessionManager_Page.CloseSession.Click();
                Thread.Sleep(3000);
                Mobile.SessionManager_Page.Dialog_Yes.Click();
                Thread.Sleep(3000);
                //cancel chrome and logout
                Mobile_Fuction.CancelAllExecutingPhase();
                edge.SwitchToEdge();
                edge.Maxsize();
                edge.Close();

            }
        }
        private void SessionManagerSort(Selenium_Driver driver, string Resultpath, string mode = "")
        {

            //go to Session manager
            Mobile.Main_Page.ManageModule.Click();
            Thread.Sleep(2000);

            int l = 0;
            List<string> head_sort = new List<string> { "User", "Order / Executing Phase", "Workstation"};
            while (l < Mobile.SessionManager_Page.TableHeads.Count)
            {
                //select column to check
                if (head_sort.Contains(Mobile.SessionManager_Page.TableHeads[l].Text))
                {
                    //asc, desc and restore data
                    for (int i = 0; i < 3; i++)
                    {
                        //sort data
                        driver.execute_script("arguments[0].click();", Mobile.SessionManager_Page.TableHeads[l]);
                        string sort = Mobile.SessionManager_Page.TableHeads[l].GetAttribute("aria-sort");
                        List<string> list = new List<string> { };
                        List<string> list_sort = new List<string> { };
                        foreach (var tr in Mobile.SessionManager_Page.TableRows)
                        {
                            var td = tr.FindElements(By.TagName("td"))[l];
                            list.Add(td.Text);
                        }
                        switch (sort)
                        {
                            case "ascending":
                                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + Mobile.SessionManager_Page.TableHeads[l].Text.Replace('/', ' ').Replace('\\', ' ') + " " + mode + " asc.PNG");
                                list_sort = list.OrderByDescending(x => x).ToList();
                                list_sort.Reverse();
                                Base_Assert.IsTrue(list_sort.SequenceEqual(list));
                                break;
                            case "descending":
                                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + Mobile.SessionManager_Page.TableHeads[l].Text.Replace('/', ' ').Replace('\\', ' ') + " " + mode + " desc.PNG");
                                list_sort = list.OrderByDescending(x => x).ToList();
                                Base_Assert.IsTrue(list_sort.SequenceEqual(list));
                                break;
                        }
                    }
                }
                l++;
            }


        }
        
    }
}