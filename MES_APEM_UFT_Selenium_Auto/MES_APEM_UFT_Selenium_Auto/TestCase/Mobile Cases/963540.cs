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

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(963540)]
        [Title("UC822659_APEM Mobile: Check events display when the number of events is more than 200")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_963540()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            
            Application.LaunchMocAndLogin();
            LogStep(@"1. import bpl");//import bpl
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("EVENTLOG510").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("EVENTLOG510.zip");
            }
            LogStep(@"2. create event log");
            //delete event log
            MOC_Fuction.DeleteEventLog();
            //create event log
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            APEM.MocmainWindow.BPLListInternalFrame.Refresh_Button.ClickSignle();
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("EVENTLOG510").Click();
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
            //need click twice
            APEM.DesignEditorWindow.ExecuteMainInternalFrame.OKButton.Click();
            APEM.ExecutionFinishedDialog.OKButton.Click();
            APEM.DesignEditorWindow.Close();
            APEM.CloseDialog.YesButton.Click();
            Thread.Sleep(3000);

            LogStep(@"3. login mobile");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            LogStep(@"4. check eventlog data 200");
            //off dark or consolidated mode if on
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOff_mode(1);
            Mobile.Setting_Page.turnOff_mode(2);
            Thread.Sleep(2000);
            //go to Event
            Mobile.Main_Page.Event.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Event log data 200.PNG");
            //get event number
            int Mcount = Mobile.EventLog_Page.EventLogTableRows.Count;
            //check Event log displays 200
            Base_Assert.AreEqual(Mcount, 200, "Event log displays 200.");
            //get 200 data text
            List<string> col = new List<string> { "Description"};
            List<string> DescriptionText200 = new List<string> { };
            int no = 0;
            int i = 0;
            foreach (IWebElement head in Mobile.EventLog_Page.EventLogTableHeads)
            {
                if (col.Contains(head.Text))
                {
                    no = i;
                    break;
                }
                i++;
            }
            var Descriptions = driver.FindElements($"//table/tbody/tr/td[{no}]");
            foreach (var des in Descriptions)
            {
                DescriptionText200.Add(des.Text);
            }
            LogStep(@"5. check eventlog data 400");
            driver.execute_script("document.getElementsByClassName('table-content scroll-bar full show-navigation desktop-mode')[0].scrollTop = 100000");
            Thread.Sleep(5000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Event log data 400.PNG");
            //get event number
            Mcount = Mobile.EventLog_Page.EventLogTableRows.Count;
            //check Event log displays 400
            Base_Assert.AreEqual(Mcount, 400, "Event log displays 400.");
            //get 400 data, original 200 results are existed.
            List<string> DescriptionText400 = new List<string> { };
            Descriptions = driver.FindElements($"//table/tbody/tr/td[{no}]");
            foreach (var des in Descriptions)
            {
                DescriptionText400.Add(des.Text);
            }
            bool exit = DescriptionText200.TrueForAll(item => DescriptionText400.Contains(item));
            Base_Assert.IsTrue(exit, "original 200 results are existed");
            LogStep(@"5. check eventlog data 510");
            driver.execute_script("document.getElementsByClassName('table-content scroll-bar full show-navigation desktop-mode')[0].scrollTop = 100000");
            Thread.Sleep(5000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Event log data 510.PNG");
            //get event number
            Mcount = Mobile.EventLog_Page.EventLogTableRows.Count;
            //check Event log displays 510
            Base_Assert.AreEqual(Mcount, 510, "Event log displays 510.");
            //get 510 data, original 400 results are existed.
            List<string> DescriptionText510 = new List<string> { };
            Descriptions = driver.FindElements($"//table/tbody/tr/td[{no}]");
            foreach (var des in Descriptions)
            {
                DescriptionText510.Add(des.Text);
            }
            exit = DescriptionText400.TrueForAll(item => DescriptionText510.Contains(item));
            Base_Assert.IsTrue(exit, "original 400 results are existed");
            //back to top
            driver.execute_script("document.getElementsByClassName('table-content scroll-bar full show-navigation desktop-mode')[0].scrollTop = 0");
            Thread.Sleep(5000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Event log data back to TOP.PNG");
            //get event number
            Mcount = Mobile.EventLog_Page.EventLogTableRows.Count;
            //check Event log displays 510
            Base_Assert.AreEqual(Mcount, 510, "Event log displays 510 after go to top.");
            //get 510 data, original 510 results are existed.
            List<string> DescriptionText510N = new List<string> { };
            Descriptions = driver.FindElements($"//table/tbody/tr/td[{no}]");
            foreach (var des in Descriptions)
            {
                DescriptionText510N.Add(des.Text);
            }
            exit = DescriptionText510N.Count == DescriptionText510.Count && DescriptionText510N.TrueForAll(item => DescriptionText510.Contains(item));
            Base_Assert.IsTrue(exit, "original 510 results are existed");


            //restore data
            driver.Close();

            MOC_Fuction.DeleteEventLog();
            APEM.ExitApplication();
        }

    }
}