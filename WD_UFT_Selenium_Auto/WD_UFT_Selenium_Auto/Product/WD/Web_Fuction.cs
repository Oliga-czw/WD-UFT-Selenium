using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;

namespace WD_UFT_Selenium_Auto.Product.WD
{
    class Web_Fuction
    {


        #region operate fuction
        public static void gotoWDWeb(Selenium_Driver driver)
        {
            string servername = Environment.MachineName;
            string Url = "http://" + servername + ".qae.aspentech.com/WeighDispense/";
            Console.WriteLine(Url);
            driver.Navigate(Url);
            driver.Maxsize();
        }
        public static void gotoWeb(Selenium_Driver driver, string url)
        {
            driver.Navigate(url);
            driver.Maxsize();
        }
        public static void login()
        {
            Web.Login_Page.username.SendKeys(UserName.qaone1);
            Web.Login_Page.password.SendKeys(PassWord.qaone1);
            Web.Login_Page.login.Click();
        }

        public static void gotoTab(string tabname)

        {
            switch (tabname)
            {
                case "Administration":
                    Web.Main_Page.Administration.Click();
                    break;
                case "Equipment":
                    Web.Main_Page.Equipment.Click();
                    break;
                case "Material":
                    Web.Main_Page.Material.Click();
                    break;
                case "Inventory":
                    Web.Main_Page.Inventory.Click();
                    break;
                case "Order":
                    Web.Main_Page.Order.Click();
                    break;
                case "Report":
                    Web.Main_Page.Report.Click();
                    break;
            }


        }

        public static void administration_Apply(string text)
        {
            if (Web.Administration_Page.Apply.isEnable())
            {
                Web.Administration_Page.Apply.Click();
                string message = Web.Web_Page.Message._Selenium_WebElement.FindElement(By.XPath("//div[@class='gwt-Label Alert_Label']")).Text;
                Base_Assert.AreEqual(text, message);
                Web.Web_Page.Message._Selenium_WebElement.FindElement(By.XPath("//button[text()='OK']")).Click();
            }
            else
            {
                Base_logger.Info("Apply is diable.Not change.");
            }
        }
        public static void TakeScreenshot(IWebDriver driver, string path)
        {
            ITakesScreenshot ssdriver = driver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);

        }
        #endregion

        #region table fuction
        public static List<ReadOnlyCollection<IWebElement>> get_table(Selenium_WebElement table)
        {
            List<ReadOnlyCollection<IWebElement>> table_List = new List<ReadOnlyCollection<IWebElement>>();

            ReadOnlyCollection<IWebElement> rows = table._Selenium_WebElement.FindElements(By.CssSelector("tr"));
            foreach (IWebElement row in rows)
            {

                ReadOnlyCollection<IWebElement> cells = row.FindElements(By.CssSelector("td"));
                table_List.Add(cells);
            }

            return table_List;
        }

        public static ReadOnlyCollection<IWebElement> get_table_row(Selenium_WebElement table)
        {

            ReadOnlyCollection<IWebElement> rows = table._Selenium_WebElement.FindElements(By.CssSelector("tr"));
            return rows;
        }

        public static ReadOnlyCollection<IWebElement> get_table_head(Selenium_WebElement table)
        {

            IWebElement head = table._Selenium_WebElement.FindElements(By.CssSelector("tr"))[0];
            ReadOnlyCollection < IWebElement > headname = head.FindElements(By.CssSelector("td"));

            return headname;
        }

        public static int get_select_rowIndex(List<ReadOnlyCollection<IWebElement>> table, string text)
        {
            int r = 0;
            for (int i = 0; i < table.Count; i++)
            {
                for (int j = 0; j < table[0].Count; j++)
                {
                    if (table[i][j].Text == text)
                    {
                        r = i;
                        break;
                    }
                    
                }
                
            }
            return r;
        }
        public static int get_select_colIndex(List<ReadOnlyCollection<IWebElement>> table, string text)
        {
            int c = 0;
            for (int i = 0; i < table.Count; i++)
            {
                for (int j = 0; j < table[0].Count; j++)
                {
                    if (table[i][j].Text == text)
                    {
                        c = j;
                        break;
                    }

                }

            }
            return c;
        }
        #endregion
        #region scale function
        public static void select_scale(string simulator)
        {
            string  xpath = "//td[text()='"+simulator+"']";
            Web.Equipment_Page.body._Selenium_WebElement.FindElement(By.XPath(xpath)).Click();
        }
        #endregion
    }
}
