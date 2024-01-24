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
using MES_APEM_UFT_Selenium_Auto.Product.DataBaseWizard;

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

            //start tomcat and sql service
            Base_Function.ResartServices(ServiceName.SQL);
            Base_Function.ResartServices(ServiceName.Tomcat);
            //config apem DB
            Wizard_Fuction.CreateApemDB();
            //AeBRSInstaller
            APEM.AeBRSInstaller();
            //update afw
            AFW_Fuction.ReplaceAFWDB();
            //install  and config aprm
            APRM_Fuction.FirstInitailAPRMWD();
            //wia to false
            Mobile_Fuction.UpdateAutoLogin();
            //set apem server and registration
            APEM.setServerAndRegistration();

            //install SoapMsi
            Base_Function.InstallMsi("soap3.0.msi");
            Base_Function.InstallMsi("msxml6.msi");

            //WD
            Web_Fuction.FirstGrantPermission();
            //import material/bom exception xml
            string material = "04 aspen wd material bulk load.xml";
            string bomExc = "06 aspen wd bom exception bulk load.xml";
            WD_Fuction.Bulkload(material);
            WD_Fuction.Bulkload(bomExc);


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