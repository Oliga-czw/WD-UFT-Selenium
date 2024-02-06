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
    public partial class APEM_TestCase
    {
        [TestCaseID(957475)]
        [Title("UC822659_APEM Mobile: Sort function of Event log page in APEM Mobile")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_957475()
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
            LogStep(@"2. create event log");
            //delete event log
            MOC_Fuction.DeleteEventLog();
            //create event log
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
            //create second data after 30s
            Thread.Sleep(30000);
            APEM.DesignEditorWindow.ExecuteMainInternalFrame.LogEventAutoButton.ClickSignle();
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
            LogStep(@"4. check eventlog data sort");
            //off dark or consolidated mode if on
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOff_mode(1);
            Mobile.Setting_Page.turnOff_mode(2);
            Thread.Sleep(2000);
            EventLogSort(driver, Resultpath);
            LogStep(@"5. Change to dark");
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOn_mode(1);
            LogStep(@"6. Check event log sort in dark mode");
            EventLogSort(driver, Resultpath, "dark");
            LogStep(@"7. Change to consolidated");
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOn_mode(2);
            LogStep(@"8. Check event log sort inconsolidated");
            EventLogSort(driver, Resultpath, "consolidated");

            //restore data
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOff_mode(1);
            Mobile.Setting_Page.turnOff_mode(2);
            driver.Close();

            MOC_Fuction.DeleteEventLog();
            APEM.ExitApplication();
        }


        public void EventLogSort(Selenium_Driver driver, string Resultpath,string mode="")
        {
            //go to Event
            Mobile.Main_Page.Event.Click();
            Thread.Sleep(2000);
            int l = 0;
            List<string> head_sort = new List<string> { "Code", "Date", "Description" };
            while (l < Mobile.EventLog_Page.EventLogTableHeads.Count)
            {
                //select column to check
                if (head_sort.Contains(Mobile.EventLog_Page.EventLogTableHeads[l].Text))
                {
                    //asc, desc and restore data
                    for (int i = 0; i < 3; i++)
                    {
                        //sort data
                        driver.execute_script("arguments[0].click();", Mobile.EventLog_Page.EventLogTableHeads[l]);
                        string sort = Mobile.EventLog_Page.EventLogTableHeads[l].GetAttribute("aria-sort");
                        List<string> list = new List<string> { };
                        List<string> list_sort = new List<string> { };
                        foreach (var tr in Mobile.EventLog_Page.EventLogTableRows)
                        {
                            var td = tr.FindElements(By.TagName("td"))[l];
                            list.Add(td.Text);
                        }
                        //processing the string to date of Date and End Date
                        if (Mobile.EventLog_Page.EventLogTableHeads[l].Text.Contains("Date"))
                        {

                        }
                        switch (sort)
                        {
                            case "ascending":
                                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + Mobile.EventLog_Page.EventLogTableHeads[l].Text +" "+ mode + " asc.PNG");
                                list_sort = list.OrderByDescending(x => x).ToList();
                                list_sort.Reverse();
                                Base_Assert.IsTrue(list_sort.SequenceEqual(list));
                                break;
                            case "descending":
                                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + Mobile.EventLog_Page.EventLogTableHeads[l].Text + " " + mode + " desc.PNG");
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