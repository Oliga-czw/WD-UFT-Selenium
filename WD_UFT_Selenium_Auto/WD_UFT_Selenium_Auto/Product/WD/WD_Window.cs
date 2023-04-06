using HP.LFT.SDK;
using HP.LFT.SDK.Java;
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
        public Material_Selection_InterFrame Material_SelectionInternalFrame => new Material_Selection_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Material Selection']");
        public ScaleCheck_InterFrame ScaleCheckInternalFrame => new ScaleCheck_InterFrame(_UFT_Window, "//InterFrame[@ObjectName = 'SCALECHECK']");
        public CheckWeight_InterFrame CheckWeightInternalFrame => new CheckWeight_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Check Weight']");
        public SelectAnOrderToKitting_InterFrame SelectAnOrderToKittingFrame => new SelectAnOrderToKitting_InterFrame(_UFT_Window, "//InterFrame[@TagName = 'Select an order to kitting']");
        public CampaignSelection_InterFrame CampaignSelectionInternalFrame => new CampaignSelection_InterFrame(_UFT_Window, "//InterFrame[@ObjectName = 'Main']");
        #endregion
        #region dialog
        public UFT_Dialog Dialog => new UFT_Dialog(_UFT_Window, "//Dialog[@Index = '0']");

        #endregion



    }


}
