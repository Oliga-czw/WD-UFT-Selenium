using HP.LFT.SDK.StdWin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestMethod]
        public void Test_AFW()
        {
            //Application.LaunchWDAndLogin();
            //WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();

            Application.LaunchAFW();

            string role = AFWRole.User;
            string accout = UserName.qaone2;
            AFW_Fuction.addRole(role, accout);

            AFW_Fuction.closeAFW();
        }
        
    }
}
