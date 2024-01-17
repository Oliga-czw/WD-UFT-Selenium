using System;

namespace MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary
{
    public abstract class TestCase
    {
        protected string _CaseID;
        public virtual string CaseID => this._CaseID;

        protected string _Description;
        public virtual string Description => this._Description;

    }
    public class VSTS : TestCase
    {
        public VSTS(string value)
        {

            _CaseID = value.Split('_')[1];
        }
    }
    public class Description : TestCase
    {
        public Description(string value)
        {
            _Description = value;
        }
    }
    public static class TestCaseManage
    {
        public static TestCase GetCase(string value)
        {

            if (value == null)
            {

                throw new ArgumentNullException();
            }
            else
            {
                return new VSTS(value);
            }

        }

        public static TestCase GetDescription(string value)
        {
            if (value == null)
            {
                throw new ArgumentException();
            }
            else
            {
                return new Description(value);
            }
        }
    }
    public abstract class Timeout
    {
        public const int TimeoutLong = 300000;
        public const int TimeoutMiddle = 90000;
        public const int TimeoutShort = 15000;
    }
    public class Tolerance
    {
        public const double AbsoluteTolerance = 0.001;
        public const double RelativeTolerance = 0.05;
        public const double EATolerance = 0.03;
        public const double EETolerance = 0.08;
    }

}
