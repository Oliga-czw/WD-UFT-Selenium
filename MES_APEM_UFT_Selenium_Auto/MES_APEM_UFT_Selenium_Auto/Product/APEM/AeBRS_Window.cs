using HP.LFT.SDK;
using HP.LFT.SDK.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM
{
    public class AeBRSConfigure_Window : WinForms_Window
    {

        public AeBRSConfigure_Window(IWindow window) : base(window)
        {
        }
        public IEditField ServerName => _WinForms_Window.Describe<IEditField>(new EditFieldDescription
        {
            ObjectName = @"txtServerName"
        });
        public IButton OkButton => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            Text = @"Ok"
        });
    }

}
