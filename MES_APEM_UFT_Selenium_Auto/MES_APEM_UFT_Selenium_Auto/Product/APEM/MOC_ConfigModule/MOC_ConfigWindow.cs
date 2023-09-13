using HP.LFT.SDK;
using HP.LFT.SDK.Java;

using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM
{
    public class MOC_ConfigWindow:UFT_JavaWindow
    {
        public MOC_ConfigWindow()
        {
        }
        public MOC_ConfigWindow(string xpath) : base(xpath)
        {
        }
        public UFT_Button Workstations => new UFT_Button(_UFT_Window, "//Button[@Label = 'Workstations' and @IsWrapped = 'True']");
        public UFT_Button Import_ReplaceMerge => new UFT_Button(_UFT_Window, "//Button[@Label = 'Import replace/merge' and @IsWrapped = 'True']");

        public ConfigImport_Dialog ConfigImportDialog => new ConfigImport_Dialog(_UFT_Window, "//Dialog[@Title = 'Import replace/merge from File']");
        public AddReason_Dialog AddReasonDialog => new AddReason_Dialog(_UFT_Window, "//Dialog[@Title = 'Audit Reason']");
    }


    public class ConfigImport_Dialog : UFT_Dialog
    {
        public ConfigImport_Dialog(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Editor FileName => new UFT_Editor(_UFT_Dialog, "//Editor[@AttachedText = 'File Name:']");
    }

}