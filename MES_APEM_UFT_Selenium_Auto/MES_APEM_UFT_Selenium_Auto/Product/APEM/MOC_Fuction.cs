
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
        public static void ImportRPLDesign(string filename)
        {
            APEM.PFCEditorWindow.FileImport.Import.Select();
            Thread.Sleep(4000);
            APEM.PFCEditorWindow.OpenDesignDialog.HomeButton.ClickSignle();
            Thread.Sleep(4000);
            APEM.PFCEditorWindow.OpenDesignDialog.LookInList._UFT_IList.ActivateItem("This PC");
            APEM.PFCEditorWindow.OpenDesignDialog.LookInList._UFT_IList.ActivateItem("Local Disk (C:)");
            string InputFile = Base_Directory.InputDir + "\\"+ filename;
            Console.WriteLine(InputFile);
            string[] sArray = InputFile.Split('\\');
            for (int i = 1; i <= sArray.Length; i++)
            {
                Console.WriteLine(sArray[i].ToString());
                APEM.PFCEditorWindow.OpenDesignDialog.LookInList._UFT_IList.ActivateItem(sArray[i].ToString());
                APEM.PFCEditorWindow.OpenDesignDialog.OpenDesignButton.ClickSignle();
            }
            Thread.Sleep(4000);
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
            var BeginNode_X = APEM.PFCEditorWindow.PFCDesignAppInternalFrame.BeginNodeUiObject._UFT_UiObject.AbsoluteLocation.X;
            var BeginNode_Width = APEM.PFCEditorWindow.PFCDesignAppInternalFrame.BeginNodeUiObject._UFT_UiObject.Size.Width;
            var PFCDesign_X = APEM.PFCEditorWindow.PFCDesignAppInternalFrame._UFT_InterFrame.AbsoluteLocation.X;
            var PFCDesign_Width = APEM.PFCEditorWindow.PFCDesignAppInternalFrame._UFT_InterFrame.Size.Width;
            var x = BeginNode_X - PFCDesign_X;
            var width = (PFCDesign_Width - BeginNode_Width) / 2;
            Base_Assert.ReferenceEquals(x, width);
        }
    }

    
}
