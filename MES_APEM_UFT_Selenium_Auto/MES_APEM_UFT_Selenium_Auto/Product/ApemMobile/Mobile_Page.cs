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
        public Selenium_WebElement Dialog => new Selenium_WebElement(_Selenium_Driver, "//mat-dialog-container");
        //Re-enter User dialog
        public Selenium_WebElement Title => new Selenium_WebElement(_Selenium_Driver, "//mat-dialog-container//h1");
        public Selenium_WebElements Inputs => new Selenium_WebElements(_Selenium_Driver, "//mat-dialog-container//mat-form-field//input");
        public Selenium_WebElement Login => new Selenium_WebElement(_Selenium_Driver, "//mat-dialog-container//button/span[text()=' OK ']");
        public Selenium_WebElement LogOut => new Selenium_WebElement(_Selenium_Driver, "//mat-dialog-container//button/span[text()=' Log Out ']");
        //dialog 
        public Selenium_WebElement Dialog_Yes => new Selenium_WebElement(_Selenium_Driver, "//mat-dialog-container//button/span[text()=' Yes ']");
        public Selenium_WebElement Dialog_No => new Selenium_WebElement(_Selenium_Driver, "//mat-dialog-container//button/span[text()=' No ']");
        //queue
        public Selenium_WebElement QueueButton => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='queue']/../..");
        public Selenium_WebElement QueueText => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='queue']/..");
        public Selenium_WebElement OrderName => new Selenium_WebElement(_Selenium_Driver, "//div[@id='dialog']/div/div[2]/div[1]/div[1]");
        public Selenium_WebElement PhaseName => new Selenium_WebElement(_Selenium_Driver, "//div[@id='dialog']/div/div[2]/div[1]/div[2]");
        public Selenium_WebElement QueueExecut => new Selenium_WebElement(_Selenium_Driver, "//div[@id='dialog']/div/div[2]/div[1]/mat-icon");
    }


    public class Login_Page : Mobile_Page

    {
        public Login_Page(IWebDriver driver) : base(driver)
        {
        }

        public Selenium_WebElement username => new Selenium_WebElement(_Selenium_Driver, "//input[@id='username']");
        public Selenium_WebElement password => new Selenium_WebElement(_Selenium_Driver, "//input[@type='password']");
        public Selenium_WebElement login => new Selenium_WebElement(_Selenium_Driver, "//button[@id='signInBtn']");
        //public Selenium_WebElement error => new Selenium_WebElement(_Selenium_Driver, "//div[@class='gwt-HTML Home_Login_Msg_Html']");
        
        //public static IWebElement username1 = _Selenium_Driver.FindElement(By.XPath("//input[@class='gwt-TextBox']"));
        //public static IWebElement password = _Selenium_Driver.FindElement(By.XPath("//input[@class='gwt-PasswordTextBox']"));
        //public static IWebElement login = _Selenium_Driver.FindElement(By.XPath("//button[@class='Home_Login_Button']"));

    }
    public class Logout_Page : Mobile_Page

    {
        public Logout_Page(IWebDriver driver) : base(driver)
        {
        }
        public Selenium_WebElement login => new Selenium_WebElement(_Selenium_Driver, "//button");


    }
    public class Main_Page : Mobile_Page

    {
        public Main_Page(IWebDriver driver) : base(driver)
        {
        }
        public Selenium_WebElement account => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='account']");
        public Selenium_WebElement logout => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='logout']");
        public Selenium_WebElement help => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='help']");

        #region Tabs
        public Selenium_WebElement ProcessOrder => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='process_order']");
        public Selenium_WebElement Tracking => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='tracking']");
        public Selenium_WebElement BPList => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='Tab-table']");
        public Selenium_WebElement Setting => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='settings']");
        public Selenium_WebElement Event => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='Event-List']");
        public Selenium_WebElement ManageModule => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='manageModule-32']");
        public Selenium_WebElement Consolidated => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='consolidated']");
        //public static IWebElement Administration = _Selenium_Driver.FindElement(By.XPath("//div[text()='Administration']"));

        #endregion
    }



    public class OrderProcess_Page : Mobile_Page

    {
        public OrderProcess_Page(IWebDriver driver) : base(driver)
        {
        }
        public Selenium_WebElement OrderSearch => new Selenium_WebElement(_Selenium_Driver, "//input[@id='ordersearch']");
        public Selenium_WebElement RefreshButton => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@svgicon='refresh']");
        public Selenium_WebElement SearchButton => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[data-mat-icon-name='search']");
        public Selenium_WebElement GotoTracking => new Selenium_WebElement(_Selenium_Driver,"//table/tbody/tr/td/a");
        public Selenium_WebElement ExecutionButton => new Selenium_WebElement(_Selenium_Driver, "//table/tbody/tr/td//a");
        public ReadOnlyCollection<IWebElement> OrderPhaseTableRows => _Selenium_Driver.FindElements(By.XPath("//table/tbody/tr"));
        public ReadOnlyCollection<IWebElement> OrderPhaseTableHeads => _Selenium_Driver.FindElements(By.XPath("//table/thead/tr/th/div/div[1]/div"));
        public Selenium_WebElement OrderHeader => new Selenium_WebElement(_Selenium_Driver, "//div[@id='headerOrder / Batch Code']/../../.."); 
    }
    public class BPList_Page : Mobile_Page

    {
        public BPList_Page(IWebDriver driver) : base(driver)
        {
        }
        public ReadOnlyCollection<IWebElement> BPListTableRows => _Selenium_Driver.FindElements(By.XPath("//table/tbody/tr"));
        public ReadOnlyCollection<IWebElement> BPListTableHeads => _Selenium_Driver.FindElements(By.XPath("//table/thead/tr/th/div/div[1]/div"));
        public Selenium_WebElement BPListTable => new Selenium_WebElement(_Selenium_Driver, "//table/tbody");
        public Selenium_WebElement BPSearch => new Selenium_WebElement(_Selenium_Driver, "//input[@id='ordersearch']");
        public Selenium_WebElement BPQueueButton => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='queue']/../..");
        public Selenium_WebElement BPQueuebpName => new Selenium_WebElement(_Selenium_Driver, "//div[@id='dialog']/div/div[2]/div[1]/div[1]");
        public Selenium_WebElement BPQueueExecut => new Selenium_WebElement(_Selenium_Driver, "//div[@id='dialog']/div/div[2]/div[1]/mat-icon");
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
        public ReadOnlyCollection<IWebElement> OrderPhaseNames => _Selenium_Driver.FindElements(By.XPath("//div[@class='phase-name-text']"));

        public Selenium_WebElement ReadyPhase => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@svgicon='phase_state_enabled']");
        public Selenium_WebElement PhaseHeader => new Selenium_WebElement(_Selenium_Driver, "//div[@id='headerPhase']/../../..");
        public Selenium_WebElement PFCButton => new Selenium_WebElement(_Selenium_Driver, "//mat-button-toggle[@value = 'pfc']");
        public Selenium_WebElement ParamtersAButton => new Selenium_WebElement(_Selenium_Driver, "//button[@aria-label = 'Toggle A']");
        public Selenium_WebElement PhaseListButton => new Selenium_WebElement(_Selenium_Driver, "//mat-button-toggle[@value = 'list']");
        public ReadOnlyCollection<IWebElement> OrderPhaseparam => _Selenium_Driver.FindElements(By.XPath("//div[@class='collapse-text description phase-with-param']"));

    }
    //Consolidated_Page
    public class Consolidated_Page : Mobile_Page

    {
        public Consolidated_Page(IWebDriver driver) : base(driver)
        {
        }
        public Selenium_WebElement OrderSearch => new Selenium_WebElement(_Selenium_Driver, "//input[@id='ordersearch']");
        public Selenium_WebElement RefreshButton => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@svgicon='refresh']");
        //Queue
        public Selenium_WebElement QueueButton => new Selenium_WebElement(_Selenium_Driver, "//div[@class='queue']/button");
        public Selenium_WebElements QueueIcon => new Selenium_WebElements(_Selenium_Driver, "//div[@id='dialog']//mat-icon");
        public ReadOnlyCollection<IWebElement> OrderPhaseTableRows => _Selenium_Driver.FindElements(By.XPath("//table/tbody/tr"));
        public Selenium_WebElement OrderPhaseTable => new Selenium_WebElement(_Selenium_Driver, "//table/tbody");
        public ReadOnlyCollection<IWebElement> OrderPhaseTableHeads => _Selenium_Driver.FindElements(By.XPath("//table/thead/tr/th/div/div[1]/div"));
        public Selenium_WebElement SelectMenu => new Selenium_WebElement(_Selenium_Driver, "//button[@id='selectmenu']");
        public Selenium_WebElement ExecutionButton => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@svgicon ='phase_state_enabled']"); 
        public Selenium_WebElement OrderHeader => new Selenium_WebElement(_Selenium_Driver, "//div[@id='headerOrder / Batch Code']/../../..");
    }
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

        
        public Selenium_WebElement PhaseTitle => new Selenium_WebElement(_Selenium_Driver, "/html/body/app-root/div/app-execution/div/div[1]/div[2]");
        public Selenium_WebElement OKButton => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' OK ']/../..");
        public Selenium_WebElement CancelButton => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' Cancel ']/../..");
        public Selenium_WebElement StopButton => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='toolbar_stop']");
        public Selenium_WebElement Confirmation_Text => new Selenium_WebElement(_Selenium_Driver, "//div[@id='dialog']//textarea");
        public Selenium_WebElement Confirmation_password => new Selenium_WebElement(_Selenium_Driver, "//input[@type='password']");
        public Selenium_WebElement ConfirmationOK_button => new Selenium_WebElement(_Selenium_Driver, "//span[text()=' OK ']/..");
        //629827
        public Selenium_WebElement FooterOKButton => new Selenium_WebElement(_Selenium_Driver, "//div[text()='OK']/../..");
        public Selenium_WebElement Password => new Selenium_WebElement(_Selenium_Driver, "//input[@id='main.Password0']");

        //Soap
        public Selenium_WebElement SOAP_CALL2_EXButton => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' SOAP_CALL2_EX ']/../..");
        public Selenium_WebElement SOAP_CALL2_Button => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' SOAP_CALL2 ']/../..");
        public Selenium_WebElement BPC_Button => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' BPC ']/../..");
        public Selenium_WebElement MainField1 => new Selenium_WebElement(_Selenium_Driver, "//input[@id='Main.Field1']");
        public Selenium_WebElement MainField0 => new Selenium_WebElement(_Selenium_Driver, "//input[@id='main.Field0']");
        public Selenium_WebElement ConfirmYesButton => new Selenium_WebElement(_Selenium_Driver, "//div[@id='dialog']/div/div[3]/button[1]");
        public Selenium_WebElement Wait_message => new Selenium_WebElement(_Selenium_Driver, "//div[@class='padding']/div[2]/div/div/div");
        //961928
        public Selenium_WebElement SET_CURRENT_USER_Button => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' SET_CURRENT_USER (\"qae\\qaone2\") ']/../..");
        public Selenium_WebElements Labels => new Selenium_WebElements(_Selenium_Driver, "//div[@id='execution']//label");
        //962328
        public Selenium_WebElement DELETE_ORDER_Button => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' DELETE_ORDER ']/../..");
        //962338
        public Selenium_WebElements Numbers => new Selenium_WebElements(_Selenium_Driver, "//p[@class='ng-star-inserted']");
        //920999
        public Selenium_WebElement tranfer2field4_input => new Selenium_WebElement(_Selenium_Driver, "//input[@id='TEST.F_Scan2']");
        public Selenium_WebElement Selectline3_button => new Selenium_WebElement(_Selenium_Driver, "//button[@id='TEST.btnPrintLabel']");
        public Selenium_WebElement Field3_input => new Selenium_WebElement(_Selenium_Driver, "//input[@id='TEST.F_Scan3']");
        public Selenium_WebElement Execution_Message => new Selenium_WebElement(_Selenium_Driver, "//div[@class='padding']/div[2]/div/div/div/p");
        public Selenium_WebElement MessageOK_button => new Selenium_WebElement(_Selenium_Driver, "//span[text()=' OK ']/..");
        public Selenium_WebElement Field4 => new Selenium_WebElement(_Selenium_Driver, "//input[@id='TEST.F_Scan4']/../../../..");
        public ReadOnlyCollection<IWebElement> TableRows => _Selenium_Driver.FindElements(By.XPath("//tbody/tr[@role='row']"));
        //919004
        public Selenium_WebElement Field => new Selenium_WebElement(_Selenium_Driver, "//input[@id='TEST.Field0']");
        public Selenium_WebElement message_label => new Selenium_WebElement(_Selenium_Driver, "//label[text()='segunda screen']/..");
        //918007
        public Selenium_WebElement WritedatainBR_button  => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' Write data in BR ']/../..");
        public Selenium_WebElement ReadData_button => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' read data ']/../..");
        public Selenium_WebElement Path_table => new Selenium_WebElement(_Selenium_Driver, "//tbody[@role='rowgroup']/tr[2]//input");
        //771207  
        public Selenium_WebElement time_label => new Selenium_WebElement(_Selenium_Driver, "//div[@id='screen']/app-aebrs-label[2]/label");
        //468668
        public Selenium_WebElement GET_ORDER_STATEButton => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' GET_ORDER_STATE ']/../..");
        //481813
        public Selenium_WebElement SQL_SELECT_ONEButton => new Selenium_WebElement(_Selenium_Driver, "//button[@id='Main.Button0']");
        //540082
        public Selenium_WebElement HU_field => new Selenium_WebElement(_Selenium_Driver, "//input[@id='TEST.F_HU']");
        public Selenium_WebElement ResetHU_button => new Selenium_WebElement(_Selenium_Driver, "//button[@id='TEST.B_HU']");
        //1002790
        public Selenium_WebElement Habilitar_Button => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' habilitar ']/../..");
        public Selenium_WebElement Deshabilitar_Button => new Selenium_WebElement(_Selenium_Driver, "//div[text()=' deshabilitar ']/../..");
        public Selenium_WebElement check_box1 => new Selenium_WebElement(_Selenium_Driver, "//mat-checkbox[@id='MAIN.Check1']//input");
        public Selenium_WebElement check_box2 => new Selenium_WebElement(_Selenium_Driver, "//mat-checkbox[@id='MAIN.Check2']//input");
        public Selenium_WebElement check_box_label1 => new Selenium_WebElement(_Selenium_Driver, "//mat-checkbox[@id='MAIN.Check1']//label");
        public Selenium_WebElement check_box_label2 => new Selenium_WebElement(_Selenium_Driver, "//mat-checkbox[@id='MAIN.Check2']//label");
        public Selenium_WebElement check_value => new Selenium_WebElement(_Selenium_Driver, "//div[@id='screen']/app-aebrs-label[2]/label");
        //29671
        public Selenium_WebElement TestInArray => new Selenium_WebElement(_Selenium_Driver, "//button[@id='Main.Button0']");
        //1365479
        public Selenium_WebElement column0_0 => new Selenium_WebElement(_Selenium_Driver, "//input[@id='SCREEN.Tb_Defect0_0']");
        public Selenium_WebElement column0_1 => new Selenium_WebElement(_Selenium_Driver, "//input[@id='SCREEN.Tb_Defect0_1']");
        public Selenium_WebElement column1_0 => new Selenium_WebElement(_Selenium_Driver, "//input[@id='SCREEN.Tb_Defect1_0']");
        public Selenium_WebElement column1_1 => new Selenium_WebElement(_Selenium_Driver, "//input[@id='SCREEN.Tb_Defect1_1']");
        public ReadOnlyCollection<IWebElement> Value_Options => _Selenium_Driver.FindElements(By.XPath("//div[@role='listbox']/mat-option"));
        //41512
        public Selenium_WebElement Createorder => new Selenium_WebElement(_Selenium_Driver, "//button[@id='main.Button0']");
        public Selenium_WebElement Createorder_Version => new Selenium_WebElement(_Selenium_Driver, "//app-aebrs-label[2]/label");
        

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

        public Selenium_WebElement DarkMode => new Selenium_WebElement(_Selenium_Driver, "//div[@class='show-navigation'][3]/div/div[2]/mat-slide-toggle");

        public Selenium_WebElement Consolidated => new Selenium_WebElement(_Selenium_Driver, "//div[@class='show-navigation'][4]/div/div[2]/mat-slide-toggle");

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
    public class SessionManager_Page : Mobile_Page

    {
        public SessionManager_Page(IWebDriver driver) : base(driver)
        {
        }

        public Selenium_WebElement Table => new Selenium_WebElement(_Selenium_Driver, "//table");
        public Selenium_WebElement Search => new Selenium_WebElement(_Selenium_Driver, "//input[@id='ordersearch']");
        public Selenium_WebElement RefreshButton => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@svgicon='refresh']");
        public Selenium_WebElement CancePhase => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='CancePhase_manageModule']");
        public ReadOnlyCollection<IWebElement> CancePhases => _Selenium_Driver.FindElements(By.XPath("//mat-icon[@data-mat-icon-name='CancePhase_manageModule']"));
        public Selenium_WebElement CloseSession => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='CloseSession_manageModule']");
        public Selenium_WebElement CloseSessionDark => new Selenium_WebElement(_Selenium_Driver, "//mat-icon[@data-mat-icon-name='CloseSession_darkMode']");
        public ReadOnlyCollection<IWebElement> CloseSessions => _Selenium_Driver.FindElements(By.XPath("//mat-icon[@id='closeBtn']"));
        public ReadOnlyCollection<IWebElement> TableRows => _Selenium_Driver.FindElements(By.XPath("//table/tbody/tr"));
        public ReadOnlyCollection<IWebElement> TableHeads => _Selenium_Driver.FindElements(By.XPath("//table/thead/tr/th"));


       

    }
}


