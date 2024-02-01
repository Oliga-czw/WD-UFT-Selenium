
using System;
using System.Diagnostics;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM
{
    class MOC_Fuction
    {

        public static void ConfigClose()
        {

            APEM.MOCConfigWindow.Close();
            APEM.CloseDialog.YesButton.Click();

        }
        public static void AuditClose()
        {

            APEM.MOCAuditWindow.Close();
            APEM.CloseDialog.YesButton.Click();

        }

        public static void MocClose()
        {

            APEM.MocmainWindow.Close();
            APEM.CloseDialog.YesButton.Click();

        }
        public static void DesignEditorClose()
        {

            APEM.DesignEditorWindow.Close();
            APEM.CloseDialog.YesButton.Click();
            Thread.Sleep(2000);

        }
        public static void ImportCHKDesign(string filename)
        {
            APEM.DesignEditorWindow.FileImport.Import.Select();
            Thread.Sleep(4000);
            APEM.DesignEditorWindow.OpenDesignDialog.HomeButton.ClickSignle();
            Thread.Sleep(4000);
            APEM.DesignEditorWindow.OpenDesignDialog.LookInList._UFT_IList.ActivateItem("This PC");
            APEM.DesignEditorWindow.OpenDesignDialog.LookInList._UFT_IList.ActivateItem("Local Disk (C:)");
            string InputFile = Base_Directory.InputDir + "\\CHK\\" + filename;
            Console.WriteLine(InputFile);
            string[] sArray = InputFile.Split('\\');
            for (int i = 1; i < sArray.Length; i++)
            {
                Console.WriteLine(sArray[i].ToString());
                APEM.DesignEditorWindow.OpenDesignDialog.LookInList._UFT_IList.ActivateItem(sArray[i].ToString());
                //APEM.DesignEditorWindow.OpenDesignDialog.OpenDesignButton.ClickSignle();
            }
            Thread.Sleep(4000);
        }
        public static void VerifyBPL(string BPLName)
        {
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            Thread.Sleep(5000);
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLName).Click();
            Thread.Sleep(2000);
            if (APEM.MocmainWindow.BPLListInternalFrame.VerifyButton.IsEnabled)
            {
                APEM.MocmainWindow.BPLListInternalFrame.VerifyButton.ClickSignle();
                Thread.Sleep(3000);
                APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
                APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
                APEM.MocmainWindow.ConfirmDialog.OK.Click();
                Thread.Sleep(3000);
            }

        }
        public static void CertifyBPL(string BPLName)
        {
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            Thread.Sleep(5000);
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLName).Click();
            Thread.Sleep(2000);
            if (APEM.MocmainWindow.BPLListInternalFrame.CertifyButton.IsEnabled)
            {
                APEM.MocmainWindow.BPLListInternalFrame.CertifyButton.Click();
                Thread.Sleep(3000);
                APEM.BPLCertifyDialog.YesButton.Click();
                Thread.Sleep(3000);
                APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
                APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
                APEM.MocmainWindow.ConfirmDialog.OK.Click();
                Thread.Sleep(3000);
            }

        }
        public static void VerifyRPL(string RPLName)
        {
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Click();
            Thread.Sleep(2000);
            if (APEM.MocmainWindow.RPLDesignInternalFrame.VerifyButton.IsEnabled) 
            {
                APEM.MocmainWindow.RPLDesignInternalFrame.VerifyButton.ClickSignle();
                Thread.Sleep(3000);
                APEM.VerifyDialog.NoButton.Click();
                Thread.Sleep(3000);
                APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
                APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
                APEM.MocmainWindow.ConfirmDialog.OK.Click();
                Thread.Sleep(3000);
            }
            
        }
        public static void CertifyRPL(string RPLName)
        {
            APEM.MocmainWindow.RPLDesign.ClickSignle();
            Thread.Sleep(5000);
            APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Click();
            Thread.Sleep(2000);
            if (APEM.MocmainWindow.RPLDesignInternalFrame.CertifyButton.IsEnabled) 
            {
                APEM.MocmainWindow.RPLDesignInternalFrame.CertifyButton.ClickSignle();
                Thread.Sleep(3000);
                APEM.CertifyDialog.YesButton.Click();
                Thread.Sleep(3000);
                APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
                APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
                APEM.MocmainWindow.ConfirmDialog.OK.Click();
                Thread.Sleep(3000);
            }
            
        }

        //into MR data
        public static void VerifyMR()
        {
            //verify
            APEM.MocmainWindow.MasterRecipeDataInterFrame.VerifyButton.Click();
            APEM.CheckParametersDialog.OKButton.Click();
            APEM.VerifyDialog.NoButton.Click();
            APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
            APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
            APEM.MocmainWindow.ConfirmDialog.OK.Click();
            Thread.Sleep(3000);
        }
        //into MR data
        public static void CertifyMR()
        {
            //certify
            APEM.MocmainWindow.MasterRecipeDataInterFrame.CertifyButton.Click();
            APEM.CheckParametersDialog.OKButton.Click();
            APEM.CertifyDialog.YesButton.Click();
            APEM.MocmainWindow.ConfirmDialog.PasswordEditor.SendKeys(PassWord.qaone1);
            APEM.MocmainWindow.ConfirmDialog.Reason.SendKeys("Test");
            APEM.MocmainWindow.ConfirmDialog.OK.Click();
            Thread.Sleep(3000);
        }

        //click plan from plan to create order 
        public static void PlanFromRPL(string RPLName,string OrderName,bool active = true)
        {
            string RPLSelect = RPLName + "#1";
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.CheckRowSelection();
            //if exit order cancel it
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText(OrderName);//filter order
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            var count = APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Rowscount();
            for(int i = 0; i < count; i++)
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
            APEM.MocmainWindow.OrderListInternalFrame.PlanFromRPL_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderPlanDialog.CodeEditor.SendKeys(OrderName);
            APEM.MocmainWindow.OrderPlanDialog.DescriptionEditor.SendKeys("test");
            APEM.MocmainWindow.OrderPlanDialog.RPLList.Select(RPLSelect);
            APEM.MocmainWindow.OrderPlanDialog.POEditor.SendKeys("PO");
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
        }
        public static void PlanFromRPL_GML(string RPLName, string OrderName, bool active = true)
        {
            string RPLSelect = RPLName + "#1";
            APEM.MocmainWindow.Orders.ClickSignle();
            Thread.Sleep(2000);
            MOC_Fuction.CheckRowSelection();
            //if exit order cancel it
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText(OrderName);//filter order
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
            APEM.MocmainWindow.OrderListInternalFrame.PlanFromRPL_Button.ClickSignle();
            Thread.Sleep(2000);
            APEM.MocmainWindow.OrderPlanDialog.CodeEditor.SendKeys(OrderName);
            APEM.MocmainWindow.OrderPlanDialog.DescriptionEditor.SendKeys("test");
            APEM.MocmainWindow.OrderPlanDialog.RPLList.Select(RPLSelect);
            APEM.MocmainWindow.OrderPlanDialog.POEditor.SendKeys("PO");
            APEM.MocmainWindow.OrderPlanDialog.POStepEditor.SendKeys("POStep");
            APEM.MocmainWindow.OrderPlanDialog.ArticleEditor.SendKeys("Article");
            APEM.MocmainWindow.OrderPlanDialog.BatchEditor.SendKeys("Batch");
            APEM.MocmainWindow.OrderPlanDialog.QuantityEditor.SendKeys("123.65");
            APEM.MocmainWindow.OrderPlanDialog.Quantity_unitEditor.SendKeys("kg");
            APEM.MocmainWindow.OrderPlanDialog.DateEditor.SendKeys("12/12/22, 3:23:00 AM");
            APEM.MocmainWindow.OrderPlanDialog.END_DateEditor.SendKeys("5/6/26, 10:23:34 PM");
            APEM.MocmainWindow.OrderPlanDialog.WorkcenterList.Select("ProcessCellLine2");
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
        }
        public static void AddRPL_OpenDesign(String RPLName,String BPLName)
        {

            if (APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Existing)
            {
                APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable.Row(RPLName).Click();
                Thread.Sleep(2000);
                APEM.MocmainWindow.RPLDesignInternalFrame.LoadDesigner_Button.ClickSignle();
                Thread.Sleep(3000);
            }
            else
            {
                APEM.MocmainWindow.RPLDesignInternalFrame.AddRPL_Button.ClickSignle();
                Thread.Sleep(4000);
                APEM.MocmainWindow.RPLManagementInternalFrame.RPLName.SendKeys(RPLName);
                APEM.MocmainWindow.RPLManagementInternalFrame.RPLDescription.SendKeys("for testhahhah");
                APEM.MocmainWindow.RPLManagementInternalFrame.ConfirmChanges_Button.ClickSignle();
                MOC_Fuction.AddReason();
                Thread.Sleep(4000);
                APEM.MocmainWindow.RPLManagementInternalFrame.RPLTabControl.Select("Basic Phase Libraries");
                Thread.Sleep(3000);
                APEM.MocmainWindow.RPLManagementInternalFrame.SelectBPL_Button.ClickSignle();
                Thread.Sleep(5000);
                //"AAA_BPL (Version 1)"
                APEM.MocmainWindow.AvailableBPLDialog.AvailableBPLList.SelectItems(BPLName);
                APEM.MocmainWindow.AvailableBPLDialog.OK.Click();
                Thread.Sleep(3000);
                APEM.MocmainWindow.RPLManagementInternalFrame.RPLTabControl.Select("RPL Data");
                Thread.Sleep(3000);
                APEM.MocmainWindow.RPLManagementInternalFrame.LoadDesigner_Button.ClickSignle();
                Thread.Sleep(3000);
            }
        }
        //add reason
        public static void AddReason_config()
        {
            if (APEM.MOCConfigWindow.AddReasonDialog.IsExist())
            {
                APEM.MOCConfigWindow.AddReasonDialog.Reason.SetText("GML Config");
                APEM.MOCConfigWindow.AddReasonDialog.OK.Click();
            }
            
        }

        public static void AddReason()
        {
            if (APEM.MocmainWindow.AddReasonDialog.IsExist())
            {
                APEM.MocmainWindow.AddReasonDialog.Reason.SetText("for UFT test");
                APEM.MocmainWindow.AddReasonDialog.OK.Click();
            }

        }
        public static void Add_MakeUsableBPL(string BPLName,string BPLDescription)
        {
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            APEM.MocmainWindow.BPLListInternalFrame.AddBPL_Button.ClickSignle();
            Thread.Sleep(4000);
            APEM.MocmainWindow.BPLDataInternalFrame.BPLName.SendKeys(BPLName);
            APEM.MocmainWindow.BPLDataInternalFrame.BPLDescription.SendKeys(BPLDescription);
            APEM.MocmainWindow.BPLDataInternalFrame.ConfirmChanges_Button.ClickSignle();
            AddReason();
            Thread.Sleep(4000);
            APEM.MocmainWindow.BPLDataInternalFrame.MakeUsable_Button.ClickSignle();
            AddReason();
        }
        public static void AssertDesignWindow()
        {
            var BeginNode_X = APEM.DesignEditorWindow.PFCDesignAppInternalFrame.BeginNodeUiObject._UFT_UiObject.AbsoluteLocation.X;
            var BeginNode_Width = APEM.DesignEditorWindow.PFCDesignAppInternalFrame.BeginNodeUiObject._UFT_UiObject.Size.Width;
            var PFCDesign_X = APEM.DesignEditorWindow.PFCDesignAppInternalFrame._UFT_InterFrame.AbsoluteLocation.X;
            var PFCDesign_Width = APEM.DesignEditorWindow.PFCDesignAppInternalFrame._UFT_InterFrame.Size.Width;
            var x = BeginNode_X - PFCDesign_X;
            var width = (PFCDesign_Width - BeginNode_Width) / 2;
            Base_Assert.ReferenceEquals(x, width);
        }

        public static void CheckRowSelection()
        {
            if (APEM.RowSelectionDialog.IsExist())
            {
                APEM.RowSelectionDialog.YesButton.Click();
            }
        }
        public static void DeleteEventLog()
        {
            //delete event log
            APEM.MocmainWindow.Tools.EventLog.Select();
            APEM.RowSelectionDialog.YesButton.Click();
            if (APEM.MocmainWindow.EventLogListInterFrame.Delete.IsEnabled)
            {
                APEM.MocmainWindow.EventLogListInterFrame.Delete.ClickSignle();
                APEM.DeleteEventLogDialog.YesButton.Click();
            }
        }
    }
   

}
