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
        public ITable _UFT_Table
        {
            get;
            private set;
        }
        public UFT_CheckBox(ITable table)
        {
            _UFT_Table = table;
        }
        public UFT_CheckBox(ITestObject parentObject, string xpath)
        {

            _UFT_Table = UFT_Xpath.GetChildObject<ITable>(parentObject, xpath);
        }
    }  
}


