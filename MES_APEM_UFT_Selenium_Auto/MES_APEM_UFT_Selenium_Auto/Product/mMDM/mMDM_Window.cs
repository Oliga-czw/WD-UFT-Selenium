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
    //mMDM Admin
    public class mMDMAdmin_Window : WinForms_Window
    {

        public mMDMAdmin_Window(IWindow window) : base(window)
        {
        }

        public IWindow CreatingDatabaseWindow => _WinForms_Window.Describe<IWindow>(new WindowDescription
        {
            WindowTitleRegExp = @"Creating Database Schema"
        });

    }
    public class AdminWizard_Window : WinForms_Window
    {

        public AdminWizard_Window(IWindow window) : base(window)
        {
        }

        public IRadioButton DirectDbConn => _WinForms_Window.Describe<IRadioButton>(new RadioButtonDescription
        {
            WindowTitleRegExp = @"Direct &database connection",
            ObjectName = @"radioButtonDatasourceDatabase"
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

    //mMDM BulkLoad
    public class mMDMBulkLoad_Window : WinForms_Window
    {

        public mMDMBulkLoad_Window(IWindow window) : base(window)
        {
        }

        public IWindow CreatingDatabaseWindow => _WinForms_Window.Describe<IWindow>(new WindowDescription
        {
            WindowTitleRegExp = @"Creating Database Schema"
        });

        public IButton SaveButton => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            Text = @" &Save"
        });

            
    }
    public class BulkLoadWizard_Window : WinForms_Window
    {

        public BulkLoadWizard_Window(IWindow window) : base(window)
        {
        }

        public IRadioButton ImportDataFromOneOrMoreBulkLoadFiles => _WinForms_Window.Describe<IRadioButton>(new RadioButtonDescription
        {
            WindowTitleRegExp = @"&Import data from one or more Bulk Load files into an mMDM datastore.",
            ObjectName = @"radioButtonLoadData"
        });

        public IButton Select => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            Text = @"&Select..."
        });

        public IRadioButton SQLServer => _WinForms_Window.Describe<IRadioButton>(new RadioButtonDescription
        {
            WindowTitleRegExp = @"Microsoft SQL Server"
        });

    }
    public class AspenWorkSpace_Window : WinForms_Window
    {

        public AspenWorkSpace_Window(IWindow window) : base(window)
        {
        }

        public IButton Advanced => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            ObjectName = @"buttonAdvanced"
        });
        public ICheckBox ConnectToASpecificWorkspace => _WinForms_Window.Describe<ICheckBox>(new CheckBoxDescription
        {
            Text = @"Connect to a specific Workspace:"
        });

        public IComboBox WorkspaceComboBox => _WinForms_Window.Describe<IComboBox>(new ComboBoxDescription
        {
            ObjectName = @"comboBoxWorkspace"
        });

        
    }
    public class BulkLoadImportWizard_Window : WinForms_Window
    {

        public BulkLoadImportWizard_Window(IWindow window) : base(window)
        {
        }

        public IRadioButton importDataFromOneOrMoreBulkLoadFiles => _WinForms_Window.Describe<IRadioButton>(new RadioButtonDescription
        {
            WindowTitleRegExp = @"&Import data from one or more Bulk Load files into an mMDM datastore.",
            ObjectName = @"radioButtonLoadData"
        });

        public IButton Add => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            Text = @"Add..."
        });

        public IRadioButton SQLServer => _WinForms_Window.Describe<IRadioButton>(new RadioButtonDescription
        {
            WindowTitleRegExp = @"Microsoft SQL Server"
        });

    }
}
