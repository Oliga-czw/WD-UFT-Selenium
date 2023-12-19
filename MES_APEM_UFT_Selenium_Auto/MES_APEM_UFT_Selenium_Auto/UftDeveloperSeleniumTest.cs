using HP.LFT.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System.Windows.Forms;
using System;
using HP.LFT.SDK.UIAPro;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using OpenQA.Selenium;

namespace MES_APEM_UFT_Selenium_Auto
{
    [TestClass]
    public class UftDeveloperSeleniumTest
    {

        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            Thread.Sleep(3000);
            //MOC_Fuction.VerifyRPL("FOR_STATUS");
            //MOC_Fuction.CertifyRPL("FOR_STATUS");
            //Thread.Sleep(3000);
            //MOC_Fuction.PlanFromRPL("FOR_STATUS", "ORDRE997428", false);
            //APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Phases");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            driver.Wait();
            Mobile_Fuction.login();
            driver.Wait();
            Thread.Sleep(5000);
            Mobile.Main_Page.Setting.Click();
            Mobile.Setting_Page.turnOn_mode(2);
            Mobile.Main_Page.Consolidated.Click();
            Thread.Sleep(3000);
            Mobile.Consolidated_Page.OrderSearch.SendKeys("ORDRE997428");
            Thread.Sleep(2000);
            int no = 0;
            int index = 0;
            int i = 0;
            foreach (IWebElement head in Mobile.Consolidated_Page.OrderPhaseTableHeads)
            {
                if (head.Text == "Status")
                {
                    no = i;
                }
                if (head.Text == "Name")
                {
                    index = i;
                }
                i++;
            }
            Console.WriteLine(Mobile.Consolidated_Page.OrderPhaseTable._Selenium_WebElement.Size.Width.ToString());
            Console.WriteLine(Mobile.Consolidated_Page.OrderPhaseTable._Selenium_WebElement.Size.Height.ToString());
            
