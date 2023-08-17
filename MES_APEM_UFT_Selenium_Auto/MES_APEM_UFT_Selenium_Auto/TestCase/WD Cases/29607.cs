using System;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Text;
using OpenQA.Selenium;
using System.Linq;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(29607)]
        [Title("Administration Cleaning Rules:cleaning rules-transitions")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_29607()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            //System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", Resultpath, Encoding.Default);
            LogStep(@"1. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.CleaningRules.Click();
            Thread.Sleep(3000);
            // add an Evevt
            LogStep(@"2. add an Evevt");
            Web.Administration_Page.Transitions.Click();
            Thread.Sleep(3000);
            var beforeAdded_events = driver.FindElements("//*[@id='WDView']/table/tbody/tr[3]/td/table/tbody/tr/td[2]/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr/td/table/tbody/tr[@class !='List_Background_Color']");
            Assert.AreEqual(beforeAdded_events.Count(), 6);
            Web.CleanRules_Page.Add_Event.Click();
            Web.CleanRules_Page.cleanRules_Event.Click();
            Web.CleanRules_Page.cleanRules_Event.SendKeys(Keys.Control + "A");
            Web.CleanRules_Page.cleanRules_Event.SendKeys("testEvent");
            Web.CleanRules_Page.cleanRules_Event.SendKeys(Keys.Enter);
            Web.CleanRules_Page.Edit_Button.Click();
            Web.CleanRules_Page.Use_Action.Click();
            Web.CleanRules_Page.Usable_state.Click();
            Web.CleanRules_Page.Dialog_OK.Click();
            var afterAdded_events = driver.FindElements("//*[@id='WDView']/table/tbody/tr[3]/td/table/tbody/tr/td[2]/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr/td/table/tbody/tr[@class !='List_Background_Color']");
            //save rules
            LogStep(@"3. save rules");
            Web.CleanRules_Page.SaveRules_Button.Click();
            Thread.Sleep(2000);
            //System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", Web.CleanRules_Page.Alert_meaasge.GetAttribute("innerText"), Encoding.Default);
            Assert.AreEqual(Web.CleanRules_Page.Alert_meaasge.GetAttribute("innerText"), "Clean rules saved successfully.");
            Web.CleanRules_Page.Alert_OK_Button.Click();
            Assert.AreEqual(afterAdded_events.Count(), beforeAdded_events.Count() + 1);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Added_an_Event.PNG");
            //move up an event
            LogStep(@"4. move up an event");
            Web.CleanRules_Page.MoveUp.Click();
            Thread.Sleep(2000);
            var now_selectedEvent = driver.FindElement("//tr[@class='List_Background_Color']/../tr[7]/td[1]/input");
            Assert.AreEqual(now_selectedEvent.GetProperty("value"), "testEvent");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "MoveUp_an_Event.PNG");
           
           // System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", move_up_state, Encoding.Default);
            do
            {
                Web.CleanRules_Page.MoveUp.Click();
            }
            while (Web.CleanRules_Page.MoveUp.GetAttribute("class").Contains("Disable") is false);
            var now_MoveEvent = driver.FindElement("//tr[@class='List_Background_Color']/../tr[2]/td[1]/input");
            Assert.AreEqual(now_selectedEvent.GetProperty("value"), "testEvent");
            //move down an event
            LogStep(@"5. Move down an event");
            Web.CleanRules_Page.MoveDown.Click();
            Thread.Sleep(2000);
            var now_MoveDownEvent = driver.FindElement("//tr[@class='List_Background_Color']/../tr[3]/td[1]/input");
            Assert.AreEqual(now_selectedEvent.GetProperty("value"), "testEvent");
            do {
                Web.CleanRules_Page.MoveDown.Click();
            }
            while (Web.CleanRules_Page.MoveDown.GetAttribute("class").Contains("Disable") is false);
            var now_downindex = driver.FindElement("//tr[@class='List_Background_Color']/../tr[8]/td[1]/input");
            Assert.AreEqual(now_selectedEvent.GetProperty("value"), "testEvent");
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "MoveDown_an_Event.PNG");
            //delete an event
            LogStep(@"6. delete an event");
            Web.CleanRules_Page.delete_button.Click();
            Thread.Sleep(2000);
            Web.CleanRules_Page.delete_alert_ok.Click();
            Thread.Sleep(2000);
            Web.CleanRules_Page.SaveRules_Button.Click();
            Thread.Sleep(2000);
            Web.CleanRules_Page.Alert_OK_Button.Click();
            var now_EventList = driver.FindElements("//*[@id='WDView']/table/tbody/tr[3]/td/table/tbody/tr/td[2]/table/tbody/tr[2]/td/table/tbody/tr/td/table/tbody/tr/td/table/tbody/tr[@class !='List_Background_Color']");
            Assert.AreEqual(now_EventList.Count(), afterAdded_events.Count() - 1);

            //test rules
            LogStep(@"7. test rules");
            Web.CleanRules_Page.TestRules.Click();
            Thread.Sleep(2000);
            driver.FindElement("//select[@class='gwt-ListBox']/option[@value='Usable']").Click();
            driver.Wait();
            driver.FindElement("//select[@class='gwt-ListBox']/option[@value='Use']").Click();
            Web.CleanRules_Page.TestTransition.Click();
            Thread.Sleep(2000);
            var Final_State = Web.CleanRules_Page.testRules_FinalState.GetAttribute("innerText");
            System.IO.File.WriteAllText("C:/Users/qaone1/Desktop/eee.txt", Final_State, Encoding.Default);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Test_rules.PNG");
            Base_Assert.AreEqual(Final_State, "In use");
            Web.CleanRules_Page.testRules_close.Click();
            Thread.Sleep(2000);
            // commit rules
            LogStep(@"8. commit rules");
            Web.CleanRules_Page.Add_Event.Click();
            Web.CleanRules_Page.cleanRules_Event.Click();
            Web.CleanRules_Page.cleanRules_Event.SendKeys("Testadded");
            Web.CleanRules_Page.cleanRules_Event.SendKeys(Keys.Enter);
            Web.CleanRules_Page.Edit_Button.Click();
            Web.CleanRules_Page.Use_Action.Click();
            Web.CleanRules_Page.Usable_state.Click();

            Web.CleanRules_Page.Dialog_OK.Click();

            Web.CleanRules_Page.EffectiveDate.SendKeys("29/3/23, 3:32:36 PM");
            Web.CleanRules_Page.CommitRules.Click();
            Thread.Sleep(2000);
            Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "commitRules.PNG");
            Assert.AreEqual(Web.CleanRules_Page.Alert_meaasge.GetAttribute("innerText"), "Clean rules committed successfully.");
            Web.CleanRules_Page.Alert_OK_Button.Click();
            //delete an event
            Web.CleanRules_Page.delete_button.Click();
            Thread.Sleep(2000);
            Web.CleanRules_Page.delete_alert_ok.Click();
            Thread.Sleep(2000);
            Web.CleanRules_Page.SaveRules_Button.Click();
            Thread.Sleep(2000);
            Web.CleanRules_Page.Alert_OK_Button.Click();


            driver.Close();

        }
    }
}