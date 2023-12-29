using HP.LFT.SDK;
using HP.LFT.SDK.StdWin;
using System;
using System.Collections.Generic;


namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
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

        public IStatic StaticText => _STD_Dialog.Describe<IStatic>(new StaticDescription
        {
            NativeClass = @"Static"
        });



    }

    public class OpenFile_Dialog : STD_Dialog
    {
        public OpenFile_Dialog(string xpath) : base(xpath)
        {
        }
        public OpenFile_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public IButton Open => _STD_Dialog.Describe<IButton>(new ButtonDescription
        {
            NativeClass = @"Button",
            Text = @"&Open"
        });
        public IEditField FileName => _STD_Dialog.Describe<IEditField>(new EditFieldDescription
        {
            NativeClass = @"Edit"
        });

    }


    #region afw dialog
    public class Login_Dialog : STD_Dialog
    {
        public Login_Dialog(string xpath) : base(xpath)
        {
        }
        public new IButton OK => _STD_Dialog.Describe<IButton>(new ButtonDescription
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
    #endregion


    #region APRM batch dialog
    //batch data
    public class BatchCharacteristic_Dialog : STD_Dialog
    {
        public BatchCharacteristic_Dialog(string xpath) : base(xpath)
        {
        }
        public IEditor Value => _STD_Dialog.Describe<IEditor>(new EditorDescription
        {
            AttachedText = @"Value:",
            Index = 0
        });

        public ICalendar ValueCalendar => _STD_Dialog.Describe<ICalendar>(new CalendarDescription
        {
            NativeClass = @"SysDateTimePick32",
            Index = 0
        });
        //public new IButton Cancel => _STD_Dialog.Describe<IButton>(new ButtonDescription
        //{
        //    Text = @"Cancel"
        //});

    }
    //batch option
    public class Option_Dialog : STD_Dialog
    {
        public Option_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public IComboBox DataSource => _STD_Dialog.Describe<IComboBox>(new ComboBoxDescription
        {
            NativeClass = @"ComboBox",
            AttachedText = @"Production Record Manager data &source:"
        });

        public IComboBox DataArea => _STD_Dialog.Describe<IComboBox>(new ComboBoxDescription
        {
            NativeClass = @"ComboBox",
            AttachedText = @"&Area:"
        });

        public IButton SetAsDefaultButton => _STD_Dialog.Describe<IButton>(new ButtonDescription
        {
            NativeClass = @"Button",
            Text = @"Set as &default"
        });


    }
    //aprm admin
    public class Open_Dialog : STD_Dialog
    {
        public Open_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public IEditField Filename => _STD_Dialog.Describe<IEditField>(new EditFieldDescription
        {
            NativeClass = @"Edit",
            AttachedText = @"File &name:"
        });

    }




    #endregion

    #region apem dialog
    //apem admin
    public class ExtractorProperty_Dialog : STD_Dialog
    {
        public ExtractorProperty_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public IComboBox DataSource => _STD_Dialog.Describe<IComboBox>(new ComboBoxDescription
        {
            NativeClass = @"ComboBox",
            AttachedText = @"APRM Data Source:"
        });

        public IComboBox DataArea => _STD_Dialog.Describe<IComboBox>(new ComboBoxDescription
        {
            NativeClass = @"ComboBox",
            WindowId = 315
        });

        public IButton SetupButton => _STD_Dialog.Describe<IButton>(new ButtonDescription
        {
            NativeClass = @"Button",
            Text = @"Setup"
        });

        public IButton TestConnectionButton => _STD_Dialog.Describe<IButton>(new ButtonDescription
        {
            NativeClass = @"Button",
            Text = @"Test Connection"
        });

    }
    public class CreateArchive_Dialog : STD_Dialog
    {
        public CreateArchive_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
      

        public IEditField ArchiveName => _STD_Dialog.Describe<IEditField>(new EditFieldDescription
        {
            AttachedText = @"Archive Name:"
        });

        public IEditField Comments => _STD_Dialog.Describe<IEditField>(new EditFieldDescription
        {
            AttachedText = @"Comments:"
        });
        
    }
    public class SelectionConditions_Dialog : STD_Dialog
    {
        public SelectionConditions_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }


        public IEditField OrderCode => _STD_Dialog.Describe<IEditField>(new EditFieldDescription
        {
            AttachedText = @"Value:",
            Index = 0
        });


    }
    public class ArchiveBuilt_Dialog : STD_Dialog
    {
        public ArchiveBuilt_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }


        public ICheckBox DeleteAll => _STD_Dialog.Describe<ICheckBox>(new CheckBoxDescription
        {
            Text = @"Delete all orders from the Production Execution database NOW."
        });
        public IEditor Comments => _STD_Dialog.Describe<IEditor>(new EditorDescription
        {
            AttachedText = @"Comments:"
        });

    }
    public class ArchiveRestore_Dialog : STD_Dialog
    {
        public ArchiveRestore_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }


        
        public IEditor Comments => _STD_Dialog.Describe<IEditor>(new EditorDescription
        {
            NativeClass = @"Edit"
        });

    }
    #endregion
    #region wizard dialog
    public class WizardData_Dialog : STD_Dialog
    {
        public WizardData_Dialog(string xpath) : base(xpath)
        {
        }
        #region provider
        public IListView DBDataList => _STD_Dialog.Describe<IListView>(new ListViewDescription
        {
            AttachedText = @"Select the data you want to connect to:"
        });


        public HP.LFT.SDK.UIAPro.IText DBserver => _STD_Dialog.Describe<HP.LFT.SDK.UIAPro.IList>(new HP.LFT.SDK.UIAPro.ListDescription
        {
            Name = @"Select the data you want to connect to:",
            Path = @"Window;Pane;List"
        })
            .Describe<HP.LFT.SDK.UIAPro.IListItem>(new HP.LFT.SDK.UIAPro.ListItemDescription
            {
                Name = @"Microsoft OLE DB Provider for SQL Server"
            })
            .Describe<HP.LFT.SDK.UIAPro.IText>(new HP.LFT.SDK.UIAPro.TextDescription
            {
                Name = @"Microsoft OLE DB Provider for SQL Server"
            });

        public IButton next => _STD_Dialog.Describe<IButton>(new ButtonDescription
        {
            Text = @"&Next >>"
        });

        #endregion

        #region connection
        public IEditField servername => _STD_Dialog.Describe<IEditField>(new EditFieldDescription
        {
            NativeClass = @"Edit",
            AttachedText = @"1. Select or enter a s&erver name:"

        });
        public IEditField username => _STD_Dialog.Describe<IEditField>(new EditFieldDescription
        {
            NativeClass = @"Edit",
            AttachedText = @"User &name:"
        });

        public IEditField password => _STD_Dialog.Describe<IEditField>(new EditFieldDescription
        {
            NativeClass = @"Edit",
            AttachedText = @"&Password:"
        });

        public ICheckBox savingPWD => _STD_Dialog.Describe<ICheckBox>(new CheckBoxDescription
        {
            Text = @"Allow &saving password"
        });


        public IButton TestConnection => _STD_Dialog.Describe<IButton>(new ButtonDescription
        {
            Text = @"&Test Connection"
        });

        public IEditField DataBase => _STD_Dialog.Describe<IEditField>(new EditFieldDescription
        {
            NativeClass = @"Edit",
            WindowId = 1001,
            Index = 1
        });



        public ITabControl TabControl => _STD_Dialog.Describe<ITabControl>(new TabControlDescription
        {
            NativeClass = @"SysTabControl32"
        });



        #endregion


    }



    #endregion


    #region mMDM dialog
    //apem admin
    public class Success_Dialog : STD_Dialog
    {
        public Success_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public IStatic Message => _STD_Dialog.Describe<IStatic>(new StaticDescription
        {
            NativeClass = @"Static"
        });


    }
    #endregion

}
