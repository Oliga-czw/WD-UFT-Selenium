
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Keys = OpenQA.Selenium.Keys;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(40873)]
        [Title("Permission: not grant any permission")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Low)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_40873()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
 


            LogStep(@"1. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            //without permission
            Web.Login_Page.username.SendKeys(UserName.qaone3);
            Web.Login_Page.password.SendKeys(PassWord.qaone3);
            Web.Login_Page.login.Click();
            Thread.Sleep(5000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "WD web login Home.PNG");
            Base_Assert.IsTrue(Web.Main_Page.Tabs.Count() == 1, "Home tab");
            Base_Assert.AreEqual("Home", Web.Main_Page.Tabs.getElement(0).Text, "Home Tab");
            driver.Close();

        }

    }
}