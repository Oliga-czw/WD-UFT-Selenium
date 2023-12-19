using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using System.Collections.ObjectModel;
using HP.LFT.SDK;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace MES_APEM_UFT_Selenium_Auto.Product.ApemMobile
{
    public class Mobile_Page : Selenium_Driver
    {
        public Mobile_Page(IWebDriver driver) : base(driver)
        {
        }

        public Selenium_WebElement body => new Selenium_WebElement(_Selenium_Driver, "//body");
        public Selenium_WebElement Message => new Selenium_WebElement(_Selenium_Driver, "//div[@title='Message']");
        public Selenium_WebElement Confirm => new Selenium_WebElement(_Selenium_Driver, "//div[@title='Confirm']");
        public Selenium_WebElement body_div => new Selenium_WebElement(_Selenium_Driver, "/html/body/div[@class='cdk-overlay-container']");

    }


    public class Login_Page : Mobile_Page

    {
        public Login_Page(IWebDriver driver) : base(driver)
        {
        }

        public Selenium_WebElement username => new Selenium_WebElement(_Selenium_Driver, "//input[@id='username']");
        public Selenium_WebElement password => new Selenium_WebElement(_Selenium_Driver, "//input[@id='mat-input-1']");
        public Selenium_WebElement login => new Selenium_WebElement(_Selenium_Driver, "//button[@id='signInBtn']");
        //public Selenium_WebElement error => new Selenium_WebElement(_Selenium_Driver, "//div[@class='gwt-HTML Home_Login_Msg_Html']");
        
        //public static IWebElement username1 = _Selenium_Driver.FindElement(By.XPath("//input[@class='gwt-TextBox']"));
        //public static IWebElement password = _Selenium_Driver.FindElement(By.XPath("//input[@class='gwt-PasswordTextBox']"));
        //public static IWebElement login = _Selenium_Driver.FindElement(By.XPath("//button[@class='Home_Login_Button']"));

    }

    public class Main_Page : Mobile_Page

    {
        public Main_Page(IWebDriver driver) : base(driver)
        {
        }
        #region Tabs
        public Selenium_WebElement ProcessOrder => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='process_order']");
        public Selenium_WebElement Tracking => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='tracking']");
        public Selenium_WebElement BPList => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='Tab-table']");
        public Selenium_WebElement Setting => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='settings']");
        public Selenium_WebElement Event => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='Event-List']");
        public Selenium_WebElement ss => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='Event-ss']");
        //public static IWebElement Administration = _Selenium_Driver.FindElement(By.XPath("//div[text()='Administration']"));

        #endregion
    }



    public class OrderProcess_Page : Mobile_Page

    {
        public OrderProcess_Page(IWebDriver driver) : base(driver)
        {
        }
        public Selenium_WebElement OrderSearch => new Selenium_WebElement(_Selenium_Driver, "//input[@id='ordersearch']");
        public Selenium_WebElement SearchButton => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[data-mat-icon-name='search']");
        public Selenium_WebElement GotoTracking => new Selenium_WebElement(_Selenium_Driver,"//table/tbody/tr/td/a");
        public Selenium_WebElement ExecutionButton => new Selenium_WebElement(_Selenium_Driver, "//table/tbody/tr/td//a");
    }
    public class OrderTracking_Page : Mobile_Page

    {
        public OrderTracking_Page(IWebDriver driver) : base(driver)
        {
        }
        public ReadOnlyCollection<IWebElement> OrderPhaseTableRows => _Selenium_Driver.FindElements(By.XPath("//table/tbody/tr"));
        public ReadOnlyCollection<IWebElement> OrderPhaseTableHeads => _Selenium_Driver.FindElements(By.XPath("//table/thead/tr/th/div/div[1]/div"));
        public Selenium_WebElement SelectMenu => new Selenium_WebElement(_Selenium_Driver, "//button[@id='selectmenu']");
        public Selenium_WebElement ExecutionButton => new Selenium_WebElement(_Selenium_Driver, "//table/tbody/tr/td//a");
    }
    //PrintReport_Page
    public class PrintReport_Page : Mobile_Page

    {
        public PrintReport_Page(IWebDriver driver) : base(driver)
        {
        }
        //public Selenium_WebElement OrderSearch => new Selenium_WebElement(_Selenium_Driver, "//input[@id='ordersearch']");

        //public Selenium_WebElement GotoTracking => new Selenium_WebElement(_Selenium_Driver, "//table/tbody/tr/td/a");
        public Selenium_WebElement ExecuteAPP => new Selenium_WebElement(_Selenium_Driver, "/html/body/p[6]");
    }
    public class OrderExecution_Page : Mobile_Page

    {
        public OrderExecution_Page(IWebDriver driver) : base(driver)
        {
        }
        public Selenium_WebElement OKButton => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' OK ']/../..");
        public Selenium_WebElement CancelButton => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' Cancel ']/../..");
        public Selenium_WebElement StopButton => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[data-mat-icon-name='toolbar_stop']");
        //Soap
        public Selenium_WebElement SOAP_CALL2_EXButton => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' SOAP_CALL2_EX ']/../..");
        public Selenium_WebElement SOAP_CALL2_Button => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' SOAP_CALL2 ']/../..");
        public Selenium_WebElement BPC_Button => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' BPC ']/../..");
        public Selenium_WebElement MainField1 => new Selenium_WebElement(_Selenium_Driver, "//input[@id='Main.Field1']");
        public Selenium_WebElement MainField0 => new Selenium_WebElement(_Selenium_Driver, "//input[@id='main.Field0']");
        public Selenium_WebElement ConfirmYesButton => new Selenium_WebElement(_Selenium_Driver, "//div[@id='dialog']/div/div[3]/button[1]");
    }



    public class EventLog_Page : Mobile_Page

    {
        public EventLog_Page(IWebDriver driver) : base(driver)
        {
        }

        public Selenium_WebElement EventLogTable => new Selenium_WebElement(_Selenium_Driver, "//table");

        public Selenium_WebElement SelectMenu => new Selenium_WebElement(_Selenium_Driver, "//button[@id='selectmenu']");

        public Selenium_WebElement Search => new Selenium_WebElement(_Selenium_Driver, "//input[@id='ordersearch']");


        public ReadOnlyCollection<IWebElement> EventLogTableRows => _Selenium_Driver.FindElements(By.XPath("//table/tbody/tr"));
        public ReadOnlyCollection<IWebElement> EventLogTableHeads => _Selenium_Driver.FindElements(By.XPath("//table/thead/tr/th"));
        public ReadOnlyCollection<IWebElement> ColumnMenu => _Selenium_Driver.FindElements(By.XPath("//div[@role='menu']//div[@class='mat-list-text']"));

        public ReadOnlyCollection<IWebElement> ColumnCheckboxes => _Selenium_Driver.FindElements(By.XPath("//mat-pseudo-checkbox"));

        public void Sellect_all_col()
        {
            Mobile.EventLog_Page.SelectMenu.Click();
            var checkboxes = ColumnCheckboxes;
            foreach (IWebElement checkbox in checkboxes)
            {
                if (!checkbox.GetAttribute("class").Contains("checked"))
                {
                    checkbox.Click();
                }
            }
            //press tab to infect
            //click twice avoide tab no infect
            checkboxes[0].Click();
            checkboxes[0].Click();
            Keyboard.KeyDown(Keyboard.Keys.Tab);
            Keyboard.KeyUp(Keyboard.Keys.Tab);
            Thread.Sleep(2000);
        }

        public void delete_col(List<string> delete_col)
        {
            Mobile.EventLog_Page.SelectMenu.Click();
            int i = 0;
            foreach (IWebElement col in Mobile.EventLog_Page.ColumnMenu)
            {
                //string script = $"\"//div[@role='menu']//div[@class='mat-list-text']\")[{i}].textContent";
                //Console.WriteLine(driver.execute_script_return("arguments[0].textContent;", col));
                //Console.WriteLine(col.GetAttribute("innerText"));
                if (delete_col.Contains(col.GetAttribute("innerText").Trim()))
                {
                    col.Click();
                    Thread.Sleep(1000);
                }
                Thread.Sleep(1000);
                i++;
            }
            //press tab to infect
            Keyboard.KeyDown(Keyboard.Keys.Tab);
            Keyboard.KeyUp(Keyboard.Keys.Tab);
            Thread.Sleep(2000);
        }
    }
    public class Setting_Page : Mobile_Page

    {
        public Setting_Page(IWebDriver driver) : base(driver)
        {
        }

        public Selenium_WebElement DarkMode => new Selenium_WebElement(_Selenium_Driver, "//div[@class='show-navigation'][4]/div/div[2]/mat-slide-toggle");

        public Selenium_WebElement Consolidated => new Selenium_WebElement(_Selenium_Driver, "//div[@class='show-navigation'][5]/div/div[2]/mat-slide-toggle");

        /// <summary>  
        /// turn on mode. 1 = "dark",2 = "consolidated"
        /// </summary>  
        /// <param name="mode">dd</param> 
        public void turnOn_mode(int number)
        { switch (number)
            {
                case 1:
                    if (Mobile.Setting_Page.DarkMode._Selenium_WebElement.FindElement(By.TagName("input")).GetAttribute("aria-checked") == "false")
                    {
                        Mobile.Setting_Page.DarkMode.Click();
                        Thread.Sleep(5000);
                    }
                    break;
                case 2:
                    if (Mobile.Setting_Page.Consolidated._Selenium_WebElement.FindElement(By.TagName("input")).GetAttribute("aria-checked") == "false")
                    {
                        Mobile.Setting_Page.Consolidated.Click();
                        Thread.Sleep(5000);
                    }
                    break;
            }
                
        }
        /// <summary>  
        /// turn on mode. 1 = "dark",2 = "consolidated"
        /// </summary>  
        /// <param name="mode">dd</param> 
        public void turnOff_mode(int number)
        {
            switch (number)
            {
                case 1:
                    if (Mobile.Setting_Page.DarkMode._Selenium_WebElement.FindElement(By.TagName("input")).GetAttribute("aria-checked") == "true")
                    {
                        Mobile.Setting_Page.DarkMode.Click();
                        Thread.Sleep(5000);
                    }
                    break;
                case 2:
                    if (Mobile.Setting_Page.Consolidated._Selenium_WebElement.FindElement(By.TagName("input")).GetAttribute("aria-checked") == "true")
                    {
                        Mobile.Setting_Page.Consolidated.Click();
                        Thread.Sleep(5000);
                    }
                    break;
            }

        }
    }
}


