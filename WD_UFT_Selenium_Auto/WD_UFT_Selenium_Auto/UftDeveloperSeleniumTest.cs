using HP.LFT.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using System;
using System.Drawing;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto
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
            //Application.LaunchBatchDetailDisplay();
            //Batch_Fuction.findBatch("test2");
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            WD.BatchMainWindow.TreeView.GetNode("Batch").Expand();
            WD.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1]").Expand();
            WD.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1]").Expand();
            WD.BatchMainWindow.TreeView.GetNode("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1]").Expand();
            WD.BatchMainWindow.TreeView.Selete("Batch;WEIGH_AND_DISPENSE [1];BOM [1];Material [1];Action [1]");
        }
        [TestCleanup]
        public void TestCleanup()
        {
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }
    }
}