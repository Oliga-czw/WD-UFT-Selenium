using HP.LFT.SDK;
using HP.LFT.SDK.WinForms;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using IWindow = HP.LFT.SDK.StdWin.IWindow;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;

namespace MES_APEM_UFT_Selenium_Auto
{
    [TestClass]
    public class UftDeveloperSeleniumTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {

            mMDM_Fuction.GML_Configure_mMDM_Editor();



            //APRM_Fuction.CleanAprmDB();
            //APRM_Fuction.WizardAprmDB();
            //APRM_Fuction.ImportAprmAdmin();


            //Application.LaunchBatchDetailDisplay();
            //Batch_Fuction.setOptionData();
            //APRM_Fuction.InitailAPRM();
            //APRM_Fuction.ConfigAPEMAdmin();

            //SdkConfiguration config = new SdkConfiguration();
            //SDK.Init(config);


            //var closeButton = Desktop.Describe<IWindow>(new WindowDescription
            //{
            //	WindowTitleRegExp = @"Aspen Database Wizard"
            //})
            //.Describe<IButton>(new ButtonDescription
            //{
            //	WindowTitleRegExp = @"C&lose"
            //});
            //Console.WriteLine(closeButton.IsEnabled);

            //Console.WriteLine(Wizard.AdminWizardWindow.btnClose.IsEnabled);


            //         var grossText = WD.BatchMainWindow.ListView._STD_ListView
            //.Describe<IListItem>(new ListItemDescription
            //{
            //	ProcessName = @"BatchDetailDisplay",
            //	Name = @"Gross",
            //	Path = @"Window;Pane;Pane;Pane;Pane;Pane;Pane;List;ListItem",
            //	SupportedPatterns = new string[] { @"Invoke", @"LegacyIAccessible", @"ScrollItem", @"SelectionItem" },
            //	FrameworkId = @"Win32",
            //	ControlType = @"ListItem",
            //	AutomationId = @"ListViewItem-8"
            //})
            //.Describe<IText>(new TextDescription
            //{
            //	ProcessName = @"BatchDetailDisplay",
            //	Name = @"Gross",
            //	Path = @"Window;Pane;Pane;Pane;Pane;Pane;Pane;List;ListItem;Text",
            //	SupportedPatterns = new string[] { @"GridItem", @"LegacyIAccessible", @"TableItem" },
            //	FrameworkId = @"Win32",
            //	ControlType = @"Text",
            //	AutomationId = @"ListViewSubItem-0"
            //});

            //Console.WriteLine(grossText.Name);
            //grossText.Click();

            ////add
            //SLM.SLMConfigWindow.ServerEdit.SendKeys("shslmtest");
            //SLM.SLMConfigWindow.AddServer.Click();
            ////wait for adding
            //Thread.Sleep(5000);
            //SLM.SLMConfigWindow.Apply.Click();
            ////wait for applying
            //Thread.Sleep(5000);


            //SLM.SLMConfigWindow.Close();
            //Thread.Sleep(2000);
            //SLM.SLMmainWindow.Close();



            //Batch_Fuction.findBatch("test2");
            //SdkConfiguration config = new SdkConfiguration();
            //SDK.Init(config);

            //SqlHelper helper = new SqlHelper();
            //string sql = $"SELECT BEGIN_SOURCE_GROSS,END_SOURCE_GROSS FROM EBR_WD_WEIGH_HISTORY";
            //var dt = helper.Execute(sql);
            //Console.WriteLine(dt[0][0]);

            ////add key
            //FileStream fs = new FileStream(path, FileMode.Append);
            //StreamWriter sw = new StreamWriter(fs);
            //sw.WriteLine("NET_REMOVAL_REQUIRE_TARGET_TARE = 1");
            //sw.Close();

            ////delete key
            //string all = File.ReadAllText(path);
            //all = Regex.Replace(all, @"NET_REMOVAL_REQUIRE_TARGET_TARE = \d{1}", "");
            //Console.WriteLine(all);
            //File.WriteAllText(path, all);

            ////search
            //string[] str = File.ReadAllLines(path);
            //foreach (string text in str)
            //{
            //    if(Regex.Match(text, @"NET_REMOVAL_REQUIRE_TARGET_TARE = \d{1}") != null)
            //    {
            //        Console.WriteLine(Regex.Match(text, @"NET_REMOVAL_REQUIRE_TARGET_TARE = \d{1}$"));
            //    }

            //}



            //StreamReader sr = new StreamReader(path);
            //string content = sr.ReadToEnd();
            //sr.Close();
            //Console.WriteLine(content);
        }


      
        [TestCleanup]
        public void TestCleanup()
        {
            
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }
        public string CaseID = "prepareInitial";
    }
}
        