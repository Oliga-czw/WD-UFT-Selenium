using HP.LFT.SDK;
using HP.LFT.SDK.StdWin;
using System;
using System.Collections.Generic;


namespace WD_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class STD_Dialog
    {
        public IDialog _STD_Dialog
        {
            get;
            protected set;
        }
        public STD_Dialog(ITestObject parentObject, string xpath)
        {
            if (parentObject == null)
                _STD_Dialog = STD_Xpath.GetDesktopDialog<IDialog>(xpath);
            else
                _STD_Dialog = STD_Xpath.GetDialogChildObject<IDialog>(parentObject, xpath);
        }
        public STD_Dialog(string xpath)
        {
            _STD_Dialog = STD_Xpath.GetDesktopDialog<IDialog>(xpath);
            _STD_Dialog.WaitUntilVisible();
        }
        public void SetActive(uint timeoutSeconds = 30)
        {
            _STD_Dialog.Exists(timeoutSeconds);
            _STD_Dialog.Activate();
        }

        public void HighLight()
        {
            _STD_Dialog.Highlight();
        }

        public void Close()
        {
            _STD_Dialog.Close();
        }

        public bool IsExist(uint TimeoutSecond = 3)
        {
            bool isExist = false;
            for (int i = 0; i < TimeoutSecond && isExist == false; i++)
            {
                isExist = _STD_Dialog.Exists(1);
            }
            return isExist;
        }

        protected TChild Describe<TChild>(IDescription description) where TChild : class, ITestObject
        {
            return _STD_Dialog.Describe<TChild>(description);
        }
        //public List<T> FindChildren<T>() where T : class, ITestObject
        //{
        //    return _STD_Dialog.FindChildren<T>();
        //}
        

        public IButton OK => _STD_Dialog.Describe<IButton>(new ButtonDescription
        {
            NativeClass = @"Button",
            Text = @"OK"
        });

        public IButton Yes => _STD_Dialog.Describe<IButton>(new ButtonDescription
        {
            NativeClass = @"Button",
            Text = @"&Yes"
        });
        public IButton NO => _STD_Dialog.Describe<IButton>(new ButtonDescription
        {
            NativeClass = @"Button",
            Text = @"&No"
        });
        public IButton Cancel => _STD_Dialog.Describe<IButton>(new ButtonDescription
        {
            NativeClass = @"Button",
            Text = @"Cancel"
        });
        public IEditField nameEditField => _STD_Dialog.Describe<IEditField>(new EditFieldDescription
        {
            AttachedText = @"Name:",
            NativeClass = @"Edit"
        });
        public IEditField descriptionEditField => _STD_Dialog.Describe<IEditField>(new EditFieldDescription
        {
            AttachedText = @"Description:",
            NativeClass = @"Edit"
        });
        
    }
    public class Login_Dialog : STD_Dialog
    {
        public Login_Dialog(string xpath) : base(xpath)
        {
        }
        public IButton OK => _STD_Dialog.Describe<IButton>(new ButtonDescription
        {
            NativeClass = @"Button",
            Text = @"&OK"
        });
    }

        public class Property_Dialog : STD_Dialog
    {
        public Property_Dialog(string xpath) : base(xpath)
        {
        }
        public ITabControl tab => _STD_Dialog.Describe<ITabControl>(new TabControlDescription
        {
            NativeClass = @"SysTabControl32"
        });
        public IButton Add => _STD_Dialog.Describe<IButton>(new ButtonDescription
        {
            NativeClass = @"Button",
            Text = @"Add..."
        });
        public IButton Remove => _STD_Dialog.Describe<IButton>(new ButtonDescription
        {
            NativeClass = @"Button",
            Text = @"Remove"
        });


        public STD_ListView ListView => new STD_ListView(_STD_Dialog, "//Treeview[@NativeClass = 'SysListView32']");

    }

    public class SelectUser_Dialog : STD_Dialog
    {
        public SelectUser_Dialog(string xpath) : base(xpath)
        {
        }
        public IUiObject inputbox => _STD_Dialog.Describe<IUiObject>(new UiObjectDescription
        {
            WindowClassRegExp = @"RICHEDIT50W"
        });
         
        //public IButton OK_selectUser => _STD_Dialog.Describe<IButton>(new ButtonDescription
        //{
        //    NativeClass = @"Button",
        //    Text = @"OK"
        //});
    }

    }
