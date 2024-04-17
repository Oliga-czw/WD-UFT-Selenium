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
        [TestCaseID(771207)]
        [Title("Inspired by custom_'Refresh_Screen()' can refresh the time(now()) on APEM web.")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_771207()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string Configpath = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string ConfigKey = "EXEC_FRAME_HEADER = 0";
            string ConfigKey1 = "EXEC_FRAME_HEADER = 1";
            string RPLname = "WEB_TEST";
            string Ordername = "ORDER771207";
            try
            {
                LogStep(@"Set key in config file");
                Base_Function.EditConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                Application.LaunchMocAndLogin();
                if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Existing)
                {
                    MOC_TemplatesFunction.Importtemplates("TEST_THREAD_REFRESHABLE - LABEL_WEB_CLIENT.zip");

                }
                
                MOC_Fuction.PlanFromRPL(RPLname, Ordername); 
                LogStep(@"Execute in moc");
                APEM.MocmainWindow.WorkstationBP.ClickSignle();
                MOC_Fuction.CheckRowSelection();
                Thread.Sleep(3000);
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(Ordername);
                APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
                APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
                Thread.Sleep(15000);
                APEM.PhaseExecWindow.GetSnapshot(Resultpath + "Executing_MOC.PNG");
                var time1 = APEM.PhaseExecWindow.ExecutionInternalFrame.current_time.AttachedText;
                Thread.Sleep(5000);
                var time2 = APEM.PhaseExecWindow.ExecutionInternalFrame.current_time.AttachedText;
                DateTime current_time1 = DateTime.Parse(time1);
                DateTime current_time2 = DateTime.Parse(time2);
                TimeSpan timeDifference = current_time2.Subtract(current_time1);
                int seconds = (int)timeDifference.TotalSeconds;
                Console.WriteLine(seconds);
                Base_Assert.AreEqual(seconds, 5);
                APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
                Thread.Sleep(1000);
                APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
                Thread.Sleep(2000);
                MOC_Fuction.MocClose();
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Mobile_Fuction.gotoApemMobile(driver);
                driver.Wait();
                Mobile_Fuction.login();
                driver.Wait();
                Mobile.OrderProcess_Page.OrderSearch.SendKeys(Ordername);
                Thread.Sleep(1000);
                Mobile.OrderProcess_Page.GotoTracking.Click();
                Thread.Sleep(1000);
                Mobile.OrderTracking_Page.ExecutionButton.Click();
                Thread.Sleep(10000);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "MobileExecute.PNG");
                Thread.Sleep(5000);
                var time_mobile1 = Mobile.OrderExecution_Page.time_label.GetAttribute("innerText");
                Thread.Sleep(5000);
                var time_mobile2 = Mobile.OrderExecution_Page.time_label.Text();
                DateTime current_Mobiletime1 = DateTime.Parse(time_mobile1);
                DateTime current_Mobiletime2 = DateTime.Parse(time_mobile2);
                TimeSpan MobiletimeDifference = current_Mobiletime2.Subtract(current_Mobiletime1);
                int Mobile_seconds = (int)MobiletimeDifference.TotalSeconds;
                Console.WriteLine(Mobile_seconds);
                Base_Assert.AreEqual(Mobile_seconds, 5);
                Mobile.OrderExecution_Page.CancelButton.Click();
                Thread.Sleep(2000);
                Mobile.OrderExecution_Page.ConfirmYesButton.Click();
                Thread.Sleep(4000);
                driver.Close();



            }
            finally
            {
                LogStep(@"6.Restone config key ");
                Base_Function.EditConfigKey(Configpath, ConfigKey1);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
            }


        }

    }
}
