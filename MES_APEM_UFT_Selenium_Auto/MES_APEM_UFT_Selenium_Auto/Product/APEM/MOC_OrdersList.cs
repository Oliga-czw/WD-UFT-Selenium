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
    public class MOCOrdersInterFrame : MOCMainInterFrame
    {

        public MOCOrdersInterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button  PlanfromRPL=> new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Plan from RPL']");
        


    }
    public class OrderPlan_Dialog : UFT_Dialog
    {
        public OrderPlan_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public IEditor CodeEditor => _UFT_Dialog.Describe<IEditor>(new EditorDescription
        {
            NativeClass = @"m2r.Card.m2rTextField",
            Index = 0
        });

        public IEditor DescriptionEditor => _UFT_Dialog.Describe<IEditor>(new EditorDescription
        {
            NativeClass = @"m2r.Card.m2rTextField",
            Index = 1
        });
        public IList RPLList => _UFT_Dialog.Describe<IList>(new ListDescription
        {
            TagName = @"m2rMultiComboBox",
            NativeClass = @"m2r.Card.m2rMultiComboBox"
        });
        public IEditor POEditor => _UFT_Dialog.Describe<IEditor>(new EditorDescription
        {
            NativeClass = @"m2r.Card.m2rTextField",
            Index = 2
        });
        public IEditor POStepEditor => _UFT_Dialog.Describe<IEditor>(new EditorDescription
        {
            NativeClass = @"m2r.Card.m2rTextField",
            Index = 3
        });

        public IEditor ArticleEditor => _UFT_Dialog.Describe<IEditor>(new EditorDescription
        {
            NativeClass = @"m2r.Card.m2rTextField",
            Index = 4
        });

        public IEditor BatchEditor => _UFT_Dialog.Describe<IEditor>(new EditorDescription
        {
            NativeClass = @"m2r.Card.m2rTextField",
            Index = 5
        });

        public IEditor QuantityEditor => _UFT_Dialog.Describe<IEditor>(new EditorDescription
        {
            NativeClass = @"m2r.Card.m2rTextField",
            Index = 6
        });

        public IEditor Quantity_unitEditor => _UFT_Dialog.Describe<IEditor>(new EditorDescription
        {
            NativeClass = @"m2r.Card.m2rTextField",
            Index = 7
        });
        public IEditor DateEditor => _UFT_Dialog.Describe<IEditor>(new EditorDescription
        {
            NativeClass = @"m2r.Card.m2rTextField",
            Index = 8
        });

        public IEditor END_DateEditor => _UFT_Dialog.Describe<IEditor>(new EditorDescription
        {
            NativeClass = @"m2r.Card.m2rTextField",
            Index = 9
        });

        public IEditor Batch_AreaEditor => _UFT_Dialog.Describe<IEditor>(new EditorDescription
        {
            NativeClass = @"m2r.Card.m2rTextField",
            Index = 12
        });

        public IList WorkcenterList => _UFT_Dialog.Describe<IList>(new ListDescription
        {
            NativeClass = @"Order.chkOrderCard$4"
        });
        public ICheckBox Auto_ActivateCheckBox => _UFT_Dialog.Describe<ICheckBox>(new CheckBoxDescription
        {
            TagName = @"m2rCheck"
        });
    }


    

    }