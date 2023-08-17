using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using System.Threading;

namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class UFT_Editor
    {
        public IEditor _UFT_Editor
        {
            get;
            set;
        }
        public UFT_Editor(IEditor editfield)
        {
            _UFT_Editor = editfield;
        }

        public UFT_Editor(ITestObject parentObject, string xpath)
        {
            _UFT_Editor = UFT_Xpath.GetChildObject<IEditor>(parentObject, xpath);
        }

        public void Click()
        {
            
            _UFT_Editor.Click();
            Base_Assert.IsTrue(_UFT_Editor.IsFocused);
        }

        public string Text
        {
            get
            {
                return _UFT_Editor.Text;
            }
        }

        public string GetVisibleText()
        {
            return _UFT_Editor.GetVisibleText();

        }

        public string SetText(string text, bool TypeEnter = false)
        {
            if (TypeEnter == false)
            {
                _UFT_Editor.SetText(text);
                Base_Assert.AreEqual(text, _UFT_Editor.Text);
            }
            else
            {
                _UFT_Editor.SetText(text);
                Base_Assert.AreEqual(text, _UFT_Editor.Text);
                Keyboard.PressKey(Keyboard.Keys.Enter);
            }
            return text;
        }

        public string SetSecure(string text, bool TypeEnter = false)
        {
            if (TypeEnter == false)
            {
                _UFT_Editor.SetSecure(text);
                Base_Assert.AreEqual(text, _UFT_Editor.Text);
            }
            else
            {
                _UFT_Editor.SetSecure(text);
                Base_Assert.AreEqual(text, _UFT_Editor.Text);
                Keyboard.PressKey(Keyboard.Keys.Enter);
            }
            return text;
        }

        public void SendKeys(string text)
        {
            _UFT_Editor.SendKeys(text);
            
            Keyboard.PressKey(Keyboard.Keys.Enter);
        }

        public void TypeKeys(string Text)
        {
            _UFT_Editor.DoubleClick();
            Keyboard.SendString(Text);
            Thread.Sleep(500);
            Keyboard.PressKey(Keyboard.Keys.Enter);
            Thread.Sleep(1000);
        }

        public void Clear()
        {
            _UFT_Editor.SendKeys(Keys.Delete);
            Thread.Sleep(1000);
            Keyboard.PressKey(Keyboard.Keys.Enter);
            Thread.Sleep(1000);
        }

        public bool IsReadOnly
        {
            get
            {
                return _UFT_Editor.IsReadOnly;
            }
        }

        public string FontColor
        {
            get
            {
                return _UFT_Editor.GetObjectProperty<string>("ForegroundColor");
               //return _UFT_Editor.Property("Foreground").Value.ToString();
            }
        }
    }
}
