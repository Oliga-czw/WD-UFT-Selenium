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
    public class mMDM_Window : WinForms_Window
    {


        //public Wizard_Window()
        //{
        //}

        public mMDM_Window(IWindow window) : base(window)
        {
        }

        public IWindow CreatingDatabaseWindow => _WinForms_Window.Describe<IWindow>(new WindowDescription
        {
            WindowTitleRegExp = @"Creating Database Schema"
        });

    }

    public class mMDMWizard_Window : WinForms_Window
    {


        //public Wizard_Window()
        //{
        //}

        public mMDMWizard_Window(IWindow window) : base(window)
        {
        }


        public IButton Next => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            ObjectName = @"btnNext"
        });

        public IRadioButton DirectDbConn => _WinForms_Window.Describe<IRadioButton>(new RadioButtonDescription
        {
            WindowTitleRegExp = @"Direct &database connection",
            ObjectName = @"radioButtonDatasourceDatabase"
        });

        public IButton Finish => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            Text = @"&Finish"
        });

        public IRadioButton SQLServer => _WinForms_Window.Describe<IRadioButton>(new RadioButtonDescription
        {
            WindowTitleRegExp = @"Microsoft SQL Server"
        });

        public IEditField ServerName => _WinForms_Window.Describe<IEditField>(new EditFieldDescription
        {
            NativeClass = @"Edit",
            Index = 1
        });

        public IEditField UserName => _WinForms_Window.Describe<IEditField>(new EditFieldDescription
        {
            ObjectName = @"txtUserName"
        });
        public IEditField Password => _WinForms_Window.Describe<IEditField>(new EditFieldDescription
        {
            ObjectName = @"txtPassword"
        });

        public IButton Test => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            Text = @"Test"
        });

        public IEditField DBName => _WinForms_Window.Describe<IEditField>(new EditFieldDescription
        {
            NativeClass = @"Edit",
            Index = 0
        });

        public IRadioButton EmptyDatabase => _WinForms_Window.Describe<IRadioButton>(new RadioButtonDescription
        {
            WindowTitleRegExp = @"Empty database"
        });

        public IButton Create => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            Text = @"Create"
        });

    }

    
    
}
