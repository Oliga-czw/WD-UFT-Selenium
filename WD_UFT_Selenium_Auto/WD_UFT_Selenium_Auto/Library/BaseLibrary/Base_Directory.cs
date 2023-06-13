using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;

namespace WD_UFT_Selenium_Auto.Library.BaseLibrary
{
    public static class Base_Directory
    {
        private static Base_Registry registry = new Base_Registry();

        internal const string InputFolder = "Data\\Input";
        internal const string OutputFolder = "Data\\Output";



        public static string WDDir = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Aspen Manufacturing Execution\Aspen Weigh and Dispense Execution.lnk";

        public static string AFWDir = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\AspenTech\Aspen Security\AFW Security Manager(64-bit).lnk";
        public static string MOCDir = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Aspen Production Execution Manager MOC.lnk";

        public static string BDDDir = @"C:\Program Files (x86)\AspenTech\Batch.21\Client\BatchDetailDisplay.exe";

        public static string WDBulkload
        {
            get
            {
                return registry.GetValueData("AeBRS", "data");
            }
        }

        //public static string APropDir
        //{
        //    get
        //    {
        //        return Path.Combine(registry.GetValueData("Aspen Properties", "mmxeq"), "AspenProperties.exe");
        //    }
        //}
        public static string APlusExampleDir
        {
            get
            {
                return Path.Combine(registry.GetValueData("Aspen Plus", "mmtop"), "Examples");
            }
        }

        public static string ProjectDir
        {
            get
            {

                string currentFolder = Directory.GetCurrentDirectory();
                if (currentFolder.Contains("WD_UFT_Selenium_Auto"))
                {
                    currentFolder = currentFolder.Substring(0, currentFolder.IndexOf("bin"));
                }

                return currentFolder;
            }
        }
        public static string InputDir
        {
            get
            {
                string inputDir = Path.Combine(ProjectDir, InputFolder);

                return inputDir;
            }
        }
        public static string OutputDir
        {
            get
            {
                string outputDir = Path.Combine(ProjectDir, OutputFolder);

                return outputDir;
            }
        }
        public static string ResultsDir => Path.Combine(ProjectDir, "Results\\");
        public static string LogDir => Path.Combine(ProjectDir, "Log\\");
        public static string DebugLogDir => Path.Combine(ProjectDir, "DebugLog\\");
        public static string ReportDir => Path.Combine(ProjectDir, "Report\\");
        public static string GenerateInputFileDir(string caseName, string fileName)
        {
            string inputFileDir = Path.Combine(InputDir, caseName);
            if (!Directory.Exists(inputFileDir))
            {
                Directory.CreateDirectory(inputFileDir);
            }
            return Path.Combine(inputFileDir, fileName);
        }
        public static string GenerateOutputFileDir(string caseName, string fileName)
        {
            if (!Directory.Exists(OutputDir))
            {
                Directory.CreateDirectory(OutputDir);
            }
            string outPutFileDir = Path.Combine(OutputDir, caseName);
            if (!Directory.Exists(outPutFileDir))
            {
                Directory.CreateDirectory(outPutFileDir);
            }
            return Path.Combine(outPutFileDir, fileName);
        }
        //public static string GenerateExampleFileDir(string caseName, string fileName)
        //{
        //    string exampleFileDir = Path.Combine(APlusExampleDir, fileName);

        //    return exampleFileDir;
        //}

        //public static string DownloadFileDir => "C:\\Users\\" + RegistryHive.CurrentUser + "\\Downloads";
        public static string DownloadFileDir => "C:\\Users\\"+ Environment.UserName + "\\Downloads";

        public static string BulkLoadDir => Base_Directory.ProjectDir + "Data\\Input\\BulkLoad\\";

        public static string InventoryDownloadDir => "C:\\ProgramData\\AspenTech\\AeBRS\\WDDownload\\Pending\\Inventory";
        public static string MaterialDownloadDir => "C:\\ProgramData\\AspenTech\\AeBRS\\WDDownload\\Pending\\Material";
        public static string OrdersDownloadDir => "C:\\ProgramData\\AspenTech\\AeBRS\\WDDownload\\Pending\\Orders";



    }
}
