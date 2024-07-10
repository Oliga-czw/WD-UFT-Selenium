
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

        [TestCaseID(31310)]
        [Title("Equipment:Two users edit the same booth at the same time")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Low)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_31310()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID+"-";
            string booth = "booth1";    


            LogStep(@"1. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            LogStep(@"2. Go to booth and edit");
            Web_Fuction.gotoTab(WDWebTab.equipment);
            Web_Fuction.edit_booth(booth);
            LogStep(@"3. Open WD web and login another");
            Selenium_Driver edge = new Selenium_Driver(Browser.edge);
            Web_Fuction.gotoWDWeb(edge);
            edge.Wait();
            Web_Fuction.login();
            edge.Wait();
            Web_Fuction.gotoTab(WDWebTab.equipment);
            LogStep(@"4.update the data in edge");
            Web_Fuction.edit_booth(booth);
            Web.Equipment_Page.booth_description.SendKeys("for test");
            Web.Equipment_Page.booth_description.SendKeys(Keys.Enter);
            Thread.Sleep(1000);
            Web.Equipment_Page.Apply.Click();
            Thread.Sleep(1000);
            LogStep(@"5.update the data in chrome");
            edge.SwitchToChrome();
            Web.Equipment_Page.booth_description.SendKeys("for test");
            Web.Equipment_Page.booth_description.SendKeys(Keys.Enter);
            Thread.Sleep(1000);
            Web.Equipment_Page.Apply.Click();
            Thread.Sleep(3000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "error message.PNG");
            string text = "E4125: Another user has changed the data, you must perform a refresh from the server.";
            string message = Web.Web_Page.Message._Selenium_WebElement.FindElement(By.XPath("//div[@class='gwt-Label Alert_Label']")).Text;
            Base_Assert.AreEqual(text, message);
            Web.Web_Page.MessageOK.Click();
            driver.Close();

        }

    }
}