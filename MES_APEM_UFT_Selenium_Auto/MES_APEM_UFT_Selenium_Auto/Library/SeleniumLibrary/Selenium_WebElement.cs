using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary
{
    public class Selenium_WebElement
    {
        public IWebElement _Selenium_WebElement
        {
            get;
            protected set;
        }

        public ReadOnlyCollection<IWebElement> _Selenium_WebElements
        {
            get;
            protected set;
        }

        public Selenium_WebElement(IWebElement element)
        {
            _Selenium_WebElement = element;
        }

        public Selenium_WebElement(IWebDriver driver, string xpath)
        {
            _Selenium_WebElement = driver.FindElement(By.XPath(xpath));
        }
        public Selenium_WebElement(IWebDriver driver, string xpaths,int index )
        {
            _Selenium_WebElement = driver.FindElements(By.XPath(xpaths))[index];
        }
        public void Click()
        {
            _Selenium_WebElement.Click();
        }

        public void SendKeys(string text)
        {
            _Selenium_WebElement.SendKeys(text);
        }

        public bool isEnable()
        {
            return _Selenium_WebElement.Enabled;
        }
        public void Clear()
        {
            _Selenium_WebElement.Clear();
        }

        public string GetAttribute(string items)
        {
            return _Selenium_WebElement.GetAttribute(items);
        }
        public void select_option(string target)
        {
            var options = _Selenium_WebElement.FindElements(By.TagName("option"));
            foreach (var option in options)
            {
                if (option.Text == target)
                {
                    option.Click();
                    break;
                }
                    
                
            }
        }

    }
}
