using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using OpenQA.Selenium;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;
using System.Linq;
using OpenQA.Selenium.Interactions;
using System.Net;
using Cookie = OpenQA.Selenium.Cookie;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class Mobile_TestCase
    {
        [TestCaseID(580276)]
        [Title("UC558888_APEM mobile client login_Alternative workflow I_ Session is no longer valid")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_580276()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";

            Mobile_Fuction.UpdateSessionOut("3");
            //restart tomcat
            Base_Test.KillProcess("tomcat10");
            Thread.Sleep(30000);
            Base_Function.ResartServices(ServiceName.Tomcat);
            Thread.Sleep(60000);
            try
            {
                LogStep(@"1. login mobile");
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Mobile_Fuction.gotoApemMobile(driver);
                Mobile_Fuction.login();
                LogStep(@"2. check session functions");
                //Close broswer
                driver.Close();
                Selenium_Driver driver2 = new Selenium_Driver(Browser.chrome);
                Mobile_Fuction.gotoApemMobile(driver2);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Login page-Close broswer.PNG");
                Base_Assert.IsTrue(driver2.is_element_exist(Mobile.Login_Page.login));
                Mobile_Fuction.login();
                //Kill broswer
                Base_Test.KillProcess("chrome");
                Selenium_Driver driver3 = new Selenium_Driver(Browser.chrome);
                Mobile_Fuction.gotoApemMobile(driver3);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Login page-Kill broswer.PNG");
                Base_Assert.IsTrue(driver3.is_element_exist(Mobile.Login_Page.login));
                Mobile_Fuction.login();
                //manually logs out
                Mobile.Main_Page.account.Click();
                Mobile.Main_Page.logout.Click();
                Thread.Sleep(5000);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Logout page-Manually.PNG");
                Base_Assert.IsTrue(driver3.GetUrl().Contains("logout"), "Logout page");
                Mobile.Logout_Page.login.Click();
                Thread.Sleep(5000);
                Mobile_Fuction.login();
                //Session time out
                Thread.Sleep(240000);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Logout page-Session time out.PNG");
                Base_Assert.IsTrue(driver3.GetUrl().Contains("logout"), "Logout page");
                Mobile.Logout_Page.login.Click();
                Thread.Sleep(5000);
                Mobile_Fuction.login();
                //change the cookie value 
                var Cookies = driver3.Cookies;
                Cookie myCookie = new Cookie("JSESSIONID", "testValue", "/ApemMobile");
                Cookies.DeleteCookieNamed(myCookie.Name);
                Cookies.AddCookie(myCookie);
                Mobile.OrderProcess_Page.RefreshButton.Click();
                Thread.Sleep(2000);
                Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Logout page-change the cookie value.PNG");
                Base_Assert.IsTrue(driver3.is_element_exist(Mobile.Login_Page.login));
                //request using an invalid cookie
                var Cookies2 = driver3.Cookies;
                Cookie ValideCookie1 = Cookies2.GetCookieNamed("JSESSIONID");
                System.Net.Cookie ValideCookie2 = new System.Net.Cookie(ValideCookie1.Name, ValideCookie1.Value + "test", ValideCookie1.Path, ValideCookie1.Domain);
                var uri = new Uri($"http://{UserName.qaone1.Split('\\')[1]}:{PassWord.qaone1}@{Environment.MachineName}.qae.aspentech.com/ApemMobile/servlet/mobileOrderListServlet?tz=America/Los_Angeles&states=0%7C2%7C3%7C6&init=200&num=200"); // 替换为你要请求的API URL 

                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "POST";
                request.Host = Environment.MachineName + ".qae.aspentech.com";
                request.ContentLength = 0;
                var cookieContainer = new CookieContainer();
                cookieContainer.Add(ValideCookie2);
                request.CookieContainer = cookieContainer;
                try
                {
                    var response = (HttpWebResponse)request.GetResponse();
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                }
                catch (WebException e)
                {
                    //HTTP Request failed: The remote server returned an error: (401) Unauthorized.
                    Base_Assert.IsTrue(e.Message.Contains("401"));
                    Console.WriteLine("HTTP Request failed: " + e.Message);
                }
            }
            finally
            {
                Mobile_Fuction.UpdateSessionOut("180");
                //restart tomcat
                Base_Test.KillProcess("tomcat10");
                Thread.Sleep(30000);
                Base_Function.ResartServices(ServiceName.Tomcat);
                Thread.Sleep(60000);
            }

        }

    }
}