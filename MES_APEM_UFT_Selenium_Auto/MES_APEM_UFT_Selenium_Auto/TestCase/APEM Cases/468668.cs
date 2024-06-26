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

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(468668)]
        [Title("'GET_ORDER_STATE' API calls with customer database")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1500000)]

        [TestMethod]
        public void VSTS_468668()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string RPLname = "R468668";
            string Ordername = "ORDER468668_1";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("API_ORDER_STATE.zip");
            }
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.CheckRowSelection();
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText("t_order1");//filter order
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            Thread.Sleep(3000);
            MOC_Fuction.CheckRowSelection();
            if (!APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("t_order1").Existing)
            {
                APEM.MocmainWindow.BPLDesign.ClickSignle();
                APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("TEST_ORDER_BP","Name").Click();
                APEM.MocmainWindow.BPLListInternalFrame.LoadDesigner_Button.Click();
                APEM.DesignEditorWindow.ExecuteButton.ClickSignle();
                Thread.Sleep(3000);
                if (APEM.AuditReasonDialog.Reason.Exists())
                {
                    APEM.AuditReasonDialog.Reason.SendKeys("for test");
                    APEM.AuditReasonDialog.OK.Click();
                    Thread.Sleep(3000);
                }
                
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
            Mobile.OrderExecution_Page.GET_ORDER_STATEButton.Click();
            Thread.Sleep(2000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Mobile.PNG");
            Base_Assert.AreEqual(Mobile.OrderExecution_Page.Message.Text(), "ORDER RAWW STATUS:ACTIVE");
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
            Base_Assert.AreEqual(APEM.PhaseExecWindow.MessageInternalFrame.Label.Text, "ORDER RAWW STATUS:ACTIVE");
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "MOC.PNG");
            APEM.PhaseExecWindow.MessageInternalFrame.OKButton.Click();
            APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
            Thread.Sleep(1000);
            APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
            Thread.Sleep(2000);
            driver.Close();



        }

    }
}