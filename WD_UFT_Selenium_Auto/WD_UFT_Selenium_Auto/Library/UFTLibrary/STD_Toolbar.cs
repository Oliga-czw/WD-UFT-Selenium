using HP.LFT.SDK;
using HP.LFT.SDK.StdWin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WD_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class STD_Toolbar
    {
        public IToolBar _STD_Toolbar;

        public STD_Toolbar(IToolBar toolbar)
        {
            _STD_Toolbar = toolbar;
        }
        public STD_Toolbar(ITestObject parentObject, string xpath)
        {
            _STD_Toolbar = STD_Xpath.GetChildObject<IToolBar>(parentObject, xpath);
        }

        public void PressButton(string text)
        {
            _STD_Toolbar.GetButton(text).Press() ;
        }
    }
}
