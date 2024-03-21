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
            //SdkConfiguration config = new SdkConfiguration();
            //SDK.Init(config);
            //Thread.Sleep(3000);







        }
    }

}
