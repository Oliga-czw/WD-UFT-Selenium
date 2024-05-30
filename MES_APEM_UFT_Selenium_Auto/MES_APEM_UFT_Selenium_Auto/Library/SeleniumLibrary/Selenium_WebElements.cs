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
    public class Selenium_WebElements
    {

        public ReadOnlyCollection<IWebElement> _Selenium_WebElements
        {
            get;
            protected set;
        }

        public Selenium_WebElements(ReadOnlyCollection<IWebElement> elements)
        {
            _Selenium_WebElements = elements;
        }

        public Selenium_WebElements(IWebDriver driver, string xpath)
        {
            _Selenium_WebElements = driver.FindElements(By.XPath(xpath));
        }

        public int Count()
        {
            return _Selenium_WebElements.Count();
        }

        public IWebElement getElement(int i)
        {
            return _Selenium_WebElements[i];
        }

        public ReadOnlyCollection<IWebElement> getAll()
        {
            return _Selenium_WebElements;
        }
    }
}
