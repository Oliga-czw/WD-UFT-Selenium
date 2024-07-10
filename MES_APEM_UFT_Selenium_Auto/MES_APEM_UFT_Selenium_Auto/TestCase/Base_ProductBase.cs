using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using MES_APEM_UFT_Selenium_Auto.Product.WD;
using MES_APEM_UFT_Selenium_Auto.Product.APEM;

namespace MES_APEM_UFT_Selenium_Auto.TestCase
{
    [TestClass]
    public partial class WD_TestCase : UnitTestClassBase<WD_TestCase>
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            //create resultdir
            if (!Directory.Exists(Base_Directory.ResultsDir))
            {
                Directory.CreateDirectory(Base_Directory.ResultsDir);
            }
            GlobalSetup(context);
            //AFW_Fuction.ReplaceAFWDB();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            GlobalTearDown();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Base_logger.GenerateLogFile(CaseID);
            Base_logger.Info("Test Initialize");
            Base_Test.KillProcess("BatchDetailDisplay");
            Base_Test.KillProcess("javaw");
            Base_Test.KillProcess("mmc");
            Base_Test.KillProcess("BatchQueryTool");
            Base_Test.KillProcess("DatabaseWizard");
            Base_File.CleanWorkFolder(Base_Directory.GenerateOutputFileDir(CaseID, ""));
            //Initial data
            WD_Fuction.initial_data();
            Base_Test.UFTInitializes();


        }

        [TestCleanup]
        public void TestCleanup()
        {
            Base_logger.Info("Clean Up");
            Base_logger.SaveLogFile();
            //Base_Reporter report = new Base_Reporter(TestContext);
            //Type ClassType = this.GetType();
            //string _Descrpt = Base_Attribute.GetTestDescription(ClassType);
            //report.GenerateReportFile(_Descrpt);
            Base_Test.KillProcess("javaw");
            Base_Test.KillProcess("chrome");
            Base_Test.KillProcess("msedge");
            Base_Test.KillProcess("AspenOneLicensingTool");
            Base_Test.KillProcess("mmc");
            Base_Test.KillProcess("BatchQueryTool");
            Base_Test.KillProcess("BatchDetailDisplay");
            Base_Test.KillProcess("chromedriver");
            Base_Test.KillProcess("AspenOneLicensingTool");

        }

        public string CaseID => this.GetType().Name == null ? throw new ArgumentNullException() : TestCaseManage.GetCase(this.TestContext.TestName).CaseID;

        
        
        public void PrintAttributes(System.Type t)
        {
            System.Attribute[] attr = System.Attribute.GetCustomAttributes(t);
            attr.ToList().ForEach(item => Console.WriteLine("Attribute:" + item));
        }
        public static string GetTestDescription(MethodBase testMethodInfo)
        {

            var t = (DescriptionAttribute)testMethodInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return t == null ? string.Empty : t.Description;
        }
        public void LogStep(string step)
        {
            Base_logger.Step(step);
        }
        public void LogMessage(string message)
        {
            Base_logger.Message(message);
        }

    }

    [TestClass]
    public partial class GML_TestCase : UnitTestClassBase<GML_TestCase>
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            //create resultdir
            if (!Directory.Exists(Base_Directory.ResultsDir))
            {
                Directory.CreateDirectory(Base_Directory.ResultsDir);
            }
            GlobalSetup(context);
            AFW_Fuction.ReplaceAFWDB();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            GlobalTearDown();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Base_logger.GenerateLogFile(CaseID);
            Base_logger.Info("Test Initialize");
            Base_Test.KillProcess("BatchDetailDisplay");
            //Base_Test.KillProcess("javaw");
            Base_Test.KillProcess("AtOdmAdministrator");
            Base_File.CleanWorkFolder(Base_Directory.GenerateOutputFileDir(CaseID, ""));
            //Initial data
            Base_Test.UFTInitializes();
            //GML_Function.GML_ConfigAll();


        }

        [TestCleanup]
        public void TestCleanup()
        {
            Base_logger.Info("Clean Up");
            Base_logger.SaveLogFile();
            //Base_Reporter report = new Base_Reporter(TestContext);
            //Type ClassType = this.GetType();
            //string _Descrpt = Base_Attribute.GetTestDescription(ClassType);
            //report.GenerateReportFile(_Descrpt);
            Base_Test.KillProcess("javaw");
            Base_Test.KillProcess("chrome");
            Base_Test.KillProcess("AtOdmAdministrator");
            

        }

        public string CaseID => this.GetType().Name == null ? throw new ArgumentNullException() : TestCaseManage.GetCase(this.TestContext.TestName).CaseID;



        public void PrintAttributes(System.Type t)
        {
            System.Attribute[] attr = System.Attribute.GetCustomAttributes(t);
            attr.ToList().ForEach(item => Console.WriteLine("Attribute:" + item));
        }
        public static string GetTestDescription(MethodBase testMethodInfo)
        {

            var t = (DescriptionAttribute)testMethodInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return t == null ? string.Empty : t.Description;
        }
        public void LogStep(string step)
        {
            Base_logger.Step(step);
        }
        public void LogMessage(string message)
        {
            Base_logger.Message(message);
        }

    }
    [TestClass]
    public partial class APEM_TestCase : UnitTestClassBase<APEM_TestCase>
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            //create resultdir
            if (!Directory.Exists(Base_Directory.ResultsDir))
            {
                Directory.CreateDirectory(Base_Directory.ResultsDir);
            }
            GlobalSetup(context);
            //AFW_Fuction.ReplaceAFWDB();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            GlobalTearDown();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Base_logger.GenerateLogFile(CaseID);
            Base_logger.Info("Test Initialize");
            //Base_Test.KillProcess("BatchDetailDisplay");
            Base_Test.KillProcess("javaw");
            Base_Test.KillProcess("mmc");
            Base_File.CleanWorkFolder(Base_Directory.GenerateOutputFileDir(CaseID, ""));
            Base_Test.UFTInitializes();

        }

        [TestCleanup]
        public void TestCleanup()
        {
            Base_logger.Info("Clean Up");
            Base_logger.SaveLogFile();
            //Base_Reporter report = new Base_Reporter(TestContext);
            //Type ClassType = this.GetType();
            //string _Descrpt = Base_Attribute.GetTestDescription(ClassType);
            //report.GenerateReportFile(_Descrpt);
            Base_Test.KillProcess("javaw");
            Base_Test.KillProcess("chrome");
            Base_Test.KillProcess("BatchQueryTool");
            Base_Test.KillProcess("BatchDetailDisplay");
            Base_Test.KillProcess("mmc");
            Base_Test.KillProcess("chromedriver");
            Base_Test.KillProcess("sqlplus");
            Base_Test.KillProcess("AspenOneLicensingTool");
        }

        public string CaseID => this.GetType().Name == null ? throw new ArgumentNullException() : TestCaseManage.GetCase(this.TestContext.TestName).CaseID;



        public void PrintAttributes(System.Type t)
        {
            System.Attribute[] attr = System.Attribute.GetCustomAttributes(t);
            attr.ToList().ForEach(item => Console.WriteLine("Attribute:" + item));
        }
        public static string GetTestDescription(MethodBase testMethodInfo)
        {

            var t = (DescriptionAttribute)testMethodInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return t == null ? string.Empty : t.Description;
        }
        public void LogStep(string step)
        {
            Base_logger.Step(step);
        }
        public void LogMessage(string message)
        {
            Base_logger.Message(message);
        }

    }
    [TestClass]
    public partial class Mobile_TestCase : UnitTestClassBase<Mobile_TestCase>
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            //create resultdir
            if (!Directory.Exists(Base_Directory.ResultsDir))
            {
                Directory.CreateDirectory(Base_Directory.ResultsDir);
            }
            GlobalSetup(context);
            //AFW_Fuction.ReplaceAFWDB();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            GlobalTearDown();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Base_logger.GenerateLogFile(CaseID);
            Base_logger.Info("Test Initialize");
            //Base_Test.KillProcess("BatchDetailDisplay");
            Base_Test.KillProcess("javaw");
            Base_Test.KillProcess("mmc");
            Base_File.CleanWorkFolder(Base_Directory.GenerateOutputFileDir(CaseID, ""));
            Base_Test.UFTInitializes();

        }

        [TestCleanup]
        public void TestCleanup()
        {
            Base_logger.Info("Clean Up");
            Base_logger.SaveLogFile();
            //Base_Reporter report = new Base_Reporter(TestContext);
            //Type ClassType = this.GetType();
            //string _Descrpt = Base_Attribute.GetTestDescription(ClassType);
            //report.GenerateReportFile(_Descrpt);
            Base_Test.KillProcess("javaw");
            Base_Test.KillProcess("chrome");
            Base_Test.KillProcess("msedge");
            Base_Test.KillProcess("BatchQueryTool");
            Base_Test.KillProcess("BatchDetailDisplay");
            Base_Test.KillProcess("mmc");
            Base_Test.KillProcess("chromedriver");
            Base_Test.KillProcess("sqlplus");
            Base_Test.KillProcess("AspenOneLicensingTool");
        }

        public string CaseID => this.GetType().Name == null ? throw new ArgumentNullException() : TestCaseManage.GetCase(this.TestContext.TestName).CaseID;



        public void PrintAttributes(System.Type t)
        {
            System.Attribute[] attr = System.Attribute.GetCustomAttributes(t);
            attr.ToList().ForEach(item => Console.WriteLine("Attribute:" + item));
        }
        public static string GetTestDescription(MethodBase testMethodInfo)
        {

            var t = (DescriptionAttribute)testMethodInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return t == null ? string.Empty : t.Description;
        }
        public void LogStep(string step)
        {
            Base_logger.Step(step);
        }
        public void LogMessage(string message)
        {
            Base_logger.Message(message);
        }

    }
}
