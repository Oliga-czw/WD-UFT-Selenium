﻿using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.IO;
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;
using System.Linq;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.Collections.Generic;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(850438)]
        [Title("UC846822 _ Integration with APRM _ When the area of the API isn't specified and the batch area is determined by the area of the key APRM_AREA(Default area)")]
        [TestCategory(ProductArea.Integration_APEM)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_850438()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName1 = "Order850438";
            string RPLName1 = "RPL850438";
            string Batch = "Batch";


            bool active = true;

            LogStep(@"1. config APRM admin and config enviroment");
            //APRM
            GML_Function.GMLAPRMConfig();
            //Environment
            GML_Function.ConfigEnviroment(Base_Directory.BatchConfig);
            LogStep(@"2. import BPL and copy RPL to add Batch Area");
            Application.LaunchMocAndLogin();
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("BPL850438").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE850438.zip");
            }
            LogStep(@"3. Create data order from RPL and Execute ");
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            if (APEM.RowSelectionDialog.IsExist())
            {
                APEM.RowSelectionDialog.YesButton.Click();
            }
            //if exit order cancel it
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText(OrderName1);//filter order
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            var count = APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Rowscount();
            for (int i = 0; i < count; i++)
            {
                APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.SelectRows(i);
                if (APEM.MocmainWindow.OrderListInternalFrame.Cancel_Button.IsEnabled)
                {
                    APEM.MocmainWindow.OrderListInternalFrame.Cancel_Button.ClickSignle();
                    APEM.MocmainWindow.CancelOrderDialog.YesButton.Click();
                    MOC_Fuction.AddReason();
                }
            }
            string RPLSelect1 = RPLName1 + "#1";
            APEM.MocmainWindow.OrderListInternalFrame.PlanFromRPL_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderPlanDialog.CodeEditor.SendKeys(OrderName1);
            APEM.MocmainWindow.OrderPlanDialog.DescriptionEditor.SendKeys("test");
            APEM.MocmainWindow.OrderPlanDialog.RPLList.Select(RPLSelect1);
            APEM.MocmainWindow.OrderPlanDialog.POEditor.SendKeys(OrderName1);
            //Batch Area empty
            APEM.MocmainWindow.GetSnapshot(Resultpath + "Batch Area disable and empty in Order.PNG");
            string OBatchRPL = APEM.MocmainWindow.OrderPlanDialog.Batch_AreaEditor.Text;
            bool Oedit = APEM.MocmainWindow.OrderPlanDialog.Batch_AreaEditor.IsEnabled;
            Base_Assert.AreEqual("", OBatchRPL, "Batch area");
            Base_Assert.IsFalse(Oedit, "Batch Area disable by creating order");

            Thread.Sleep(3000);
            if (active)
            {
                APEM.MocmainWindow.OrderPlanDialog.Auto_ActivateCheckBox.Click();
            }
            Thread.Sleep(3000);
            APEM.MocmainWindow.OrderPlanDialog.OK.Click();
            Thread.Sleep(3000);
            APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("for test");
            APEM.MocmainWindow.AddReasonDialog.OK.Click();
            Thread.Sleep(3000);
            //Execute the Order
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(OrderName1);
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.Click();
            Thread.Sleep(5000);
            //Batch write and read
            APEM.PhaseExecWindow.ExecutionInternalFrame.BatchWriteRead_Button.Click();
            Thread.Sleep(2000);
            //check Batch char1 in message
            string RPL_char1 = APEM.PhaseExecWindow.MessageInternalFrame.Label.Text.Substring(6);//Charl:Add new values
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "Order batch char1 .PNG");
            APEM.PhaseExecWindow.MessageInternalFrame.OKButton.Click();
            //finish order
            APEM.PhaseExecWindow.ExecutionInternalFrame.OK_Button.Click();
            Thread.Sleep(2000);
            APEM.ExitApplication();

            LogStep(@"6. check Batch in Query Tool");
            Application.LaunchBatchQueryTool();
            BatchQueryTool.BatchQueryToolWindow.SetActive();
            BatchQueryTool.BatchQueryToolWindow.Maximize();
            //set Batch Area
            BatchQueryTool.SelectBatchOption();
            BatchQueryTool.SelectBatchArea(Batch);
            //open new query
            BatchQueryTool.NewQuery();
            //check record from aprm
            BatchQueryTool.BatchQueryToolWindow.GetSnapshot(Resultpath + "APRM Batch.PNG");
            //check record: Order850438
            var items = BatchQueryTool.BatchQueryToolWindow.ListView.Items();
            List<string> POActual = new List<string> { };
            List<string> POExpect = new List<string> { OrderName1};
            foreach (var item in items)
            {
                POActual.Add(item.Text);
            }
            Console.WriteLine(POExpect.All(PO => POActual.Contains(PO)));
            //open batch detail display
            BatchQueryTool.BatchQueryToolWindow.ListView.ActivateItem(OrderName1);
            //wait for loading
            Thread.Sleep(15000);
            APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;UNITPROCEDURE5 [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;UNITPROCEDURE5 [1];OPERATION11 [1]").Expand();
            APRM.BatchMainWindow.TreeView.Select("Batch;UNITPROCEDURE5 [1];OPERATION11 [1];PHASE17 [1]");
            //wait for loading
            Thread.Sleep(5000);
            APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM Batch char1.PNG");
            //StartTime
            APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("CHAR1");
            string RPL_char1Batch = APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text;
            Base_Assert.AreEqual(RPL_char1, RPL_char1Batch, "char1 in Batch and Order message");

            APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
            APRM.BatchMainWindow.Close();
            BatchQueryTool.BatchQueryToolWindow.Close();
            //dialog message
            if (BatchQueryTool.BatchQueryToolWindow.Save_Dialog.IsExist())
            {
                BatchQueryTool.BatchQueryToolWindow.Save_Dialog.NO.Click();
            }

        }

    }
}