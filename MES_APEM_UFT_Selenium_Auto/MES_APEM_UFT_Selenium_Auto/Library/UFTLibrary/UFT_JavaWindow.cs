using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class UFT_JavaWindow
    {
        public IWindow _UFT_Window
        {
            get;
            protected set;
        }


        public UFT_JavaWindow()
        {
            _UFT_Window = Desktop.Describe<IWindow>(new WindowDescription
            {
                Path = @"WDWorkstation;",
                Title = @"Aspen Weigh and Dispense Execution"
            });
            _UFT_Window.WaitUntilVisible();
        }
        public UFT_JavaWindow(ITestObject parentObject, string xpath)
        {
            _UFT_Window = UFT_Xpath.GetChildObject<IWindow>(parentObject, xpath);
            _UFT_Window.WaitUntilVisible();
        }
        public UFT_JavaWindow(string xpath)
        {
            _UFT_Window = UFT_Xpath.GetDesktopWindow<IWindow>(xpath);
            _UFT_Window.WaitUntilVisible();
        }

        public UFT_JavaWindow(IWindow window)
        {
            _UFT_Window = window;
        }
        public void SetActive()
        {
            _UFT_Window.Activate();
        }
        public void Maximize(uint timeoutSeconds = 30)
        {
            _UFT_Window.Exists(timeoutSeconds);
            _UFT_Window.WaitUntilEnabled<IWindow>();
            if (_UFT_Window.WindowState != WindowState.Maximized)
            {
                _UFT_Window.Maximize();
            }

            _UFT_Window.Activate();
        }
        public void Close()
        {
            _UFT_Window.Close();
        }

        public void WaitUntilVisible(int timeout = 30 * 1000)
        {
            _UFT_Window.WaitUntilVisible(timeout);
        }
        public void Exists()
        {
            _UFT_Window.Exists();
        }
        public bool IsExist(uint TimeoutSecond = 3)
        {
            bool isExist = false;
            for (int i = 0; i < TimeoutSecond && isExist == false; i++)
            {
                isExist = _UFT_Window.Exists(1);
            }
            return isExist;
        }
        public void IsEnabled()
        {
            Assert.IsTrue(_UFT_Window.IsEnabled);
        }
        protected TChild Describe<TChild>(IDescription description) where TChild : class, ITestObject
        {
            return _UFT_Window.Describe<TChild>(description);
        }
        //public List<T> FindChildren<T>() where T : class, ITestObject
        //{
        //    return _UFT_Window.FindChildren<T>();
        //}

        public void GetSnapshot(string path)
        {
            Image image =  _UFT_Window.GetSnapshot();
            image.Save(path, ImageFormat.Png);
        }
    }
}
