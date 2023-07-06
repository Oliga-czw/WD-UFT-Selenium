using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using HP.LFT.SDK;
using HP.LFT.SDK.WPF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WD_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class WPF_Window
    {
        public IWindow _WPF_Window
        {
            get;
            protected set;
        }


        public WPF_Window()
        {
            _WPF_Window = Desktop.Describe<IWindow>(new WindowDescription
            {
                ObjectName = @"aspenONE SLM License Manager"
            });
            _WPF_Window.WaitUntilVisible();
        }
        //public WPF_Window(ITestObject parentObject, string xpath)
        //{
        //    _WPF_Window = UFT_Xpath.GetChildObject<IWindow>(parentObject, xpath);
        //    _WPF_Window.WaitUntilVisible();
        //}
        //public WPF_Window(string xpath)
        //{
        //    _WPF_Window = UFT_Xpath.GetDesktopWindow<IWindow>(xpath);
        //    _WPF_Window.WaitUntilVisible();
        //}

        public WPF_Window(IWindow window)
        {
            _WPF_Window = window;
        }
        public void SetActive()
        {
            _WPF_Window.Activate();
        }
        public void Maximize(uint timeoutSeconds = 30)
        {
            _WPF_Window.Exists(timeoutSeconds);
            _WPF_Window.WaitUntilEnabled<IWindow>();
            if (_WPF_Window.WindowState != WindowState.Maximized)
            {
                _WPF_Window.Maximize();
            }

            _WPF_Window.Activate();
        }
        public void Close()
        {
            _WPF_Window.Close();
        }

        public void WaitUntilVisible(int timeout = 30 * 1000)
        {
            _WPF_Window.WaitUntilVisible(timeout);
        }
        public void Exists()
        {
            _WPF_Window.Exists();
        }
        public bool IsExist(uint TimeoutSecond = 3)
        {
            bool isExist = false;
            for (int i = 0; i < TimeoutSecond && isExist == false; i++)
            {
                isExist = _WPF_Window.Exists(1);
            }
            return isExist;
        }
        public void IsEnabled()
        {
            Assert.IsTrue(_WPF_Window.IsEnabled);
        }
        protected TChild Describe<TChild>(IDescription description) where TChild : class, ITestObject
        {
            return _WPF_Window.Describe<TChild>(description);
        }
        //public List<T> FindChildren<T>() where T : class, ITestObject
        //{
        //    return _WPF_Window.FindChildren<T>();
        //}

        public void GetSnapshot(string path)
        {
            Image image =  _WPF_Window.GetSnapshot();
            image.Save(path, ImageFormat.Png);
        }
    }
}
