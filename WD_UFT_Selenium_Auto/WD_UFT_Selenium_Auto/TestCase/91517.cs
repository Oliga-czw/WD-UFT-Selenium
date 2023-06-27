using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Library.SeleniumLibrary;
using WD_UFT_Selenium_Auto.Product.WD;
using Keys = OpenQA.Selenium.Keys;

namespace WD_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {

        [TestCaseID(91517)]
        [Title("folder directory for upload xml - ERP upload.")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.Medium)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]

        [TestMethod]
        public void VSTS_91517()
        {
            string order1 = "test1";
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            LogStep(@"1. create a campaign with order test1");
            Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
            Web_Fuction.gotoWDWeb(driver);
            driver.Wait();
            Web_Fuction.login();
            driver.Wait();
            Web_Fuction.gotoTab(WDWebTab.order);
            Thread.Sleep(3000);
            Web.Order_Page.Refresh.Click();
            Thread.Sleep(3000);
            Web_Fuction.active_order(order1);
            Thread.Sleep(3000);
            Web_Fuction.gotoTab(WDWebTab.admin);
            Web.Administration_Page.Integration.Click();
            Thread.Sleep(3000);
            // create a folder without write permission
            Directory.CreateDirectory("C:\\testhahhaha");
            DirectoryInfo diInfo = new DirectoryInfo("C:\\testhahhaha");
            DirectorySecurity dirSecurity = diInfo.GetAccessControl();
            dirSecurity.AddAccessRule(new FileSystemAccessRule("qaone1", FileSystemRights.Write, AccessControlType.Deny));
            diInfo.SetAccessControl(dirSecurity);
            string[] folders = new string[3] { "C:\\ProgramData\\AspenTech\\AeBRS\\WDUpload", "C:\\testhahhaha", "C:\\Program\\AspenTech\\AeBRS\\WDUpload" };
            string[] barcodes = new string[3] { "X0125001", "M801890001", "1072003" };
            for (int i=0;i<=2;i++)
            {
                Web.Administration_Page.folder_for_upload.Clear();
                Web.Administration_Page.folder_for_upload.SendKeys(folders[i]);
                Web.Administration_Page.folder_for_upload.SendKeys(Keys.Enter);
                Thread.Sleep(2000);
                Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "folder"+i.ToString() + ".PNG");
                //Console.Write(driver.FindElement("//button[text()='Apply']").GetAttribute("disabled"));
                if (i!=0)
                {
                    Web_Fuction.administration_Apply("Configuration successfully saved");
                    Thread.Sleep(3000);
                }
                
                if (Directory.Exists(folders[i]))
                {
                    Base_File.ClearFolder(folders[i]);
                }
                

                Application.LaunchWDAndLogin();
                Thread.Sleep(5000);
                WD.mainWindow.HomeInternalFrame.OrderDispensing.Click();
                Thread.Sleep(2000);
                WD.mainWindow.DispensingInternalFrame.orderTable.SelectRows(0);
                WD.mainWindow.DispensingInternalFrame.next.Click();
                Thread.Sleep(2000);
                WD.mainWindow.MaterialInternalFrame.materialTable.SelectRows(0);
                WD.mainWindow.MaterialInternalFrame.next.Click();
                if (WD.mainWindow.BoothCleanInternalFrame.IsExist())
                {
                    WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
                }
                Thread.Sleep(3000);
                if (WD.mainWindow.HandleInformationInterFrame.IsExist())
                {
                    WD.mainWindow.HandleInformationInterFrame.Acknowledge.ClickSignle();
                }
                WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys(barcodes[i]);

                if (WD.ConfirmationDialog._UFT_Dialog.Exists())
                {
                    WD.ConfirmationDialog.YesButton.Click();
                }
                WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
                WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
                WD.SimulatorWindow.weight.SetText("90");
                Thread.Sleep(2000);
                WD.SimulatorWindow.OK.Click();
                WD.mainWindow.ScaleWeightInternalFrame.accept.Click(); 

                if (i == 0)
                {
                    if (WD.ErrorDialog.IsExist())
                    {
                        WD.ErrorDialog.OKButton.Click();
                    }
                    if (WD.mainWindow.MaterialInternalFrame.IsExist())
                    {
                        WD.mainWindow.MaterialInternalFrame.cancel.Click();
                    }
                    Thread.Sleep(3000);
                    WD_Fuction.Close();
                    string[] files = Directory.GetFiles(folders[i]);
                    List<string> list = new List<string>(files);
                    Base_Assert.AreNotEqual(list.Count(), 0);
                }
                else
                {
                    WD.mainWindow.GetSnapshot(Resultpath + "Store_failed("+i.ToString()+").PNG");
                    if (WD.ErrorDialog.IsExist())
                    {
                        WD.ErrorDialog.OKButton.Click();
                    }
                    if (WD.ErrorDialog.IsExist())
                    {
                        WD.ErrorDialog.OKButton.Click();
                    }
                    if (WD.ErrorDialog.IsExist())
                    {
                        WD.ErrorDialog.OKButton.Click();
                    }
                    if (WD.mainWindow.MaterialInternalFrame.IsExist())
                    {
                        WD.mainWindow.MaterialInternalFrame.cancel.Click();
                    }
                    Thread.Sleep(3000);
                    WD_Fuction.Close();
                    string[] files = Directory.GetFiles("C:\\ProgramData\\AspenTech\\AeBRS\\TmpErpUpload");
                    List<string> list = new List<string>(files);
                    Base_Assert.AreNotEqual(list.Count(), 0);
                }

                //Web.Administration_Page.folder_for_upload.Clear();
                //Web.Administration_Page.folder_for_upload.SendKeys("C:\\ProgramData\\AspenTech\\AeBRS\\WDUpload");
                //Thread.Sleep(2000);
                //Web_Fuction.administration_Apply("Configuration successfully saved");
            }
            
        }


    }
}