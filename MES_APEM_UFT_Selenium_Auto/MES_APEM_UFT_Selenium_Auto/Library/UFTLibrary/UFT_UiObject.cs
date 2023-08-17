using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using System.Threading;

namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class UFT_UiObject
    {
        public IUiObject _UFT_UiObject
        {
            get;
            set;
        }
        public UFT_UiObject(IUiObject editfield)
        {
            _UFT_UiObject = editfield;
        }

        public UFT_UiObject(ITestObject parentObject, string xpath)
        {
            _UFT_UiObject = UFT_Xpath.GetChildObject<IUiObject>(parentObject, xpath);
        }

        public void Click()
        {
            
            _UFT_UiObject.Click();
            Base_Assert.IsTrue(_UFT_UiObject.IsFocused);
        }

        public string Text
        {
            get
            {
                return _UFT_UiObject.Text;
            }
        }

        public string GetVisibleText()
        {
            return _UFT_UiObject.GetVisibleText();

        }

    }
}
