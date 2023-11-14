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
    }
    public class RPLDesign_InterFrame : MOCMainInterFrame
    {
        public RPLDesign_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button AddRPL_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Insert row' and @IsWrapped = 'True']");
        public IEditor NameEditor => _UFT_InterFrame.Describe<IEditor>(new EditorDescription
        {
            AttachedText = @"Name  "
        });

        //public UFT_Editor CodeEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Code  ']");
        public UFT_Button VerifyButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Verify']");
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
        public ITabControl RPLTabControl => _UFT_InterFrame.Describe<ITabControl>(new TabControlDescription
        {
            TagName = @"m2rTabbedPanel"
        });
        public UFT_Button SelectBPL_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Insert row' and @IsWrapped = 'True']");
        public UFT_Button LoadDesigner_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Load designer' and @IsWrapped = 'True']");
        public UFT_Button VerifyButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Verify']");
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
		

    }
    public class PFCDesignApp_InterFrame : MOCMainInterFrame
    {
        public PFCDesignApp_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_UiObject BeginNodeUiObject => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@NativeClass = 'PFCTree.View.BeginNode']");
        public UFT_UiObject ControlLinkUiObject => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@NativeClass = 'PFCTree.View.ControlLink']");
        public UFT_UiObject EndNodeUiObject => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@NativeClass = 'PFCTree.View.EndNode']");
        public UFT_UiObject ParallelDivergentUiObject => new UFT_UiObject(_UFT_InterFrame, "//UiObject[@NativeClass = 'PFCTree.View.ParallelDivergent']");
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
        public IUiObject PhaseUiObject1 => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription {
				NativeClass = @"PFCTree.View.Phase",
				Index = 1
			});
        public IUiObject ParallelDivergent => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription
        {
            NativeClass = @"PFCTree.View.ParallelDivergent"
        });
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
        public UFT_Button AddBP_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Insert row' and @IsWrapped = 'True']");
        public UFT_Table BPListTable => new UFT_Table(_UFT_InterFrame, "//Table[@AttachedText = 'No.\\s\\s']");
        public IEditor NoEditor => _UFT_InterFrame.Describe<IEditor>(new EditorDescription {
				AttachedText = @"No.  ",
				IsWrapped = true
			});
        public ICheckBox WebCheckBox => _UFT_InterFrame.Describe<ICheckBox>(new CheckBoxDescription
        {
            AttachedText = @"No.  ",
            IsWrapped = true
        });
        public UFT_Button LoadDesigner_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Load designer' and @IsWrapped = 'True']");
    }
    public class Execution_InterFrame : MOCMainInterFrame
    {

        public Execution_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button SOAP_CALL2_EX_Button => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'Button0']");
        public UFT_Editor CheckField => new UFT_Editor(_UFT_InterFrame, "//Editor[@TagName = 'chkField']");

        public UFT_Button OK_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'OK']");
        public UFT_Button Cancel_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Cancel']");
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
    }
    //ExecuteMain_InterFrame
    public class ExecuteMain_InterFrame : MOCMainInterFrame
    {

        public ExecuteMain_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button SOAP_CALL2_EX_Button => new UFT_Button(_UFT_InterFrame, "//Button[@ObjectName = 'Button0']");
        public UFT_Editor CheckField => new UFT_Editor(_UFT_InterFrame, "//Editor[@TagName = 'chkField']");
        public UFT_Button Cancel_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Cancel']");
        //logevent
        public UFT_Button LogEventAutoButton => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Log Event Auto']");
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
}

