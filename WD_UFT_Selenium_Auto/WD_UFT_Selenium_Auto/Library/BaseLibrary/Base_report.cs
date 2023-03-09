using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace WD_UFT_Selenium_Auto.Library.BaseLibrary
{
    public class Base_Reporter
    {
        private static string _reportFile = "TestReport.csv";
        private static string _datetimeFormat = "yyyy.MMdd.HHmmss";
        private static string _reportHeader = "TestCaseId" + "," + "Title" + "," + "Outcome" + "," + "Time" + "\r\n";

        private TestContext _testContext;

        public Base_Reporter(TestContext TestContext)
        {
            _testContext = TestContext;

        }
        public TestContext TestContext
        {
            get => _testContext;
        }

        public void GenerateReportFile(string _descrpt = null)
        {
            string fileDate = DateTime.Now.ToString(_datetimeFormat);
            string _logFilePath = Path.Combine(Base_Directory.ReportDir, _reportFile);

            if (!File.Exists(_logFilePath))
            {
                if (!Directory.Exists(Base_Directory.ReportDir))
                {
                    Directory.CreateDirectory(Base_Directory.ReportDir);
                }
                File.AppendAllText(_logFilePath, _reportHeader);
            }
            LogCaseResult(_logFilePath, _descrpt);
        }
        private void LogCaseResult(string filePath, string _descrpt = null)
        {
            UnitTestOutcome _outcome = TestContext.CurrentTestOutcome;
            LogCaseResult(filePath, _outcome, _descrpt);
        }
        private void LogCaseResult(string filePath, UnitTestOutcome outCome, string _caseTitle = null)
        {
            string _caseID = TestContext.TestName.Split('_')[1];
            string _currentTime = DateTime.Now.ToString();
            string resultLog = _caseID + "," + _caseTitle + "," + outCome + "," + _currentTime + "\r\n";
            File.AppendAllText(filePath, resultLog);
        }

    }
}

