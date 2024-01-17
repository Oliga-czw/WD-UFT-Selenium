using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MES_APEM_UFT_Selenium_Auto.Library.BaseLibrary
{
    public static class Base_Assert
    {
        public static void IsTrue(bool condition, string message = null)
        {
            if (condition == true)
            {
                Base_logger.Info($"Assert True condition {condition} Passed ---" + message);

            }
            else

            {
                Base_logger.Error($"Assert True condition {condition} Failed  ---" + message);
            }
            Assert.IsTrue(condition, message);
        }
        public static void IsFalse(bool condition, string message = null)
        {
            if (condition == false)
            {
                Base_logger.Info($"Assert False condition {condition} Passed ---" + message);
            }
            else
            {
                Base_logger.Error($"Assert False condition {condition} Failed  ---" + message);
            }

            Assert.IsFalse(condition, message);
        }
        public static void AreNotEqual(object expected, object actual, string message = null)
        {
            if (expected.Equals(actual) == false)
            {
                Base_logger.Info("Assert No Equal condition Passed ---" + $"Expected value is: {expected}, Actual value is  {actual} ---" + message);
            }
            else
            {
                Base_logger.Error("Assert No Equal condition Failed ---" + $"Expected and Actual values are: {actual} ---" + message);
            }

            Assert.AreNotEqual(expected, actual, message);
        }
        public static void AreEqual(object expected, object actual, string message = null)
        {
            if (expected.Equals(actual) == true)
            {
                Base_logger.Info("Assert Equal condition Passed ---" + $"Expected and Actual values are: {actual} ---" + message);
            }
            else
            {
                Base_logger.Error("Assert No Equal condition Failed ---" + $"Expected value is: {expected}, Actual value is  {actual} ---" + message);
            }

            Assert.AreEqual(expected, actual, message);
        }
        public static void Fail(string failMessage)
        {
            Base_logger.Error(failMessage);
            Assert.Fail(failMessage);
        }
    }


}
