
using System;
using System.Diagnostics;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using Microsoft.Win32;

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
            string InputFile = Base_Directory.InputDir + "\\"+ filename;
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
        public static void AddRPL_OpenDesign(String RPLName,String BPLName)
        {
            int Count = APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable._UFT_Table.Rows.Count;
            Console.WriteLine(Count.ToString());
            var tableText = APEM.MocmainWindow.RPLDesignInternalFrame.RPLListTable._UFT_Table.GetVisibleText();
            Console.WriteLine(tableText);
            if (tableText.Contains(RPLName))
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
                if (APEM.MocmainWindow.AddReasonDialog.IsExist())
                {
                    APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("for UFT test");
                    APEM.MocmainWindow.AddReasonDialog.OK.Click();
                }
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
                APEM.MOCConfigWindow.AddReasonDialog.Reason.SendKeys("GML Config");
                APEM.MOCConfigWindow.AddReasonDialog.OK.Click();
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
            if (APEM.MocmainWindow.AddReasonDialog.IsExist())
            {
                APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("for UFT test");
                APEM.MocmainWindow.AddReasonDialog.OK.Click();
            }
            Thread.Sleep(4000);
            APEM.MocmainWindow.BPLDataInternalFrame.MakeUsable_Button.ClickSignle();
            if (APEM.MocmainWindow.AddReasonDialog.IsExist())
            {
                APEM.MocmainWindow.AddReasonDialog.Reason.SendKeys("for UFT test");
                APEM.MocmainWindow.AddReasonDialog.OK.Click();
            }
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

        public static void SkipRegister() {
            //Base_Registry registry = new Base_Registry();
            //registry.SkipRegister();

            Registry.CurrentUser.OpenSubKey($"Software\\AspenTech").SetValue("DoNotRegister", "1");


        }
    }

    
}
