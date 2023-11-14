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
        [TestCaseID(957446)]
        [Title("UC822659_APEM Mobile: Check event log page and its display")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Created)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_957446()
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
            APEM.BPLDesignEditorWindow.ExecuteButton.ClickSignle();
            //add reason
            if (APEM.AuditReasonDialog.IsExist())
            {
                APEM.AuditReasonDialog.Reason.SendKeys("Execute");
                APEM.AuditReasonDialog.OK.Click();
            }
            APEM.BPLDesignEditorWindow.BPLExecutionInterFrame.LogEventAutoButton.Click();
            if (APEM.BPLDesignEditorWindow.MessageInterFrame.message.AttachedText == "Sucess")
            {
                //log
                APEM.BPLDesignEditorWindow.MessageInterFrame.OKButton.ClickSignle();
            }
            //need click twice
            APEM.BPLDesignEditorWindow.BPLExecutionInterFrame.OKButton.Click();
            APEM.ExecutionFinishedDialog.OKButton.Click();
            APEM.BPLDesignEditorWindow.Close();
            APEM.CloseDialog.YesButton.Click();
            Thread.Sleep(3000);

            LogStep(@"3. login mobile");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            LogStep(@"4. check eventlog icon");//check eventlog icon
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Event log icon.PNG");
            try
            {
                var element = Mobile.Main_Page.Event;
                Base_logger.Message("Element exit!");
                Console.WriteLine("Element exit!");
            }
            catch (NoSuchElementException)
            {
                Base_logger.Message("Element not exit!");
                Console.WriteLine("Element not exit!");
            }
            LogStep(@"5. check eventlog data");
            //off dark or consolidated mode if on
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOff_mode(1);
            Mobile.Setting_Page.turnOff_mode(2);
            Thread.Sleep(2000);
            //go to Event
            Mobile.Main_Page.Event.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Event log data.PNG");
            //get event number
            string Mcount = Mobile.EventLog_Page.EventLogTableRows.Count.ToString();
            SqlHelper helper = new SqlHelper();
            string SQL = $"SELECT COUNT(*) FROM EBR_EVENT_LOG";
            List<List<string>> DBconut = helper.Execute(SQL);
            //check data if same
            Base_Assert.AreEqual(Mcount, DBconut[0][0], "All data displays.");
            LogStep(@"6. Select some columns");
            //select all
            Mobile.EventLog_Page.Sellect_all_col();
            List<string> delete_col = new List<string> { "Description", "Module" };
            Mobile.EventLog_Page.delete_col(delete_col);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Event log column.PNG");
            bool exsit = true;
            int no = 0;
            int i = 0;
            foreach (IWebElement head in Mobile.EventLog_Page.EventLogTableHeads)
            {
                if (delete_col.Contains(head.Text))
                {
                    exsit = false;
                }
                if (head.Text == "Detail")
                {
                    no = i;
                }
                i++;
            }
            Base_Assert.IsTrue(exsit, "Columns work well.");
            LogStep(@"7. move to detail and check tooltip");
            Thread.Sleep(2000);
            var detail = Mobile.EventLog_Page.EventLogTableRows[0].FindElements(By.TagName("td"))[no];
            Point point = new Point();
            point.Offset(detail.Location.X + 50, detail.Location.Y + 180);
            Mouse.Move(point);
            Thread.Sleep(5000);
            Base_Function.DesktopSnipping(Resultpath + "Event log tooltip.PNG");
            string title = detail.GetAttribute("title");
            string text = detail.Text;
            string cursor = detail.GetCssValue("cursor");
            Base_Assert.AreEqual(text, title, "tooltip text right.");
            Base_Assert.AreEqual("pointer", cursor, "tooltip work well.");
            LogStep(@"8. Change to dark mode");
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOn_mode(1);
            LogStep(@"9. Check event log in dark mode");
            //check eventlog icon
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Event log icon in dark mode.PNG");
            try
            {
                var element = Mobile.Main_Page.Event;
                Base_logger.Message("Element exit in dark mode!");
                Console.WriteLine("Element exit!");
            }
            catch (NoSuchElementException)
            {
                Base_logger.Message("Element exit in dark mode!");
                Console.WriteLine("Element not exit!");
            }
            //check eventlog data
            Mobile.Main_Page.Event.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Event log data in dark mode.PNG");
            //get event number
            string McountDark = Mobile.EventLog_Page.EventLogTableRows.Count.ToString();
            //check data if same
            Base_Assert.AreEqual(McountDark, DBconut[0][0], "All data displays in dark mdoe.");
            LogStep(@"6. Select some columns");
            //select all
            Mobile.EventLog_Page.Sellect_all_col();
            List<string> delete_col_dark = new List<string> { "Module", "Source" };
            Mobile.EventLog_Page.delete_col(delete_col_dark);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Event log column in dark mode.PNG");
            exsit = true;
            no = 0;
            i = 0;
            foreach (IWebElement head in Mobile.EventLog_Page.EventLogTableHeads)
            {

                if (delete_col_dark.Contains(head.Text))
                {
                    exsit = false;
                }
                if (head.Text == "Detail")
                {
                    no = i;
                }
                i++;
            }
            Base_Assert.IsTrue(exsit, "Columns work well in dark mdoe.");
            //move to detail and check tooltip");
            Thread.Sleep(2000);
            detail = Mobile.EventLog_Page.EventLogTableRows[0].FindElements(By.TagName("td"))[no];
            Point pointD = new Point();
            pointD.Offset(detail.Location.X + 50, detail.Location.Y + 180);
            Mouse.Move(pointD);
            Thread.Sleep(5000);
            Base_Function.DesktopSnipping(Resultpath + "Event log tooltip in dark mode.PNG");
            title = detail.GetAttribute("title");
            text = detail.Text;
            cursor = detail.GetCssValue("cursor");
            Base_Assert.AreEqual(text, title, "tooltip text right in dark mode.");
            Base_Assert.AreEqual("pointer", cursor, "tooltip work well in dark mode.");
            LogStep(@"10. Change to consolidated");
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOn_mode(2);
            LogStep(@"11. Check event log inconsolidated");
            //check eventlog icon
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Event log icon in consolidated.PNG");
            try
            {
                var element = Mobile.Main_Page.Event;
                Base_logger.Message("Element exit in consolidated!");
                Console.WriteLine("Element exit!");
            }
            catch (NoSuchElementException)
            {
                Base_logger.Message("Element exit in consolidated!");
                Console.WriteLine("Element not exit!");
            }
            //check eventlog data
            Mobile.Main_Page.Event.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Event log data in consolidated.PNG");
            //get event number
            string McountCons = Mobile.EventLog_Page.EventLogTableRows.Count.ToString();
            //check data if same
            Base_Assert.AreEqual(McountCons, DBconut[0][0], "All data displays in consolidated.");
            LogStep(@"12. Select some columns");
            //add fuction select all
            Mobile.EventLog_Page.Sellect_all_col();
            List<string> delete_col_cons = new List<string> { "Module", "Date" };
            Mobile.EventLog_Page.delete_col(delete_col_cons);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Event log column in consolidated.PNG");
            exsit = true;
            no = 0;
            i = 0;
            foreach (IWebElement head in Mobile.EventLog_Page.EventLogTableHeads)
            {

                if (delete_col_cons.Contains(head.Text))
                {
                    exsit = false;
                }
                if (head.Text == "Detail")
                {
                    no = i;
                }
                i++;
            }
            Base_Assert.IsTrue(exsit, "Columns work well in consolidated.");
            //move to detail and check tooltip");
            Thread.Sleep(2000);
            detail = Mobile.EventLog_Page.EventLogTableRows[0].FindElements(By.TagName("td"))[no];
            Point pointC = new Point();
            pointC.Offset(detail.Location.X + 50, detail.Location.Y + 180);
            Mouse.Move(pointC);
            Thread.Sleep(5000);
            Base_Function.DesktopSnipping(Resultpath + "Event log tooltip in consolidated.PNG");
            title = detail.GetAttribute("title");
            text = detail.Text;
            cursor = detail.GetCssValue("cursor");
            Base_Assert.AreEqual(text, title, "tooltip text right in consolidated.");
            Base_Assert.AreEqual("pointer", cursor, "tooltip work well in consolidated.");
            //restore data
            Mobile.EventLog_Page.Sellect_all_col();
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

    }
}