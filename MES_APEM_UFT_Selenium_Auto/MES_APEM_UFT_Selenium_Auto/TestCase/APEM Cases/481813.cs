using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.Windows.Forms;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(481813)]
        [Title("V12.0_'SQL_SELECT_ONE ' API calls with customer database")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1500000)]

        [TestMethod]
        public void VSTS_481813()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string RPLname = "R481813";
            string RPLname1 = "RB481812";
            string Ordername = "ORDER481813";
            string Ordername1 = "ORDER481813_1";
            string Configpath = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string ConfigKey = "SQL_SELECT1_ARRAY1 = 0";
            string ConfigKey1 = "SQL_SELECT1_SCALAR_NO_OPTIMIZE = 1";
            Application.LaunchMocAndLogin();
            try
            { 
                APEM.MocmainWindow.RPLDesign.ClickSignle();
                if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Existing)
                {
                    MOC_TemplatesFunction.Importtemplates("API_ORDER_STATE.zip");
                }
                APEM.MocmainWindow.Orders.ClickSignle();
                Thread.Sleep(2000);
                MOC_Fuction.CheckRowSelection();
                APEM.MocmainWindow.OrderListInternalFrame.Search.SetText("");//filter order
                APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
                Thread.Sleep(3000);
                if (!APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("t_order1").Existing)
                {
                    APEM.MocmainWindow.BPLDesign.ClickSignle();
                    APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("TEST_ORDER_BP", "Name").Click();
                    APEM.MocmainWindow.BPLListInternalFrame.LoadDesigner_Button.Click();
                    APEM.DesignEditorWindow.ExecuteButton.ClickSignle();
                    Thread.Sleep(3000);
                    APEM.AuditReasonDialog.Reason.SendKeys("for test");
                    APEM.AuditReasonDialog.OK.Click();
                    Thread.Sleep(3000);
                    APEM.ExecutionFinishedDialog.OK.Click();
                    Thread.Sleep(2000);
                    MOC_Fuction.DesignEditorClose();
                }
                MOC_Fuction.PlanFromRPL(RPLname, Ordername);
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Mobile_Fuction.gotoApemMobile(driver);
                Mobile_Fuction.login();
                Mobile.OrderProcess_Page.OrderSearch.SendKeys(Ordername);
                Thread.Sleep(2000);
                //go to tracking page,execute the phase
                Mobile.OrderProcess_Page.GotoTracking.Click();
                driver.Wait(1000);
                Mobile.OrderTracking_Page.ExecutionButton.Click();
                Thread.Sleep(2000);
                Mobile.OrderExecution_Page.SQL_SELECT_ONEButton.Click();
                Thread.Sleep(2000);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Mobile.PNG");
                Base_Assert.AreEqual(Mobile.OrderExecution_Page.Message.Text(), "ORDER STATUS: 6");
                Mobile.OrderExecution_Page.MessageOK_button.Click();
                Mobile.OrderExecution_Page.CancelButton.Click();
                Thread.Sleep(2000);
                Mobile.OrderExecution_Page.ConfirmYesButton.Click();
                Thread.Sleep(3000);
                APEM.MocmainWindow.WorkstationBP.ClickSignle();
                MOC_Fuction.CheckRowSelection();
                Thread.Sleep(3000);
                //LogStep(@"Execute the order");
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(Ordername);
                APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
                //Excution
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution").Click();
                APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
                Thread.Sleep(3000);
                APEM.PhaseExecWindow.ExecutionInternalFrame.Order_state_Button.ClickSignle();
                Thread.Sleep(2000);
                Base_Assert.AreEqual(APEM.PhaseExecWindow.MessageInternalFrame.Label.Text, "ORDER STATUS: 6");
                APEM.PhaseExecWindow.GetSnapshot(Resultpath + "MOC.PNG");
                APEM.PhaseExecWindow.MessageInternalFrame.OKButton.Click();
                APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
                Thread.Sleep(1000);
                APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
                Thread.Sleep(2000);
                driver.Close();
                Base_Function.AddConfigKey(Configpath, ConfigKey1);
                Base_Function.AddConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                MOC_Fuction.PlanFromRPL(RPLname1, Ordername1);
                Selenium_Driver driver2 = new Selenium_Driver(Browser.chrome);
                Mobile_Fuction.gotoApemMobile(driver2);
                Mobile_Fuction.login();
                Mobile.OrderProcess_Page.OrderSearch.SendKeys(Ordername1);
                Thread.Sleep(2000);
                //go to tracking page,execute the phase
                Mobile.OrderProcess_Page.GotoTracking.Click();
                driver2.Wait(1000);
                Mobile.OrderTracking_Page.ExecutionButton.Click();
                Thread.Sleep(2000);
                Mobile.OrderExecution_Page.SQL_SELECT_ONEButton.Click();
                Thread.Sleep(2000);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Mobile_one.PNG");
                Base_Assert.AreEqual(Mobile.OrderExecution_Page.Message.Text(), "testorder");
                Mobile.OrderExecution_Page.MessageOK_button.Click();
                Thread.Sleep(1000);
                Mobile.OrderExecution_Page.MessageOK_button.Click();
                Thread.Sleep(1000);
                Mobile.OrderExecution_Page.MessageOK_button.Click();
                Thread.Sleep(3000);
                Mobile.OrderExecution_Page.StopButton.Click();
                Thread.Sleep(5000);
                Mobile.OrderExecution_Page.Confirmation_Text.SendKeys("for test");
                Mobile.OrderExecution_Page.Confirmation_password.SendKeys(PassWord.qaone1);
                Mobile.OrderExecution_Page.ConfirmationOK_button.Click();
                Thread.Sleep(3000);
                APEM.MocmainWindow.WorkstationBP.ClickSignle();
                MOC_Fuction.CheckRowSelection();
                Thread.Sleep(3000);
                //LogStep(@"Execute the order");
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(Ordername1);
                APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
                //Excution
                APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Interrupted").Click();
                APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
                Thread.Sleep(3000);
                APEM.PhaseExecWindow.ExecutionInternalFrame.Order_state_Button.ClickSignle();
                Thread.Sleep(2000);
                Base_Assert.AreEqual(APEM.PhaseExecWindow.MessageInternalFrame.Label.Text, "testorder");
                APEM.PhaseExecWindow.GetSnapshot(Resultpath + "MOC_one.PNG");
                APEM.PhaseExecWindow.MessageInternalFrame.OKButton.Click();
                Thread.Sleep(1000);
                APEM.PhaseExecWindow.MessageInternalFrame.OKButton.Click();
                Thread.Sleep(1000);
                APEM.PhaseExecWindow.MessageInternalFrame.OKButton.Click();
                Thread.Sleep(3000);
                APEM.PhaseExecWindow.StopPhaseButton.ClickSignle();
                Thread.Sleep(3000);
                SendKeys.SendWait("{Enter}");
                Thread.Sleep(1000);
                APEM.PhaseExecWindow.UserConfirmationInternalFrame.PassWord.SendKeys(PassWord.qaone1);
                APEM.PhaseExecWindow.UserConfirmationInternalFrame.Comment.SendKeys("for test");
                APEM.PhaseExecWindow.UserConfirmationInternalFrame.OKButton.Click();
                Thread.Sleep(20000);
                driver2.Close();

            }
            finally
            {
                LogStep(@"6.Restone config key ");
                Base_Function.DeleteConfigKey(Configpath, ConfigKey1);
                Base_Function.DeleteConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);
            }


        }

    }
}