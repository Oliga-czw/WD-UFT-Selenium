using HP.LFT.SDK;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using WD_UFT_Selenium_Auto.Product.WD;


namespace WD_UFT_Selenium_Auto.Library.BaseLibrary
{

    public static class Base_Test
    {
        public static void LaunchApp(string AppPath)
        {
            Process process = new Process();
            process.StartInfo.FileName = AppPath;
            //process.StartInfo.FileName = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Aspen Manufacturing Execution\Aspen Weigh and Dispense Execution.lnk";
            //can't start .cmd file
            //process.StartInfo.FileName = @"C:\Program Files\AspenTech\AeBRS\WD.cmd";
            process.Start();
            Thread.Sleep(10000);
            //process.WaitForInputIdle();
            //int i = 0;
            //while (process.MainWindowHandle == IntPtr.Zero)
            //{
            //    Thread.Sleep(100);
            //    process.Refresh();
            //    i++;
            //    if (i > 3000)
            //    {
            //        Base_logger.Error($"Time Out! Application {process.ProcessName} GUI cannot be Launched over 5 minutes");
            //        break;
            //    }
            //}
        }
        public static void KillProcess(string processName, bool partName = false)
        {
            string killProcessName = null;
            if (partName)
            {
                killProcessName = GetFullProcessName(processName);
            }
            else
            {
                killProcessName = processName;
            }

            Process[] processes = Process.GetProcessesByName(killProcessName);
            foreach (Process process in processes)
            {
                if (!process.HasExited)
                {
                    process.Kill();
                    int i = 0;
                    while (!process.HasExited)
                    {
                        Thread.Sleep(100);
                        process.Refresh();
                        i++;
                        if (i > 30)
                        {
                            throw new Exception("Time Out! Process :" + process.ProcessName + "Cannot be killed in 30 seconds");
                        }
                    }

                }
            }
        }
        public static void CleanWorkFolder(string workFolder, bool recursive = true)
        {
            if (Directory.Exists(workFolder))
            {
                string[] files = Directory.GetFileSystemEntries(workFolder);

                foreach (string file in files)
                {
                    if (Directory.Exists(file))
                    {
                        if (recursive)
                        {
                            CleanWorkFolder(file);
                            try
                            {
                                Directory.Delete(file);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(file + "was not deleted");
                                Console.WriteLine("Message:{0}", e.Message);
                                Console.WriteLine("Source:{0}", e.Source);
                                Console.WriteLine("Stack:{0}", e.StackTrace);
                            }
                        }
                    }
                    else if (File.Exists(file))
                    {
                        try
                        {
                            FileInfo fileInfo = new FileInfo(file);
                            fileInfo.Attributes = FileAttributes.Normal;

                            fileInfo.Delete();
                        }
                        catch
                        {
                            Console.WriteLine(file + " was not deleted!");
                        }
                    }
                }


            }
        }
        private static string GetFullProcessName(string processNamePart)
        {
            string fullProcessName = null;
            Process[] processesAll = Process.GetProcesses();

            bool existing = false;
            foreach (Process temp in processesAll)
            {
                existing = temp.ProcessName.Contains(processNamePart);
                if (existing == true)
                {
                    fullProcessName = temp.ProcessName;
                    break;
                }
            }
            return fullProcessName;
        }
        public static bool ProcessExists(string processNamePart)
        {
            string processResult = GetFullProcessName(processNamePart);

            bool existing = (processResult != null);

            return existing;
        }

        public static void Login(string username,string password)
        {
            WD.mainWindow.LogonInternalFrame.userNameEditor.SetText(username);
            WD.mainWindow.LogonInternalFrame.passwordEditor.SetSecure(password);
            WD.mainWindow.LogonInternalFrame.loginbutton.Click();
        }
    }
    public static class Application
    {
        private static string application = "javaw";
        public static void LaunchWDAndLogin()
        {
            Base_Test.LaunchApp(Base_Directory.WDDir);
            //SdkConfiguration config = new SdkConfiguration();
            //SDK.Init(config);
            Thread.Sleep(5000);
            Base_Test.Login(UserName.qaone1, PassWord.qaone1);
        }
        public static void LaunchAFW()
        {
            Base_Test.LaunchApp(Base_Directory.AFWDir);
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            Thread.Sleep(2000);
            WD.AFWloginDialog.OK.Click();
            WD.AFWSecuredDialog.OK.Click();

        }
        public static void LaunchMocAndLogin()
        {
            Base_Test.LaunchApp(Base_Directory.MOCDir);
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            MOC.MocmainWindow.LogonInternalFrame.userNameEditor.SetText(UserName.qaone1);
            MOC.MocmainWindow.LogonInternalFrame.passwordEditor.SetSecure(PassWord.qaone1);
            MOC.MocmainWindow.LogonInternalFrame.loginbutton.ClickSignle();
        }

        public static void LaunchBatchDetailDisplay()
        {
            Base_Test.LaunchApp(Base_Directory.BDDDir);
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            Thread.Sleep(5000);
         }


        public static void LaunchSLM()
        {
            Base_Test.LaunchApp(Base_Directory.SLMDir);
            SdkConfiguration config = new SdkConfiguration();
            SDK.Init(config);
            Thread.Sleep(5000);
        }

        public static void KillWD()
        {
            Base_Test.KillProcess(application);
        }

    }


}
