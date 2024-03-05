using MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary;
using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using System.Threading;

namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class UFT_Label
    {
        public ILabel _UFT_Label
        {
            get;
            set;
        }

        public UFT_Label(ITestObject parentObject, string xpath)
        {
            _UFT_Label = UFT_Xpath.GetChildObject<ILabel>(parentObject, xpath);
        }
        

        public string AttachedText
        {
            get
            {
                return _UFT_Label.AttachedText;
            }
            
        }

        public void DoubleClick()
        {
            _UFT_Label.DoubleClick();
        }



    }
}
