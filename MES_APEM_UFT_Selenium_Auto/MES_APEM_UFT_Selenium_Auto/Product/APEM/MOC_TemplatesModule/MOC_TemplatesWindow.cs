using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.MOC_TemplatesModule
{
    public class MOC_TemplatesWindow : UFT_JavaWindow
    {

        public MOC_TemplatesWindow(string xpath) : base(xpath)
        {
        }

        public UFT_Menu Templates => new UFT_Menu(_UFT_Window, "//Menu[@Label = 'Templates' and @ObjectName ='menu_Function']");
        public UFT_Button Check_Template => new UFT_Button(_UFT_Window, "//Button[@Label = 'Check Template' and @IsWrapped = 'True']");
        public UFT_Button Execute_Template => new UFT_Button(_UFT_Window, "//Button[@Label = 'Execute Template' and @IsWrapped = 'True']");
        public UFT_Button RPL => new UFT_Button(_UFT_Window, "//Button[@Label = 'RPL' and @IsWrapped = 'True']");
        public UFT_Dialog LogWindow => new UFT_Dialog(_UFT_Window, "//Dialog[@TagName = 'Log window']");
        public FileImport_Dialog FileImport => new FileImport_Dialog(_UFT_Window, "//Dialog[@TagName = 'File Import']");
        public FileExport_Dialog FileExport => new FileExport_Dialog(_UFT_Window, "//Dialog[@TagName = 'File Export']");
        public TemplateExport_Dialog TemplateExport => new TemplateExport_Dialog(_UFT_Window, "//Dialog[@TagName = 'Template Export']");
        public RPLList_InterFrame RPLListInterFrame => new RPLList_InterFrame(_UFT_Window, "//InterFrame[@Label = 'Recipe Procedure Logic List']");

    }
    public class FileImport_Dialog : UFT_Dialog
    {
        public FileImport_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Button HomeButton => new UFT_Button(_UFT_Dialog, "//Button[@Label = 'Home']");
        public UFT_List LookInList => new UFT_List(_UFT_Dialog, "//List[@AttachedText = 'Look In:' and @NativeClass = 'sun.swing.FilePane$4']");
        public UFT_Button FileImportButton => new UFT_Button(_UFT_Dialog, "//Button[@Label = 'File Import']");

    }
    public class TemplateExport_Dialog : UFT_Dialog
    {
        public TemplateExport_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }
        public UFT_Button FileButton => new UFT_Button(_UFT_Dialog, "//Button[@Label = 'File...']");
        public UFT_Button ConfirmChangesButton => new UFT_Button(_UFT_Dialog, "//Button[@Label = 'Confirm changes' and @IsWrapped = 'True']");
        public UFT_Editor TemplateName => new UFT_Editor(_UFT_Dialog, "//Editor[@AttachedText = 'Template Name']");
        public UFT_Editor Description => new UFT_Editor(_UFT_Dialog, "//Editor[@AttachedText = 'Description']");
        public UFT_Editor Version => new UFT_Editor(_UFT_Dialog, "//Editor[@AttachedText = 'Version']");

    }
    public class FileExport_Dialog : UFT_Dialog
    {
        public FileExport_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Button HomeButton => new UFT_Button(_UFT_Dialog, "//Button[@Label = 'Home']");
        public UFT_Button FileExportButton => new UFT_Button(_UFT_Dialog, "//Button[@Label = 'File Export']"); 

    }
    public class RPLList_InterFrame : UFT_InterFrame
    {
        public RPLList_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Table RPLListTable => new UFT_Table(_UFT_InterFrame, "//Table[@NativeClass = 'm2r.Table.m2rTableView']");
        public UFT_Editor SearchName => new UFT_Editor(_UFT_InterFrame, "//Editor[@NativeClass = 'javax.swing.JTextField']");
        public UFT_Button SelectAll => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Select All' and @IsWrapped = 'True']");
        public UFT_Button LocalFilter => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'Local filter' and @IsWrapped = 'True']");

    }
}