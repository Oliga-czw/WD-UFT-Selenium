using HP.LFT.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System.Windows.Forms;
using System;
using HP.LFT.SDK.UIAPro;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;

namespace MES_APEM_UFT_Selenium_Auto
{
    [TestClass]
    public class UftDeveloperSeleniumTest
    {

        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            Thread.Sleep(3000);
            string actual = SQLplus.SQLplusWindow.ResultArea.Text;
            //string exepect = "CreateOrder: 1\nOrderState: PLAN\nOrderState: ACTIVE\nOrderState: FINISH\nresult: 0\nScript has completed!\n";

            string exepect = @"CreateOrder: 1
OrderState: PLAN
OrderState: ACTIVE
OrderState: FINISH
result: 0
Script has completed!";

            Console.WriteLine("11"+ exepect + "11");
            Console.WriteLine("11" + actual + "11");
            bool a = SQLplus.SQLplusWindow.ResultArea.Text.Contains(exepect);
            Console.WriteLine(a);
        }





    }

}
