using HP.LFT.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;
using HP.LFT.SDK.StdWin;

namespace MES_APEM_UFT_Selenium_Auto.Product.WD
{
    public class SQLplusWindow : STD_Window
    {


        public SQLplusWindow(string xpath) : base(xpath)
        {
        }

        public selectSQLplusServer selectSQLplusServerDialog => new selectSQLplusServer(_STD_Window, "//Dialog[@Text = 'Select SQLplus Server']");

        public STD_Toolbar Toolbar => new STD_Toolbar(_STD_Window, "//Toolbar[@NativeClass = 'ToolbarWindow32']");

        public OpenFile_Dialog OpenFile_Dialog => new OpenFile_Dialog(_STD_Window, "//Dialog[@Text = 'Open']");

        public IUiObject ResultArea => _STD_Window.Describe<IWindow>(new WindowDescription
        {
            WindowClassRegExp = @"Afx:",
            IsChildWindow = true,
            Index = 0
        })
            .Describe<IUiObject>(new UiObjectDescription
            {
                NativeClass = @"RICHEDIT50W",
                Index = 1
            });

    }
    public class selectSQLplusServer : STD_Dialog
    {


        public selectSQLplusServer(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {

        }

        public IListBox ListBox => _STD_Dialog.Describe<IListBox>(new ListBoxDescription
        {
            NativeClass = @"ListBox"
        });
    }



    }
