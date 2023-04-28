using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WD_UFT_Selenium_Auto.Library.BaseLibrary;
using WD_UFT_Selenium_Auto.Product.WD;

namespace WD_UFT_Selenium_Auto.TestCase
{
    [TestClass]
    public partial class WD_TestCase : UnitTestClassBase<WD_TestCase>
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            GlobalSetup(context);
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
            //Base_Test.KillProcess("javaw");
            Base_File.CleanWorkFolder(Base_Directory.GenerateOutputFileDir(CaseID, ""));
            //Initial data
            WD_Fuction.initial_data();
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
