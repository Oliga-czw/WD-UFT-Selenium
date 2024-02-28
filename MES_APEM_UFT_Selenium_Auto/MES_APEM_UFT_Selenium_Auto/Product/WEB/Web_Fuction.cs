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
using System.Drawing;
using HP.LFT.SDK;
using System.Windows.Forms;

namespace MES_APEM_UFT_Selenium_Auto.Product.WD
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
            Thread.Sleep(4000);
            if (Web.Administration_Page.Apply.GetAttribute("disabled") is null)
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
        public static void permissionUpdate(IWebDriver driver, string[] permission_list)
        {
            var permissionList = driver.FindElements(By.XPath("//span[@class='gwt-CheckBox']/label"));
            //int count001 = permissionList.Count;

            foreach (var permission in permissionList)
            {
                //System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", count001.ToString(), Encoding.Default);
                var inputXpath = "//label[text()='" + permission.Text + "']/../input";
                if (permission_list.Length == 2)
                {
                    if (permission.Text == permission_list[0] || permission.Text == permission_list[1])
                    {
                        var NameList = driver.FindElements(By.XPath(inputXpath));
                        foreach (var Name in NameList)
                        {
                            if (Name.GetAttribute("checked") != null)
                            {
                                Name.Click();
                            }
                        }
                    }
                }
                else
                {
                    if (permission.Text.ToLower().Contains(permission_list[0].ToLower()))
                    {
                        var NameList = driver.FindElements(By.XPath(inputXpath));
                        foreach (var Name in NameList)
                        {
                            if (Name.GetAttribute("checked") != null)
                            {
                                Name.Click();
                            }
                        }
                    }
                }

            }
            driver.FindElement(By.XPath("//button[text()='Apply']")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//button[text()='OK']")).Click();
        }
        public static void RestorePermission(IWebDriver driver)
        {
            var checkbox = driver.FindElements(By.XPath("//span[@class='gwt-CheckBox']/input"));
            foreach (var input in checkbox)
            {
                if (input.GetAttribute("checked") == null)
                {
                    input.Click();
                }
            }
            //check apply button enable
            if (driver.FindElement(By.XPath("//button[text()='Apply']")).Enabled)
            {
                driver.FindElement(By.XPath("//button[text()='Apply']")).Click();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//button[text()='OK']")).Click();
            }
        }

        public static void FirstGrantPermission()
        {
            //first grant all permission
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait(30000);
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Permissions.Click();
            Web_Fuction.RestorePermission(Selenium_Driver._Selenium_Driver);
            driver.Close();
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

        public static void check_report_date(DateTime execute_time)
        {
            var date = Web.Report_Page.Report_Table._Selenium_WebElement.FindElements(By.XPath("//table[@class='Report_Paper_Border_Shading']/tbody/tr[4]/td/table/tbody/tr/td[@class='Inner_Column_Left']"))[0].Text;
            DateTime report_time = Convert.ToDateTime(date);
            Base_Assert.IsTrue(Math.Abs(report_time.Subtract(execute_time).TotalSeconds) < 30, "date time is right");
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

        public static void Check_audit(List<string> columns, List<string> datatexts)
        {

            var data_list = new List<List<string>>();
            var head_list = new List<string>();
            var head = Web.Report_Page.body._Selenium_WebElement.FindElements(By.XPath("//td[@class='Audit_Report_Table_Header_Left']//a"));
            //get head list
            foreach (var h in head)
            {
                head_list.Add(h.Text);
            }
            var row = Web.Report_Page.body._Selenium_WebElement.FindElements(By.XPath("//td[@class='Inner_Column_Left']/.."));
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
                //check first row reason
                if(columns[i] == "Reason for change")
                {
                    Base_Assert.AreEqual(datatext, data_list[0][number],"audit report");
                }
                else 
                {
                    for (int m = 0; m < data_list.Count; m++)
                    {
                        Base_Assert.AreEqual(datatext, data_list[m][number], "audit report");
                    }
                }
                

            }

            
        }
        public static void Check_audit_difference(List<string> columns, List<string> datatexts)
        {
            //expand first row
            Web.Report_Page.body._Selenium_WebElement.FindElements(By.XPath("//td[@class='Inner_Column_Left']/..//img"))[0].Click();

            var data_list = new List<List<string>>();
            var head_list = new List<string>();
            var head = Web.Report_Page.body._Selenium_WebElement.FindElements(By.XPath("//td[@class='Inner_Column_Left']/div[@class='gwt-Label']/../../../tr//a"));
            //get head list
            foreach (var h in head)
            {
                head_list.Add(h.Text);
            }
            var row = Web.Report_Page.body._Selenium_WebElement.FindElements(By.XPath("//td[@class='Inner_Column_Left']/div[@class='gwt-Label']/../.."));
            
            //get table data

            for (int j = 0; j < row.Count; j++)
            {
                var single_row_text = new List<string>();
                var cells = row[j].FindElements(By.CssSelector("div.gwt-Label"));
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
                //check first row reason
              
                    for (int m = 0; m < data_list.Count; m++)
                    {
                        Base_Assert.AreEqual(datatext, data_list[m][number],"show difference");
                    }


            }


        }
        public static void check_audit_date(DateTime execute_time)
        {
            var date = Web.Report_Page.body._Selenium_WebElement.FindElements(By.XPath("//td[@class='Inner_Column_Left']/../td[@class='Inner_Column_Left']"))[0].Text;
            DateTime report_time = Convert.ToDateTime(date);
            Base_Assert.IsTrue(Math.Abs(report_time.Subtract(execute_time).TotalSeconds) < 30, "date time is right");
        }
        public static (string,string) check_weighing_report_source(string action)
        {
            Thread.Sleep(3000);
            Web.Report_Page.Generate_Report.Click();
            Thread.Sleep(3000);
            var head = Web.Report_Page.Report_Heads;
            //get head list
            int i = 0;
            int Begin_Source_index = 0;
            int End_Source_index = 0;
            int Action_index = 0;
            foreach (var h in head)
            {
                if (h.Text == "Begin Source")
                {
                    Begin_Source_index = i;
                }
                else if (h.Text == "End Source")
                {
                    End_Source_index = i;
                }
                else if (h.Text == "Action")
                {
                    Action_index = i;
                }
                i++;
            }
            var table_datas = Web.Report_Page.Report_Table_Rows;
            int j = 0;
            int index = 0;
            foreach (var table_data in table_datas)
            {
                if (table_data.FindElements(By.TagName("td"))[Action_index].Text == action)
                {
                    index = j;
                    break;
                }
                j++;
            }
            var begin_source = table_datas[index].FindElements(By.TagName("td"))[Begin_Source_index].Text;
            var end_source = table_datas[index].FindElements(By.TagName("td"))[End_Source_index].Text;
            return (begin_source, end_source);
        }
        #endregion

        #region order function
        public static void active_order(string ordername)
        {
            Thread.Sleep(2000);
            string xpath = "//td[text()='" + ordername + "']";
            //get order
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).Clear();
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).SendKeys(ordername);
            Thread.Sleep(2000);
            var order = Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath(xpath));
            //check order status and active
            string status = order.FindElement(By.XPath("../td[7]")).Text;
            if (status == "Planned")
            {
                order.FindElement(By.XPath("..//td/span[@class='gwt-CheckBox']")).Click();
                Web.Order_Page.Activate.Click();
            }
            Thread.Sleep(2000);
