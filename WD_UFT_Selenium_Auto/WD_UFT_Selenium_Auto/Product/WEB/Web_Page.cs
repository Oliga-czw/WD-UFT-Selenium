using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;

namespace WD_UFT_Selenium_Auto.Product.WD
{
    public class Web_Page : Selenium_Driver
    {
        public Web_Page(IWebDriver driver) : base(driver)
        {
        }

        public Selenium_WebElement body => new Selenium_WebElement(_Selenium_Driver, "//body");
        public Selenium_WebElement Message => new Selenium_WebElement(_Selenium_Driver, "//div[@title='Message']");

    }
    public class Login_Page : Web_Page

    {
        public Login_Page(IWebDriver driver) : base(driver)
        {
        }

        public Selenium_WebElement username => new Selenium_WebElement(_Selenium_Driver, "//input[@class='gwt-TextBox']");
        public Selenium_WebElement password => new Selenium_WebElement(_Selenium_Driver, "//input[@class='gwt-PasswordTextBox']");
        public Selenium_WebElement login => new Selenium_WebElement(_Selenium_Driver, "//button[@class='Home_Login_Button']");
        public Selenium_WebElement error => new Selenium_WebElement(_Selenium_Driver, "//div[@class='gwt-HTML Home_Login_Msg_Html']");

        //public static IWebElement username1 = _Selenium_Driver.FindElement(By.XPath("//input[@class='gwt-TextBox']"));
        //public static IWebElement password = _Selenium_Driver.FindElement(By.XPath("//input[@class='gwt-PasswordTextBox']"));
        //public static IWebElement login = _Selenium_Driver.FindElement(By.XPath("//button[@class='Home_Login_Button']"));

    }

    public class Main_Page : Web_Page

    {
        public Main_Page(IWebDriver driver) : base(driver)
        {
        }
        #region Tabs
        public Selenium_WebElement Administration => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Administration']");
        public Selenium_WebElement Material => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Material']");
        public Selenium_WebElement Inventory => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Inventory']");
        public Selenium_WebElement Order => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Order']");
        public Selenium_WebElement Report => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Report']");
        public Selenium_WebElement Equipment => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Equipment']");
        //public static IWebElement Administration = _Selenium_Driver.FindElement(By.XPath("//div[text()='Administration']"));

        #endregion
    }

    public class Administration_Page : Web_Page

    {
        public Administration_Page(IWebDriver driver) : base(driver)
        {
        }

        #region Administration
        public Selenium_WebElement CleaningRules => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Cleaning Rules']");
        public Selenium_WebElement Deviations => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Deviations']");
        public Selenium_WebElement General => new Selenium_WebElement(_Selenium_Driver, "//div[text()='General']");
        public Selenium_WebElement Signatures => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Signatures']");
        public Selenium_WebElement Permissions => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Permissions']");
        public Selenium_WebElement UserExits => new Selenium_WebElement(_Selenium_Driver, "//div[text()='User Exits']");
        


        public Selenium_WebElement Apply => new Selenium_WebElement(_Selenium_Driver, "//button[text()='Apply']");
        //public static IWebElement CleaningRules = _Selenium_Driver.FindElement(By.XPath("//div[text()='Cleaning Rules']"));
        //public static IWebElement States = _Selenium_Driver.FindElement(By.XPath("//div[text()='States']"));
        #endregion
        #region clean rules
        public Selenium_WebElement States => new Selenium_WebElement(_Selenium_Driver, "//div[text()='States']");
        public Selenium_WebElement Data => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Data']");
        public Selenium_WebElement Actions => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Actions']");
        public Selenium_WebElement Types => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Types']");
        public Selenium_WebElement Transitions => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Transitions']");
        #endregion

        #region deviations
        public Selenium_WebElement deviation_table => new Selenium_WebElement(_Selenium_Driver, "//table[@class='Permission_Table_body_Style']/tbody", 0);

        #endregion
        #region UserExits
        public Selenium_WebElement ERPDownload => new Selenium_WebElement(_Selenium_Driver, "//div[text()='ERP Download']");



        public Selenium_WebElement ERPInventoryDownload => new Selenium_WebElement(_Selenium_Driver, "//div[text()='ERP Inventory Download']");
        public Selenium_WebElement ERPOrderDownload => new Selenium_WebElement(_Selenium_Driver, "//div[text()='ERP Order Download']");
        public Selenium_WebElement ERPMaterialDownload => new Selenium_WebElement(_Selenium_Driver, "//div[text()='ERP Material Download']");
        #endregion

