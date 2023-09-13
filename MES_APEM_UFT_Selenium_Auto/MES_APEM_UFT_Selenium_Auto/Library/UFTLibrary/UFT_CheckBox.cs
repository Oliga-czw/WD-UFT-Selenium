using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class UFT_CheckBox
    {
        public ICheckBox _UFT_CheckBox
        {
            get;
            private set;
        }
        public UFT_CheckBox(ICheckBox checkbox)
        {
            _UFT_CheckBox = checkbox;
        }
        public UFT_CheckBox(ITestObject parentObject, string xpath)
        {

            _UFT_CheckBox = UFT_Xpath.GetChildObject<ICheckBox>(parentObject, xpath);
        }
    }  
}


