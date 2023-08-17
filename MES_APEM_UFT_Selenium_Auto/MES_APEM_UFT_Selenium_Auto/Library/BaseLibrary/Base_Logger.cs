using System;
using System.Collections.Generic;
using System.IO;

namespace MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary
{ 
    public static class Base_logger
    {
        private static string _logSuffix = ".log";
        private static string _datetimeFormat = "yyyy.MMdd.HHmmss";
        private static string _logFilePath = string.Empty;
        private static List<string> _logList = null;

        public static string GenerateLogFile(string CaseID)
        {
            string fileDate = DateTime.Now.ToString(_datetimeFormat);

            if (!Directory.Exists(Base_Directory.LogDir))
            {
                Directory.CreateDirectory(Base_Directory.LogDir);
            }

            _logFilePath = Path.Combine(Base_Directory.LogDir, CaseID + "-" + fileDate + _logSuffix);
            if (!File.Exists(_logFilePath))
            {
                FileStream fileStream = File.Create(_logFilePath);
                fileStream.Close();
                _logList = new List<string>();
                LogStart();
            }
            return _logFilePath;
        }

        public static void WriteLog(string output, string logTitle = null, bool toWriteConsole = false)
        {
            if (logTitle == null)
            {
                logTitle = "---- Log Message ----";
            }
            string log = logTitle + output;

            // Print the log on console
            if (toWriteConsole == true)
            {
                Console.WriteLine(log);
            }

            _logList.Add(log);
        }

        public static string SaveLogFile()
        {
            LogEnd();
            File.WriteAllLines(_logFilePath, _logList);
            return _logFilePath;
        }
        public static void Message(string message)
        {
            string log = "---- Log Message ----";
            WriteLog(message, log);
        }
        public static void Info(string message)
        {
            string log = "---- Log INFO ----";
            WriteLog(message, log);
        }
        public static void ListInfo(List<string> message)
        {
            string log = "---- Log LIST INFO ----";
            foreach (string item in message)
            {
                WriteLog(item, log);
            }
        }
        public static void Error(string message)
        {
            string log = "---- Log ERROR ----";
            WriteLog(message, log);
        }
        public static void Warning(string message)
        {
            string log = "---- Log WARNING ----";
            WriteLog(message, log);
        }
        public static void Exception(Exception e)
        {
            string log = string.Format("Exception:{0}\r\n  source:{1}\r\n  callstack: {2}\r\n", e.Message, e.Source, e.StackTrace);
            WriteLog(log);
        }
        public static void Step(string content)
        {
            string log = "Step: " + content;
            WriteLog(log, "----Log STEP----");
        }
        public static void LogStart()
        {
            _logList.Add("---- Log START ----" + DateTime.Now.ToString());
        }
        public static void LogEnd()
        {
            _logList.Add("---- Log END ----" + DateTime.Now.ToString());
        }
        public static void LogStart(string log)
        {
            WriteLog(log, "----start----" + $"({DateTime.Now.ToString()})");
        }
        public static void LogEnd(string log)
        {
            WriteLog(log, "----end----" + $"({DateTime.Now.ToString()})");
        }

    }
}

