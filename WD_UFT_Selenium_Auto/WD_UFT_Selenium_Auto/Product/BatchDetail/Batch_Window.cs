using HP.LFT.SDK;
using HP.LFT.SDK.StdWin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WD_UFT_Selenium_Auto.Library.UFTLibrary;

namespace WD_UFT_Selenium_Auto.Product.WD
{
    public class BatchMainWindow : STD_Window
    {


        public BatchMainWindow(string xpath) : base(xpath)
        {
        }


        public IUiObject Toolbar =>  _STD_Window.Describe<IUiObject>(new UiObjectDescription
        {
            WindowClassRegExp = @"Afx:",
            WindowId = 59392
        });

        public FindBatchWindow FindBatchWindow => new FindBatchWindow(_STD_Window, "//Window[@WindowTitleRegExp = 'Find Batch']");

        public STD_TreeView TreeView => new STD_TreeView(_STD_Window, "//Treeview[@NativeClass = 'SysTreeView32']");

        public STD_ListView ListView => new STD_ListView(_STD_Window, "//Treeview[@NativeClass = 'SysListView32' and @Index = '0']");

        public BatchCharacteristic_Dialog BatchCharacteristicDialog => new BatchCharacteristic_Dialog("//Dialog[@Text = 'Modify Characteristic']");

        public STD_Dialog AFWCloseDialog => new STD_Dialog("//Dialog[@Text = 'Microsoft Management Console']");

        public Option_Dialog OptionDialog => new Option_Dialog(_STD_Window, "//Window[@Text = 'Options']");


    }


    
    
}



public class FindBatchWindow : STD_Window
{


    public FindBatchWindow(ITestObject parentObject, string xpath) : base(parentObject, xpath)
    {
    }

    public IUiObject Textbox => _STD_Window.Describe<IUiObject>(new UiObjectDescription
    {
        WindowClassRegExp = @"ThunderRT6TextBox"
    });

    public IUiObject OK => _STD_Window.Describe<IUiObject>(new UiObjectDescription
    {
        Text = @"OK"
    });


}


