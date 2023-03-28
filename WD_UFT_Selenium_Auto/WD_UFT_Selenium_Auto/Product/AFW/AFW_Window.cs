using HP.LFT.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WD_UFT_Selenium_Auto.Library.UFTLibrary;

namespace WD_UFT_Selenium_Auto.Product.WD
{
    public class AFWMainWindow : STD_Window
    {


        public AFWMainWindow(string xpath) : base(xpath)
        {
        }

        public AFWSubWindow AFWSubWindow => new AFWSubWindow(_STD_Window, "//Window[@NativeClass = 'MMCChildFrm']");

        public STD_Toolbar toolbar => new STD_Toolbar(_STD_Window, "//Window[@WindowId = '4098']");

        public STD_Dialog AFWCloseDialog => new STD_Dialog("//Dialog[@Text = 'Microsoft Management Console']");

    }
    public class AFWSubWindow : STD_Window
    {


        public AFWSubWindow(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public STD_TreeView TreeView => new STD_TreeView(_STD_Window, "//Treeview[@NativeClass = 'SysTreeView32']");

        public STD_ListView ListView => new STD_ListView(_STD_Window, "//Treeview[@NativeClass = 'SysListView32']");


    }

}
