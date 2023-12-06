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
using OpenQA.Selenium;

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

            //MOC_Fuction.VerifyRPL("FOR_STATUS");
            //MOC_Fuction.CertifyRPL("FOR_STATUS");
            //Thread.Sleep(3000);
            //MOC_Fuction.PlanFromRPL("FOR_STATUS", "ORDRE824732");
            //APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Phases");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            driver.Wait();
            Mobile_Fuction.login();
            driver.Wait();
            Thread.Sleep(5000);
            Mobile.OrderProcess_Page.OrderSearch.SendKeys("ORDRE824732");
            Thread.Sleep(2000);
            Mobile.OrderProcess_Page.GotoTracking.Click();
            Thread.Sleep(3000);
            int no = 0;
            int i = 0;
            foreach (IWebElement head in Mobile.OrderTracking_Page.OrderPhaseTableHeads)
            {
                if (head.Text == "Status")
                {
                    no = i;
                }
                i++;
            }
            foreach (IWebElement Phase in Mobile.OrderTracking_Page.OrderPhaseTableRows)
            {
                var Status = Phase.FindElements(By.TagName("td"))[no].Text;
                Console.WriteLine(Status);
                Assert.IsTrue((Status.Contains("Ready")) || (Status.Contains("Not ready")));
            } 
        }





    }

}
