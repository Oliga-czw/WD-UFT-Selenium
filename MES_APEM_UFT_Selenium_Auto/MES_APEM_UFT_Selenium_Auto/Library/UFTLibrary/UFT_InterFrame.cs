using HP.LFT.SDK;
using HP.LFT.SDK.Java;
using System.Threading;

namespace MES_APEM_UFT_Selenium_Auto.Library.UFTLibrary
{
    public class UFT_InterFrame
    {
        public IInternalFrame _UFT_InterFrame
        {
            get;
            protected set;
        }
        public UFT_InterFrame(IInternalFrame interframe)
        {
            _UFT_InterFrame = interframe;
        }
        public UFT_InterFrame(ITestObject parentObject, string xpath)
        {
            _UFT_InterFrame = UFT_Xpath.GetChildObject<IInternalFrame>(parentObject, xpath);
        }
        public void Click(int waitingTime = 1000)
        {
            _UFT_InterFrame.WaitUntilEnabled<IInternalFrame>();
            _UFT_InterFrame.Click();
            Thread.Sleep(waitingTime);
        }
        public bool IsExist(uint TimeoutSecond = 3)
        {
            bool isExist = false;
            for (int i = 0; i < TimeoutSecond && isExist == false; i++)
            {
                isExist = _UFT_InterFrame.Exists(1);
            }
            return isExist;
        }

        public bool IsEnabled
        {
            get
            { return _UFT_InterFrame.IsEnabled; }
        }
        public bool WaitUntilEnabled()
        {
            return _UFT_InterFrame.WaitUntilEnabled();
        }

        public bool WaitUntilEnabled(int timeout)
        {
            return _UFT_InterFrame.WaitUntilEnabled(timeout);
        }


       
        #region sub controller in mainclass
 
        //protected TChild Describe<TChild>(IDescription description) where TChild : class, ITestObject
        //{
        //    return _UFT_InterFrame.Describe<TChild>(description);
        //}

        //public IEditor userNameEditor => Describe<IEditor>(new EditorDescription
        //{
        //    AttachedText = @"User name:"
        //});

        //public IEditor passwordEditor => Describe<IEditor>(new EditorDescription
        //{
        //    AttachedText = @"Password:"
        //});

        //public HP.LFT.SDK.Java.IButton loginbutton => Describe<HP.LFT.SDK.Java.IButton>(new ButtonDescription
        //{
        //    Label = @"<html><center>L<u>o</u>g on</center><html>"
        //});
        #endregion
    }



}
