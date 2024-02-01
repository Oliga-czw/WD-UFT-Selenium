using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(106688)]
        [Title("V8.8.6_CQ00775245:Enable weighing comments")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_106688()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";
            string method = WDMethod.Net;
            string barcode = "X0125001";
            string scale = "simulator";
            LogStep(@"1. Open WD web and login");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            LogStep(@"2. Go and admin and General");
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.General.Click();
            LogStep(@"3.Do not check Disable Weighing comments, and click 'Apply' button.");
            if (Web.Administration_Page.Disable_Weighing_comments.GetAttribute("checked") == "true")
            {
                Web.Administration_Page.Disable_Weighing_comments.Click();
                Web_Fuction.administration_Apply("Configuration successfully saved");
            }
            LogStep(@"4. Active an order");
            Web_Fuction.gotoTab(WDWebTab.order);
            Web_Fuction.active_order(order);
            LogStep(@"5. Dispense a material, on weigh page, check 'Comment' button.");
            Thread.Sleep(2000);
            Application.LaunchWDAndLogin();
            WD.mainWindow.HomeInternalFrame.MaterialDispensing.Click();
            WD.mainWindow.Material_SelectionInternalFrame.materialTable.Row("X0125").Click();
            var Required_value = WD.mainWindow.Material_SelectionInternalFrame.materialTable._UFT_Table.GetCell(0, "Clean Required").Value;

            WD.mainWindow.Material_SelectionInternalFrame.next.Click();
            if (Required_value.ToString() == "Yes")
            {
                WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
            }
            //select method and input barcode
            WD_Fuction.SelectMehod(method, barcode);

            //select simulator
            if (WD.MessageDialog.IsExist())
            {
                WD.MessageDialog.OKButton.Click();
            }
            WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
            //zeor
            WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
            WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
            WD.SimulatorWindow.weight.SetText("200");
            WD.SimulatorWindow.OK.Click();
            //Comment is Enabled
            Base_Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.comment.IsEnabled);
            WD.mainWindow.GetSnapshot(Resultpath + "CommentEnabled.PNG");
            //click accept dispense
            WD.mainWindow.ScaleWeightInternalFrame.comment.ClickSignle();
            Thread.Sleep(2000);
            WD.mainWindow.CommentInternalFrame.commentEditor.SendKeys("For Test");
            WD.mainWindow.CommentInternalFrame.OKButton.Click();
            Thread.Sleep(2000);
            WD.mainWindow.ScaleWeightInternalFrame.accept.ClickSignle();
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
            if (WD.ErrorDialog.IsExist())
            {
                WD.ErrorDialog.OKButton.Click();
            }
        }

    }
}