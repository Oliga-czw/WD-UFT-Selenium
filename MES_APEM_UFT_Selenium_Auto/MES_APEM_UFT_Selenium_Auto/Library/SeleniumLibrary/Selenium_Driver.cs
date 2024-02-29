using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary
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
        public void Refresh()
        {
            _Selenium_Driver.Navigate().Refresh();
        }
        public void Maxsize()
        {
            _Selenium_Driver.Manage().Window.Maximize();
        }
        public  void Close()
        {
            _Selenium_Driver.Close();
        }
        public string  GetUrl()
        {
            return _Selenium_Driver.Url;
        }
        public void Wait(double time = 1000)
        {
            _Selenium_Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);
        }

        public IWebElement FindElement(string xpath)
        {
            return _Selenium_Driver.FindElement(By.XPath(xpath));
        }
        public ReadOnlyCollection<IWebElement> FindElements(string xpath)
        {
            return _Selenium_Driver.FindElements(By.XPath(xpath));
        }
        public void execute_script(string script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_Selenium_Driver;
            js.ExecuteScript(script);
        }
        public object execute_script_return(string script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_Selenium_Driver;
            return js.ExecuteScript(script);
        }
        public object execute_script_return(string script, IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_Selenium_Driver;
            return js.ExecuteScript(script, element);
        }
        public void execute_script(string script,IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_Selenium_Driver;
            js.ExecuteScript(script, element);
        }
        public bool is_element_exist(string xpath)
        {
            try
            {
                var element = _Selenium_Driver.FindElement(By.XPath(xpath));

                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public void action_move_to_element(IWebElement element)
        {
            Actions action = new Actions(_Selenium_Driver);
            int offsetX = (int)(element.Size.Width * (0.49 - 0.5));
            int offsetY = (int)(element.Size.Height * (0.56 - 0.5));
            action.MoveToElement(element, offsetX, offsetY).Perform();
            Thread.Sleep(5000);
        }
        public void action_move_to_element_click(IWebElement element)
        {
            Actions action = new Actions(_Selenium_Driver);
            int offsetX = (int)(element.Size.Width * (0.49 - 0.5));
            int offsetY = (int)(element.Size.Height * (0.56 - 0.5));
            action.MoveToElement(element, offsetX, offsetY).Click().Perform();
            Thread.Sleep(5000);
        }
    }
}
