using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Keys = OpenQA.Selenium.Keys;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(41399)]
        [Title("Only one user is allowed to modify data at same time")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Low)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_41399()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;

            LogStep(@"1. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            LogStep(@"2. Go and admin and General");
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.General.Click();
            LogStep(@"3. Open WD web and login another");
            Selenium_Driver edge = new Selenium_Driver(Browser.edge);
            Web_Fuction.gotoWDWeb(edge);
            edge.Wait();
            Web_Fuction.login();
            edge.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.General.Click();
            LogStep(@"4.update the data");
            Web.Administration_Page.Inactivity_Period.Clear();
            Web.Administration_Page.Inactivity_Period.SendKeys("15");
            Web.Administration_Page.Inactivity_Period.SendKeys(Keys.Enter);
            Thread.Sleep(1000);
            Web_Fuction.administration_Apply("Configuration successfully saved");
            try
            {
                edge.SwitchToChrome();
                Web.Administration_Page.Inactivity_Period.Clear();
                Web.Administration_Page.Inactivity_Period.SendKeys("15");
                Web.Administration_Page.Inactivity_Period.SendKeys(Keys.Enter);
                Thread.Sleep(1000);
                Web.Administration_Page.Apply.Click();
                Thread.Sleep(1000);
                Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Apply error.PNG");
                string text = "E4125: Another user has changed the data, you must perform a refresh from the server.";
                string message = Web.Web_Page.Message._Selenium_WebElement.FindElement(By.XPath("//div[@class='gwt-Label Alert_Label']")).Text;
                Base_Assert.AreEqual(text, message);
                Web.Web_Page.Message._Selenium_WebElement.FindElement(By.XPath("//button[text()='OK']")).Click();
            }
            finally
            {
                LogStep(@"5.restore the data");
                driver.SwitchToEdge();
                Web.Administration_Page.Inactivity_Period.Clear();
                Web.Administration_Page.Inactivity_Period.SendKeys("15");
                Web.Administration_Page.Inactivity_Period.SendKeys(Keys.Enter);
                Thread.Sleep(1000);
                Web_Fuction.administration_Apply("Configuration successfully saved");
                edge.Close();
            }
        }

    }
}