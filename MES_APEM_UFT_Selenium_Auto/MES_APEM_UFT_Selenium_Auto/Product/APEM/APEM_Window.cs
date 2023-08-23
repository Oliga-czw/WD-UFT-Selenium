using HP.LFT.SDK;
using HP.LFT.SDK.Java;

using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM
{


    public class MOCMainWindow : UFT_JavaWindow
    {

        public MOCMainWindow()
        {
        }
        public MOCMainWindow(string xpath) : base(xpath)
        {
        }

       // private IWindow Window = UFT_Xpath.GetDesktopWindow<IWindow>("//JavaWindow[@ObjectName = 'MOC']");
        #region interframe
        public User_InterFrame LogonInternalFrame => new User_InterFrame(_UFT_Window, "//InterFrame[@Label = 'User Identification']");

        #endregion

        public UFT_Button Audit_moudle => new UFT_Button(_UFT_Window, "//Button[@Label = 'Audit Module' and @IsWrapped = 'True']");
        public UFT_Button WorkstationBP => new UFT_Button(_UFT_Window, "//Button[@Label = 'Workstation BP' and @IsWrapped = 'True']");
        public UFT_Button Templates_moudle => new UFT_Button(_UFT_Window, "//Button[@Label = 'Templates Module' and @IsWrapped = 'True']");
        public UFT_Button OrderTracking => new UFT_Button(_UFT_Window, "//Button[@Label = 'Order Tracking' and @IsWrapped = 'True']");
        public UFT_Button RPLDesign => new UFT_Button(_UFT_Window, "//Button[@Label = 'Order Tracking' and @IsWrapped = 'True']");
        public UFT_Button RPLVerify => new UFT_Button(_UFT_Window, "//Button[@Label = 'Order Tracking' and @IsWrapped = 'True']");
        public UFT_Button BPLDesign => new UFT_Button(_UFT_Window, "//Button[@Label = 'Order Tracking' and @IsWrapped = 'True']");
        public UFT_Button Orders => new UFT_Button(_UFT_Window, "//Button[@Label = 'Order Tracking' and @IsWrapped = 'True']");
        public UFT_Button Config_moudle => new UFT_Button(_UFT_Window, "//Button[@Label = 'Config Moudle' and @IsWrapped = 'True']");

    }
    public class APEMMainWindow : UFT_JavaWindow
    {

        public APEMMainWindow()
        {
        }
        public APEMMainWindow(string xpath) : base(xpath)
        {
        }

        #region interframe
        //public User_InterFrame LogonInternalFrame => new User_InterFrame(_UFT_Window, "//InterFrame[@Label = 'User Identification']");

        #endregion

        public UFT_Editor Password => new UFT_Editor(_UFT_Window, "//Editor[@AttachedText = 'Password: ']");
        public UFT_Editor EnterPasswordAgain => new UFT_Editor(_UFT_Window, "//Editor[@AttachedText = 'Enter password again:']");
        public UFT_Button OKButton => new UFT_Button(_UFT_Window, "//Button[@Label = 'OK']");
        public UFT_Editor UID => new UFT_Editor(_UFT_Window, "//Editor[@AttachedText = 'UID used to uniquely identify Templates exported from this system']");
        public UFT_CheckBox ImportGMLTemplates => new UFT_CheckBox(_UFT_Window, "//CheckBox[@AttachedText='Import GML v14.0 template']");

    }



}
