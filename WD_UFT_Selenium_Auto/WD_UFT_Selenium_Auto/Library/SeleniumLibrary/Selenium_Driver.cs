using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WD_UFT_Selenium_Auto.Library.SeleniumLibrary
{
    public class Selenium_Driver
    {
        public static IWebDriver _Selenium_Driver
        {
            get;
            protected set;
        }

        //public Selenium_Driver()
        //{
        //    if (browser == "chrome")
        //    {
        //        _Selenium_Driver = new ChromeDriver();
        //    }
        //    else if (browser == "edge")
        //    {
        //        _Selenium_Driver = new EdgeDriver();
        //    }
        //    else
        //    {
        //        _Selenium_Driver = new ChromeDriver();
        //    }

        //}

        public Selenium_Driver(IWebDriver driver)
        {
            _Selenium_Driver= driver;
        }

        public  Selenium_Driver(string browser)
        {
            Initial(browser);
        }
        //public  Selenium_Driver(string browser)
        //{
        //    if (browser == "chrome")
        //    {
        //        _Selenium_Driver = new ChromeDriver();
        //    }
        //    else if (browser == "edge")
        //    {
        //        _Selenium_Driver = new EdgeDriver();
        //    }
        //    else
        //    {
        //        _Selenium_Driver = new ChromeDriver();
        //    }
        //}

        public void Initial(string browser)
        {
            if (browser == "chrome")
            {
                _Selenium_Driver = new ChromeDriver();
            }
            else if (browser == "edge")
            {
                _Selenium_Driver = new EdgeDriver();
            }
            else
            {
                _Selenium_Driver = new ChromeDriver();
            }

            _Selenium_Driver.Manage().Window.Maximize();
        }
        public void Navigate(string url)
        {
            _Selenium_Driver.Navigate().GoToUrl(url);
        }
        public void Maxsize()
        {
            _Selenium_Driver.Manage().Window.Maximize();
        }
        public  void Close()
        {
            _Selenium_Driver.Close();
        }

        public void Wait(double time = 1000)
        {
            _Selenium_Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);
        }
    }
}
