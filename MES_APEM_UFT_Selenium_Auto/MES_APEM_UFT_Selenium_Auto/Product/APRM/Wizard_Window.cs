using HP.LFT.SDK;
using HP.LFT.SDK.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.WD
{
    public class Wizard_Window : WinForms_Window
    {
    

        //public Wizard_Window()
        //{
        //}

        public Wizard_Window(IWindow window) : base(window)
        {
        }


        public IButton next => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            WindowTitleRegExp = @"&Next >"
        });
        //public AFWSubWindow AFWSubWindow => new AFWSubWindow(_STD_Window, "//Window[@NativeClass = 'MMCChildFrm']");
        public IButton Finish => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            WindowTitleRegExp = @"&Finish"
        });

        public IButton btnClose => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            ObjectName = @"_cmdNav_4"
        });

            
        public IRadioButton lstAPRM => _WinForms_Window.Describe<IRadioButton>(new RadioButtonDescription
        {
            WindowTitleRegExp = @"Aspen Production Record Manager"
        });

        public IEditField DBLocation => _WinForms_Window.Describe<IEditField>(new EditFieldDescription
        {
            ObjectName = @"txtDBLocation"
        });

        public IEditField BatchEnterPassword => _WinForms_Window.Describe<IEditField>(new EditFieldDescription
        {
            ObjectName = @"txtPassword1"
        });

        public IEditField BatchVerifyPassword => _WinForms_Window.Describe<IEditField>(new EditFieldDescription
        {
            ObjectName = @"txtPassword2"
        });
        public IEditField DomainUser => _WinForms_Window.Describe<IEditField>(new EditFieldDescription
        {
            ObjectName = @"txtDomainUser"
        });

        public ICheckBox chkCreateADSA => _WinForms_Window.Describe<ICheckBox>(new CheckBoxDescription
        {
            ObjectName = @"chkCreateADSA"
        });
    }

            


}
