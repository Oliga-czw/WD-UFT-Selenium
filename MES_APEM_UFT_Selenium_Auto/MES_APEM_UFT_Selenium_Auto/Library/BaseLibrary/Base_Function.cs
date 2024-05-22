﻿using HP.LFT.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary
{
    public class Base_Function
    {
        public static void ResartServices(string serviceName)
        {
            //service
            //string serviceName = "Tomcat9";
            int timeoutMilliseconds = 200000;
            ServiceController service = new ServiceController(serviceName);
            int millisec1 = Environment.TickCount; ;
            TimeSpan timeout;
            if (service.Status == ServiceControllerStatus.Running)
            {
                
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            // count the rest of the timeout
            int millisec2 = Environment.TickCount;
            timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));
            service.Start();
            service.WaitForStatus(ServiceControllerStatus.Running, timeout);

        }

        public static void AddConfigKey(string path, string Key)
        {
            //add key
            FileStream fs = new FileStream(path, FileMode.Append);//
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(Key);//"NET_REMOVAL_REQUIRE_TARGET_TARE = 0"
            sw.Close();
            Base_logger.Info("Add config key successfully.");

        }
        public static void AddConfigKeyInRightPlace(string filePath, string insertText, string searchText)
        {
            string fileContent = File.ReadAllText(filePath);
            int index = fileContent.IndexOf(searchText);
            if (index != -1)
            {
                fileContent = fileContent.Insert(index + searchText.Length, insertText);
                File.WriteAllText(filePath, fileContent);
                Base_logger.Info($"{insertText} has been inserted after {searchText}.");
            }

        }
      

        public static void EditConfigKey(string path, string Key)
        {
            //CDM_RESOURCE_SERVICE_ENABLE = 1
            string KeyName = Key.Split('=')[0];
            string KeyValue = Key.Split('=')[1];
            //delete key
            string all = File.ReadAllText(path);
            string pattern = $"{KeyName}= " + @".+";
            string replacedContent = Regex.Replace(all, pattern, Key);//@"CDM_RESOURCE_SERVICE_ENABLE = .+"
            File.WriteAllText(path, replacedContent);
            Base_logger.Info("Delete config key successfully.");

        }
        public static void MouseClick(Point point)
        {
            //int WaitPoint_x = point.X - 70;
            //int WaitPoint_y = point.Y - 70;
            Point WaitPoint = new Point(200, 400);
            Mouse.Move(WaitPoint);
            Thread.Sleep(5000);
            Mouse.Click(point);

        }

        public static void DeleteConfigKey(string path, string Key)
        {
            //delete key
            string all = File.ReadAllText(path);
            string pattern = Key;
            string replacedContent = all.Replace(pattern, "");
            File.WriteAllText(path, replacedContent);
            Base_logger.Info("Delete config key successfully.");

        }
        public static void DesktopSnipping(string path)
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            // 创建一个Graphics对象，使用bitmap对象作为画布  
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // 将屏幕的内容复制到bitmap中  
                g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
            }
            // 保存bitmap到文件  
            bitmap.Save(path);
        }

        public static void InstallMsi(string msiName)
        {
            string msiPath = Base_Directory.InputDir + "\\Msi\\" + msiName; 
            Process process = new Process();
            process.StartInfo.FileName = "msiexec.exe";
            process.StartInfo.Arguments = $"/i {msiPath} /qn";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();

        }

        public static void ReplaceTextInNewFile(string oldPath,string newPath,string oldText,string newText)
        {

            // 读取文件内容  
            string fileContent = File.ReadAllText(oldPath);
            // 替换旧文本为新文本  
            string replacedContent = fileContent.Replace(oldText, newText);
            // 将替换后的内容写回文件  
            File.WriteAllText(newPath, replacedContent);
        }
        public static string RandomString(int index)
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, index);
        }

        public static void AddHost()
        {
            string hostName = Dns.GetHostName();
            // get IP list  
            IPHostEntry ipHost = Dns.GetHostEntry(hostName);
            string ip4 = "";
            foreach (IPAddress ip in ipHost.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip4 = ip.ToString();
                }
            }
            Base_Function.AddConfigKey(Base_Directory.Host, ip4 + " " + hostName + ".qae.aspentech.com");
        }
        public static void CaptureScreenToFile(string filename, ImageFormat format)
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                bitmap.Save(filename, format);
            }
        }

    }

    public static class Keyboards
    {

        public class ClassKeyboard

        {

            /// <summary>

            /// Keyboard Event

            /// </summary>

            /// <param name="bVk"></param> Virtual Keyboard Value

            /// <param name="bScan"></param> Normally, it is 0.

            /// <param name="dwFlags"></param> Here is Int Value. 0 value means push down. 2 value is release up

            /// <param name="dwExtraInfo"></param> Here is Int value. Normally, it is 0.

            [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "keybd_event")]

            public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

            public byte keyByte

            {

                private set;

                get;

            }

            //public string keyName

            //{

            //    private set;

            //    get;

            //}

            public ClassKeyboard(byte keyByte)

            {

                this.keyByte = keyByte;

                //this.keyName = keyName;

                //Console.WriteLine("keyName: " + keyName);

            }

            public void Click()

            {



                keybd_event(this.keyByte, 0, 0, 0);

                keybd_event(this.keyByte, 0, 2, 0);




                Thread.Sleep(1000);

            }

            public void Click(int times)

            {

                for (int clickTime = 1; clickTime <= times; clickTime++)

                {

                    keybd_event(this.keyByte, 0, 0, 0);

                    keybd_event(this.keyByte, 0, 2, 0);

                    Thread.Sleep(100);

                }

            }

            public void Press()

            {



                keybd_event(this.keyByte, 0, 0, 0);





                Thread.Sleep(1000);

            }

            public void Release()

            {



                keybd_event(this.keyByte, 0, 2, 0);



                Thread.Sleep(1000);

            }

        }

        public static void Click(ClassKeyboard key1, ClassKeyboard key2)

        {


            ClassKeyboard.keybd_event(key1.keyByte, 0, 0, 0);

            ClassKeyboard.keybd_event(key2.keyByte, 0, 0, 0);

            ClassKeyboard.keybd_event(key2.keyByte, 0, 2, 0);

            ClassKeyboard.keybd_event(key1.keyByte, 0, 2, 0);



            Thread.Sleep(1000);

        }

        public static void Click(ClassKeyboard key1, ClassKeyboard key2, ClassKeyboard key3)

        {



            ClassKeyboard.keybd_event(key1.keyByte, 0, 0, 0);

            ClassKeyboard.keybd_event(key2.keyByte, 0, 0, 0);

            ClassKeyboard.keybd_event(key3.keyByte, 0, 0, 0);

            ClassKeyboard.keybd_event(key3.keyByte, 0, 2, 0);

            ClassKeyboard.keybd_event(key2.keyByte, 0, 2, 0);

            ClassKeyboard.keybd_event(key1.keyByte, 0, 2, 0);



            Thread.Sleep(1000);

        }

        public readonly static ClassKeyboard Home = new ClassKeyboard((byte)System.Windows.Forms.Keys.Home);

        public readonly static ClassKeyboard ControlKey = new ClassKeyboard((byte)System.Windows.Forms.Keys.ControlKey);

        public readonly static ClassKeyboard LControlKey = new ClassKeyboard((byte)System.Windows.Forms.Keys.LControlKey);

        public readonly static ClassKeyboard RControlKey = new ClassKeyboard((byte)System.Windows.Forms.Keys.RControlKey);


        public readonly static ClassKeyboard Control = new ClassKeyboard(unchecked((byte)System.Windows.Forms.Keys.Control));
        public readonly static ClassKeyboard Alt = new ClassKeyboard(unchecked((byte)System.Windows.Forms.Keys.Alt));
        public readonly static ClassKeyboard Shift = new ClassKeyboard((byte)System.Windows.Forms.Keys.ShiftKey);

        public readonly static ClassKeyboard Esc = new ClassKeyboard((byte)System.Windows.Forms.Keys.Escape);

        public readonly static ClassKeyboard Enter = new ClassKeyboard((byte)System.Windows.Forms.Keys.Enter);

        public readonly static ClassKeyboard Backspace = new ClassKeyboard((byte)System.Windows.Forms.Keys.Back);

        public readonly static ClassKeyboard Space = new ClassKeyboard((byte)System.Windows.Forms.Keys.Space);

        public readonly static ClassKeyboard Tab = new ClassKeyboard((byte)System.Windows.Forms.Keys.Tab);

        public readonly static ClassKeyboard F11 = new ClassKeyboard((byte)System.Windows.Forms.Keys.F11);

        public readonly static ClassKeyboard Right = new ClassKeyboard((byte)System.Windows.Forms.Keys.Right);

        public readonly static ClassKeyboard Left = new ClassKeyboard((byte)System.Windows.Forms.Keys.Left);

        public readonly static ClassKeyboard Up = new ClassKeyboard((byte)System.Windows.Forms.Keys.Up);

        public readonly static ClassKeyboard Down = new ClassKeyboard((byte)System.Windows.Forms.Keys.Down);

        public readonly static ClassKeyboard PageDown = new ClassKeyboard((byte)System.Windows.Forms.Keys.PageDown);

        public readonly static ClassKeyboard C = new ClassKeyboard((byte)System.Windows.Forms.Keys.C);

        public readonly static ClassKeyboard H = new ClassKeyboard((byte)System.Windows.Forms.Keys.H);

        public readonly static ClassKeyboard I = new ClassKeyboard((byte)System.Windows.Forms.Keys.I);

        public readonly static ClassKeyboard P = new ClassKeyboard((byte)System.Windows.Forms.Keys.P);

        public readonly static ClassKeyboard V = new ClassKeyboard((byte)System.Windows.Forms.Keys.V);

        public readonly static ClassKeyboard X = new ClassKeyboard((byte)System.Windows.Forms.Keys.X);

        public readonly static ClassKeyboard A = new ClassKeyboard((byte)System.Windows.Forms.Keys.A);
        public readonly static ClassKeyboard S = new ClassKeyboard((byte)System.Windows.Forms.Keys.S);
    }

}

