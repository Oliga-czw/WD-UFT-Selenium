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
    public class BatchQueryToolWindow : STD_Window
    {


        public BatchQueryToolWindow(string xpath) : base(xpath)
        {
        }

      

        public STD_ListView ListView => new STD_ListView(_STD_Window, "//ListView[@NativeClass = 'ListView20WndClass']");

        public STD_Dialog NoDefaultData_Dialog => new STD_Dialog(_STD_Window, "//Dialog[@Text = 'BatchQueryTool']");

        public STD_Dialog Save_Dialog => new STD_Dialog(_STD_Window, "//Dialog[@Text = 'Save Current Query']");
        public ConfigQueryWindow ConfigQueryWindow => new ConfigQueryWindow(_STD_Window, "//Window[@Text = 'Configure query.*']");

        public OptionsWindow OptionsWindow => new OptionsWindow(_STD_Window, "//Window[@Text = 'Options']");

    }


    public class ConfigQueryWindow : STD_Window
    {


        public ConfigQueryWindow(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {

        }
        //advance
        public IUiObject Start => _STD_Window.Describe<IUiObject>(new UiObjectDescription
        {
            WindowClassRegExp = @"ThunderRT6TextBox",
            WindowId = 28
        });
        public IUiObject End => _STD_Window.Describe<IUiObject>(new UiObjectDescription
        {
            WindowClassRegExp = @"ThunderRT6TextBox",
            WindowId = 29
        });
        //time
        public IUiObject SpecifyTimeCheckBox => _STD_Window.Describe<IUiObject>(new UiObjectDescription
        {
            Text = @"Specify a time range"
        });

        public IUiObject OK => _STD_Window.Describe<IUiObject>(new UiObjectDescription
        {
            Text = @"&OK"
        });
    }
    public class OptionsWindow : STD_Window
    {


        public OptionsWindow(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {

        }

        public IUiObject BatchArea => _STD_Window.Describe<IUiObject>(new UiObjectDescription
        {
            WindowClassRegExp = @"ThunderRT6ComboBox",
            WindowId = 18
        });
       
        public IUiObject OK => _STD_Window.Describe<IUiObject>(new UiObjectDescription
        {
            Text = @"&OK"
        });

        public IUiObject SetasDefault => _STD_Window.Describe<IUiObject>(new UiObjectDescription
        {
            Text = @"Set as Default"
        });


    }
}
