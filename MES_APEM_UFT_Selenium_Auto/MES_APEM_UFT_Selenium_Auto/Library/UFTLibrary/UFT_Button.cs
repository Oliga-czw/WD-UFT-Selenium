using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using System.Threading;

namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class UFT_Button
    {
        public HP.LFT.SDK.Java.IButton _UFT_Button
        {
            get;
            protected set;
        }
        public UFT_Button(HP.LFT.SDK.Java.IButton button)
        {
            _UFT_Button = button;
        }
        public UFT_Button(ITestObject parentObject, string xpath)
        {
            _UFT_Button = UFT_Xpath.GetChildObject<HP.LFT.SDK.Java.IButton>(parentObject, xpath);
        }
        public void Click(int waitingTime = 1000)
        {
            _UFT_Button.WaitUntilEnabled();
            if (!_UFT_Button.IsFocused)
            {
                _UFT_Button.Click();
                Thread.Sleep(waitingTime);
            }
            if (_UFT_Button.Exists() && _UFT_Button.IsEnabled)
            {
                _UFT_Button.Click();
            }
            
            Thread.Sleep(waitingTime);
        }

        public void ClickSignle(int waitingTime = 1000)
        {
            _UFT_Button.WaitUntilEnabled();
            _UFT_Button.Click();
            Thread.Sleep(waitingTime);
        }
        public void DoubleClick(int waitingTime = 1000)
        {
            _UFT_Button.WaitUntilEnabled();
            _UFT_Button.DoubleClick();
            Thread.Sleep(waitingTime);
        }
        public bool IsExist(uint TimeoutSecond = 3)
        {
            bool isExist = false;
            for (int i = 0; i < TimeoutSecond && isExist == false; i++)
            {
                isExist = _UFT_Button.Exists(1);
            }
            return isExist;
        }

        public bool IsEnabled
        {
            get
            { return _UFT_Button.IsEnabled; }
        }
        public bool WaitUntilEnabled()
        {
            return _UFT_Button.WaitUntilEnabled();
        }

        public bool WaitUntilEnabled(int timeout)
        {
            return _UFT_Button.WaitUntilEnabled(timeout);
        }
    }
}
