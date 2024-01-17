using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary;

namespace MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_ConfigModule
{

    public class LoginFailure_InterFrame :UFT_InterFrame
    {
        public LoginFailure_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Editor userNameEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'User']");

        public UFT_Table auditTable => new UFT_Table(_UFT_InterFrame, "//Table[@Index = '0']");
    }
}