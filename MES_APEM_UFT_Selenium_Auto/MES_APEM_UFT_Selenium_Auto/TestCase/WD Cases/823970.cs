using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.Windows.Forms;
using System.Drawing;
using HP.LFT.SDK;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;
using System.IO;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(823970)]
        [Title("UC814509_W&D enhancement for Source as Target_order report if DO not set SOURCE_TARGET_REQUIRE_TARGET_TARE key or set SOURCE_TARGET_REQUIRE_TARGET_TARE = 0")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Accepted)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(600000)]
        [Defect("1365244")]

        [TestMethod]
        public void VSTS_823970()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";
            string method = WDMethod.SourceAsTarget;
            string barcode = "X0125001";
            string source_left = "300";
            string source_start = "320";
            string scale = "simulator";
            string Configpath = Base_Directory.ConfigDirx86 + "config.m2r_cfg";
            string ConfigKey = "SOURCE_TARGET_REQUIRE_TARGET_TARE = 0";

            try
            {
                LogStep(@"1. Set key in config file");
                Base_Function.AddConfigKey(Configpath,ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_allx86);
                LogStep(@"2. Active orders");
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Web_Fuction.gotoWDWeb(driver);
                driver.Wait();
                Web_Fuction.login();
                driver.Wait();
                Web_Fuction.gotoTab(WDWebTab.order);
                Web_Fuction.active_order(order);
                //driver.Close();
                LogStep(@"3. Open WD client");
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
                WD.mainWindow.GetSnapshot(Resultpath + "SourceAsTarget.PNG");
                Thread.Sleep(2000);
                WD.mainWindow.ScaleWeightInternalFrame.SourceTare.SendKeys("100");
                //start weight
                WD.SimulatorWindow.weight.SetText(source_start);
                WD.SimulatorWindow.OK.Click();
                WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
                //assert the weight
                WD.mainWindow.GetSnapshot(Resultpath + "weight_1.PNG");
                Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.WeightNumber._UFT_Label.Text, "220.0");
                //input remove weight
                WD.SimulatorWindow.weight.SetText(source_left);
                WD.SimulatorWindow.OK.Click();
                Assert.AreEqual(WD.mainWindow.ScaleWeightInternalFrame.WeightNumber._UFT_Label.Text, "200.0");
                WD.mainWindow.GetSnapshot(Resultpath + "weight_removal.PNG");
                //Check the "Initial gross", "Finial gross", and "Difference" quantity on the right panel 
                var Initial_gross = WD.mainWindow.ScaleWeightInternalFrame.Data_InitailGross.AttachedText;
                var Finial_gross = WD.mainWindow.ScaleWeightInternalFrame.Data_FinalGross.AttachedText;
                var Difference = WD.mainWindow.ScaleWeightInternalFrame.Data_Diffenence.AttachedText;
                Base_Assert.AreEqual(Initial_gross, "320.0");
                Base_Assert.AreEqual(Finial_gross,"300.0");
                Base_Assert.AreEqual(Difference,"220.0");
                //click accept dispense
                WD.mainWindow.ScaleWeightInternalFrame.accept.ClickSignle();
                if (WD.ErrorDialog.IsExist())
                {
                    WD.ErrorDialog.OKButton.Click();
                }
                if (WD.ErrorDialog.IsExist())
                {
                    WD.ErrorDialog.OKButton.Click();
                }

                //two other method
                WD.mainWindow.Material_SelectionInternalFrame.materialTable.Row("M801890").Click();
                WD.mainWindow.Material_SelectionInternalFrame.next.Click();
                Thread.Sleep(3000);
                if (WD.mainWindow.BoothCleanInternalFrame.IsExist())
                {
                    WD.mainWindow.BoothCleanInternalFrame.cleanComplete.Click();
                }
                if (WD.mainWindow.HandleInformationInterFrame.IsExist())
                {
                    WD.mainWindow.HandleInformationInterFrame.Acknowledge.ClickSignle();
                }
                Thread.Sleep(3000);
                WD.mainWindow.ScaleWeightInternalFrame.barcode.SendKeys("M801890001");
                if (WD.ConfirmationDialog.IsExist())
                {
                    WD.ConfirmationDialog.YesButton.Click();
                }
                if (WD.MessageDialog.IsExist())
                {
                    WD.MessageDialog.OKButton.Click();
                }
                WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
                Thread.Sleep(3000);
                WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
                WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
                WD.SimulatorWindow.weight.SetText("200");
                WD.SimulatorWindow.OK.Click();
                //click accept dispense
                WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
                if (WD.ErrorDialog.IsExist())
                {
                    WD.ErrorDialog.OKButton.Click();
                }
                if (WD.ErrorDialog.IsExist())
                {
                    WD.ErrorDialog.OKButton.Click();
                }
                Thread.Sleep(3000);
                WD_Fuction.Close();
                LogStep(@"4. check order print report");
                Web.Order_Page.SearchInput.SendKeys("test1");
                Thread.Sleep(2000);
                Web.Order_Page.orderCheckbox.Click();
                Thread.Sleep(2000);
                Web.Order_Page.PrintReport.Click();
                Thread.Sleep(10000);
                Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "orderPrint.PNG");
                var urlll = Selenium_Driver._Selenium_Driver.FindElement(By.XPath("//iframe[@class='gwt-Frame']")).GetAttribute("src");
                string[] parts = urlll.Split('/');
                string ReportFileName = parts[parts.Length - 1];
                Console.WriteLine(ReportFileName);
                string ReportText = Web_Fuction.OrderPrint(ReportFileName);
                int lineCount = 0;
                using (StringReader reader = new StringReader(ReportText))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lineCount++;
                        if (lineCount == 16) // 跳转到指定行号  
                        {
                            Base_Assert.IsTrue(line.Contains("X0125001"));
                        }
                        else if (lineCount == 19)
                        {
                            Base_Assert.IsTrue(line.Contains("Begin Source          320.0 G"));
                        }
                        else if (lineCount == 20)
                        {
                            Base_Assert.IsTrue(line.Contains("End Source           300.0 G"));
                        }
                        else if (lineCount == 21)
                        {
                            Base_Assert.IsTrue(line.Contains("Difference          220.0 G"));
                        }
                        else if (lineCount == 24)
                        {
                            Base_Assert.IsTrue(line.Contains("M801890001"));
                        }
                        else if (lineCount == 27)
                        {
                            Base_Assert.IsTrue(line.Contains("0.00%"));
                        }
                    }
                }

            }
            finally
            {
                LogStep(@"4.delete config key ");
                Base_Function.DeleteConfigKey(Configpath, ConfigKey);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_allx86);
            }
            

        }

    }
}