        #region General
        public Selenium_WebElement log_on_required_chx => new Selenium_WebElement(_Selenium_Driver, "//label[text()='Log on required for Execution System']/../input");

        #endregion
    }
    public class Equipment_Page : Web_Page

    {
        public Equipment_Page(IWebDriver driver) : base(driver)
        {
        }

        
        public Selenium_WebElement Apply => new Selenium_WebElement(_Selenium_Driver, "//button[text()='Apply']");
        public Selenium_WebElement booth_copy_row => new Selenium_WebElement(_Selenium_Driver, "//a[text()='Copy selected row']", 0);
        #region scales
        public Selenium_WebElement scale_copy_row => new Selenium_WebElement(_Selenium_Driver, "//a[text()='Copy selected row']", 1);
        public Selenium_WebElement simultor_name => new Selenium_WebElement(_Selenium_Driver, "//input[@name='scaleTag']");
        public Selenium_WebElement simultor_status => new Selenium_WebElement(_Selenium_Driver, "//select[@name='statusValue']");
        public Selenium_WebElement simultor_description => new Selenium_WebElement(_Selenium_Driver, "//input[@name='description']");


        #endregion

        #region booth
        public Selenium_WebElement booth_status => new Selenium_WebElement(_Selenium_Driver, "//select[@name='boothStatusValue']");
        #endregion
    }

    public class Report_Page : Web_Page

    {
        public Report_Page(IWebDriver driver) : base(driver)
        {
        }
        #region report
        public Selenium_WebElement Cleaning => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Cleaning']");
        public Selenium_WebElement Weighing => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Weighing']");
        public Selenium_WebElement ScaleCheck => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Scale Check']");
        public Selenium_WebElement LabelReprint => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Label Reprint']");
        
        public Selenium_WebElement Permissions => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Permissions']");
        #endregion
        public Selenium_WebElement Start_Time => new Selenium_WebElement(_Selenium_Driver, "//input[@class='Date_TextBox_Style']/../../td/img",0);
        public Selenium_WebElement End_Time => new Selenium_WebElement(_Selenium_Driver, "//input[@class='Date_TextBox_Style']/../../td/img",1);
        //public Selenium_WebElement Type => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Cleaning']");
        //public Selenium_WebElement Booth => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Cleaning']");
        //public Selenium_WebElement Operator => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Cleaning']");
       
        public Selenium_WebElement Generate_Report => new Selenium_WebElement(_Selenium_Driver, "//button[text()='Generate Report']");
        public Selenium_WebElement Generate_Audit => new Selenium_WebElement(_Selenium_Driver, "//button[text()='Generate Audit']");
        public Selenium_WebElement Report_Table => new Selenium_WebElement(_Selenium_Driver, "//table[@class='Order_Table_body_Style_Collapse']/tbody");


        public Selenium_WebElement SaveAs => new Selenium_WebElement(_Selenium_Driver, "//a[text()='Save As']");
        public Selenium_WebElement Print => new Selenium_WebElement(_Selenium_Driver, "//a[text()='Print']");
        public Selenium_WebElement difference => new Selenium_WebElement(_Selenium_Driver, "//label[text()='Show Difference Only']");


    }

    public class Order_Page : Web_Page

    {
        public Order_Page(IWebDriver driver) : base(driver)
        {
        }
        public Selenium_WebElement Refresh => new Selenium_WebElement(_Selenium_Driver, "//a[text()='Refresh']");
        public Selenium_WebElement Activate => new Selenium_WebElement(_Selenium_Driver, "//a[text()='Activate']");
        public Selenium_WebElement Redispense => new Selenium_WebElement(_Selenium_Driver, "//a[text()='Redispense a Material']");
        public Selenium_WebElement ReprintLable => new Selenium_WebElement(_Selenium_Driver, "//a[text()='Reprint Label']");
        //ReprintLable
        public Selenium_WebElement ReprintContainerLabel => new Selenium_WebElement(_Selenium_Driver, "//button[text()='Reprint Container Label']");
        public Selenium_WebElement ReprintLableClose => new Selenium_WebElement(_Selenium_Driver, "//button[text()='Close']");


    }
    public class CleanRules_Page : Web_Page

