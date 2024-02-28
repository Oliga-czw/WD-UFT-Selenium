using HP.LFT.SDK;
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
        public WorkstationBP_InterFrame WorkstationBPInternalFrame => new WorkstationBP_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Workstation']");
        public OrderTracking_InterFrame OrderTrackingInternalFrame => new OrderTracking_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Order Tracking']");
        public OrderTrackingPFC_InterFrame OrderTrackingPFCInternalFrame => new OrderTrackingPFC_InterFrame(_UFT_Window, "//InterFrame[@NativeClass = 'recipe.recipeViewer']");
        public RPLDesign_InterFrame RPLDesignInternalFrame => new RPLDesign_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Recipe Procedure Logic List']");
        public BPLList_InterFrame BPLListInternalFrame => new BPLList_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Basic Phase Library List']");
        public BPLData_InterFrame BPLDataInternalFrame => new BPLData_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Basic Phase Library Data*']");
        public Subdocuments_InterFrame SubdocumentsFrame => new Subdocuments_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Subdocuments']");
        public Document_InterFrame DocumentFrame => new Document_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Document']");
        public OrderList_InterFrame OrderListInternalFrame => new OrderList_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Orders*']");
        public RPLManagement_InterFrame RPLManagementInternalFrame => new RPLManagement_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'RPL Management*']");
        public MasterRecipe_InterFrame MasterRecipeInterFrame => new MasterRecipe_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Master Recipe List']");
        public MasterRecipeData_InterFrame MasterRecipeDataInterFrame => new MasterRecipeData_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Master Recipe Data*']");
        public EventLogList_InterFrame EventLogListInterFrame => new EventLogList_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Event Log List']");
        #endregion

        public UFT_Button Audit_moudle => new UFT_Button(_UFT_Window, "//Button[@Label = 'Audit Module' and @IsWrapped = 'True']");
        public UFT_Button WorkstationBP => new UFT_Button(_UFT_Window, "//Button[@Label = 'Workstation BP' and @IsWrapped = 'True']");
        public UFT_Button Templates_moudle => new UFT_Button(_UFT_Window, "//Button[@Label = 'Templates Module' and @IsWrapped = 'True']");
        public UFT_Button OrderTracking => new UFT_Button(_UFT_Window, "//Button[@Label = 'Order Tracking' and @IsWrapped = 'True']");
        public UFT_Button RPLDesign => new UFT_Button(_UFT_Window, "//Button[@Label = 'RPL Design' and @IsWrapped = 'True']");
        public UFT_Button RPLVerify => new UFT_Button(_UFT_Window, "//Button[@Label = 'RPL Verify' and @IsWrapped = 'True']");
        public UFT_Button BPLDesign => new UFT_Button(_UFT_Window, "//Button[@Label = 'BPL Design' and @IsWrapped = 'True']");
        public UFT_Button Orders => new UFT_Button(_UFT_Window, "//Button[@Label = 'Orders' and @IsWrapped = 'True']");
        public UFT_Button MasterRecipes => new UFT_Button(_UFT_Window, "//Button[@Label = 'Master Recipes' and @IsWrapped = 'True']");
        public UFT_Button Config_moudle => new UFT_Button(_UFT_Window, "//Button[@Label = 'Config Module' and @IsWrapped = 'True']");
        public MOC_Menu Tools => new MOC_Menu(_UFT_Window, "//Menu[@Label = 'Tools']");

        #region dialog
        public UFT_Dialog VerifyDialog => new UFT_Dialog("//Dialog[@Index = '0']");
        public UFT_Dialog ScreenshotDialog => new UFT_Dialog("//Dialog[@Title = 'Screenshot Not Found']");
        public PrintReport_Dialog PrintReportDialog => new PrintReport_Dialog(_UFT_Window, "//Dialog[@Title = 'Print Report']");
        public UFT_Dialog PhaseExecutionsDialog => new UFT_Dialog(_UFT_Window, "//Dialog[@Title = 'Phase Executions']");
        public OrderPlan_Dialog OrderPlanDialog => new OrderPlan_Dialog(_UFT_Window, "//Dialog[@Title = 'Plan']");
        public AddReason_Dialog AddReasonDialog => new AddReason_Dialog(_UFT_Window, "//Dialog[@Title = 'Audit Reason']");
        public UFT_Dialog ReadOnly_Dialog => new UFT_Dialog("//Dialog[@Title = 'Read Only File']");
        public UFT_Dialog ExeCancelDialog => new UFT_Dialog("//Dialog[@Title = 'Close Button Pressed: Execution Cancellation']");
        public AvailableBPL_Dialog AvailableBPLDialog => new AvailableBPL_Dialog(_UFT_Window, "//Dialog[@Title = 'Available Basic Phase Libraries']");
        //m2r.Window.m2rDialog$FixedSize
        public Confirm_Dialog ConfirmDialog => new Confirm_Dialog(_UFT_Window, "//Dialog[@NativeClass = 'm2r.Window.m2rDialog$FixedSize']");
        public SelectMasterRecipe_Dialog SelectMasterRecipeDialog => new SelectMasterRecipe_Dialog(_UFT_Window, "//Dialog[@Title = 'Select Master Recipe']");


        //orderlist
        public UFT_Dialog CancelOrderDialog => new UFT_Dialog("//Dialog[@Title = 'Cancel Order']");
        public UFT_Dialog DeleteOrderDialog => new UFT_Dialog("//Dialog[@Title = 'Delete current row']");
        public UFT_Dialog ArchiveOrderDialog => new UFT_Dialog("//Dialog[@Title = 'Archive Order']");
        public RowsToView_Dialog RowsToViewDialog => new RowsToView_Dialog(_UFT_Window, "//Dialog[@Title = 'Rows to View']");
        public UFT_Dialog ErrorDialog => new UFT_Dialog("//Dialog[@Title = 'Error']");
        #endregion
        //UIA Pro Webbrowser
        public HP.LFT.SDK.UIAPro.IEdit BrowserURL => Desktop.Describe<HP.LFT.SDK.UIAPro.IWindow>(new HP.LFT.SDK.UIAPro.WindowDescription
        {
            ProcessName = @"iexplore",
            Path = @"Window",
            SupportedPatterns = new string[] { @"LegacyIAccessible", @"Transform", @"Window" },
            FrameworkId = @"Win32",
            ControlType = @"Window",
            AutomationId = string.Empty
        })
            .Describe<HP.LFT.SDK.UIAPro.IPane>(new HP.LFT.SDK.UIAPro.PaneDescription
            {
                ProcessName = @"iexplore",
                Name = @"Address Bar",
                Path = @"Window;Pane;Pane;Pane",
                SupportedPatterns = new string[] { @"LegacyIAccessible" },
                FrameworkId = @"Win32",
                ControlType = @"Pane",
                AutomationId = @"41477"
            })
            .Describe<HP.LFT.SDK.UIAPro.IEdit>(new HP.LFT.SDK.UIAPro.EditDescription
            {
                ProcessName = @"iexplore",
                Name = @"Address and search using Bing",
                Path = @"Window;Pane;Pane;Pane;Edit",
                SupportedPatterns = new string[] { @"LegacyIAccessible", @"Text", @"Text2", @"Value" },
                FrameworkId = @"Win32",
                ControlType = @"Edit",
                AutomationId = string.Empty
            });
        public STD_Window MicrosoftEdgeWindow => new STD_Window("//Window[@WindowTitleRegExp = ' Microsoft​ Edge']");
    }
        
    public class MOC_Menu : UFT_Menu
    {
        public MOC_Menu(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Menu EventLog => new UFT_Menu(_UFT_Menu, "//Menu[@Label = 'Event Log']");
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
    public class DesignEditorWindow : UFT_JavaWindow
    {

        public DesignEditorWindow()
        {
        }
        public DesignEditorWindow(string xpath) : base(xpath)
        {
        }

        #region interframe
        public PFCDesignApp_InterFrame PFCDesignAppInternalFrame => new PFCDesignApp_InterFrame(_UFT_Window, "//InterFrame[@NativeClass = 'design.designApp$1' ]");
        public BPLExecutionMessageInterFrame MessageInterFrame => new BPLExecutionMessageInterFrame(_UFT_Window, "//InterFrame[@Label = 'Message']");
        public Confirmation_InterFrame ConfirmationInternalFrame => new Confirmation_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Confirmation']");
        public ExecuteMain_InterFrame ExecuteMainInternalFrame => new ExecuteMain_InterFrame(_UFT_Window, "//InterFrame[@NativeClass = 'runtime.vm.chkVMRuntimeMDI$RTFrame']");

        #endregion
        #region RPLDesign
        public UFT_CheckBox UnitProcedure => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Unit Procedure' and @IsWrapped = 'True']");
        public UFT_CheckBox Parallel => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Parallel' and @IsWrapped = 'True']");
        public UFT_CheckBox Serial => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Serial' and @IsWrapped = 'True']");
        public UFT_CheckBox Transition => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Transition' and @IsWrapped = 'True']");
        public UFT_Button CopyButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'Copy' and @IsWrapped = 'True']");
        public UFT_Button UndoButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'Undo' and @IsWrapped = 'True']");
        public UFT_Button SaveButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'Save' and @IsWrapped = 'True']");
        public UFT_Button CutButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'Cut' and @IsWrapped = 'True']");
        public UFT_Button BackButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'Back to Code' and @IsWrapped = 'True']");
        public UFT_Button PasteButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'Paste' and @IsWrapped = 'True']");
        public UFT_Button ExecuteButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'Execute' and @IsWrapped = 'True']");
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
        #endregion
        public UFT_Dialog PasteRenamedDialog => new UFT_Dialog(_UFT_Window, "//Dialog[@Title = 'Copy/Paste Renamed Components List']");
        public UFT_Menu FileImport => new UFT_Menu(_UFT_Window, "//Menu[@Label = 'File']");
        public UFT_Menu Build => new UFT_Menu(_UFT_Window, "//Menu[@Label = 'Build']");
        public UFT_Menu DesignMenu => new UFT_Menu(_UFT_Window, "//Menu[@Label = 'Design']");
        public OpenDesign_Dialog OpenDesignDialog => new OpenDesign_Dialog(_UFT_Window, "//Dialog[@Title = 'Open design ...']");
       
        #region BPLDesign
        public UFT_CheckBox Actions => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Actions' and @IsWrapped = 'True']");
        public UFT_CheckBox Concurrent_Actions => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Concurrent actions' and @IsWrapped = 'True']");
        public UFT_CheckBox Window => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Window' and @IsWrapped = 'True']");
        public UFT_CheckBox FinishWithYES => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Finish with YES' and @IsWrapped = 'True']");
        public UFT_CheckBox FinishWithNO => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Finish with NO' and @IsWrapped = 'True']");
        public UFT_CheckBox LinkNode => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText = 'Link node' and @IsWrapped = 'True']");

        #endregion


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

    public class DesignVerificationWindow : UFT_JavaWindow
    {

        public DesignVerificationWindow()
        {
        }
        public DesignVerificationWindow(string xpath) : base(xpath)
        {
        }

 
        public UFT_List ErrorList => new UFT_List(_UFT_Window, "//List[@TagName = 'JList']");
        


    }
    public class DesignCompilationWindow : UFT_JavaWindow
    {

        public DesignCompilationWindow()
        {
        }
        public DesignCompilationWindow(string xpath) : base(xpath)
        {
        }


        public UFT_List ErrorList => new UFT_List(_UFT_Window, "//List[@TagName = 'JList']");
    }
    public class PhaseExecWindow : UFT_JavaWindow
    {

        public PhaseExecWindow()
        {
        }
        public PhaseExecWindow(string xpath) : base(xpath)
        {
        }

        public Execution_InterFrame ExecutionInternalFrame => new Execution_InterFrame(_UFT_Window, "//InterFrame[@NativeClass = 'runtime.vm.chkRTFrame']");
        public PopUp_InterFrame PopUpInternalFrame => new PopUp_InterFrame(_UFT_Window, "//InterFrame[@NativeClass = 'runtime.vm.chkVMRuntimeFrame$MDIFrameMessageInputSupport$2']");
        public Confirmation_InterFrame ConfirmationInternalFrame => new Confirmation_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Confirmation']");
        public UserConfirmation_InterFrame UserConfirmationInternalFrame => new UserConfirmation_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'User Confirmation']");
        public UFT_Button StopPhaseButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'Stop phase' and @IsWrapped = 'True']");
        public UFT_Button CancelPhaseButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'Cancel phase' and @IsWrapped = 'True']");
        public Message_InterFrame MessageInternalFrame => new Message_InterFrame(_UFT_Window, "//InterFrame[@Label = 'Message']");
    }

    public class RegistrationWindow : WPF_Window
    {

        public RegistrationWindow()
        {
        }
        public RegistrationWindow(HP.LFT.SDK.WPF.IWindow window) : base(window)
        {
        }
        public HP.LFT.SDK.WPF.ICheckBox doNotShowCheckBox => _WPF_Window.Describe<HP.LFT.SDK.WPF.ICheckBox>(new HP.LFT.SDK.WPF.CheckBoxDescription
        {
            Text = @"Do not show me this dialog again",
            Index = 1
        });

        public HP.LFT.SDK.WPF.IButton logInButton => _WPF_Window.Describe<HP.LFT.SDK.WPF.IButton>(new HP.LFT.SDK.WPF.ButtonDescription
            {
                Text = @"Log In"
            });

    }


}
