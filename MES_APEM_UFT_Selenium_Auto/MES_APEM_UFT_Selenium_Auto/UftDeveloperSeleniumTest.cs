using HP.LFT.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using System.Windows.Forms;
using System;
using HP.LFT.SDK.StdWin;
using System.Diagnostics;
using MES_APEM_UFT_Selenium_Auto.Library.SeleniumLibrary;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;
using OpenQA.Selenium;
using System.Drawing;
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.IO;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using System.Collections.Generic;
using MES_APEM_UFT_Selenium_Auto.Product.DataBaseWizard;
using OpenQA.Selenium.Interactions;
using Spire.Pdf;
using System.Text;
using Spire.Pdf.Texts;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;
using MES_APEM_UFT_Selenium_Auto.Product.SQLplus;
using Application = MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary.Application;
using System.Linq;
using Keys = OpenQA.Selenium.Keys;

namespace MES_APEM_UFT_Selenium_Auto
{
    [TestClass]
    public class UftDeveloperSeleniumTest
    {

        [TestInitialize]
        public void TestInitialize()
        {
            Base_logger.GenerateLogFile("TestMethod1");
        }

        [TestMethod]
        public void TestMethod1()
        {
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            Thread.Sleep(2000);
            string RPLname = "FOR_STATUS";
            string Ordername = "ORDER539370";
            string BPLname = "BPL539370";
            string PO_Value = "PO123";
            GML_Function.GMLAPRMConfig();
            //Library.BaseLibrary.Application.LaunchMocAndLogin();
            //APEM.MocmainWindow.RPLDesign.ClickSignle();
            //if (!APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname).Existing)
            //{
            //    MOC_TemplatesFunction.Importtemplates("TEMP539370.zip");
            //}
            MOC_Fuction.PlanFromRPL(RPLname, Ordername,true,PO_Value);
            APEM.MocmainWindow.BPLDesign.Click();
            APEM.MocmainWindow.BPLListInternalFrame.BPLList_Table.Row(BPLname, "Name").Click();
            Thread.Sleep(2000);
            APEM.MocmainWindow.BPLListInternalFrame.LoadDesigner_Button.Click();
            Thread.Sleep(2000);
            APEM.DesignEditorWindow.Execute.Run_Environment.Select();
            Thread.Sleep(4000);
            APEM.DesignEditorWindow.RunEnvironmentInternalFrame.SelectOrder.SelectItems(Ordername);
            APEM.DesignEditorWindow.RunEnvironmentInternalFrame.OKButton.Click();
            Thread.Sleep(3000);
            APEM.DesignEditorWindow.MessageInterFrame.OKButton.Click();
            APEM.ExecutionFinishedDialog.OKButton.Click();
            APEM.DesignEditorWindow.Close();
            //Open Batch query tool
            Application.LaunchBatchQueryTool();
            Thread.Sleep(3000);
            //open new query
            BatchQueryTool.NewQuery();
            //open batch detail display
            BatchQueryTool.BatchQueryToolWindow.ListView._STD_ListView.ActivateItem(PO_Value);
            //wait for loading
            Thread.Sleep(15000);
            APRM.BatchMainWindow.TreeView.GetNode("Batch").Expand();

        }
    }

}
