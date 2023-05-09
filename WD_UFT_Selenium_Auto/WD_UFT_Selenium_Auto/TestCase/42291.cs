using System;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Text;
using OpenQA.Selenium;
using System.Linq;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(42291)]
        [Title("Administration Cleaning Rules:cleaning rules-transitions")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_42291()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Thread.Sleep(2000);
            Web.Administration_Page.CleaningRules.Click();
            Thread.Sleep(3000);
            // add a type
            driver.FindElement("//div[text()='Types']").Click();
            Thread.Sleep(3000);
            var beforeAdded_types = driver.FindElements("//*[@id='WDView']/table/tbody/tr[3]/td/table/tbody/tr/td[2]/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr/td/table/tbody/tr[@class !='List_Background_Color']");
            driver.FindElement("//a[text()='Add a Type']").Click();
            driver.FindElement("//input[@name='CleanRule_Type']").Click();
            driver.FindElement("//input[@name='CleanRule_Type']").SendKeys(Keys.Control + "A");
            driver.FindElement("//input[@name='CleanRule_Type']").SendKeys(Keys.Delete);
            driver.FindElement("//input[@name='CleanRule_Type']").SendKeys("testType_42291");
            driver.FindElement("//textarea[@name='Description']").Click();
            driver.FindElement("//textarea[@name='Description']").SendKeys("for test");
            driver.FindElement("//select[@name='CleanRule_Event']/option[text()='Full Clean']").Click();
            driver.FindElement("//textarea[@name='CleanRule_Instructions']").Click();
            driver.FindElement("//textarea[@name='Description']").SendKeys("for test");
            driver.FindElement("//button[text()='Apply']").Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Added_an_Type.PNG");
            var afterAdded_types = driver.FindElements("//*[@id='WDView']/table/tbody/tr[3]/td/table/tbody/tr/td[2]/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr/td/table/tbody/tr[@class !='List_Background_Color']");
            Assert.AreEqual(afterAdded_types.Count(), beforeAdded_types.Count() + 1);

            //move up a type
            driver.FindElement("//a[text()='Move Up']").Click();
            Thread.Sleep(2000);
            var now_typeList = driver.FindElements("//tr[@class='List_Background_Color']/../tr[@class !='List_Background_Color']");
            int type_index = now_typeList.IndexOf(driver.FindElement("//tr[@id='clicked_Row_Style']"));
            Assert.AreEqual(type_index, afterAdded_types.Count() - 2);
            //var move_up_state = driver.FindElement("//a[text()='Move Up']").GetAttribute("class");
            do
            {
                Web.CleanRules_Page.MoveUp.Click();
            }
            while (Web.CleanRules_Page.MoveUp.GetAttribute("class").Contains("Disable") is false);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "MoveUp_an_Type.PNG");
            var only_move_up = driver.FindElements("//tr[@class='List_Background_Color']/../tr[@class !='List_Background_Color']");
            int now_index = only_move_up.IndexOf(driver.FindElement("//tr[@id='clicked_Row_Style']"));
            Base_Assert.AreEqual(now_index, 0);
            
            //move down a type
            driver.FindElement("//a[text()='Move Down']").Click();
            Thread.Sleep(2000);
            var nowdown_eventList = driver.FindElements("//tr[@class='List_Background_Color']/../tr[@class !='List_Background_Color']");
            int eventDown_index = nowdown_eventList.IndexOf(driver.FindElement("//tr[@id='clicked_Row_Style']"));
            Base_Assert.AreEqual(eventDown_index, 1);
            do
            {
                Web.CleanRules_Page.MoveDown.Click();
            }
            while (Web.CleanRules_Page.MoveDown.GetAttribute("class").Contains("Disable") is false);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "MoveDown_an_Type.PNG");
            var only_move_down = driver.FindElements("//tr[@class='List_Background_Color']/../tr[@class !='List_Background_Color']");
            int now_downindex = only_move_down.IndexOf(driver.FindElement("//tr[@id='clicked_Row_Style']"));
            Base_Assert.AreEqual(now_downindex, afterAdded_types.Count() - 1);

            //edit the type
            driver.FindElement("//*[@id='clicked_Row_Style']/td[2]/img").Click();
            Thread.Sleep(2000);
            driver.FindElement("//textarea[@name='CleanRule_Instructions']").Click();
            driver.FindElement("//textarea[@name='CleanRule_Instructions']").SendKeys("this is for test");
            driver.FindElement("//textarea[@name='CleanRule_Instructions']").SendKeys(Keys.Tab);
            driver.FindElement("//button[text()='Apply']").Click();
            Base_Assert.IsTrue(driver.FindElement("//*[@id='clicked_Row_Style']/td[5]/table/tbody/tr/td").Text.Contains("this is for test"));
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Edit_an_Type.PNG");
            var before_delete = driver.FindElements("//tr[@class='List_Background_Color']/../tr[@class !='List_Background_Color']");
            // delete a type
            driver.FindElement("//*[@id='clicked_Row_Style']/td[6]/img").Click();
            Thread.Sleep(2000);
            Base_Assert.IsTrue(driver.FindElement("//div[@class='gwt-Label Alert_Label']").Text.Contains("Are you sure you want to delete"));
            driver.FindElement("//button[@class='gwt-Button OkStyle']").Click();
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "delete_an_Type.PNG");
            var after_delete = driver.FindElements("//tr[@class='List_Background_Color']/../tr[@class !='List_Background_Color']");
            Base_Assert.AreEqual(before_delete.Count(), after_delete.Count() + 1);
        }

       
    }
}