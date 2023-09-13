using HP.LFT.SDK;
using HP.LFT.SDK.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;
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