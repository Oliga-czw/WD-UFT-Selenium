using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using System.Threading;

namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class UFT_Menu
    {
        public IMenu _UFT_Menu
        {
            get;
            protected set;
        }
        public UFT_Menu(IMenu menu)
        {
            _UFT_Menu = menu;
        }
        public UFT_Menu(ITestObject parentObject, string xpath)
        {
            _UFT_Menu = UFT_Xpath.GetChildObject<IMenu>(parentObject, xpath);
        }
        
        public void Click(int waitingTime = 1000)
        {
            _UFT_Menu.WaitUntilEnabled();
            _UFT_Menu.Click();
            Thread.Sleep(waitingTime);
            _UFT_Menu.Click();
            Thread.Sleep(waitingTime);
        }
        public void Select(int waitingTime = 1000)
        {
            _UFT_Menu.WaitUntilEnabled();
            _UFT_Menu.Select();
            Thread.Sleep(waitingTime);
        }
        public void ClickSignle(int waitingTime = 1000)
        {
            _UFT_Menu.WaitUntilEnabled();
            _UFT_Menu.Click();
            Thread.Sleep(waitingTime);
        }
        public void DoubleClick(int waitingTime = 1000)
        {
            _UFT_Menu.WaitUntilEnabled();
            _UFT_Menu.DoubleClick();
            Thread.Sleep(waitingTime);
        }
        public bool IsExist(uint TimeoutSecond = 3)
        {
            bool isExist = false;
            for (int i = 0; i < TimeoutSecond && isExist == false; i++)
            {
                isExist = _UFT_Menu.Exists(1);
            }
            return isExist;
        }

        public bool IsEnabled
        {
            get
            { return _UFT_Menu.IsEnabled; }
        }
        public bool WaitUntilEnabled()
        {
            return _UFT_Menu.WaitUntilEnabled();
        }

        public bool WaitUntilEnabled(int timeout)
        {
            return _UFT_Menu.WaitUntilEnabled(timeout);
        }
        public IMenu Import => _UFT_Menu.Describe<IMenu>(new MenuDescription
        {
            Label = @"Import"
        });
        public IMenu Save => _UFT_Menu.Describe<IMenu>(new MenuDescription
        {
            Label = @"Save"
        });
        public IMenu Verify => _UFT_Menu.Describe<IMenu>(new MenuDescription
        {
            Label = @"Verify"
        });
        public IMenu Compile => _UFT_Menu.Describe<IMenu>(new MenuDescription
        {
            Label = @"Compile"
        });
    }
}