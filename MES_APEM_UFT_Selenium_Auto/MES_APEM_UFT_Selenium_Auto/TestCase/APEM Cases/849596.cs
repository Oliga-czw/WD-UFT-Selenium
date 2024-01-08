using System.Collections;
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
    public partial class WD_TestCase
    {
        [TestCaseID(849596)]
        [Title("UC846822 _ Integration with APRM _ When the batch area is determined by the value of the key APRM_AREA(Default area)")]
        [TestCategory(ProductArea.Integration_APEM)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_849596()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName1 = "Order849596";
            string OrderName2 = "Order849596MR";
            string RPLName1 = "RPL849596";
            string Batch = "Batch";
            string MRName1 = "MR849596";

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
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("BPL849596").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE849596.zip");
            }
            LogStep(@"3. Create MR from RPL");
            string RPLSelect1 = RPLName1 + "#1";
            APEM.MocmainWindow.MasterRecipes.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.MasterRecipeInterFrame.Add_Button.ClickSignle();
            APEM.MocmainWindow.MasterRecipeDataInterFrame.Name.SetText(MRName1);
            APEM.MocmainWindow.MasterRecipeDataInterFrame.Description.SetText(Batch);
            APEM.MocmainWindow.MasterRecipeDataInterFrame.RPLList.SelectItems(RPLSelect1);
            //check Batch Area empty
            APEM.MocmainWindow.GetSnapshot(Resultpath + "Batch Area disable and empty in MR.PNG");
            string MRBatchRPL = APEM.MocmainWindow.MasterRecipeDataInterFrame.BatchArea.Text;
            bool MRedit = APEM.MocmainWindow.MasterRecipeDataInterFrame.BatchArea.IsEnabled;
            Base_Assert.AreEqual("", MRBatchRPL, "Batch area");
            Base_Assert.IsFalse(MRedit, "Batch Area disable in MR");
            APEM.MocmainWindow.MasterRecipeDataInterFrame.ConfirmChanges_Button.ClickSignle();
            MOC_Fuction.AddReason();

            MOC_Fuction.VerifyMR();
            MOC_Fuction.CertifyMR();
            LogStep(@"4. Create Order from RPL and MR");
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            //if exit order cancel it
            APEM.MocmainWindow.OrderListInternalFrame.Refresh_Button.Click();
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
            //create order from MR
            APEM.MocmainWindow.OrderListInternalFrame.PlanFromMRecipe_Button.ClickSignle();
            APEM.MocmainWindow.SelectMasterRecipeDialog.MRTable.Row(MRName1);
            APEM.MocmainWindow.SelectMasterRecipeDialog.OK.Click();
            APEM.MocmainWindow.OrderPlanDialog.CodeEditor.SendKeys(OrderName2);
            APEM.MocmainWindow.OrderPlanDialog.DescriptionEditor.SendKeys("test");
            APEM.MocmainWindow.OrderPlanDialog.POEditor.SendKeys(OrderName2);
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
            //Execute order from MR
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(OrderName2);
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(5000);
            //finish order
            APEM.PhaseExecWindow.ExecutionInternalFrame.OK_Button.Click();
            //
            //create order from RPL
            APEM.MocmainWindow.OrderListInternalFrame.PlanFromRPL_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderPlanDialog.CodeEditor.SendKeys(OrderName1);
            APEM.MocmainWindow.OrderPlanDialog.DescriptionEditor.SendKeys("test");
            APEM.MocmainWindow.OrderPlanDialog.RPLList.Select(RPLSelect1);
            APEM.MocmainWindow.OrderPlanDialog.POEditor.SendKeys(OrderName1);
            //check Batch Area empty
            APEM.MocmainWindow.GetSnapshot(Resultpath + "Batch Area disable and empty in Order.PNG");
            string OBatchRPL = APEM.MocmainWindow.OrderPlanDialog.Batch_AreaEditor.Text;
            bool Oedit = APEM.MocmainWindow.OrderPlanDialog.Batch_AreaEditor.IsEnabled;
            Base_Assert.AreEqual("", OBatchRPL, "Batch area");
            Base_Assert.IsFalse(Oedit, "Batch Area disable by creating order");

            APEM.MocmainWindow.OrderPlanDialog.POStepEditor.SendKeys("POStep");
            APEM.MocmainWindow.OrderPlanDialog.ArticleEditor.SendKeys("Article");
            APEM.MocmainWindow.OrderPlanDialog.BatchEditor.SendKeys("Batch");
            APEM.MocmainWindow.OrderPlanDialog.QuantityEditor.SendKeys("123.65");
            APEM.MocmainWindow.OrderPlanDialog.Quantity_unitEditor.SendKeys("kg");
            APEM.MocmainWindow.OrderPlanDialog.DateEditor.SendKeys("12/12/22, 3:23:00 AM");
            APEM.MocmainWindow.OrderPlanDialog.END_DateEditor.SendKeys("5/6/26, 10:23:34 PM");
            //APEM.MocmainWindow.OrderPlanDialog.WorkcenterList.Select("ProcessCellLine2");
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

            LogStep(@"5. Execute Order from RPL");
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(OrderName1);
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.Click();
            Thread.Sleep(5000);
            //BatchRPL
            APEM.PhaseExecWindow.ExecutionInternalFrame.BatchDefault_Button.Click();
            Thread.Sleep(2000);
            //check Batch start time in message
            string RPL_StartTime = APEM.PhaseExecWindow.MessageInternalFrame.Label.Text.Substring(10);//StartTime:(12/26/23, 10:10:34 PM
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "Order batch start time .PNG");
            APEM.PhaseExecWindow.MessageInternalFrame.OKButton.Click();
            //finish order
            APEM.PhaseExecWindow.ExecutionInternalFrame.OK_Button.Click();
            Thread.Sleep(2000);
            APEM.ExitApplication();
            LogStep(@"6. check BatchRPL in Query Tool");
            //string RPLSP_StartTime = "12/28/23, 8:14:28 PM";
            //string RPL_StartTime = "12/28/23, 8:15:59 PM";
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
            //check two record: Order849596 and Order849596MR
            var items = BatchQueryTool.BatchQueryToolWindow.ListView.Items();
            List<string> POActual = new List<string> { };
            List<string> POExpect = new List<string> { OrderName1, OrderName2 };
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
            APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM BatchRPL StartTime.PNG");
            //StartTime
            APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("Start_Time");
            string RPL_StartTimeBatch = APRM.BatchMainWindow.BatchCharacteristicDialog.ValueCalendar.Text;//12/26/2023  10:10:34 PM
            Base_Assert.AreEqual(Convert.ToDateTime(RPL_StartTime), Convert.ToDateTime(RPL_StartTimeBatch), "Start time in Batch and Order message");

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