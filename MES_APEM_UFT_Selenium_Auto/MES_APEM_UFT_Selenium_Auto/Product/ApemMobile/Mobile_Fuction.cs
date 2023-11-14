using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using Spire.Pdf;
using Spire.Pdf.Texts;
using System.Xml;

namespace MES_APEM_UFT_Selenium_Auto.Product.ApemMobile
{
    class Mobile_Fuction
    {


        #region operate fuction
        public static void gotoApemMobile(Selenium_Driver driver)
        {
            string servername = Environment.MachineName;
            string login_alter = UserName.qaone1.Split('\\')[1] + ":" + PassWord.qaone1 + "@";
            //string Url = "http://" + login_alter + servername + ".qae.aspentech.com/ApemMobile/";
            string Url = $"http://{login_alter}{servername}.qae.aspentech.com/ApemMobile/";
            driver.Navigate(Url);
            driver.Maxsize();
            Thread.Sleep(5000);
        }
        //public static void gotoWeb(Selenium_Driver driver, string url)
        //{
        //    driver.Navigate(url);
        //    driver.Maxsize();
        //}
        public static void login()
        {
            Mobile.Login_Page.username.SendKeys(UserName.qaone1);
            Mobile.Login_Page.password.SendKeys(PassWord.qaone1);
            Mobile.Login_Page.login.Click();
            Thread.Sleep(5000);
        }

        public static void UpdateAutoLogin()
        {
            //edit xml security
            XmlDocument document = new XmlDocument();
            document.Load(Base_Directory.MobileWebconfig);
            XmlNodeList nodeList = document.SelectSingleNode("web-app/servlet[3]/init-param[4]").ChildNodes;
            //Console.WriteLine(nodeList[0].InnerText);
            if (nodeList[0].InnerText == "EnableWIA")
            {
                nodeList[1].InnerText = "false";
                //Console.WriteLine(nodeList[1].InnerText);
            }
            document.Save(Base_Directory.MobileWebconfig);
            //time out node /session-config/session-timeout

        }
        #endregion
        //    public static void gotoTab(string tabname)

        //    {
        //        switch (tabname)
        //        {
        //            case "Administration":
        //                Web.Main_Page.Administration.Click();
        //                break;
        //            case "Equipment":
        //                Web.Main_Page.Equipment.Click();
        //                break;
        //            case "Material":
        //                Web.Main_Page.Material.Click();
        //                break;
        //            case "Inventory":
        //                Web.Main_Page.Inventory.Click();
        //                break;
        //            case "Order":
        //                Web.Main_Page.Order.Click();
        //                break;
        //            case "Report":
        //                Web.Main_Page.Report.Click();
        //                break;
        //        }


        //    }

        //    public static void administration_Apply(string text)
        //    {
        //        Thread.Sleep(4000);
        //        if (Web.Administration_Page.Apply.GetAttribute("disabled") is null)
        //        {
        //            Web.Administration_Page.Apply.Click();
        //            string message = Web.Web_Page.Message._Selenium_WebElement.FindElement(By.XPath("//div[@class='gwt-Label Alert_Label']")).Text;
        //            Base_Assert.AreEqual(text, message);
        //            Web.Web_Page.Message._Selenium_WebElement.FindElement(By.XPath("//button[text()='OK']")).Click();
        //        }
        //        else
        //        {
        //            Base_logger.Info("Apply is diable.Not change.");
        //        }
        //    }
        public static void TakeScreenshot(IWebDriver driver, string path)
        {
            ITakesScreenshot ssdriver = driver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);

        }
        //    public static void permissionUpdate(IWebDriver driver, string[] permission_list)
        //    {
        //        var permissionList = driver.FindElements(By.XPath("//span[@class='gwt-CheckBox']/label"));
        //        //int count001 = permissionList.Count;

