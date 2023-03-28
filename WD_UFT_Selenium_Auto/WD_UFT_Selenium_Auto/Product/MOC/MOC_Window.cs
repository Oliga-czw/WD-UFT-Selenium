using HP.LFT.SDK;
using HP.LFT.SDK.Java;

using WD_UFT_Selenium_Auto.Library.UFTLibrary;

namespace WD_UFT_Selenium_Auto.Product.WD
{


    public class MOCMainWindow : UFT_JavaWindow
    {

        public MOCMainWindow()
        {
        }
        public MOCMainWindow(string xpath) : base(xpath)
        {
        }

        private IWindow Window = UFT_Xpath.GetDesktopWindow<IWindow>("//JavaWindow[@ObjectName = 'MOC']");
        #region interframe
        public User_InterFrame LogonInternalFrame => new User_InterFrame(_UFT_Window, "//InterFrame[@Label = 'User Identification']");

        #endregion

        public UFT_Button Audit_moudle => new UFT_Button(_UFT_Window, "//Button[@Label = 'Audit Module' and @IsWrapped = 'True']");


    }

    public class MOCAuditWindow : UFT_JavaWindow
    {

        public MOCAuditWindow()
        {
        }
        public MOCAuditWindow(string xpath) : base(xpath)
        {
        }

        public UFT_Button Users_Failures => new UFT_Button(_UFT_Window, "//Button[@Label = 'Audit Users Failures' and @IsWrapped = 'True']");

        public LoginFailure_InterFrame LoginFailureInterFrame => new LoginFailure_InterFrame(_UFT_Window, "//InterFrame[@Label = 'User Login Failure']");

    }

    }
