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
    public class WorkstationBP_InterFrame : MOCMainInterFrame
    {
        public WorkstationBP_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public IEditor CodeEditor => _UFT_InterFrame.Describe<IEditor>(new EditorDescription
        {
            AttachedText = @"Code  "
        });
        //public UFT_Editor CodeEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Code  ']");
        public ITable CodeTable => _UFT_InterFrame.Describe<ITable>(new EditorDescription
        {
            AttachedText = @"Code  "
        });
        //public UFT_Table CodeTable => new UFT_Table(_UFT_InterFrame, "//Editor[@AttachedText = 'Code  ']");
        public IButton Filterbutton => _UFT_InterFrame.Describe<IButton>(new ButtonDescription
        {
            Label = @"Local filter",
            IsWrapped = true
        }); 
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
        public ITable RPLListTable => _UFT_InterFrame.Describe<ITable>(new EditorDescription
        {
            AttachedText = @"Name  "
        });
        public UFT_Button VerifyButton => new UFT_Button(_UFT_InterFrame, "//Button[@AttachedText = 'Verify']");
        public IButton Filterbutton => _UFT_InterFrame.Describe<IButton>(new ButtonDescription
        {
            Label = @"Local filter",
            IsWrapped = true
        });
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
        public IUiObject UnitProcedureUiObject1 => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription
        {
            NativeClass = @"PFCTree.View.UnitProcedure",
            Index = 1
        });
        public IUiObject OperationUiObject1 => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription
        {
            NativeClass = @"PFCTree.View.Operation",
            Index = 1
        });
        public IUiObject LinkUiObject => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription {
				NativeClass = @"PFCTree.View.Link",
				Index = 2
			});
        public IUiObject PhaseUiObject1 => _UFT_InterFrame.Describe<IUiObject>(new UiObjectDescription {
				NativeClass = @"PFCTree.View.Phase",
				Index = 1
			});
    }
    public class BPLList_InterFrame : MOCMainInterFrame
    {

        public BPLList_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button AddBPL_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Insert row' and @IsWrapped = 'True']");
    }
    public class BPLData_InterFrame : MOCMainInterFrame
    {

        public BPLData_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button ConfirmChanges_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Confirm changes' and @IsWrapped = 'True']");
        public UFT_Editor BPLName => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Name']");
        public UFT_Editor BPLDescription => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Description']");
        public UFT_Button MakeUsable_Button => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Make usable']");
    }

}

