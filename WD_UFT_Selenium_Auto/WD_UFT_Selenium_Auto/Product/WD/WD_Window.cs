using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WD_UFT_Selenium_Auto.Library.UFTLibrary;

namespace WD_UFT_Selenium_Auto.Product.WD
{


    public class ClassMainWindow : UFT_JavaWindow
    {

        public ClassMainWindow()
        {
        }
        public ClassMainWindow(string xpath) : base(xpath)
        {
        }

        #region window
        private IWindow Window = UFT_Xpath.GetDesktopWindow<IWindow>("//JavaWindow[@Title = 'Aspen Weigh and Dispense Execution' and @NativeClass = 'wd.WDWorkstation']");
        //public class DialogWindow : UFT_JavaWindow
        //{
        //    public DialogWindow(string xpath) : base(xpath)
        //    {
        //    }

        //    public UFT_Button OKButton => new UFT_Button(_UFT_Window, "//Button[@AttachedText = 'OK']");
        //    public HP.LFT.SDK.Java.IButton YesButton => _UFT_Window.Describe<HP.LFT.SDK.Java.IButton>(new ButtonDescription
        //    {
        //        AttachedText = @"	 	 	 	Yes	 	 	 	"
        //    });
        //    public UFT_Button NoButton => new UFT_Button(_UFT_Window, "//Button[@AttachedText = '	 	 	 	No	 	 	 	']");
        //    public UFT_Button CancelButton => new UFT_Button(_UFT_Window, "//Button[@AttachedText = 'Cancel']");
        //    public UFT_Button HelpButton => new UFT_Button(_UFT_Window, "//Button[@ObjectName = 'btnHelp']");
        //    //public UFT_TextBlock MessageTextBlock => new UFT_TextBlock(_UFT_Window, "//UiObject[@NativeClass = 'System.Windows.Controls.TextBlock' and @Index = '0']");
        //    //public UFT_EditField EditField => new UFT_EditField(_UFT_Window, "//EditField[@NativeClass = 'System.Windows.Controls.TextBox']");
        //    //public UFT_Button DeleteButton => new UFT_Button(_UFT_Window, "//WPFButton[@Text = 'Delete']");
        //    //public UFT_Button RestoreButton => new UFT_Button(_UFT_Window, "//WPFButton[@Text = 'Restore']");
        //    //public UFT_Button RenameButton => new UFT_Button(_UFT_Window, "//WPFButton[@Text = 'Rename']");

        //}
        public class SimulatorWindow : UFT_JavaWindow
        {
            public SimulatorWindow(string xpath) : base(xpath)
            {
            }

            public UFT_Editor weight => new UFT_Editor(_UFT_Window, "//Editor[@TagName = 'JTextField']");
            public UFT_Button OK => new UFT_Button(_UFT_Window, "//Button[@Label = 'OK']");

        }
        #endregion
        #region interframe
        public Login_InterFrame LogonInternalFrame => new Login_InterFrame(_UFT_Window, "//InterFrame[@ObjectName = 'LOGON']");
        public Home_InterFrame HomeInternalFrame => new Home_InterFrame(_UFT_Window, "//InterFrame[@ObjectName = 'HOME']");
        public Dispensing_InterFrame DispensingInternalFrame => new Dispensing_InterFrame(_UFT_Window, "//InterFrame[@ObjectName = 'OrderList']");
        public Material_InterFrame MaterialInternalFrame => new Material_InterFrame(_UFT_Window, "//InterFrame[@ObjectName = 'Material_Selection']");
        public BoothClean_InterFrame BoothCleanInternalFrame => new BoothClean_InterFrame(_UFT_Window, "//InterFrame[@ObjectName = 'Main']");
        public Handle_Information_InterFrame HandleInformationInterFrame => new Handle_Information_InterFrame(_UFT_Window, "//InterFrame[@Label = 'Handle Information']");
        public ScaleWeight_InterFrame ScaleWeightInternalFrame => new ScaleWeight_InterFrame(_UFT_Window, "//InterFrame[@ObjectName = 'MATERIAL_DISPENSE']");
        public OpenWeight_InterFrame OpenWeighInternalFrame => new OpenWeight_InterFrame(_UFT_Window, "//InterFrame[@ObjectName = 'OPENCONSOLE']");

        #endregion

        //#region Dialog
        //public UFT_Dialog DeviationDialog => new UFT_Dialog(Window, "//Dialog[@Title = 'Create Deviation']");
        //#endregion
        //interframe-->interframe class-->uiobject
        //public ClassSpecificationsPage SpecificationsPage => new ClassSpecificationsPage(window, "//UiObject[@ParentText = 'Specifications']");
        //public class LoginWindow : UFT_JavaWindow
        //{
        //    public LoginWindow(string xpath) : base(xpath)
        //    {
        //    }

        //    public Login_InterFrame WDLogonInternalFrame => new Login_InterFrame(_UFT_Window, "//InterFrame[@ObjectName = 'LOGON']");
        //}

        //public class MenuWindow : UFT_JavaWindow
        //{
        //    public MenuWindow(string xpath) : base(xpath)
        //    {
        //    }

        //    public Home_InterFrame WDMainInternalFrame => new Home_InterFrame(_UFT_Window, "//InterFrame[@ObjectName = 'HOME']");
        //}


    }


}
