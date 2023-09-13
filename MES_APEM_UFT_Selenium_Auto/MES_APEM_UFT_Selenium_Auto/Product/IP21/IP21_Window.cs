using HP.LFT.SDK;
using HP.LFT.SDK.StdWin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.WD
{
    public class IP21MainWindow : STD_Window
    {

        public IP21MainWindow(string xpath) : base(xpath)
        {
        }

        public IP21MainWindow(IWindow window) : base(window)
        {
        }

        public STD_Dialog ShutDownDialog => new STD_Dialog("//Dialog[@Text = 'InfoPlus.21 Shutdown Script Configuration Reminder']");



        public IButton Start => _STD_Window.Describe<IButton>(new ButtonDescription
        {
            Text = @"&START InfoPlus.21"
        });

        public IStatusBar BarMessage => _STD_Window.Describe<IStatusBar>(new StatusBarDescription
        {
            NativeClass = @"msctls_statusbar32"
        });







    }
}
