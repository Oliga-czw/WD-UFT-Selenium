using HP.LFT.SDK;
using HP.LFT.SDK.WinForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using MES_APEM_UFT_Selenium_Auto.Product.APEM.MOC_TemplatesModule;
using System.Diagnostics;

namespace MES_APEM_UFT_Selenium_Auto
{
    [TestClass]
    public class Install
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Base_logger.GenerateLogFile("Install");
            Base_Test.UFTInitializes();
        }


        [TestMethod]
        public void InstallMedia()
        {

            string versionFileName = "base_version.txt";
            string versionPath = Path.Combine("C:\\Users\\QAONE1\\Desktop", versionFileName);
            if (!File.Exists(versionPath))
            {
                using (FileStream fs = File.Create(versionPath)) ;
            }

            string version = string.Empty;
            using (StreamReader sr = new StreamReader(versionPath))
            {
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    version = line;
                }
            }


            string baseFolder = @"\\shrdfile01\aspenONE_Media\aspenONEV14.5\MSC\GranularInstall";
            string newestFile = Utility.GetNewestIsoFile(baseFolder);

            if (version == newestFile)
            {
                return;
            }

            using (StreamWriter sw = new StreamWriter(versionPath))
            {
                sw.WriteLine("begin install: " + newestFile);
            }

            string basePath = newestFile;
            Process.Start(basePath);
            Thread.Sleep(5 * 1000);
            Process.Start(@"E:\Setup.exe");
            Thread.Sleep(5 * 1000);
            Install_Window.welcomeWindow.Activate();
            Thread.Sleep(5 * 1000);
            Install_Window.installButton.Click();
            Thread.Sleep(5 * 1000);
            Install_Window.InstallAspenOneButton.Click();
            Thread.Sleep(5 * 1000);
            Install_Window.iAcceptTheTermsOfThisAgreementCheckBox.Click();
            Thread.Sleep(5 * 1000);
            Install_Window.nextButton.Click();
            Thread.Sleep(5 * 1000);
            Install_Window.ChooseProduct();
            Thread.Sleep(5 * 1000);
            Install_Window.nextButton.Click();
            Thread.Sleep(180 * 1000);
            Install_Window.licenseServerInput.Click();
            Thread.Sleep(3 * 1000);
            Install_Window.licenseServerInput.SendKeys("shslmtest");
            Thread.Sleep(3 * 1000);
            Install_Window.addServerButton.Click();
            Thread.Sleep(3 * 1000);
            if (Install_Window.aspenONEInstallerDialog.Exists())
            {
                Install_Window.aspenONEInstallerDialog.Activate();
                Thread.Sleep(1000);
                //Install_Window.yesButton.Highlight();
                Install_Window.yesButton.Click();
                Thread.Sleep(3 * 1000);
            }

            Install_Window.nextButton.Click();
            Thread.Sleep(5 * 1000);
            Install_Window.userNameInput.Click();
            Thread.Sleep(3 * 1000);
            Install_Window.userNameInput.SendKeys(@"qae\qaone1");
            Thread.Sleep(5 * 1000);
            Install_Window.passwordInput.Click();
            Thread.Sleep(3 * 1000);
            Install_Window.passwordInput.SendKeys("Aspen111");
            Thread.Sleep(5 * 1000);
            Install_Window.nextButton.Click();
            Thread.Sleep(5 * 1000);
            Install_Window.InstallNowButton.Click();
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(5 * 60 * 1000);
                if (Install_Window.finishButton.Exists())
                {
                    break;
                }
            }
            Install_Window.finishButton.Click();
            Thread.Sleep(20 * 1000);
            Install_Window.autoLaunchUpdateCheckBox.Click();
            Thread.Sleep(5 * 1000);

            using (StreamWriter sw = new StreamWriter(versionPath, true))
            {
                sw.WriteLine(newestFile);
            }

            using (StreamWriter sw = new StreamWriter("C:\\Users\\QAONE1\\Desktop\\config.ini"))
            {
                string versionInshort = Path.GetFileName(newestFile);
                sw.WriteLine("version=" + versionInshort);
            }

            Utility.RunCommand("shutdown -r -f");




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