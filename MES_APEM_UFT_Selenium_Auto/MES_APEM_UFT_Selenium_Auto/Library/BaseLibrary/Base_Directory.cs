using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;

namespace MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary
{
    public static class Base_Directory
    {
        private static Base_Registry registry = new Base_Registry();

        internal const string InputFolder = "Data\\Input";
        internal const string OutputFolder = "Data\\Output";

         
        public static string CMDDir = @"C:\Windows\System32\cmd.exe";
        public static string WDDir = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Aspen Manufacturing Execution\Aspen Weigh and Dispense Execution.lnk";
        public static string AeBRSClientConfigureDir = @"C:\Program Files (x86)\AspenTech\AeBRS\AeBRSClientConfigure.exe";
        public static string AFWDir = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\AspenTech\Aspen Security\AFW Security Manager(64-bit).lnk";
        public static string MOCDir = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Aspen Production Execution Manager MOC.lnk";

        public static string SLMDir = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\aspenONE SLM License Manager.lnk";
        public static string BDDDir = @"C:\Program Files (x86)\AspenTech\Batch.21\Client\BatchDetailDisplay.exe";
        public static string BatchQueryToolDir = @"C:\Program Files (x86)\AspenTech\Batch.21\Client\BatchQueryTool.exe";

        public static string APRMDir = @"C:\APRM";
        public static string DBDir = @"C:\DB";

        public static string WizrdDir = @"C:\Program Files (x86)\Common Files\AspenTech Shared\DatabaseWizard\DatabaseWizard.exe";

        public static string AprmAdminDir = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Aspen Production Record Manager Administrator (batch).lnk";
        public static string APEMAdminDir = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Aspen Manufacturing Execution\Aspen Production Execution Manager Administrator.lnk";

        public static string mMDMAdminDir = @"C:\Program Files\AspenTech\mMDM\Bin\AtOdmAdministrator.exe";
        public static string mMDMBulkLoadDir = @"C:\Program Files\AspenTech\mMDM\Bin\AtOdmBulkLoad.exe";
        public static string mMDMEditorDir = @"C:\Program Files\AspenTech\mMDM\Bin\AtOdmEditor.exe";

        public static string IP21Dir = @"C:\Program Files\AspenTech\InfoPlus.21\db21\code\CimMgr.exe";

        public static string SQLPlusDir = @"C:\Program Files (x86)\AspenTech\InfoPlus.21\db21\code\sqlplus.exe";
        
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
                if (currentFolder.Contains("MES_APEM_UFT_Selenium_Auto"))
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

        public static string LabelPrintFileDir => "C:\\Users\\" + Environment.UserName + @"\AppData\Local\Temp\2";

        public static string DownloadFileDir => "C:\\Users\\"+ Environment.UserName + "\\Downloads";

        public static string BulkLoadDir => Base_Directory.ProjectDir + "Data\\Input\\BulkLoad\\";
        public static string AeBRSInstallerDir => Base_Directory.ProjectDir + "Data\\Input\\AeBRSInstaller.bat";
        public static string InventoryDownloadDir => "C:\\ProgramData\\AspenTech\\AeBRS\\WDDownload\\Pending\\Inventory";
        public static string MaterialDownloadDir => "C:\\ProgramData\\AspenTech\\AeBRS\\WDDownload\\Pending\\Material";
        public static string OrdersDownloadDir => "C:\\ProgramData\\AspenTech\\AeBRS\\WDDownload\\Pending\\Orders";
        public static string WDUploadDir => @"C:\ProgramData\AspenTech\AeBRS\WDUpload";


        public static string ConfigDir => @"C:\Program Files\AspenTech\AeBRS\cfg_source\";
        public static string Codify_all = @"C:\Program Files\AspenTech\AeBRS\cfg_source\codify_all.cmd";
        public static string ConfigDirx86 => @"C:\Program Files (x86)\AspenTech\AeBRS\cfg_source\";

        public static string Codify_allx86 = @"C:\Program Files (x86)\AspenTech\AeBRS\cfg_source\codify_all.cmd";



        public static string WDBatch = @"C:\Program Files (x86)\AspenTech\AeBRS\Templates\WeighDispense.xml";
        public static string BatchArea = @"C:\Program Files (x86)\AspenTech\AeBRS\Templates\BatchArea.xml";
        public static string BatchRPLArea = Base_Directory.ProjectDir + "Data\\Input\\GML\\BatchRPLArea.xml";
        public static string BatchAPIArea = Base_Directory.ProjectDir + "Data\\Input\\GML\\BatchAPIArea.xml";
        public static string EquipmentArea = @"C:\Program Files (x86)\AspenTech\AeBRS\Templates\EquipmentArea.xml";

        //mMDM
        public static string MachineAliasConfig = Base_Directory.ProjectDir + "Data\\Input\\GML\\machine.alias.config";
        public static string mMDMWorkSpace => @"C:\ProgramData\AspenTech\mMDM\Workspaces";

        public static string GMLBackup = Base_Directory.ProjectDir + "Data\\Input\\GML\\mMDM UGM GML backup.xml";
        //GML config 
        public static string GMLConfig = Base_Directory.ProjectDir + "Data\\Input\\GML\\GMLConfig.ini";

        public static string BatchConfig = Base_Directory.ProjectDir + "Data\\Input\\GML\\BatchConfig.ini";
        //GML DOC
        public static string GMLDOCDir = Base_Directory.ProjectDir + "Data\\Input\\GML\\DOCs";

        //Tomcat

        public static string MobileWebconfig = "C:\\Program Files\\Common Files\\AspenTech Shared\\Tomcat9.0.27\\webapps\\ApemMobile\\WEB-INF\\web.xml";

    }
}
