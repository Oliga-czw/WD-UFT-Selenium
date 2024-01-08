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
        [TestCaseID(850241)]
        [Title("UC846822 _ Integration with APRM _ When the batch area is determined by the batch area of the RPL")]
        [TestCategory(ProductArea.Integration_APEM)]
        [Priority(CasePriority.Critical)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_850241()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";
            string OrderName1 = "Order850241";
            string OrderName2 = "Order850241SP";
            string OrderName3 = "Order850241MR";
            string RPLName1 = "RPL850241";
            string RPLName2 = "RPL850241SP";
            string RPLName1Copy = "RPL850241COPY";
            string RPLName2Copy = "RPL850241SPCOPY";
            string BatchRPL = "BatchRPL";
            string BatchAPI = "BatchAPI";
            string MRName1 = "MR850241";
            string MRName2 = "MR850241SP";

            bool active = true;

            LogStep(@"1. config APRM admin and config enviroment");
            //APRM
            GML_Function.GMLAPRMConfig();
            //import batchRPL and batchAPI
            APRM_Fuction.ImportBatchAprmAdmin();
            //Environment
            GML_Function.ConfigEnviroment(Base_Directory.BatchConfig);
            LogStep(@"2. import BPL and copy RPL to add Batch Area");
            Application.LaunchMocAndLogin();
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("BPL850241").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CASE850241.zip");
            }
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName1).Click();
            //copy and paste
            APEM.MocmainWindow.RPLDesignInternalFrame.Copy_Button.ClickSignle();
            APEM.MocmainWindow.RPLDesignInternalFrame.Paste_Button.ClickSignle();
            APEM.MocmainWindow.RPLManagementInternalFrame.RPLName.SetText(RPLName1Copy);
            APEM.MocmainWindow.RPLManagementInternalFrame.RPLBatchArea.SetText(BatchRPL);
            APEM.MocmainWindow.RPLManagementInternalFrame.ConfirmChanges_Button.ClickSignle();
            MOC_Fuction.AddReason();
            //verify and certify RPL
            MOC_Fuction.VerifyRPL(RPLName1Copy);
            MOC_Fuction.CertifyRPL(RPLName1Copy);
            LogStep(@"3. Create Order from RPL with Spcial Batch and repeat the test");
            //RPL
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName2).Click();
            //copy and paste
            APEM.MocmainWindow.RPLDesignInternalFrame.Copy_Button.ClickSignle();
            APEM.MocmainWindow.RPLDesignInternalFrame.Paste_Button.ClickSignle();
            APEM.MocmainWindow.RPLManagementInternalFrame.RPLName.SetText(RPLName2Copy);
            APEM.MocmainWindow.RPLManagementInternalFrame.RPLBatchArea.SetText(BatchAPI);
            APEM.MocmainWindow.RPLManagementInternalFrame.ConfirmChanges_Button.ClickSignle();
            MOC_Fuction.AddReason();
            //verify and certify RPL
            MOC_Fuction.VerifyRPL(RPLName2Copy);
            MOC_Fuction.CertifyRPL(RPLName2Copy);
            //order
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            if (APEM.RowSelectionDialog.IsExist())
            {
                APEM.RowSelectionDialog.YesButton.Click();
            }
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
            //create order
            string RPLSelect2 = RPLName2Copy + "#1";
            APEM.MocmainWindow.OrderListInternalFrame.PlanFromRPL_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderPlanDialog.CodeEditor.SendKeys(OrderName2);
            APEM.MocmainWindow.OrderPlanDialog.DescriptionEditor.SendKeys("test");
            APEM.MocmainWindow.OrderPlanDialog.RPLList.Select(RPLSelect2);
            APEM.MocmainWindow.OrderPlanDialog.POEditor.SendKeys(OrderName2);
            //check Batch Area
            APEM.MocmainWindow.GetSnapshot(Resultpath + "Batch Area disable and same as RPL in OrderSP.PNG");
            string OBatchAPI = APEM.MocmainWindow.OrderPlanDialog.Batch_AreaEditor.Text;
            bool OeditSP = APEM.MocmainWindow.OrderPlanDialog.Batch_AreaEditor.IsEnabled;
            Base_Assert.AreEqual(BatchAPI, OBatchAPI, "Batch name");
            Base_Assert.IsFalse(OeditSP, "Batch Area disable by creating order");

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

            LogStep(@"4. Execute OrderSP from RPL");
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(OrderName2);
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(5000);
            //finish order
            APEM.PhaseExecWindow.ExecutionInternalFrame.OK_Button.Click();
            Thread.Sleep(2000);

            LogStep(@"5.1 Create MR from RPL");
            string RPLSelect1 = RPLName1Copy + "#1";
            APEM.MocmainWindow.MasterRecipes.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.MasterRecipeInterFrame.Add_Button.ClickSignle();
            APEM.MocmainWindow.MasterRecipeDataInterFrame.Name.SetText(MRName1);
            APEM.MocmainWindow.MasterRecipeDataInterFrame.Description.SetText(BatchRPL);
            APEM.MocmainWindow.MasterRecipeDataInterFrame.RPLList.SelectItems(RPLSelect1);
            //check Batch Area
            APEM.MocmainWindow.GetSnapshot(Resultpath + "Batch Area disable and same as RPL in MR.PNG");
            string MRBatchRPL = APEM.MocmainWindow.MasterRecipeDataInterFrame.BatchArea.Text;
            bool MRedit = APEM.MocmainWindow.MasterRecipeDataInterFrame.BatchArea.IsEnabled;
            Base_Assert.AreEqual(BatchRPL, MRBatchRPL, "Batch name");
            Base_Assert.IsFalse(MRedit, "Batch Area disable in MR");
            APEM.MocmainWindow.MasterRecipeDataInterFrame.ConfirmChanges_Button.ClickSignle();
            MOC_Fuction.AddReason();

            MOC_Fuction.VerifyMR();
            MOC_Fuction.CertifyMR();
            LogStep(@"5.2 Create Order from RPL and MR");
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            if (APEM.RowSelectionDialog.IsExist())
            {
                APEM.RowSelectionDialog.YesButton.Click();
            }
            //if exit order cancel it
            APEM.MocmainWindow.OrderListInternalFrame.Refresh_Button.Click();
            count = APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Rowscount();
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
            APEM.MocmainWindow.OrderPlanDialog.CodeEditor.SendKeys(OrderName3);
            APEM.MocmainWindow.OrderPlanDialog.DescriptionEditor.SendKeys("test");
            APEM.MocmainWindow.OrderPlanDialog.POEditor.SendKeys(OrderName3);
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
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(OrderName3);
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
            //check Batch Area
            APEM.MocmainWindow.GetSnapshot(Resultpath + "Batch Area disable and same as RPL in Order.PNG");
            string OBatchRPL = APEM.MocmainWindow.OrderPlanDialog.Batch_AreaEditor.Text;
            bool Oedit = APEM.MocmainWindow.OrderPlanDialog.Batch_AreaEditor.IsEnabled;
            Base_Assert.AreEqual(BatchRPL, OBatchRPL, "Batch name");
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

            LogStep(@"5.3 Execute Order from RPL");
            APEM.MocmainWindow.WorkstationBP.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText(OrderName1);
            APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("Ready for execution", "Status").Click();
            APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            Thread.Sleep(5000);
            //BatchRPL
            APEM.PhaseExecWindow.ExecutionInternalFrame.Batch_Button.ClickSignle();
            Thread.Sleep(2000);
            //check Batch start time in message
            string RPL_StartTime = APEM.PhaseExecWindow.MessageInternalFrame.Label.Text.Substring(10);//StartTime:(12/26/23, 10:10:34 PM
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "OrderRPL .PNG");
            APEM.PhaseExecWindow.MessageInternalFrame.OKButton.Click();
            //BatchAPI
            APEM.PhaseExecWindow.ExecutionInternalFrame.BatchSP_Button.Click();//need click twice
            Thread.Sleep(2000);
            //check Batch start time in message
            string RPLSP_StartTime = APEM.PhaseExecWindow.MessageInternalFrame.Label.Text.Substring(10);//StartTime:(12/26/23, 10:10:34 PM
            APEM.PhaseExecWindow.GetSnapshot(Resultpath + "OrderRPLSP .PNG");
            APEM.PhaseExecWindow.MessageInternalFrame.OKButton.Click();
            Thread.Sleep(2000);
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
            BatchQueryTool.SelectBatchArea(BatchRPL);
            //open new query
            BatchQueryTool.NewQuery();
            //check record from aprm
            BatchQueryTool.BatchQueryToolWindow.GetSnapshot(Resultpath + "APRM BatchRPL.PNG");
            //check two record: Order850241 and Order850241MR
            var items = BatchQueryTool.BatchQueryToolWindow.ListView.Items();
            List<string> POActual = new List<string> { };
            List<string> POExpect = new List<string> { OrderName1, OrderName3 };
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

            LogStep(@"7. check Batch in Query Tool");
            Application.LaunchBatchQueryTool();
            BatchQueryTool.BatchQueryToolWindow.SetActive();
            BatchQueryTool.BatchQueryToolWindow.Maximize();
            //set Batch Area
            BatchQueryTool.SelectBatchOption();
            BatchQueryTool.SelectBatchArea(BatchAPI);
            //open new query
            BatchQueryTool.NewQuery();
            //check record from aprm
            BatchQueryTool.BatchQueryToolWindow.GetSnapshot(Resultpath + "APRM BatchAPI.PNG");
            //open batch detail display
            BatchQueryTool.BatchQueryToolWindow.ListView._STD_ListView.ActivateItem(OrderName2);
            //wait for loading
            Thread.Sleep(15000);
            APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;UNITPROCEDURE5 [1]").Expand();
            APRM.BatchMainWindow.TreeView.GetNode("Batch;UNITPROCEDURE5 [1];OPERATION11 [1]").Expand();
            APRM.BatchMainWindow.TreeView.Select("Batch;UNITPROCEDURE5 [1];OPERATION11 [1];PHASE17 [1]");
            //wait for loading
            Thread.Sleep(5000);
            APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM BatchAPI StartTime.PNG");
            //StartTime
            APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("Start_Time");
            string RPLSP_StartTimeBatch = APRM.BatchMainWindow.BatchCharacteristicDialog.ValueCalendar.Text;//12/26/2023  10:10:34 PM
            Base_Assert.AreEqual(Convert.ToDateTime(RPLSP_StartTime), Convert.ToDateTime(RPLSP_StartTimeBatch), "Start time in Batch and Order message");

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