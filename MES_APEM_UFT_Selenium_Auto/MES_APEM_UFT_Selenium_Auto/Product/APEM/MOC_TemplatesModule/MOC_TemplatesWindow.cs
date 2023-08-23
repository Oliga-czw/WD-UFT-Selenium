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
        public UFT_Dialog LogWindow => new UFT_Dialog(_UFT_Window, "//Dialog[@TagName = 'Log window']");

    }
}