            //APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Orders");
            //Thread.Sleep(2000);
            //mocAPEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("ORDRE997428").Click();
            //APEM.MocmainWindow.OrderListInternalFrame.Activate_Button.ClickSignle();
            //Thread.Sleep(2000);
            //APEM.ActivateDialog.YesButton.Click();
            //MOC_Fuction.AddReason();
            //foreach (IWebElement Phase in Mobile.Consolidated_Page.OrderPhaseTableRows)
            //{
            //    var Status = Phase.FindElements(By.TagName("td"))[no].Text;
            //    Console.WriteLine(Status);
            //    Assert.IsTrue((Status.Contains("Ready")) || (Status.Contains("Not ready")));
            //}
            ////LogStep(@"Execute in moc");
            //APEM.MocmainWindow.WorkstationBP.ClickSignle();
            //Thread.Sleep(3000);
            //APEM.MocmainWindow.WorkstationBPInternalFrame.OrderEditor.SetText("ORDRE997428");
            //APEM.MocmainWindow.WorkstationBPInternalFrame.Filterbutton.Click();
            ////Excution
            //APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("PHASE55", "Name").Click();
            //APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            //Thread.Sleep(3000);
            //Assert.AreEqual(Mobile.Consolidated_Page.OrderPhaseTableRows.Count, PhaseCount-1);
            ////LogStep(@"Click cancel button when executing in moc");
            //APEM.PhaseExecWindow.ExecutionInternalFrame.Cancel_Button.ClickSignle();
            //Thread.Sleep(1000);
            //APEM.PhaseExecWindow.ConfirmationInternalFrame.YesButton.Click();
            //Thread.Sleep(2000);
            //foreach (IWebElement Phase in Mobile.Consolidated_Page.OrderPhaseTableRows)
            //{
            //    var Name = Phase.FindElements(By.TagName("td"))[index].FindElement(By.XPath("//div[1]")).Text;
            //    if (Name == "PHASE55")
            //    {
            //        var status2 = Phase.FindElement(By.XPath("../..")).FindElements(By.TagName("td"))[no].Text;
            //        Console.WriteLine(status2);
            //        Assert.IsTrue(status2.Contains("Ready"));
            //    }
            //}
            ////LogStep(@"Interrupted in moc");
            //APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Phases");
            //Thread.Sleep(2000);
            //APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("PHASE55", "Name").Click();
            //APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            //Thread.Sleep(5000);
            //APEM.PhaseExecWindow.StopPhaseButton.ClickSignle();
            //Thread.Sleep(3000);
            //SendKeys.SendWait("{Enter}");
            //Thread.Sleep(1000);
            //APEM.PhaseExecWindow.UserConfirmationInternalFrame.PassWord.SendKeys(PassWord.qaone1);
            //APEM.PhaseExecWindow.UserConfirmationInternalFrame.Comment.SendKeys("for test");
            //APEM.PhaseExecWindow.UserConfirmationInternalFrame.OKButton.Click();
            //Thread.Sleep(2000);
            //foreach (IWebElement Phase in Mobile.Consolidated_Page.OrderPhaseTableRows)
            //{
            //    var Name = Phase.FindElements(By.TagName("td"))[index].FindElement(By.XPath("//div[1]")).Text;
            //    if (Name == "PHASE55")
            //    {
            //        var status3 = Phase.FindElement(By.XPath("../..")).FindElements(By.TagName("td"))[no].Text;
            //        Console.WriteLine(status3);
            //        Assert.IsTrue(status3.Contains("Interrupted"));
            //    }
            //}
            ////LogStep(@"click cancel button which on toolbar in moc");
            //APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("PHASE55", "Name").Click();
            //APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            //Thread.Sleep(5000);
            //APEM.PhaseExecWindow.CancelPhaseButton.ClickSignle();
            //Thread.Sleep(3000);
            //SendKeys.SendWait("{Enter}");
            //Thread.Sleep(1000);
            //APEM.PhaseExecWindow.UserConfirmationInternalFrame.PassWord.SendKeys(PassWord.qaone1);
            //APEM.PhaseExecWindow.UserConfirmationInternalFrame.Comment.SendKeys("for test");
            //APEM.PhaseExecWindow.UserConfirmationInternalFrame.OKButton.Click();
            //Thread.Sleep(2000);

