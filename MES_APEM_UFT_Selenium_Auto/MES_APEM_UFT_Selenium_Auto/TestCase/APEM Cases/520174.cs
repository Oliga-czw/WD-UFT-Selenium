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
        [TestCaseID(520174)]
        [Title("SET_ORDER_DETAILS and SET_ORDER_DETAILS_EX function work well")]
        [TestCategory(ProductArea.API)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1500000)]

        [TestMethod]
        public void VSTS_520174()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            Thread.Sleep(2000);
            string TestRPLname = "FOR_STATUS";
            string TestOrdername1 = "Test001";
            string TestOrdername2 = "Test002";
            string TestOrdername3 = "Test005";
            string TestOrdername4 = "Test006";
            string BPLname = "BPL520174";
            string TestBPLname = "BPL520174_1";
            string RPLname = "RPL520174";
            string Ordername = "ORDER520174";
            Library.BaseLibrary.Application.LaunchMocAndLogin();
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname).Existing)
            {
                MOC_TemplatesFunction.Importtemplates("TEMP520174.zip");
            }
            MOC_Fuction.PlanFromRPL(TestRPLname, TestOrdername1, false);
            MOC_Fuction.PlanFromRPL(TestRPLname, TestOrdername2, false);
            MOC_Fuction.PlanFromRPL(TestRPLname, TestOrdername3, false);
            MOC_Fuction.PlanFromRPL(TestRPLname, TestOrdername4, false);
            MOC_Fuction.PlanFromRPL(RPLname, Ordername);
            APEM.MocmainWindow.BPLDesign.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(TestBPLname, "Name").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLListInternalFrame.LoadDesigner_Button.Click();
            Thread.Sleep(1000);
            if (APEM.MocmainWindow.ReadOnly_Dialog.IsExist())
            {
                APEM.MocmainWindow.ReadOnly_Dialog.OKButton.Click();
            }
            Thread.Sleep(1000);
            APEM.DesignEditorWindow.ExecuteButton.ClickSignle();
            Thread.Sleep(5000);
            //detail
            APEM.DesignEditorWindow.ExecuteMainInternalFrame.orderDetailButton.Click();
            Thread.Sleep(1000);
            APEM.DesignEditorWindow.MessageInterFrame.OKButton.Click();
            //detail_ex
            APEM.DesignEditorWindow.ExecuteMainInternalFrame.orderdetail_EXButton.Click();
            Thread.Sleep(1000);
            APEM.DesignEditorWindow.MessageInterFrame.OKButton.Click();
            APEM.DesignEditorWindow.GetSnapshot(Resultpath + "BPExecute.PNG");
            APEM.DesignEditorWindow.ExecuteMainInternalFrame._UFT_InterFrame.Close();
            APEM.MocmainWindow.ExeCancelDialog.YesButton.Click();
            MOC_Fuction.DesignEditorClose();
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.CheckRowSelection();
            //if exit order cancel it
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText(TestOrdername3);//filter order
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            Thread.Sleep(2000);
            Base_Assert.AreEqual(APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.GetCell(0, "Description").Value, "test for API function");
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText(TestOrdername4);//filter order
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            Thread.Sleep(2000);
            Base_Assert.AreEqual(APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.GetCell(0, "Description").Value, "test for API function");
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            MOC_Fuction.CheckRowSelection();
            Thread.Sleep(2000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(Ordername);
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(4000);
            APEM.PhaseExecWindow.ExecutionInternalFrame.orderDetailButton.Click();
            APEM.PhaseExecWindow.MessageInternalFrame.OKButton.Click();
            //detail_ex
            APEM.PhaseExecWindow.ExecutionInternalFrame.orderdetail_EXButton.Click();
            Thread.Sleep(1000);
            APEM.PhaseExecWindow.MessageInternalFrame.OKButton.Click();
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "orderExecute.PNG");
            APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.Click();
            Thread.Sleep(1000);
            APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.CheckRowSelection();
            //if exit order cancel it
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText(TestOrdername1);//filter order
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            Thread.Sleep(2000);
            Base_Assert.AreEqual(APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.GetCell(0, "Description").Value, "test for API function");
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText(TestOrdername2);//filter order
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            Thread.Sleep(2000);
            Base_Assert.AreEqual(APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.GetCell(0, "Description").Value, "test for API function");



        }

    }
}