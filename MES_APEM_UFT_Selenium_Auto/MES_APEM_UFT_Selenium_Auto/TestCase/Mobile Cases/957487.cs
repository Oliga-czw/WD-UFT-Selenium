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
using Keys = OpenQA.Selenium.Keys;
using System.Globalization;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(957487)]
        [Title("UC822659_APEM Mobile: Search function of Event log page in APEM Mobile")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_957487()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";

            Application.LaunchMocAndLogin();
            LogStep(@"1. import bpl");//import bpl
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("EVENTLOG").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("EVENTLOGFUFT.zip");
            }
            LogStep(@"2. create event log");//create event log
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            APEM.MocmainWindow.BPLListInternalFrame.Refresh_Button.ClickSignle();
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("EVENTLOG").Click();
            APEM.MocmainWindow.BPLListInternalFrame.LoadDesigner_Button.ClickSignle();
            APEM.DesignEditorWindow.ExecuteButton.ClickSignle();
            //add reason
            if (APEM.AuditReasonDialog.IsExist())
            {
                APEM.AuditReasonDialog.Reason.SendKeys("Execute");
                APEM.AuditReasonDialog.OK.Click();
            }
            APEM.DesignEditorWindow.ExecuteMainInternalFrame.LogEventAutoButton.Click();
            if (APEM.DesignEditorWindow.MessageInterFrame.message.AttachedText == "Sucess")
            {
                //log
                APEM.DesignEditorWindow.MessageInterFrame.OKButton.ClickSignle();
            }
            //need click OK twice
            APEM.DesignEditorWindow.ExecuteMainInternalFrame.OKButton.Click();
            APEM.ExecutionFinishedDialog.OKButton.Click();
            APEM.DesignEditorWindow.Close();
            APEM.CloseDialog.YesButton.Click();
            Thread.Sleep(3000);

            LogStep(@"3. login mobile");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            LogStep(@"4. check eventlog data search");
            //off dark or consolidated mode if on
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOff_mode(1);
            Mobile.Setting_Page.turnOff_mode(2);
            Thread.Sleep(2000);
            EventLogSearch(driver, Resultpath);
            LogStep(@"5. Change to dark");
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOn_mode(1);
            LogStep(@"6. Check event log sort in dark mode");
            EventLogSearch(driver, Resultpath,"dark");
            LogStep(@"7. Change to consolidated");
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOn_mode(2);
            LogStep(@"8. Check event log sort in consolidated");
            EventLogSearch(driver, Resultpath, "consolidated");

            //restore data
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOff_mode(1);
            Mobile.Setting_Page.turnOff_mode(2);
            driver.Close();

            APEM.MocmainWindow.Tools.EventLog.Select();
            APEM.RowSelectionDialog.YesButton.Click();
            APEM.MocmainWindow.EventLogListInterFrame.Delete.ClickSignle();
            APEM.DeleteEventLogDialog.YesButton.Click();
            APEM.ExitApplication();
        }


        public void EventLogSearch(Selenium_Driver driver, string Resultpath,string mode = "")
        {
            string searchWord = "Error";
            //go to Event
            Mobile.Main_Page.Event.Click();
            Thread.Sleep(2000);
            Mobile.EventLog_Page.Search.SendKeys(searchWord);
            Thread.Sleep(2000);
            //check result
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath +" "+ mode + " search.PNG");
            int l = 0;
            List<string> head_name = new List<string> { "Type" };
            foreach (var head in Mobile.EventLog_Page.EventLogTableHeads)
            {
                //check result is right
                if (head_name.Contains(head.Text))
                {
                    foreach (var tr in Mobile.EventLog_Page.EventLogTableRows)
                    {
                        var td = tr.FindElements(By.TagName("td"))[l];
                        Assert.IsTrue(td.Text.Equals(searchWord, StringComparison.OrdinalIgnoreCase));
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
                Base_Assert.IsTrue(strong.Text.Equals(searchWord, StringComparison.OrdinalIgnoreCase), " strong text");
            }
            //check filter
            Mobile.EventLog_Page.Search.Clear();
            Mobile.EventLog_Page.Search.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            List<string> filter_name = new List<string> { "Date","Type" };
            List<string> delete_col = new List<string> { "Error", "Warning" };
            string type = "Fatal";
            //set filter
            foreach (var head in Mobile.EventLog_Page.EventLogTableHeads)
            {
                if (filter_name.Contains(head.Text))
                {
                    var filter = head.FindElement(By.TagName("mat-icon"));
                    driver.execute_script("arguments[0].click();", filter);
                    if (head.Text == "Date")
                    {
                        var startDate = driver.FindElement("//input[@placeholder='Start date']");
                        var endDate = driver.FindElement("//input[@placeholder='End date']");

                        startDate.Click();
                        startDate.SendKeys(DateTime.Today.AddDays(-2).ToString("MM/dd/yyyy"));
                        endDate.Click();
                        endDate.SendKeys(DateTime.Today.ToString("MM/dd/yyyy"));
                        endDate.SendKeys(Keys.Enter);
                    }
                    else
                    {
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
                    }
                    Thread.Sleep(2000);

                }
            }
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + " " + mode + " filter.PNG");
            //check result
            l = 0;
            foreach (var head in Mobile.EventLog_Page.EventLogTableHeads)
            {
                //check result is right
                if (head.Text== "Type")
                {
                    foreach (var tr in Mobile.EventLog_Page.EventLogTableRows)
                    {
                        var td = tr.FindElements(By.TagName("td"))[l];
                        Assert.IsTrue(td.Text.Equals(type, StringComparison.OrdinalIgnoreCase));
                    }
                }
                if(head.Text == "Date")
                {
                    foreach (var tr in Mobile.EventLog_Page.EventLogTableRows)
                    {
                        var td = tr.FindElements(By.TagName("td"))[l];
                        DateTime dateTime = DateTime.ParseExact(td.Text, "MM/dd/yy, h:mm:ss tt", CultureInfo.InvariantCulture);
                        Assert.IsTrue(dateTime.ToString("MM/dd/yyyy") == DateTime.Today.ToString("MM/dd/yyyy"));
                    }
                }
                l++;
            }
            //restore data
            foreach (var head in Mobile.EventLog_Page.EventLogTableHeads)
            {
                if (filter_name.Contains(head.Text))
                {
                    var filter = head.FindElement(By.TagName("mat-icon"));
                    driver.execute_script("arguments[0].click();", filter);
                    if (head.Text == "Date")
                    {
                        var startDate = driver.FindElement("//input[@placeholder='Start date']");
                        var endDate = driver.FindElement("//input[@placeholder='End date']");

                        startDate.Click();
                        Thread.Sleep(1000);
                        startDate.SendKeys(DateTime.Today.AddDays(-2).ToString("MM/dd/yyyy"));
                        endDate.Click();
                        Thread.Sleep(1000);
                        endDate.SendKeys(DateTime.Today.ToString("MM/dd/yyyy"));
                        Thread.Sleep(1000);
                        endDate.SendKeys(Keys.Enter);
                    }
                    else
                    {
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
                    }
                    Thread.Sleep(2000);

                }
            }

        }
    }
}