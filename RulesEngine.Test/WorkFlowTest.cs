using NUnit.Framework;

namespace RulesEngine.Test
{
    class WorkFlowTest
    {
        [TestCase("", false)]
        [TestCase(@"C:\", false)]
        public void Load_BadPath_ReturnsFalse(string rulesPath, bool result)
        {
            Assert.AreEqual(result, WorkFlow.Load(rulesPath));
        }
    }
}
