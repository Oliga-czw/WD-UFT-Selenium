using HP.LFT.SDK;
using HP.LFT.SDK.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.WD
{
    public class SLMMainWindow : WPF_Window
    {

        public SLMMainWindow()
        {
        }

        public SLMMainWindow(IWindow window) : base(window)
        {
        }


        public IImage SLMConfigurationWizard => _WPF_Window.Describe<IImage>(new ImageDescription
        {
            ObjectName = @"IconImage0"
        });

        //public AFWSubWindow AFWSubWindow => new AFWSubWindow(_STD_Window, "//Window[@NativeClass = 'MMCChildFrm']");



    }

    public class SLMConfigWindow : WPF_Window
    {

        public SLMConfigWindow()
        {
        }

        public SLMConfigWindow(IWindow window) : base(window)
        {
        }


        public IButton RemoveServer => _WPF_Window.Describe<IButton>(new ButtonDescription
        {
            HelpText = @"Click here to remove this server"
        });

        public IButton Apply => _WPF_Window.Describe<IButton>(new ButtonDescription
        {
            ObjectName = @"btnApply"
        });

        public IButton AddServer => _WPF_Window.Describe<IButton>(new ButtonDescription
        {
            ObjectName = @"Add Server"
        });

        public IEditField ServerEdit => _WPF_Window.Describe<IEditField>(new EditFieldDescription
        {
            FullType = @"edit",
            Index = 0
        });

        //public IUiObject message => _WPF_Window.Describe<IUiObject>(new UiObjectDescription
        //{
        //    AbsoluteLocation = { X = 678, Y = 654 }
        //});
        //public AFWSubWindow AFWSubWindow => new AFWSubWindow(_STD_Window, "//Window[@NativeClass = 'MMCChildFrm']");



    }
}