        //        foreach (var permission in permissionList)
        //        {
        //            //System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", count001.ToString(), Encoding.Default);
        //            var inputXpath = "//label[text()='" + permission.Text + "']/../input";
        //            if (permission_list.Length == 2)
        //            {
        //                if (permission.Text == permission_list[0] || permission.Text == permission_list[1])
        //                {
        //                    var NameList = driver.FindElements(By.XPath(inputXpath));
        //                    foreach (var Name in NameList)
        //                    {
        //                        if (Name.GetAttribute("checked") != null)
        //                        {
        //                            Name.Click();
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (permission.Text.ToLower().Contains(permission_list[0].ToLower()))
        //                {
        //                    var NameList = driver.FindElements(By.XPath(inputXpath));
        //                    foreach (var Name in NameList)
        //                    {
        //                        if (Name.GetAttribute("checked") != null)
        //                        {
        //                            Name.Click();
        //                        }
        //                    }
        //                }
        //            }

        //        }
        //        driver.FindElement(By.XPath("//button[text()='Apply']")).Click();
        //        Thread.Sleep(2000);
        //        driver.FindElement(By.XPath("//button[text()='OK']")).Click();
        //    }
        //    public static void RestorePermission(IWebDriver driver)
        //    {
        //        var checkbox = driver.FindElements(By.XPath("//span[@class='gwt-CheckBox']/input"));
        //        foreach (var input in checkbox)
        //        {
        //            if (input.GetAttribute("checked") == null)
        //            {
        //                input.Click();
        //            }
        //        }
        //        driver.FindElement(By.XPath("//button[text()='Apply']")).Click();
        //        Thread.Sleep(2000);
        //        driver.FindElement(By.XPath("//button[text()='OK']")).Click();
        //    }
        //    #endregion

        //    #region table fuction
        //    public static List<ReadOnlyCollection<IWebElement>> get_table(Selenium_WebElement table)
        //    {
        //        List<ReadOnlyCollection<IWebElement>> table_List = new List<ReadOnlyCollection<IWebElement>>();

        //        ReadOnlyCollection<IWebElement> rows = table._Selenium_WebElement.FindElements(By.CssSelector("tr"));
        //        foreach (IWebElement row in rows)
        //        {

        //            ReadOnlyCollection<IWebElement> cells = row.FindElements(By.CssSelector("td"));
        //            table_List.Add(cells);
        //        }

        //        return table_List;
        //    }

        //    public static ReadOnlyCollection<IWebElement> get_table_row(Selenium_WebElement table)
        //    {

        //        ReadOnlyCollection<IWebElement> rows = table._Selenium_WebElement.FindElements(By.CssSelector("tr"));
        //        return rows;
        //    }

        //    public static ReadOnlyCollection<IWebElement> get_table_head(Selenium_WebElement table)
        //    {

        //        IWebElement head = table._Selenium_WebElement.FindElements(By.CssSelector("tr"))[0];
        //        ReadOnlyCollection < IWebElement > headname = head.FindElements(By.CssSelector("td"));

        //        return headname;
        //    }

        //    public static int get_select_rowIndex(List<ReadOnlyCollection<IWebElement>> table, string text)
        //    {
        //        int r = 0;
        //        for (int i = 0; i < table.Count; i++)
        //        {
        //            for (int j = 0; j < table[0].Count; j++)
        //            {
        //                if (table[i][j].Text == text)
        //                {
        //                    r = i;
        //                    break;
        //                }

        //            }

        //        }
        //        return r;
        //    }
        //    public static int get_select_colIndex(List<ReadOnlyCollection<IWebElement>> table, string text)
        //    {
        //        int c = 0;
        //        for (int i = 0; i < table.Count; i++)
        //        {
        //            for (int j = 0; j < table[0].Count; j++)
        //            {
        //                if (table[i][j].Text == text)
        //                {
        //                    c = j;
        //                    break;
        //                }

        //            }

        //        }
        //        return c;
        //    }
        //    #endregion
        #region Main page
       
      
        #endregion


        #region Event Log
        public static void SelectMenu()
        {
            Mobile.EventLog_Page.SelectMenu.Click();
        }

        #endregion
    }
}