//            Base_Assert.AreEqual("Active", order.FindElement(By.XPath("../td[7]")).Text,"Active order");
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).Clear();

        }
        public static void Archive_order(string ordername)
        {
            Thread.Sleep(2000);
            string xpath = "//td[text()='" + ordername + "']";
            //get order
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).Clear();
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).SendKeys(ordername);
            Thread.Sleep(2000);
            var order = Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath(xpath));
            //check order status and archive
            string status = order.FindElement(By.XPath("../td[7]")).Text;
            order.FindElement(By.XPath("..//td/span[@class='gwt-CheckBox']")).Click();
            Web.Order_Page.Archive.Click();
            Thread.Sleep(2000);
            Base_Assert.AreEqual("Archived", order.FindElement(By.XPath("../td[7]")).Text, "Archive order");
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).Clear();
        }
        public static void Finish_order(string ordername)
        {
            Thread.Sleep(2000);
            string xpath = "//td[text()='" + ordername + "']";
            //get order
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).Clear();
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).SendKeys(ordername);
            Thread.Sleep(2000);
            var order = Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath(xpath));
            string status = order.FindElement(By.XPath("../td[7]")).Text;
            order.FindElement(By.XPath("..//td/span[@class='gwt-CheckBox']")).Click();
            Web.Order_Page.Finish.Click();
            Thread.Sleep(2000);
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).Clear();
        }
        public static void Redispense_order(string ordername)
        {
            Thread.Sleep(2000);
            string xpath = "//td[text()='" + ordername + "']";
            //get order
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).Clear();
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).SendKeys(ordername);
            Thread.Sleep(2000);
            var order = Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath(xpath));
            order.FindElement(By.XPath("../td[3]/img")).Click();
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//a[text()='Redispense a Material']")).Click();
            Thread.Sleep(2000);
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//td[text()='Non-FEFO']/../td[1]/span/input")).Click();
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//button[text()='Redispense Material']")).Click();
            Thread.Sleep(2000);
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@type='password']")).SendKeys(PassWord.qaone1);
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//button[@id='Dialogbox_Bottom_OK_Button_Id']")).Click();
            Thread.Sleep(2000);
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).Clear();
        }
        public static void cancel_order(string ordername)
        {
            Thread.Sleep(2000);
            string xpath = "//td[text()='" + ordername + "']";
            //get order
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).Clear();
            Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath("//input[@class='Tab_Manu_bar_Margin Tab_Menu_Bar_Search_Box']")).SendKeys(ordername);
            Thread.Sleep(2000);
            var order = Web.Order_Page.body._Selenium_WebElement.FindElement(By.XPath(xpath));
            //check order status and active
            string status = order.FindElement(By.XPath("../td[7]")).Text;
            order.FindElement(By.XPath("..//td/span[@class='gwt-CheckBox']")).Click();
            Web.Order_Page.Cancel.Click();
            Thread.Sleep(2000);
            Base_Assert.AreEqual("Cancelled", order.FindElement(By.XPath("../td[7]")).Text, "Cancell order");

        }
        public static void edit_order(string order)
        {
            string xpath = "//td[text()='" + order + "']/../td[3]/img";
            Web.Equipment_Page.body._Selenium_WebElement.FindElement(By.XPath(xpath)).Click();
        }
        public static string OrderPrint(string reportfile)
        {
            string pdfFilePath = Base_Directory.WDReport+reportfile;
            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(pdfFilePath);
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
            string reportText = content.ToString();
            Console.WriteLine(content.ToString());
            return reportText;
        }
        #endregion
    }
}
