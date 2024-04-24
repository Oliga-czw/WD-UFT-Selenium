using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM
{


    public class MOCMainInterFrame : UFT_InterFrame
    {

        public MOCMainInterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

    }


    public class User_InterFrame : MOCMainInterFrame
    {
        public User_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Editor userNameEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'User']");
        public UFT_Editor passwordEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Password']");
        public UFT_Button loginbutton => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'OK']");
    }
    //OrderTracking_InterFrame
    public class OrderTracking_InterFrame : MOCMainInterFrame
    {
        public OrderTracking_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public IEditor CodeEditor => _UFT_InterFrame.Describe<IEditor>(new EditorDescription
        {
            AttachedText = @"Code  "
        });
        //public UFT_Editor CodeEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Code  ']");
        //public ITable OrderTable => _UFT_InterFrame.Describe<ITable>(new EditorDescription
        //{
        //    AttachedText = @"Order  "
        //});
        public UFT_Table OrderTable => new UFT_Table(_UFT_InterFrame, "//Table[@NativeClass = 'm2r.Table.m2rTableView']");
        public IButton Filterbutton => _UFT_InterFrame.Describe<IButton>(new ButtonDescription
        {
            Label = @"Local filter",
            IsWrapped = true
        });
        public UFT_Button ExecuteButton => new UFT_Button(_UFT_InterFrame, "//Button[@Label ='Execute' and @IsWrapped = 'True']");
        public UFT_Button RefreshButton => new UFT_Button(_UFT_InterFrame, "//Button[@Label ='Refresh' and @IsWrapped = 'True']");
        public UFT_Button StatusFilterButton => new UFT_Button(_UFT_InterFrame, "//Button[@Label ='Select visible rows' and @IsWrapped = 'True']");
        public IMenu Ordertracking => _UFT_InterFrame.Describe<IMenu>(new MenuDescription
        {
            Label = @"Order tracking"
        });
    }

    public class WorkstationBP_InterFrame : MOCMainInterFrame
    {
        public WorkstationBP_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public IEditor OrderEditor => _UFT_InterFrame.Describe<IEditor>(new EditorDescription
        {
            AttachedText = @"Order  "
        });
        public UFT_Editor SearchEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@NativeClass='javax.swing.JTextField']");
        //public UFT_Editor CodeEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Code  ']");
        //public ITable OrderTable => _UFT_InterFrame.Describe<ITable>(new EditorDescription
        //{
        //    AttachedText = @"Order  "
        //});
        public UFT_Button RefreshButton => new UFT_Button(_UFT_InterFrame, "//Button[@Label ='Refresh' and @IsWrapped = 'True']");
        public UFT_Table OrderTable => new UFT_Table(_UFT_InterFrame, "//Table[@NativeClass = 'm2r.Table.m2rTableView']");
        public IButton Filterbutton => _UFT_InterFrame.Describe<IButton>(new ButtonDescription
        {
            Label = @"Local filter",
            IsWrapped = true
        });
        public UFT_Button ExecuteButton => new UFT_Button(_UFT_InterFrame, "//Button[@Label ='Execute' and @IsWrapped = 'True']");
    }
    public class RPLDesign_InterFrame : MOCMainInterFrame
    {
        public RPLDesign_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button AddRPL_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Insert row' and @IsWrapped = 'True']");
        public UFT_Button Paste_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Paste row' and @IsWrapped = 'True']");
        public UFT_Button Copy_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Copy row' and @IsWrapped = 'True']");
        
        public IEditor NameEditor => _UFT_InterFrame.Describe<IEditor>(new EditorDescription
        {
            AttachedText = @"Name  "
        });
        public UFT_Editor SearchEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@NativeClass='javax.swing.JTextField']");
        //public UFT_Editor CodeEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Code  ']");
        public UFT_Button VerifyButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Verify']");
        public UFT_Button CertifyButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Certify']");
        public UFT_Table RPLListTable => new UFT_Table(_UFT_InterFrame, "//Table[@AttachedText = 'Name\\s\\s']");
        public IButton Filterbutton => _UFT_InterFrame.Describe<IButton>(new ButtonDescription
        {
            Label = @"Local filter",
            IsWrapped = true
        });
        public UFT_Button LoadDesigner_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Load designer' and @IsWrapped = 'True']");
        //public UFT_Button Verify_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Verify']");
    }
    public class RPLManagement_InterFrame : MOCMainInterFrame
    {

        public RPLManagement_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button ConfirmChanges_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Confirm changes' and @IsWrapped = 'True']");
        public UFT_Editor RPLName => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Name']");
        public UFT_Editor RPLDescription => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Description']");
        public UFT_Editor RPLBatchArea => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Batch Area']");
        public ITabControl RPLTabControl => _UFT_InterFrame.Describe<ITabControl>(new TabControlDescription
        {
            TagName = @"m2rTabbedPanel"
        });
        public UFT_Button SelectBPL_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Insert row' and @IsWrapped = 'True']");
        public UFT_Button LoadDesigner_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Load designer' and @IsWrapped = 'True']");
        public UFT_Button VerifyButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Verify']");
        public UFT_Button CertifyButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Certify']");
        public UFT_Table RPLPhasesListTable => new UFT_Table(_UFT_InterFrame, "//Table[@NativeClass = 'm2r.Table.m2rTableViewEdit']"); 
    }
    public class Document_InterFrame : MOCMainInterFrame
    {

        public Document_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button ConfirmChanges_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Confirm changes' and @IsWrapped = 'True']");
        public UFT_Editor DocName => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Name']");
        public UFT_Editor DocDescription => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Description']");
        
        public UFT_Button LoadDesigner_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Load designer' and @IsWrapped = 'True']");
        
    }
    //MasterRecipe_InterFrame
    public class MasterRecipe_InterFrame : MOCMainInterFrame
    {
        public MasterRecipe_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button Add_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Insert row' and @IsWrapped = 'True']");
        public IEditor NameEditor => _UFT_InterFrame.Describe<IEditor>(new EditorDescription
        {
            AttachedText = @"Name  "
        });

        //public UFT_Editor CodeEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Code  ']");
        public UFT_Button VerifyButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Verify']");
        public UFT_Button CertifyButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Certify']");
        public UFT_Table MRListTable => new UFT_Table(_UFT_InterFrame, "//Table[@NativeClass = 'm2r.Table.m2rTableView']");
        public IButton Filterbutton => _UFT_InterFrame.Describe<IButton>(new ButtonDescription
        {
            Label = @"Local filter",
            IsWrapped = true
        });

    }
    public class MasterRecipeData_InterFrame : MOCMainInterFrame
    {

        public MasterRecipeData_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button ConfirmChanges_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Confirm changes' and @IsWrapped = 'True']");
        public UFT_Editor Name => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Name']");
        public UFT_Editor Description => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Description']");
        public UFT_List RPLList => new UFT_List(_UFT_InterFrame, "//List[@AttachedText = 'RPL']");
        public UFT_Editor BatchArea => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Batch Area']");
        public UFT_Button VerifyButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Verify']");
        public UFT_Button CertifyButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Certify']");
        public ITabControl MRTabControl => _UFT_InterFrame.Describe<ITabControl>(new TabControlDescription
        {
            TagName = @"m2rTabbedPanel"
        });
        public UFT_Table ParametersTable => new UFT_Table(_UFT_InterFrame, "//Table[@NativeClass = 'm2r.Table.m2rTableViewEdit']");
        public UFT_UiObject message => new UFT_UiObject(_UFT_InterFrame, "//Label[@TagName = 'm2rStatus']");

    }
    //OrderTrackingPFC_InterFrame
    public class OrderTrackingPFC_InterFrame : MOCMainInterFrame
    {
        public OrderTrackingPFC_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_UiObject UnitProcedureUiObject => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@TagName = 'UnitProcedure']");
        public IUiObject OperationUiObject1 => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription
        {
            NativeClass = @"PFCTree.View.Operation",
            Index = 1
        });
        public UFT_UiObject OperationUiObject => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@TagName = 'Operation']");
        public UFT_UiObject PhaseUiObject => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@TagName = 'Phase']");
        public IUiObject Script1 => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription
        {
            NativeClass = @"PFCTree.View.Script",
            Index = 0
        });
        public IUiObject Script2 => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription
        {
            NativeClass = @"PFCTree.View.Script",
            Index = 1
        });
        public IMenu ByPassCondition => _UFT_InterFrame.Describe<IMenu>(new MenuDescription {
				Label = @"Bypass condition"
			});
        public IMenu ExecutionScreenshots => _UFT_InterFrame.Describe<IMenu>(new MenuDescription
        {
            Label = @"Execution screenshots"
        });
        public IMenu ExecuteButton => _UFT_InterFrame.Describe<IMenu>(new MenuDescription
        {
            Label = @"Execute"
        });
        public IToolBar FinishedToolBar => _UFT_InterFrame.Describe<IToolBar>(new ToolBarDescription { });

    }
    public class PFCDesignApp_InterFrame : MOCMainInterFrame
    {
        public PFCDesignApp_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        #region RPLDesign
        public UFT_UiObject BeginNodeUiObject => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@NativeClass = 'PFCTree.View.BeginNode']");
        public UFT_UiObject ControlLinkUiObject => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@NativeClass = 'PFCTree.View.ControlLink']");
        public UFT_UiObject EndNodeUiObject => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@NativeClass = 'PFCTree.View.EndNode']");
        public UFT_UiObject RPLDesignForm => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@NativeClass = 'runtime.Design.recipeDesignForm']");
        public UFT_UiObject UnitProcedureUiObject => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@TagName = 'UnitProcedure']");
        public UFT_UiObject OperationUiObject => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@TagName = 'Operation']");
        public UFT_UiObject PhaseUiObject => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@TagName = 'Phase']");
        //public UFT_UiObject FirstControlLinkUiObject => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@NativeClass = 'PFCTree.View.ControlLink' and @Index = 0]");
        public IUiObject FirstLink => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription
        {
            NativeClass = @"PFCTree.View.ControlLink",
            Index = 1
        });
        public IUiObject StartLink => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription
        {
            NativeClass = @"PFCTree.View.ControlLink",
            Index = 0
        });
        public IUiObject UnitProcedureUiObject1 => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription
        {
            NativeClass = @"PFCTree.View.UnitProcedure",
            Index = 1
        });
        public IUiObject UnitProcedureUiObject0 => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription
        {
            NativeClass = @"PFCTree.View.UnitProcedure",
            Index = 0
        });
        public IUiObject OperationUiObject1 => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription
        {
            NativeClass = @"PFCTree.View.Operation",
            Index = 1
        });
        public IUiObject LinkUiObject => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription {
				NativeClass = @"PFCTree.View.Link",
				Index = 1
			});
        public IUiObject LinkUiObject2 => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription
        {
            NativeClass = @"PFCTree.View.Link",
            Index = 2
        });
        public IUiObject PhaseUiObject1 => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription {
				NativeClass = @"PFCTree.View.Phase",
				Index = 1
			});
        //PFCTree.View.Script
        public IUiObject ScriptUiObject => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription
        {
            NativeClass = @"PFCTree.View.Script",
            Index = 0
        });
        public IUiObject TransitionUiObject => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription
        {
            NativeClass = @"PFCTree.View.Transition",
            Index = 0
        });
        public IUiObject ParallelDivergent => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription
        {
            NativeClass = @"PFCTree.View.ParallelDivergent",
            Index = 0
        });
        #endregion
        #region BPLDesign
        public UFT_Button Action0 => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Actions: Action0']");
        public UFT_Button Concurrent_Action0 => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Concurrent actions: Thread0']");
        public UFT_Button Window0 => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Window: Window0']");
        public UFT_Button FinishWithYES0 => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Finish with YES: ReturnYes0']");
        public UFT_Button FinishWithNO0 => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Finish with NO: ReturnNo0']");
        public UFT_Button LinkNode0 => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = ' LinkNodeName0*']");
        #endregion
    }
    public class BPLList_InterFrame : MOCMainInterFrame
    {

        public BPLList_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button AddBPL_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Insert row' and @IsWrapped = 'True']");
        public UFT_Button LoadDesigner_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Load designer' and @IsWrapped = 'True']");
        public UFT_Button Refresh_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Refresh' and @IsWrapped = 'True']");
        public UFT_Table BPLList_Table => new UFT_Table(_UFT_InterFrame, "//Table[@AttachedText = 'Name\\s\\s']");
        public UFT_Button Paste_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Paste row' and @IsWrapped = 'True']");
        public UFT_Button Copy_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Copy row' and @IsWrapped = 'True']");
        public UFT_Button VerifyButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Verify']");
        public UFT_Button CertifyButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Certify']");
    }

    public class BPLData_InterFrame : MOCMainInterFrame
    {

        public BPLData_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button ConfirmChanges_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Confirm changes' and @IsWrapped = 'True']");
        public UFT_Button CancelChanges_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Cancel changes' and @IsWrapped = 'True']");
        public UFT_Editor BPLName => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Name']");
        public UFT_Editor BPLDescription => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Description']");
        public UFT_Button MakeUsable_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Make usable']");
        public ITabControl TabbedPaneControl => _UFT_InterFrame.Describe<ITabControl>(new TabControlDescription
        {
            TagName = @"m2rTabbedPanel"
        });
        public UFT_Button InsertRow_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Insert row' and @IsWrapped = 'True']");
        public UFT_Table BPListTable => new UFT_Table(_UFT_InterFrame, "//Table[@AttachedText = 'No.\\s\\s']");
        public IEditor NoEditor => _UFT_InterFrame.Describe<IEditor>(new EditorDescription {
				AttachedText = @"No.  ",
				IsWrapped = true,
                Index = 1
			});
        public ICheckBox WebCheckBox => _UFT_InterFrame.Describe<ICheckBox>(new CheckBoxDescription
        {
            AttachedText = @"No.  ",
            IsWrapped = true
        });
        public UFT_Button LoadDesigner_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Load designer' and @IsWrapped = 'True']");
        public UFT_Button VerifyButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Verify']");
        public UFT_Button CertifyButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Certify']");
    }
    public class Subdocuments_InterFrame : MOCMainInterFrame
    {

        public Subdocuments_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button ConfirmChanges_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Confirm changes' and @IsWrapped = 'True']");
        public UFT_Button CancelChanges_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Cancel changes' and @IsWrapped = 'True']");
        public UFT_Editor SubdocName => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Name']");
        public UFT_Editor SubdocDescription => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Description']");
        public UFT_Button LoadDesigner_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Load designer' and @IsWrapped = 'True']");
    }
    public class OrderList_InterFrame : MOCMainInterFrame
    {

        public OrderList_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button PlanFromRPL_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Plan from RPL']");
        public UFT_Button PlanFromMRecipe_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Plan from M.Recipe']");
        //public UFT_Button LoadDesigner_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Load designer' and @IsWrapped = 'True']");
        public UFT_Button Refresh_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Refresh' and @IsWrapped = 'True']");
        public UFT_Button Delete_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Delete row' and @IsWrapped = 'True']");
        public UFT_Button Filter_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Local filter' and @IsWrapped = 'True']");
        public UFT_Button Visible_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Select visible rows' and @IsWrapped = 'True']");
        public UFT_Editor Search => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Code\\s\\s']");
        public UFT_Button Cancel_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Cancel Order']");
        public UFT_Button CancelBP_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Cancel BP']");
        public UFT_Button DisableBP_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Disable']");
        public UFT_Button Activate_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Activate']");
        public UFT_Button Reactivate_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Reactivate']");
        public UFT_Button Archive_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Archive']");
        public UFT_Table OrderList_Table => new UFT_Table(_UFT_InterFrame, "//Table[@NativeClass = 'm2r.Table.m2rTableView*']");
        public ITabControl OrderTabControl => _UFT_InterFrame.Describe<ITabControl>(new TabControlDescription
        {
            TagName = @"m2rTabbedPanel"
        });

    }
    public class Execution_InterFrame : MOCMainInterFrame
    {

        public Execution_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        //850433 Batch
        public UFT_Button BatchRPLWriteRead_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Order850433:RPLArea(BATCH_RECORD_WRITE)']");
        //850438 Batch
        public UFT_Button BatchWriteRead_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Order850438:DefaultArea(BATCH_RECORD_WRITE)']");
        //850407 Batch
        public UFT_Button BatchAPIWriteRead_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Order850407:BatchAPI(BATCH_RECORD_WRITE)']");
        //849596 Batch
        public UFT_Button BatchDefault_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Order849596:Default area']");
        //850241 Batch
        public UFT_Button Batch_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Order850241:Batch same area with order']");
        public UFT_Button BatchSP_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Order850241SP:Specific batch-BatchAPI']");
        //788897
        public UFT_Button LaunchThread_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Launch Thread']");
        public UFT_Button setvar_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'set var']");
        //918007
        public UFT_Button WrinteBR_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Write BR']");
        public UFT_Editor Field1_Editor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Field 1:']");
        //771207
        public UFT_Label current_time => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'DL0']");
        //Soap
        public UFT_Button SOAP_CALL2_EX_Button => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'Button0']");
        public UFT_Button SOAP_CALL2_Button => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'Button3']");
        public UFT_Editor CheckField => new UFT_Editor(_UFT_InterFrame, "//Editor[@TagName = 'chkField']");
        public UFT_Button BPC_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'BPC']");//TC916388
        //1002790
        public UFT_Label check_value => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'DisplayLabel0']");
        public UFT_CheckBox check_box1 => new UFT_CheckBox(_UFT_InterFrame, "//CheckBox[@ObjectName = 'Check1']");
        public UFT_CheckBox check_box2 => new UFT_CheckBox(_UFT_InterFrame, "//CheckBox[@ObjectName = 'Check2']");
        public UFT_Button habilitar_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'habilitar']");
        public UFT_Button deshabilitar_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'deshabilitar']");



        public UFT_Button OK_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'OK']");
        public UFT_Button Cancel_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Cancel']");
        public UFT_Button Exit_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Exit']");

        public UFT_Editor UserIDEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'User ID:']");
        public IEditor PHActualEditor => _UFT_InterFrame.Describe<IEditor>(new EditorDescription
        {
            AttachedText = @"Actual",
            Index = 0
        });
        public IEditor TempActualEditor => _UFT_InterFrame.Describe<IEditor>(new EditorDescription
        {
            AttachedText = @"Actual",
            Index = 1
        });
        public UFT_Editor PasswordEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Password:']");
        public UFT_Table SelectBox => new UFT_Table(_UFT_InterFrame, "//Table[@TagName = 'chkTableView']");
        public UFT_Button ViewSOP_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'View SOP']");
        public UFT_Button Deviation_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Deviation']");
        public UFT_Button Print_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Print']");
        public UFT_List DeviationTypeList => new UFT_List(_UFT_InterFrame, "//List[@ObjectName = 'cmbDeviationType']");
        public UFT_Editor DeviationDesEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Introduction']");
        public UFT_List ProductionStoppedList => new UFT_List(_UFT_InterFrame, "//List[@ObjectName = 'cmbProductionStopped']");
        public UFT_Editor ProductionResponseEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'By signing, the deviation will be recorded in the system.']");
        public UFT_Button ViewDOCURL_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'View doc URL']");
        public UFT_Button LocalPdfView_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'local PDF view']");
        public UFT_Button SharedUrlview_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Shared URL view']");
        public UFT_Button InvalidError_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'invalid error message']");
    }
    //ExecuteMain_InterFrame
    public class ExecuteMain_InterFrame : MOCMainInterFrame
    {

        public ExecuteMain_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        //Soap
        public UFT_Button SOAP_CALL2_EX_Button => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'Button0']");
        public UFT_Button SOAP_CALL2_Button => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'Button3']");
        public UFT_Editor CheckField => new UFT_Editor(_UFT_InterFrame, "//Editor[@TagName = 'chkField']");
        public UFT_Button Cancel_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Cancel']");
        public UFT_Button BPC_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'BPC']");//TC916388
        
        //logevent
        public UFT_Button LogEventAutoButton => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Log Event Auto']");
        //Set Detail 915161
        public UFT_Button SetDetailButton => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Set Detail']");
        
        public UFT_Button OKButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = '\\s{0,}OK\\s{0,}']");
    }
    public class BPLExecutionMessageInterFrame : UFT_InterFrame
    {
        public BPLExecutionMessageInterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        //logevent
        public UFT_Button OKButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = '\\s{0,}OK\\s{0,}']");
        public UFT_Label message => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'OptionPane.label']");
    }

    public class PopUp_InterFrame : MOCMainInterFrame
    {

        public PopUp_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Table SelectSOP => new UFT_Table(_UFT_InterFrame, "//Table[@TagName = 'Select the SOP Document']");
        public UFT_Button Cancel_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Cancel']");
        //public UFT_Editor UserIDEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'User ID:']");
        //public UFT_Editor PasswordEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Password:']");
        //public UFT_Table SelectBox => new UFT_Table(_UFT_InterFrame, "//Table[@TagName = 'chkTableView']");
        //public UFT_Button ViewSOP_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'View SOP']");
        //public UFT_Button Deviation_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Deviation']");
        //public UFT_Button Print_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Print']");

    }

    public class EventLogList_InterFrame : UFT_InterFrame
    {
        public EventLogList_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        //logevent
        public UFT_Button Delete => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Delete Selected View' and @IsWrapped = 'True']");
        public UFT_Label message => new UFT_Label(_UFT_InterFrame, "//Label[@ObjectName = 'OptionPane.label']");
    }
  
    public class Confirmation_InterFrame : MOCMainInterFrame
    {

        public Confirmation_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public IButton YesButton => _UFT_InterFrame.Describe<IButton>(new ButtonDescription
        {
            AttachedText = @"	 	 	 	Yes	 	 	 	"
        });
        public IButton NoButton => _UFT_InterFrame.Describe<IButton>(new ButtonDescription
        {
            AttachedText = @"	 	 	 	No	 	 	 	"
        });

    }
    public class UserConfirmation_InterFrame : MOCMainInterFrame
    {

        public UserConfirmation_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        
        public IEditor PassWord => _UFT_InterFrame.Describe<IEditor>(new EditorDescription
        {
            AttachedText = @"Password"
        });
        public IEditor Comment => _UFT_InterFrame.Describe<IEditor>(new EditorDescription
        {
            TagName = @"m2rTextBox"
        });
        public IButton OKButton => _UFT_InterFrame.Describe<IButton>(new ButtonDescription
        {
            AttachedText = @"OK"
        });
        public IButton CancelButton => _UFT_InterFrame.Describe<IButton>(new ButtonDescription
        {
            AttachedText = @"Cancel"
        });

    }
    //Message_InterFrame
    public class Message_InterFrame : MOCMainInterFrame
    {

        public Message_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public ILabel Label => _UFT_InterFrame.Describe<ILabel>(new LabelDescription
        {
            ObjectName = @"OptionPane.label"
        });

        public IButton OKButton => _UFT_InterFrame.Describe<IButton>(new ButtonDescription
        {
            AttachedText = @"	 	 	 	OK	 	 	 	"
        });
    }
    public class WaitMessage_InterFrame : MOCMainInterFrame
    {

        public WaitMessage_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public ILabel Label => _UFT_InterFrame.Describe<ILabel>(new LabelDescription
        {
            NativeClass = @"javax.swing.JLabel"
        });

    }
}

