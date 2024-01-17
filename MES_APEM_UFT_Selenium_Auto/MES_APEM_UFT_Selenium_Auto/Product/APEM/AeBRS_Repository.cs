using HP.LFT.SDK;
using HP.LFT.SDK.WinForms;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM
{
    public class AeBRS
    {
        private static IWindow AeBRSConfig_Window = Desktop.Describe<IWindow>(new WindowDescription
        {
            ObjectName = @"AeBRSClientConfigure"
        });
        public static AeBRSConfigure_Window AeBRSConfigureWindow => new AeBRSConfigure_Window(AeBRSConfig_Window);
    }
}