    {
        public CleanRules_Page(IWebDriver driver) : base(driver)
        {
        }
        public Selenium_WebElement States => new Selenium_WebElement(_Selenium_Driver, "//div[text()='States']");
        public Selenium_WebElement Data => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Data']");
        public Selenium_WebElement Actions => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Actions']");
        public Selenium_WebElement Types => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Types']");
        public Selenium_WebElement Transitions => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Transitions']");
        #region Transitions
        public Selenium_WebElement Add_Event => new Selenium_WebElement(_Selenium_Driver, "//a[text()='Add an Event']");
        public Selenium_WebElement cleanRules_Event => new Selenium_WebElement(_Selenium_Driver, "//input[@name='CleanRule_Event']");
        public Selenium_WebElement Edit_Button => new Selenium_WebElement(_Selenium_Driver, "//tr[@id='clicked_Row_Style']/td[5]//button[@class='gwt-Button']");
        public Selenium_WebElement Use_Action => new Selenium_WebElement(_Selenium_Driver, "//select[@name='CleanRule_Action']/option[text()='Use']");
        public Selenium_WebElement Usable_state => new Selenium_WebElement(_Selenium_Driver, "//select[@name='CleanRule_State']/option[text()='Usable']");
        public Selenium_WebElement Dialog_OK => new Selenium_WebElement(_Selenium_Driver, "//button[@id='Dialogbox_Bottom_OK_Button_Id']");
        public Selenium_WebElement SaveRules_Button => new Selenium_WebElement(_Selenium_Driver, "//button[text()='Save Rules']");
        public Selenium_WebElement selected_event => new Selenium_WebElement(_Selenium_Driver, "//tr[@id='clicked_Row_Style']");
        //public Selenium_WebElement EventList => new Selenium_WebElement(_Selenium_Driver, "//*[@id='WDView']/table/tbody/tr[3]/td/table/tbody/tr/td[2]/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr/td/table/tbody/tr[@class !='List_Background_Color']");
        public Selenium_WebElement Alert_meaasge => new Selenium_WebElement(_Selenium_Driver, "//div[@class='gwt-Label Alert_Label']");
        public Selenium_WebElement Alert_OK_Button => new Selenium_WebElement(_Selenium_Driver, "//button[@class='gwt-Button OkStyle']");
        public Selenium_WebElement MoveUp => new Selenium_WebElement(_Selenium_Driver, "//a[text()='Move Up']");
        public Selenium_WebElement MoveDown => new Selenium_WebElement(_Selenium_Driver, "//a[text()='Move Down']");
        public Selenium_WebElement TestRules => new Selenium_WebElement(_Selenium_Driver, "//button[text()='Test Rules...']");
        //public Selenium_WebElement ExpPeriod => new Selenium_WebElement(_Selenium_Driver, "//td[text()='ExpPeriod']/../td[3]/input");

        //public Selenium_WebElement ExpDate => new Selenium_WebElement(_Selenium_Driver, "//td[text()='ExpDate']/../td[3]/input");
        public Selenium_WebElement TestTransition => new Selenium_WebElement(_Selenium_Driver, "//button[text()='Test Transition']");
        public Selenium_WebElement EffectiveDate => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Effective date:']/../../td[2]//input");
        public Selenium_WebElement CommitRules => new Selenium_WebElement(_Selenium_Driver, "//button[text()='Commit Rules']");
        public Selenium_WebElement delete_button => new Selenium_WebElement(_Selenium_Driver, "//table[@class='List_Table_Border_Style']/tbody/tr/td/img[@class='gwt-Image']", 0);
        public Selenium_WebElement delete_alert_ok => new Selenium_WebElement(_Selenium_Driver, "//button[@class='gwt-Button OkStyle']");
        public Selenium_WebElement testRules_Event => new Selenium_WebElement(_Selenium_Driver, "//select[@class='gwt-ListBox']", 1);
        public Selenium_WebElement testRules_InitialState => new Selenium_WebElement(_Selenium_Driver, "//select[@class='gwt-ListBox']", 0);
        public Selenium_WebElement testRules_FinalState => new Selenium_WebElement(_Selenium_Driver, "//div[text()='Final State:']/../../td[2]/div");
        public Selenium_WebElement testRules_close => new Selenium_WebElement(_Selenium_Driver, "//button[text()='Close']");
        #endregion

        #region deviations
        public Selenium_WebElement deviation_table => new Selenium_WebElement(_Selenium_Driver, "//table[@class='Permission_Table_body_Style']/tbody", 0);

        #endregion
        #region General
        public Selenium_WebElement log_on_required_chx => new Selenium_WebElement(_Selenium_Driver, "//label[text()='Log on required for Execution System']/../input");

        #endregion
    }
}


