using HP.LFT.SDK;
using System.Collections.Generic;
using HP.LFT.SDK.Java;
using System.Drawing;
using System.Drawing.Imaging;

namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class UFT_Dialog
    {
        public HP.LFT.SDK.Java.IDialog _UFT_Dialog
        {
            get;
            protected set;
        }
        public UFT_Dialog(ITestObject parentObject, string xpath)
        {
            if (parentObject == null)
                _UFT_Dialog = UFT_Xpath.GetDesktopDialog<HP.LFT.SDK.Java.IDialog>(xpath);
            else
                _UFT_Dialog = UFT_Xpath.GetDialogChildObject<IDialog>(parentObject, xpath);
        }
        public UFT_Dialog(string xpath)
        {
                _UFT_Dialog = UFT_Xpath.GetDesktopDialog<IDialog>(xpath);
                _UFT_Dialog.WaitUntilVisible();
        }
        public void SetActive(uint timeoutSeconds = 30)
        {
            _UFT_Dialog.Exists(timeoutSeconds);
            _UFT_Dialog.Activate();
        }

        public void HighLight()
        {
            _UFT_Dialog.Highlight();
        }

        public void Close()
        {
            _UFT_Dialog.Close();
        }

        public bool IsExist(uint TimeoutSecond = 3)
        {
            bool isExist = false;
            for (int i = 0; i < TimeoutSecond && isExist == false; i++)
            {
                isExist = _UFT_Dialog.Exists(1);
            }
            return isExist;
        }

        protected TChild Describe<TChild>(IDescription description) where TChild : class, ITestObject
        {
            return _UFT_Dialog.Describe<TChild>(description);
        }
        //public List<T> FindChildren<T>() where T : class, ITestObject
        //{
        //    return _UFT_Dialog.FindChildren<T>();
        //}
        public IButton OK => _UFT_Dialog.Describe<IButton>(new ButtonDescription
        {
            AttachedText = @"OK"
        });
        public IButton Cancel => _UFT_Dialog.Describe<IButton>(new ButtonDescription
        {
            AttachedText = @"Cancel"
        });
        public IButton YesButton => _UFT_Dialog.Describe<IButton>(new ButtonDescription
        {
            AttachedText = @"	 	 	 	Yes	 	 	 	"
        });
        public IButton NoButton => _UFT_Dialog.Describe<IButton>(new ButtonDescription
        {
            AttachedText = @"	 	 	 	No	 	 	 	"
        });
        public IButton OKButton => _UFT_Dialog.Describe<IButton>(new ButtonDescription
        {
            AttachedText = @"	 	 	 	OK	 	 	 	"
        });

        public IEditor UserID => _UFT_Dialog.Describe<IEditor>(new EditorDescription
        {
            AttachedText = @"User ID:"
        });

        public IEditor Password => _UFT_Dialog.Describe<IEditor>(new EditorDescription
        {
            AttachedText = @"Password:"
        });

        public IEditor Comment => _UFT_Dialog.Describe<IEditor>(new EditorDescription
        {
            AttachedText = @"Comment:"
        });
        public ILabel Lable => _UFT_Dialog.Describe<ILabel>(new LabelDescription
        {
            ObjectName = @"OptionPane.label"
        });
        //public ITable DataTable => _UFT_Dialog.Describe<ITable>(new TableDescription
        //{
        //    NativeClass = @"m2r.Table.m2rTableView"
        //});
        public UFT_Table DataTable => new UFT_Table(_UFT_Dialog, "//Table[@NativeClass = 'm2r.Table.m2rTableView']");
        public void GetSnapshot(string path)
        {
            Image image = _UFT_Dialog.GetSnapshot();
            image.Save(path, ImageFormat.Png);
        }
        public IEditor Reason => _UFT_Dialog.Describe<IEditor>(new EditorDescription
        {
            AttachedText = @"Reason"
        }); 
    }


    public class License_Dialog : UFT_Dialog
    {
        public License_Dialog(string xpath) : base(xpath)
        {
        }

        public ILabel LicenseLable => _UFT_Dialog.Describe<ILabel>(new LabelDescription
        {
            ObjectName = @"OptionPane.label",
            Index = 0
        });
    }

    public class AddReason_Dialog : UFT_Dialog
    {
        public AddReason_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        //public UFT_Editor Reason => new UFT_Editor(_UFT_Dialog, "//Editor[@AttachedText = 'Reason']");
    }
    //AvailableBPL_Dialog
    public class AvailableBPL_Dialog : UFT_Dialog
    {
        public AvailableBPL_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_List AvailableBPLList => new UFT_List(_UFT_Dialog, "//List[@AttachedText = 'Select Basic Phase Library']");
    }
    public class Confirm_Dialog : UFT_Dialog
    {
        public Confirm_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Editor PasswordEditor => new UFT_Editor(_UFT_Dialog, "//Editor[@TagName = 'Password']");
        
    }
    //RowsToView_Dialog
    public class RowsToView_Dialog : UFT_Dialog
    {
        public RowsToView_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public IRadioButton ViewAll => _UFT_Dialog.Describe<IRadioButton>(new RadioButtonDescription
        {
            AttachedText = @"View all"
        });
        //public UFT_Editor PasswordEditor => new UFT_Editor(_UFT_Dialog, "//Editor[@TagName = 'Password']");

    }
    //PrintReport_Dialog
    public class PrintReport_Dialog : UFT_Dialog
    {
        public PrintReport_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button Preview => new UFT_Button(_UFT_Dialog, "//Button[@Label = 'Preview']");
        public UFT_Button Print => new UFT_Button(_UFT_Dialog, "//Button[@Label = 'Print']");

    }
}
