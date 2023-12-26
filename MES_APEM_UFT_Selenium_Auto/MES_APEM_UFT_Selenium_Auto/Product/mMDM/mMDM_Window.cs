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
        public IToolBar MenuToolBar => _WinForms_Window.Describe<IToolBar>(new ToolBarDescription
        {
            ObjectName = @"menuStrip1"
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
            ObjectName = @"buttonTest",
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

        public IRadioButton SampleDatabase => _WinForms_Window.Describe<IRadioButton>(new RadioButtonDescription
        {
            WindowTitleRegExp = @"Sample database"
        });

        public IButton Create => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            Text = @"Create"
        });

    }
    public class mMDMDatabaseAdmin_Window : WinForms_Window
    {

        public mMDMDatabaseAdmin_Window(IWindow window) : base(window)
        {
        }
        public IWindow RepopulatingWindow => _WinForms_Window.Describe<IWindow>(new WindowDescription
        {
            WindowTitleRegExp = @"Repopulating Class Structures"
        });

        public IComboBox Workspaces => _WinForms_Window.Describe<IComboBox>(new ComboBoxDescription
        {
            ObjectName = @"comboBoxWorkspaces"
        });
        public ICheckBox Advance => _WinForms_Window.Describe<ICheckBox>(new CheckBoxDescription
        {
            ObjectName = @"checkBoxShowRegenerateViews"
        });
        public IButton Repopulate => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            ObjectName = @"buttonRegenerateDatabaseViews",
            Text = @"&Repopulate"
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

    //mMDM Editor
    public class mMDMEditor_Window : WinForms_Window
    {

        public mMDMEditor_Window(IWindow window) : base(window)
        {
        }
        public ITreeView mMDMTreeView => _WinForms_Window.Describe<ITreeView>(new TreeViewDescription
        {
            ObjectName = @"treeDefinitions"
        });
        public ITable mMDMTable => _WinForms_Window.Describe<ITable>(new TableDescription
        {
            ObjectName = @"ultraGrid1"
        });
        
        public IToolBar TableMenuToolBar => _WinForms_Window.Describe<IToolBar>(new ToolBarDescription
        {
            ObjectName = @"contextMenuStrip1"
        });
        public IToolBar MainMenuToolBar => _WinForms_Window.Describe<IToolBar>(new ToolBarDescription
        {
            ObjectName = @"menuStripMain"
        });

        public IButton SaveButton => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            Text = @" &Save"
        });


    }
    public class mMDMEditorOption_Window : WinForms_Window
    {

        public mMDMEditorOption_Window(IWindow window) : base(window)
        {
        }

        public ICheckBox SetActiveDateCheckBox => _WinForms_Window.Describe<ICheckBox>(new CheckBoxDescription
        {
            ObjectName = @"checkBoxCurrentDateOnStartup"
        });
    }
    public class mMDMEditorDefinition_Window : WinForms_Window
    {

        public mMDMEditorDefinition_Window(IWindow window) : base(window)
        {
        }

        public IEditField Name => _WinForms_Window.Describe<IEditField>(new EditFieldDescription
        {
            ObjectName = @"txtName"
        });


        public IEditor Description => _WinForms_Window.Describe<IEditor>(new EditorDescription
        {
            WindowTitleRegExp = @"Geographical Address Class",
            ObjectName = @"richTextBoxDescription"
        });

    }
}
