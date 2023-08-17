using HP.LFT.SDK;
using HP.LFT.SDK.StdWin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class STD_Window
    {
        public IWindow _STD_Window
        {
            get;
            protected set;
        }

        public STD_Window(ITestObject parentObject, string xpath)
        {
            _STD_Window = STD_Xpath.GetChildObject<IWindow>(parentObject, xpath);
            _STD_Window.WaitUntilVisible();
        }
        public STD_Window(string xpath)
        {
            _STD_Window = STD_Xpath.GetDesktopWindow<IWindow>(xpath);
            _STD_Window.WaitUntilVisible();
        }

        public STD_Window(IWindow window)
        {
            _STD_Window = window;
        }

        public void SetActive()
        {
            _STD_Window.Activate();
        }
        public void Maximize(uint timeoutSeconds = 30)
        {
            _STD_Window.Exists(timeoutSeconds);
            _STD_Window.WaitUntilEnabled<IWindow>();
            if (_STD_Window.WindowState != WindowState.Maximized)
            {
                _STD_Window.Maximize();
            }

            _STD_Window.Activate();
        }
        public void Close()
        {
            _STD_Window.Close();
        }

        public void WaitUntilVisible(int timeout = 30 * 1000)
        {
            _STD_Window.WaitUntilVisible(timeout);
        }
        public void Exists()
        {
            _STD_Window.Exists();
        }
        public bool IsExist(uint TimeoutSecond = 3)
        {
            bool isExist = false;
            for (int i = 0; i < TimeoutSecond && isExist == false; i++)
            {
                isExist = _STD_Window.Exists(1);
            }
            return isExist;
        }
        public void IsEnabled()
        {
            Assert.IsTrue(_STD_Window.IsEnabled);
        }
        protected TChild Describe<TChild>(IDescription description) where TChild : class, ITestObject
        {
            return _STD_Window.Describe<TChild>(description);
        }
        //public List<T> FindChildren<T>() where T : class, ITestObject
        //{
        //    return _STD_Window.FindChildren<T>();
        //}

        public void GetSnapshot(string path)
        {
            Image image = _STD_Window.GetSnapshot();
            image.Save(path, ImageFormat.Png);
        }
    }

}
