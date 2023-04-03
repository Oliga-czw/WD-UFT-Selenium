﻿using OpenQA.Selenium;
using Spire.Pdf;
using Spire.Pdf.Texts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
        public static void edit_scale(string simulator)
        {
            string xpath = "//td[text()='" + simulator + "']/../td[3]/img";
            Web.Equipment_Page.body._Selenium_WebElement.FindElement(By.XPath(xpath)).Click();
        }

        #endregion
        #region booth function
        public static void edit_booth(string booth)
        {
            string xpath = "//td[text()='" + booth + "']/../td[3]/img";
            var img = Web.Equipment_Page.body._Selenium_WebElement.FindElements(By.XPath(xpath));
            img[0].Click();
        }
        #endregion
        #region report fuction
        public static void Check_report(List<string> columns,List<string> datatexts)
        {
            
            var data_list = new List<List<string>>();
            var head_list = new List<string>();
            var head = Web.Report_Page.Report_Table._Selenium_WebElement.FindElements(By.XPath("//table[@class='Report_Paper_Border_Shading']/tbody/tr[4]/td/table/tbody/tr/td/div//a[@class='Report_Head_Style']"));
            //get head list
            foreach (var h in head)
            {
                head_list.Add(h.Text);
            }
            var row = Web.Report_Page.Report_Table._Selenium_WebElement.FindElements(By.XPath("//table[@class='Report_Paper_Border_Shading']/tbody/tr[4]/td/table/tbody/tr/td[@class='Inner_Column_Left']/.."));
           //get table data
           
            for (int j = 0; j < row.Count; j++)
            {
                var single_row_text = new List<string>();
                var cells = row[j].FindElements(By.CssSelector("td.Inner_Column_Left"));
                foreach (var cell in cells)
                {
                    single_row_text.Add(cell.Text);
                   
                }
                data_list.Add(single_row_text);
            }
            //check selected data
            for (int i = 0; i < columns.Count; i++)
            {
                int number = head_list.IndexOf(columns[i]);
                string datatext = datatexts[i];
                for (int m = 0; m < data_list.Count; m++)
                {
                    Base_Assert.AreEqual(datatext, data_list[m][number]);
                }

            }
        }

        public static void Check_report_inner(List<List<string>> datatexts,int tablerow)
        {

            var data_list = new List<List<string>>();
            var row = Web.Report_Page.Report_Table._Selenium_WebElement.FindElements(By.XPath("//table[@class='Sub_Table_Indent']//table[@class='Order_Table_body_Style_Collapse']/tbody/tr/td[@class='Inner_Column_Left']/.."));
            //get table data

            for (int j = 0; j < tablerow; j++)
            {
                var single_row_text = new List<string>();
                var cells = row[j].FindElements(By.CssSelector("td.Inner_Column_Left"));
                foreach (var cell in cells)
                {
                    single_row_text.Add(cell.Text);

                }
                data_list.Add(single_row_text);
            }
            //check selected data
            bool same = true;
            for (int i = 0; i < data_list.Count; i++)
            {
                for (int j = 0; j < data_list[0].Count; j++)
                {
                    if(data_list[i][j] != datatexts[i][j])
                    {
                        same = false;
                        Base_logger.Error("Actual:"+data_list[i][j]+"Expected:"+datatexts[i][j]+"is not same");
                        break;
                    }
                    
                }
                break;
            }
            Base_Assert.IsTrue(same,"inner table report");

        }

        //verify the ferquency of select value
        public static void Check_PDF(string path,string filename,List<string> searchList)
        {
            var row = Web.Report_Page.Report_Table._Selenium_WebElement.FindElements(By.XPath("//table[@class='Report_Paper_Border_Shading']/tbody/tr[4]/td/table/tbody/tr/td[@class='Inner_Column_Left']/.."));
            string[] files = Directory.GetFiles(path, filename);
            Base_Assert.IsTrue(files.Length > 0, "Save pdf");
            Array.Reverse(files);
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(files[0]);
            StringBuilder content = new StringBuilder();

            foreach (PdfPageBase page in doc.Pages)
            {
                //创建一个PdfTextExtractot 对象
                PdfTextExtractor textExtractor = new PdfTextExtractor(page);
                //创建一个 PdfTextExtractOptions 对象
                PdfTextExtractOptions extractOptions = new PdfTextExtractOptions();
                extractOptions.IsExtractAllText = true;
                content.AppendLine(textExtractor.ExtractText(extractOptions));
            }
            //Console.WriteLine(content.ToString());
            string[] source = content.ToString().Split(new char[] { '.', '?' ,' ', '!', ';', ':', ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string searchTerm in searchList)
            {
                var matchQuery = from word in source
                    where word.Equals(searchTerm)//, StringComparison.InvariantCultureIgnoreCase
                                 select word;
                // Count the matches, which executes the query.  
                int wordCount = matchQuery.Count();
                Console.WriteLine(searchTerm + wordCount);
                //verify no.<tr> == ferquency(line+cerified) 
                Base_Assert.AreEqual(row.Count+1, wordCount, searchTerm);
            }
        }

        public static void Check_PDF_inner(string path, string filename, List<string> searchList)
        {
            var tablehead = Web.Report_Page.Report_Table._Selenium_WebElement.FindElements(By.XPath("//table[@class='Sub_Table_Indent']//table[@class='Order_Table_body_Style_Collapse']/tbody/tr[1]"));
            string[] files = Directory.GetFiles(path, filename);
            Array.Reverse(files);
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(files[0]);
            StringBuilder content = new StringBuilder();
            foreach (PdfPageBase page in doc.Pages)
            {
                //创建一个PdfTextExtractot 对象
                PdfTextExtractor textExtractor = new PdfTextExtractor(page);
                //创建一个 PdfTextExtractOptions 对象
                PdfTextExtractOptions extractOptions = new PdfTextExtractOptions();
                extractOptions.IsExtractAllText = true;
                content.AppendLine(textExtractor.ExtractText(extractOptions));
            }
            //Console.WriteLine(content.ToString());
            string[] source = content.ToString().Split(new char[] { '.', '?', ' ', '!', ';', ':', ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string searchTerm in searchList)
            {
                var matchQuery = from word in source
                                 where word.Equals(searchTerm)//, StringComparison.InvariantCultureIgnoreCase
                                 select word;
                // Count the matches, which executes the query.  
                int wordCount = matchQuery.Count();
                Console.WriteLine(searchTerm + wordCount);
                //verify no.<tr> == ferquency(line+cerified) 
                Base_Assert.IsTrue(tablehead.Count== wordCount, searchTerm+"Exsit");
            }
        }
        #endregion

        #region order function
        public static void active_order(string ordername)
        {
            string xpath = "//td[text()='" + ordername + "']";
            int i = 0;
            //get order
            while(i < 10)
            {
                try
                {
                    Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath(xpath));
                }
                catch(NoSuchElementException e)
                {
                    Web.Order_Page.Refresh.Click();
                    Thread.Sleep(2000);
                }
                i++;
            }
            var order = Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath(xpath));
            //check order status and active
            string status = order.FindElement(By.XPath("../td[7]")).Text;
            if (status == "Planned")
            {
                order.FindElement(By.XPath("..//td/span[@class='gwt-CheckBox']")).Click();
                Web.Order_Page.Activate.Click();
            }
            Thread.Sleep(2000);
            Base_Assert.AreEqual("Active", order.FindElement(By.XPath("../td[7]")).Text,"Active order");

        }
        #endregion
    }
}
