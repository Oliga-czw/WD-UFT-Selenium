using System.Collections;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System;
using OpenQA.Selenium;
using HP.LFT.SDK;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;
using System.Linq;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class APEM_TestCase
    {
        [TestCaseID(801579)]
        [Title("UC801108_APEM mobile: Order list load and show 200 records one time on Process Order page.")]
        [TestCategory(ProductArea.Mobile)]
        [Priority(CasePriority.Critical)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziwei)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_801579()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID + "-";

            Application.LaunchMocAndLogin();
            LogStep(@"1. import bpl");//import bpl
            //check bpl exit
            APEM.MocmainWindow.BPLDesign.ClickSignle();
            if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("CREATEORDER").Existing)
            {
                MOC_TemplatesFunction.Importtemplates("CREATEORDER.zip");
            }
            LogStep(@"2. create orders");//create 520 orders
            //check order exit
            APEM.MocmainWindow.Orders.Click();
            if (APEM.RowSelectionDialog.IsExist())
            {
                APEM.RowSelectionDialog.YesButton.Click();
            }
            APEM.MocmainWindow.OrderListInternalFrame.Search.SetText("t_order");
            APEM.MocmainWindow.OrderListInternalFrame.Filter_Button.Click();
            if (APEM.RowSelectionDialog.IsExist())
            {
                APEM.RowSelectionDialog.YesButton.Click();
            }
            else//not exit,creat order
            {
                APEM.MocmainWindow.BPLDesign.ClickSignle();
                APEM.MocmainWindow.BPLListInternalFrame.Refresh_Button.ClickSignle();
                APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row("CREATEORDER").Click();
                APEM.MocmainWindow.BPLListInternalFrame.LoadDesigner_Button.ClickSignle();
                APEM.DesignEditorWindow.ExecuteButton.ClickSignle();
                //add reason
                if (APEM.AuditReasonDialog.IsExist())
                {
                    APEM.AuditReasonDialog.Reason.SendKeys("Execute");
                    APEM.AuditReasonDialog.OK.Click();
                }
                //wait for finish
                Thread.Sleep(20000);
                if (APEM.ExecutionFinishedDialog.Lable.AttachedText == "Phase finished with NO")
                {
                    //log
                    APEM.ExecutionFinishedDialog.OKButton.Click();
                }
                APEM.DesignEditorWindow.Close();
                APEM.CloseDialog.YesButton.Click();
                Thread.Sleep(3000);
            }
            APEM.ExitApplication();
            LogStep(@"3. login mobile");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Mobile_Fuction.gotoApemMobile(driver);
            Mobile_Fuction.login();
            LogStep(@"4. check default page");
            driver.action_move_to_element(Mobile.OrderProcess_Page.OrderPhaseTableHeads[1]);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "default page.PNG");
            string url = driver.GetUrl();
            Base_Assert.IsTrue(url.Contains("process-order"), "in process-order page") ;
            LogStep(@"5. check first 200 orders load");
            //get order number
            int Fcount = Mobile.OrderProcess_Page.OrderPhaseTableRows.Count;
            //check order displays 200
            Base_Assert.AreEqual(Fcount, 200, "Orders displays 200.");
            //check 200 order sort
            List<string> col = new List<string> { "Order / Batch Code" };
            List<string> OrderName200 = new List<string> { };
            int no = 0;
            int i = 2;// process order page start 2
            foreach (IWebElement head in Mobile.OrderProcess_Page.OrderPhaseTableHeads)
            {
                if (col.Contains(head.Text))
                {
                    no = i;
                    break;
                }
                i++;
            }
            var OrderName = driver.FindElements($"//table/tbody/tr/td[{no}]");
            foreach (var des in OrderName)
            {
                OrderName200.Add(des.Text);
            }
            //sort
            List<string> OrderName200Sort = OrderName200.ToList();
            OrderName200Sort.Sort();
            bool same = OrderName200.SequenceEqual(OrderName200Sort);
            Base_Assert.IsTrue(same, "order list ascending by order name by default.");
            //check scroll bar at top
            var ScrollHeight = driver.execute_script_return("return document.getElementsByClassName('table-content scroll-bar full show-navigation desktop-mode')[0].scrollTop");
            Base_Assert.AreEqual(ScrollHeight.ToString(), "0", "Scroll bar at top.");
            LogStep(@"6.Scroll down the scroll bar to load more orders");
            //next 200 order
            driver.execute_script("document.getElementsByClassName('table-content scroll-bar full show-navigation desktop-mode')[0].scrollTop = 100000");
            Thread.Sleep(5000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Orders 400.PNG");
            //get order number
            int Ncount = Mobile.OrderProcess_Page.OrderPhaseTableRows.Count;
            //check order displays 400
            Base_Assert.AreEqual(Ncount, 400, "Orders displays 400.");

            //next 120 order
            driver.execute_script("document.getElementsByClassName('table-content scroll-bar full show-navigation desktop-mode')[0].scrollTop = 100000");
            Thread.Sleep(5000);
            driver.execute_script("document.getElementsByClassName('table-content scroll-bar full show-navigation desktop-mode')[0].scrollTop = 100000");
            Thread.Sleep(5000);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Orders more than 520.PNG");
            //get order number
            int Lcount = Mobile.OrderProcess_Page.OrderPhaseTableRows.Count;
            //check order displays>=520
            Base_Assert.IsTrue(Lcount >= 520, "Orders displays>=520.");
            LogStep(@"7.Click Refresh button");
            Mobile.OrderProcess_Page.RefreshButton.Click();
            Thread.Sleep(10000);
            driver.action_move_to_element(Mobile.OrderProcess_Page.OrderPhaseTableHeads[1]);
            Mobile_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "Refresh OrderList.PNG");
            //check scroll bar at top
            var ScrollHeight2 = driver.execute_script_return("return document.getElementsByClassName('table-content scroll-bar full show-navigation desktop-mode')[0].scrollTop");
            Base_Assert.AreEqual(ScrollHeight2.ToString(), "0", "Scroll bar at top.");
            //check Orders will be reloaded
            //get order number
            int Rcount = Mobile.OrderProcess_Page.OrderPhaseTableRows.Count;
            //check order displays 200
            Base_Assert.AreEqual(Rcount, 200, "Orders displays 200.");
            //same List
            List<string> OrderName200Last = new List<string> { };
            var OrderNameLast = driver.FindElements($"//table/tbody/tr/td[{no}]");
            foreach (var des in OrderNameLast)
            {
                OrderName200Last.Add(des.Text);
            }
            bool same2 = OrderName200.SequenceEqual(OrderName200Last);
            Base_Assert.IsTrue(same2, "Refresh order list.");

            driver.Close();
        }

    }
}