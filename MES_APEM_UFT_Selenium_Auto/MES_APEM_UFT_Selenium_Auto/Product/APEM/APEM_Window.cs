﻿using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM
{


    public class MOCMainWindow : UFT_JavaWindow
    {

        public MOCMainWindow()
        {
        }
        public MOCMainWindow(string xpath) : base(xpath)
        {
        }

       // private IWindow Window = UFT_Xpath.GetDesktopWindow<IWindow>("//JavaWindow[@ObjectName = 'MOC']");
        #region interframe
        public User_InterFrame LogonInternalFrame => new User_InterFrame(_UFT_Window, "//InterFrame[@Label = 'User Identification']");
        public WorkstationBP_InterFrame WorkstationBPInternalFrame => new WorkstationBP_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Order Tracking']");
        public RPLDesign_InterFrame RPLDesignInternalFrame => new RPLDesign_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Recipe Procedure Logic List']");
        public BPLList_InterFrame BPLListInternalFrame => new BPLList_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Basic Phase Library List']");
        public BPLData_InterFrame BPLDataInternalFrame => new BPLData_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Basic Phase Library Data*']");
        public RPLManagement_InterFrame RPLManagementInternalFrame => new RPLManagement_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'RPL Management*']");
        #endregion

        public UFT_Button Audit_moudle => new UFT_Button(_UFT_Window, "//Button[@Label = 'Audit Module' and @IsWrapped = 'True']");
        public UFT_Button WorkstationBP => new UFT_Button(_UFT_Window, "//Button[@Label = 'Workstation BP' and @IsWrapped = 'True']");
        public UFT_Button Templates_moudle => new UFT_Button(_UFT_Window, "//Button[@Label = 'Templates Module' and @IsWrapped = 'True']");
        public UFT_Button OrderTracking => new UFT_Button(_UFT_Window, "//Button[@Label = 'Order Tracking' and @IsWrapped = 'True']");
        public UFT_Button RPLDesign => new UFT_Button(_UFT_Window, "//Button[@Label = 'RPL Design' and @IsWrapped = 'True']");
        public UFT_Button RPLVerify => new UFT_Button(_UFT_Window, "//Button[@Label = 'RPL Verify' and @IsWrapped = 'True']");
        public UFT_Button BPLDesign => new UFT_Button(_UFT_Window, "//Button[@Label = 'BPL Design' and @IsWrapped = 'True']");
        public UFT_Button Orders => new UFT_Button(_UFT_Window, "//Button[@Label = 'Orders' and @IsWrapped = 'True']");
        public UFT_Button Config_moudle => new UFT_Button(_UFT_Window, "//Button[@Label = 'Config Module' and @IsWrapped = 'True']");

        #region dialog
        public UFT_Dialog VerifyDialog => new UFT_Dialog("//Dialog[@Index = '0']");
        public OrderPlan_Dialog OrderPlanDialog => new OrderPlan_Dialog(_UFT_Window, "//Dialog[@Title = 'Plan']");
        public AddReason_Dialog AddReasonDialog => new AddReason_Dialog(_UFT_Window, "//Dialog[@Title = 'Audit Reason']");
        public AvailableBPL_Dialog AvailableBPLDialog => new AvailableBPL_Dialog(_UFT_Window, "//Dialog[@Title = 'Available Basic Phase Libraries']");
        #endregion
    }
    public class APEMMainWindow : UFT_JavaWindow
    {

        public APEMMainWindow()
        {
        }
        public APEMMainWindow(string xpath) : base(xpath)
        {
        }

        #region interframe
        //public User_InterFrame LogonInternalFrame => new User_InterFrame(_UFT_Window, "//InterFrame[@Label = 'User Identification']");

        #endregion

        public UFT_Editor Password => new UFT_Editor(_UFT_Window, "//Editor[@AttachedText = 'Password: ']");
        public UFT_Editor EnterPasswordAgain => new UFT_Editor(_UFT_Window, "//Editor[@AttachedText = 'Enter password again:']");
        public UFT_Button OKButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'OK']");
        public UFT_Editor UID => new UFT_Editor(_UFT_Window, "//Editor[@AttachedText = 'UID used to uniquely identify Templates exported from this system']");
        public UFT_CheckBox ImportGMLTemplates => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText='Import GML v.* template']");

    }
    public class PFCEditorWindow : UFT_JavaWindow
    {

        public PFCEditorWindow()
        {
        }
        public PFCEditorWindow(string xpath) : base(xpath)
        {
        }

        #region interframe
        public PFCDesignApp_InterFrame PFCDesignAppInternalFrame => new PFCDesignApp_InterFrame(_UFT_Window, "//InterFrame[@NativeClass = 'design.designApp$1' ]");
        #endregion

        public UFT_CheckBox UnitProcedure => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Unit Procedure' and @IsWrapped = 'True']");
        public UFT_CheckBox Parallel => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Parallel' and @IsWrapped = 'True']");
        public UFT_CheckBox Serial => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Serial' and @IsWrapped = 'True']");
        public UFT_CheckBox Transition => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Transition' and @IsWrapped = 'True']");
        public UFT_Button CopyButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'Copy' and @IsWrapped = 'True']");
        public UFT_Button SaveButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'Save' and @IsWrapped = 'True']");
        public UFT_Button CutButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'Cut' and @IsWrapped = 'True']");
        public UFT_Button BackButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'Back to Code' and @IsWrapped = 'True']");
        public UFT_Button PasteButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'Paste' and @IsWrapped = 'True']");
        public UFT_CheckBox Operation => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Operation' and @IsWrapped = 'True']");
        //public UFT_CheckBox Phase => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Unit Procedure' and @IsWrapped = 'True']");
        public ITabControl TabbedPaneControl => _UFT_Window.Describe<ITabControl>(new TabControlDescription
        {
            TagName = @"JTabbedPane",
            IsWrapped = true
        });
        public IToolBar PhaseSelect => _UFT_Window.Describe<IToolBar>(new ToolBarDescription
        {
            ObjectName = @"RootMenuToolBar"
        });
        public ICheckBox First_Phase => _UFT_Window.Describe<ICheckBox>(new CheckBoxDescription
        {
            IsWrapped = true,
            Index = 1
        });
        public UFT_Dialog PasteRenamedDialog => new UFT_Dialog(_UFT_Window, "//Dialog[@Title = 'Copy/Paste Renamed Components List']");
        public UFT_Menu FileImport => new UFT_Menu(_UFT_Window, "//Menu[@Label = 'File']");
        public UFT_Menu DesignMenu => new UFT_Menu(_UFT_Window, "//Menu[@Label = 'Design']");
        public OpenDesign_Dialog OpenDesignDialog => new OpenDesign_Dialog(_UFT_Window, "//Dialog[@Title = 'Open design ...']");


    }

    public class OpenDesign_Dialog : UFT_Dialog
    {
        public OpenDesign_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Button HomeButton => new UFT_Button(_UFT_Dialog, "//Button[@Label = 'Home']");
        public UFT_List LookInList => new UFT_List(_UFT_Dialog, "//List[@AttachedText = 'Look In:' and @NativeClass = 'sun.swing.FilePane$4']");
        public UFT_Button OpenDesignButton => new UFT_Button(_UFT_Dialog, "//Button[@Label = 'Open design ...']");

    }
    


}