            //foreach (IWebElement Phase in Mobile.Consolidated_Page.OrderPhaseTableRows)
            //{
            //    var Name = Phase.FindElements(By.TagName("td"))[index].FindElement(By.XPath("//div[1]")).Text;
            //    if (Name == "PHASE55")
            //    {
            //        var status4 = Phase.FindElement(By.XPath("../..")).FindElements(By.TagName("td"))[no].Text;
            //        Console.WriteLine(status4);
            //        Assert.IsTrue(status4.Contains("Cancelled"));
            //    }
            //}
            ////LogStep(@"Reactive the phase in moc");
            //APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Phases");
            //Thread.Sleep(2000);
            //APEM.MocmainWindow.OrderListInternalFrame.Refresh_Button.ClickSignle();
            //Thread.Sleep(2000);
            //APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("PHASE55").Click();
            //Thread.Sleep(2000);
            //APEM.MocmainWindow.OrderListInternalFrame.Reactivate_Button.ClickSignle();
            //Thread.Sleep(2000);
            //APEM.ReactivateDialog.YesButton.Click();
            //Thread.Sleep(2000);
            //APEM.ReactivateDialog.YesButton.Click();
            //Thread.Sleep(2000);
            //MOC_Fuction.AddReason();
            //Thread.Sleep(6000);
            //APEM.MocmainWindow.OrderListInternalFrame.Refresh_Button.ClickSignle();
            //Thread.Sleep(2000);
            //foreach (IWebElement Phase in Mobile.Consolidated_Page.OrderPhaseTableRows)
            //{
            //    var Name = Phase.FindElements(By.TagName("td"))[index].FindElement(By.XPath("//div[1]")).Text;
            //    if (Name == "PHASE55")
            //    {
            //        var status5 = Phase.FindElement(By.XPath("../..")).FindElements(By.TagName("td"))[no].Text;
            //        Console.WriteLine(status5);
            //        Assert.IsTrue(status5.Contains("Interrupted"));
            //    }
            //}
            ////LogStep(@"Finished the phase in moc");
            //APEM.MocmainWindow.WorkstationBPInternalFrame.OrderTable.Row("PHASE55", "Name").Click();
            //APEM.MocmainWindow.WorkstationBPInternalFrame.ExecuteButton.ClickSignle();
            //Thread.Sleep(5000);
            //APEM.PhaseExecWindow.ExecutionInternalFrame.OK_Button.ClickSignle();
            //Thread.Sleep(1000);
            //foreach (IWebElement Phase in Mobile.Consolidated_Page.OrderPhaseTableRows)
            //{
            //    var Name = Phase.FindElements(By.TagName("td"))[index].FindElement(By.XPath("//div[1]")).Text;
            //    if (Name == "PHASE55")
            //    {
            //        var status6 = Phase.FindElement(By.XPath("../..")).FindElements(By.TagName("td"))[no].Text;
            //        Console.WriteLine(status6);
            //        Assert.IsTrue(status6.Contains("Finished"));
            //    }
            //}
            ////LogStep(@"canceled the phase in order list");
            //APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Phases");
            //APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("PHASE67").Click();
            //Thread.Sleep(2000);
            //APEM.MocmainWindow.OrderListInternalFrame.CancelBP_Button.ClickSignle();
            //Thread.Sleep(2000);
            //APEM.CancelBPDialog.YesButton.Click();
            //Thread.Sleep(2000);
            //MOC_Fuction.AddReason();
            //Thread.Sleep(2000);
            //foreach (IWebElement Phase in Mobile.Consolidated_Page.OrderPhaseTableRows)
            //{
            //    var Name = Phase.FindElements(By.TagName("td"))[index].FindElement(By.XPath("//div[1]")).Text;
            //    if (Name == "PHASE67")
            //    {
            //        var status7 = Phase.FindElement(By.XPath("../..")).FindElements(By.TagName("td"))[no].Text;
            //        Console.WriteLine(status7);
            //        Assert.IsTrue(status7.Contains("Cancelled"));
            //    }
            //}

            //APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Phases");
            //APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("PHASE67").Click();
            //Thread.Sleep(2000);
            //APEM.MocmainWindow.OrderListInternalFrame.DisableBP_Button.ClickSignle();
            //Thread.Sleep(2000);
            //APEM.DisableBPDialog.YesButton.Click();
            //Thread.Sleep(2000);
            //MOC_Fuction.AddReason();
            //Thread.Sleep(2000);
            //foreach (IWebElement Phase in Mobile.Consolidated_Page.OrderPhaseTableRows)
            //{
            //    var Name = Phase.FindElements(By.TagName("td"))[index].FindElement(By.XPath("//div[1]")).Text;
            //    if (Name == "PHASE67")
            //    {
            //        var status8 = Phase.FindElement(By.XPath("../..")).FindElements(By.TagName("td"))[no].Text;
            //        Console.WriteLine(status8);
            //        Assert.IsTrue(status8.Contains("Disabled"));
            //    }
            //}
            //APEM.MocmainWindow.OrderListInternalFrame.OrderTabControl.Select("Orders");
            //Thread.Sleep(2000);
            //APEM.MocmainWindow.OrderListInternalFrame.OrderList_Table.Row("ORDRE997428").Click();
            //APEM.MocmainWindow.OrderListInternalFrame.Cancel_Button.ClickSignle();
            //Thread.Sleep(2000);
            //APEM.MocmainWindow.CancelOrderDialog.YesButton.Click();
            //MOC_Fuction.AddReason();
            //Thread.Sleep(4000);
            //Assert.AreEqual(Mobile.Consolidated_Page.OrderPhaseTableRows.Count,0);
            ////Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "OrderCanceled.PNG");
            driver.Close();

        }





    }

}
