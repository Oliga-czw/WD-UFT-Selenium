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


    public class MOCMainInterFrame : UFT_InterFrame
    {

        public MOCMainInterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

    }


    public class User_InterFrame : MOCMainInterFrame
    {
        public User_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Editor userNameEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'User']");
        public UFT_Editor passwordEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'Password']");
        public UFT_Button loginbutton => new UFT_Button(_UFT_InterFrame, "//Button[@Label = 'OK']");
    }

    public class LoginFailure_InterFrame : MOCMainInterFrame
    {
        public LoginFailure_InterFrame(ITestObject parentObject, string xpath) : base(parentObject, xpath)
        {
        }

        public UFT_Editor userNameEditor => new UFT_Editor(_UFT_InterFrame, "//Editor[@AttachedText = 'User']");

        public UFT_Table auditTable => new UFT_Table(_UFT_InterFrame, "//Table[@AttachedText = 'Workstation  ']");
    }

    }
    
