using HP.LFT.SDK;
using HP.LFT.SDK.WinForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using IWindow = HP.LFT.SDK.WinForms.IWindow;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;
using MES_APEM_UFT_Selenium_Auto.Product.APRM;
using MES_APEM_UFT_Selenium_Auto.Product.ApemMobile;

namespace MES_APEM_UFT_Selenium_Auto
{
    [TestClass]
    public class Prepare
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
        }


        //first to run install and config aprm
        [TestMethod]
        public void PrepareConfig()
        {

            //install  and config aprm
            //APRM_Fuction.FirstInitailAPRM();
            //wia to false
            Mobile_Fuction.UpdateAutoLogin();
            //APRM_Fuction.InitailAPRM();




            //SdkConfiguration config = new SdkConfiguration();
            //SDK.Init(config);


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