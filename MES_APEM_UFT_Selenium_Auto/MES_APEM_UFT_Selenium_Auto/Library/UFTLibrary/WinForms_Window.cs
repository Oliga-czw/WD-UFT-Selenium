using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using HP.LFT.SDK;
using HP.LFT.SDK.WinForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class WinForms_Window
    {
        public IWindow _WinForms_Window
        {
            get;
            protected set;
        }


        //public WinForms_Window()
        //{
        //    _WinForms_Window = Desktop.Describe<IWindow>(new WindowDescription
        //    {
        //        ObjectName = @"aspenONE SLM License Manager"
        //    });
        //    _WinForms_Window.WaitUntilVisible();
        //}
        //public WPF_Window(ITestObject parentObject, string xpath)
        //{
        //    _WinForms_Window = UFT_Xpath.GetChildObject<IWindow>(parentObject, xpath);
        //    _WinForms_Window.WaitUntilVisible();
        //}
        //public WPF_Window(string xpath)
        //{
        //    _WinForms_Window = UFT_Xpath.GetDesktopWindow<IWindow>(xpath);
        //    _WinForms_Window.WaitUntilVisible();
        //}

        public WinForms_Window(IWindow window)
        {
            _WinForms_Window = window;
        }
        public void SetActive()
        {
            _WinForms_Window.Activate();
        }
        public void Maximize(uint timeoutSeconds = 30)
        {
            _WinForms_Window.Exists(timeoutSeconds);
            _WinForms_Window.WaitUntilEnabled<IWindow>();
            if (_WinForms_Window.WindowState != WindowState.Maximized)
            {
                _WinForms_Window.Maximize();
            }

            _WinForms_Window.Activate();
        }
        public void Close()
        {
            _WinForms_Window.Close();
        }

        public void WaitUntilVisible(int timeout = 30 * 1000)
        {
            _WinForms_Window.WaitUntilVisible(timeout);
        }
        public void Exists()
        {
            _WinForms_Window.Exists();
        }
        public bool IsExist(uint TimeoutSecond = 3)
        {
            bool isExist = false;
            for (int i = 0; i < TimeoutSecond && isExist == false; i++)
            {
                isExist = _WinForms_Window.Exists(1);
            }
            return isExist;
        }
        public bool IsEnabled()
        {
            return _WinForms_Window.IsEnabled;
        }
        protected TChild Describe<TChild>(IDescription description) where TChild : class, ITestObject
        {
            return _WinForms_Window.Describe<TChild>(description);
        }
        //public List<T> FindChildren<T>() where T : class, ITestObject
        //{
        //    return _WinForms_Window.FindChildren<T>();
        //}

        public void GetSnapshot(string path)
        {
            Image image =  _WinForms_Window.GetSnapshot();
            image.Save(path, ImageFormat.Png);
        }

        public IButton Next => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            ObjectName = @"btnNext"
        });

        public IButton OK => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            Text = @"OK"
        });

        public IButton Finish => _WinForms_Window.Describe<IButton>(new ButtonDescription
        {
            Text = @"&Finish"
        });
    }
}
