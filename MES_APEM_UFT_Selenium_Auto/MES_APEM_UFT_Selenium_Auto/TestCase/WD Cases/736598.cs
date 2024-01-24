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
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;
using System.IO;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    public partial class WD_TestCase
    {
        [TestCaseID(736598)]
        [Title("UC693790_W&D_Enhance Net Removal: record gross weight before and after weighing when partial dispense/Accept")]
        [TestCategory(ProductArea.WD)]
        [Priority(CasePriority.High)]
        [TestCategory(CaseState.Started)]
        [TestCategory(AutomationTool.UFT_Selenium)]
        [Owner(AutomationEngineer.Ziru)]
        [Timeout(1200000)]

        [TestMethod]
        public void VSTS_736598()
        {
            string Resultpath = Base_Directory.ResultsDir + CaseID;
            string order = "test1";
            string method = WDMethod.Netremoval;
            string barcode = "X0125001";
            string source_remove = "600";
            string source_start = "1000";
            string scale = "simulator";
            string Configpath = Base_Directory.ConfigDir + "flags.m2r_cfg";
            string ConfigKey1 = "NET_REMOVAL_REQUIRE_TARGET_TARE =1";
            string Configkey2 = "NET_REMOVAL_REQUIRE_TARGET_TARE =0";
            try
            {
                LogStep(@"1. Set key in config file");
                Base_Function.AddConfigKey(Configpath, ConfigKey1);
                //codify all and restart tomcat
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                Base_Function.ResartServices(ServiceName.Tomcat);
                Thread.Sleep(13000);
                //LogStep(@"2. config APRM admin and apem admin");
                APRM_Fuction.InitailAPRMWD();
                LogStep(@"3. Active orders");
                Selenium_Driver driver = new Selenium_Driver(Browser.chrome);
                Web_Fuction.gotoWDWeb(driver);
                driver.Wait();
                Web_Fuction.login();
                driver.Wait();
                Web_Fuction.gotoTab(WDWebTab.order);
                Web_Fuction.active_order(order);
                driver.Close();
                LogStep(@"5. Open WD client");
                
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
                Assert.IsTrue(WD.mainWindow.ScaleWeightInternalFrame.tare_editor.IsEnabled);
                WD.mainWindow.ScaleWeightInternalFrame.tare_editor.SendKeys("50");
                WD.mainWindow.GetSnapshot(Resultpath + "NetRemoveWithTare.PNG");
                ////start weight
                WD.SimulatorWindow.weight.SetText(source_start);
                WD.SimulatorWindow.OK.Click();
                WD.mainWindow.ScaleWeightInternalFrame.start.Click();
                //input remove weight
                WD.SimulatorWindow.weight.SetText(source_remove);
                WD.SimulatorWindow.OK.Click();
                WD.mainWindow.ScaleWeightInternalFrame.Partial.Click();
                if (WD.ErrorDialog.IsExist())
                {
                    WD.ErrorDialog.OKButton.Click();
                }

                if (WD.ErrorDialog.IsExist())
                {
                    WD.ErrorDialog.OKButton.Click();
                }
                SqlHelper helper = new SqlHelper();
                string SQL = $"SELECT BEGIN_SOURCE_GROSS,END_SOURCE_GROSS FROM EBR_WD_WEIGH_HISTORY";
                List<List<string>> Source = helper.Execute(SQL);
                var Begin_Source = Source[0][0];
                var End_Source = Source[0][1];
                Base_Assert.AreEqual(Begin_Source, "1000.0");
                Base_Assert.AreEqual(End_Source, "600.0");
                /////modify the Configkey
                Base_Function.DeleteConfigKey(Configpath, ConfigKey1);
                Base_Function.AddConfigKey(Configpath,Configkey2);
                //codify all and restart tomcat
                Base_Test.LaunchApp(Base_Directory.Codify_all);
                Base_Function.ResartServices(ServiceName.Tomcat);
                Thread.Sleep(15000);
                WD.mainWindow.ScaleWeightInternalFrame.cancel.Click();
                WD_Fuction.Close();
                Application.LaunchWDAndLogin();
                WD.mainWindow.HomeInternalFrame.MaterialDispensing.Click();
                WD.mainWindow.Material_SelectionInternalFrame.materialTable.Row("X0125").Click();
                WD.mainWindow.Material_SelectionInternalFrame.next.Click();

                //select method and input barcode
                WD_Fuction.SelectMehod(method, "X0125002");

                //select simulator
                if (WD.MessageDialog.IsExist())
                {

                    WD.MessageDialog.OKButton.Click();
                }
                WD.mainWindow.ScaleWeightInternalFrame.scale.SelectItems(scale);
                //zeor
                WD.mainWindow.ScaleWeightInternalFrame.zero.Click();
                //WD.mainWindow.ScaleWeightInternalFrame.tare_editor.SendKeys("50");
                WD.mainWindow.GetSnapshot(Resultpath + "NetRemoveWithoutTare.PNG");
                ////start weight
                WD.SimulatorWindow.weight.SetText(source_start);
                WD.SimulatorWindow.OK.Click();
                WD.mainWindow.ScaleWeightInternalFrame.tare.Click();
                //input remove weight
                WD.SimulatorWindow.weight.SetText(source_remove);
                WD.SimulatorWindow.OK.Click();
                WD.mainWindow.ScaleWeightInternalFrame.accept.Click();
                if (WD.ErrorDialog.IsExist())
                {
                    WD.ErrorDialog.OKButton.Click();
                }
                if (WD.ErrorDialog.IsExist())
                {
                    WD.ErrorDialog.OKButton.Click();
                }
                SqlHelper helper1 = new SqlHelper();
                List<List<string>> Source1 = helper1.Execute(SQL);
                var Begin_Source1 = Source1[1][0];
                var End_Source1 = Source1[1][1];
                //Console.WriteLine(Begin_Source1);
                //Console.WriteLine(End_Source1);
                Base_Assert.AreEqual(Begin_Source1, "1000.0");
                Base_Assert.AreEqual(End_Source1, "600.0");
                ////other method weigh
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
                SqlHelper helper2 = new SqlHelper();
                //string SQL = $"SELECT BEGIN_SOURCE_GROSS,END_SOURCE_GROSS FROM EBR_WD_WEIGH_HISTORY";
                List<List<string>> Source2 = helper2.Execute(SQL);
                var Begin_Source2 = Source2[2][0];
                var End_Source2 = Source2[2][1];
                Base_Assert.AreEqual(Begin_Source2, "");
                Base_Assert.AreEqual(End_Source2, "");
                //LogStep(@"6. Open Batch query tool ");
                Application.LaunchBatchQueryTool();
                Thread.Sleep(3000);
                //open new query
                BatchQueryTool.NewQuery();
                //open batch detail display
                BatchQueryTool.BatchQueryToolWindow.ListView._STD_ListView.ActivateItem(order);
                //wait for loading
                Thread.Sleep(15000);
                //X0125Partial :Begin_Source_Gross is 1000 and End_Source_Gross is 600
                APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
                APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1]").Expand();
                APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1]").Expand();
                APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1]").Expand();
                APRM.BatchMainWindow.TreeView.Select("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1];Container [1]");
                //wait for loading
                Thread.Sleep(5000);
                APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM Batch detail(Partial).PNG");
                APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("End Source Gross");
                Base_Assert.AreEqual("600", APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text, "End Source Gross");
                APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
                APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("Begin Source Gross");
                Base_Assert.AreEqual("1000", APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text, "Begin Source Gross");
                APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
                //X0125Accept :Begin_Source_Gross is 1000 and End_Source_Gross is 600
                APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();
                APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1]").Expand();
                APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1]").Expand();
                APRM.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1]").Expand();
                APRM.BatchMainWindow.TreeView.Select("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1];Container [2]");
                //wait for loading
                Thread.Sleep(5000);
                APRM.BatchMainWindow.GetSnapshot(Resultpath + "APRM Batch detail(Accept).PNG");
                APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("End Source Gross");
                Base_Assert.AreEqual("600", APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text, "End Source Gross");
                APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
                APRM.BatchMainWindow.ListView._STD_ListView.ActivateItem("Begin Source Gross");
                Base_Assert.AreEqual("1000", APRM.BatchMainWindow.BatchCharacteristicDialog.Value.Text, "Begin Source Gross");
                APRM.BatchMainWindow.BatchCharacteristicDialog.Cancel.Click();
                APRM.BatchMainWindow.Close();
                BatchQueryTool.BatchQueryToolWindow.Close();
                BatchQueryTool.BatchQueryToolWindow.Save_Dialog.NO.Click();
                /////report
                ///print report_order
                Selenium_Driver driver2 = new Selenium_Driver(Browser.chrome);
                Web_Fuction.gotoWDWeb(driver2);
                driver2.Wait();
                Web_Fuction.login();
                driver.Wait();
                Web_Fuction.gotoTab(WDWebTab.order);
                Web.Order_Page.SearchInput.SendKeys("test1");
                Thread.Sleep(5000);
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
                            Base_Assert.IsTrue(line.Contains("Begin Source         1,000.0 G"));
                        }
                        else if (lineCount == 20)
                        {
                            Base_Assert.IsTrue(line.Contains("End Source           600.0 G"));
                        }

                    }
                }
                Web.Order_Page.printreportDialogCloseButton.Click();
                ////weigh report
                Web_Fuction.gotoTab(WDWebTab.report);
                Thread.Sleep(3000);
                Web.Report_Page.Weighing.Click();
                Thread.Sleep(3000);
                Web.Report_Page.Generate_Report.Click();
                Thread.Sleep(3000);
                Web_Fuction.TakeScreenshot(Selenium_Driver._Selenium_Driver, Resultpath + "WeighReport.PNG");
                var heads = Web.Report_Page.Report_Heads;
                int begin_headindex = 0;
                int end_headindex = 0;
                int i = 0;
                foreach (var h in heads)
                {
                    if (h.Text == "Begin Source")
                    {
                        begin_headindex = i;
                    }
                    else if (h.Text == "End Source")
                    {
                        end_headindex = i;
                    }
                    i++;
                }
                var table_datas = Web.Report_Page.Report_Table_Rows;
                string weigh1_beginsource = table_datas[2].FindElements(By.TagName("td"))[begin_headindex].Text;
                string weigh1_endsource = table_datas[2].FindElements(By.TagName("td"))[end_headindex].Text;
                //Console.WriteLine(weigh1_beginsource);
                //Console.WriteLine(weigh1_endsource);
                Base_Assert.AreEqual(weigh1_beginsource, "1,000.0");
                Base_Assert.AreEqual(weigh1_endsource,"600.0");
                driver2.Close();




            }
            finally
            {
                LogStep(@"4.delete config key ");
                Base_Function.DeleteConfigKey(Configpath, Configkey2);
                Base_Function.DeleteConfigKey(Configpath, ConfigKey1);
                //codify all
                Base_Test.LaunchApp(Base_Directory.Codify_all);

                Base_Function.ResartServices(ServiceName.Tomcat);
            }
            

        }

    }
}
