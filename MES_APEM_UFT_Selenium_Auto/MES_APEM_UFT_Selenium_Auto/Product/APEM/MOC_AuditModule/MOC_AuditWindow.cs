using HP.LFT.SDK;
using HP.LFT.SDK.Java;

using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_ConfigModule;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_AuditModule
{
    public class MOC_AuditWindow : UFT_JavaWindow
    {

        public MOC_AuditWindow()
        {
        }
        public MOC_AuditWindow(string xpath) : base(xpath)
        {
        }

        public UFT_Button Users_Failures => new UFT_Button(_UFT_Window, "//Button[@Label = 'Audit Users Failures' and @IsWrapped = 'True']");

        public LoginFailure_InterFrame LoginFailureInterFrame => new LoginFailure_InterFrame(_UFT_Window, "//InterFrame[@Label = 'User Login Failure']");

    }
}