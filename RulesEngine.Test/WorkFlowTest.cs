using NUnit.Framework;
using System;

namespace RulesEngine.Test
{
    class WorkFlowTest
    {
        [TestCase("", -1)]
        [TestCase(@"C:\Not\A\Folder", -1)]
        public void GetLogic_BadPath_ReturnsMinusOne(string rulesPath, int result)
        {
            WorkFlow wf = new WorkFlow();

            Assert.AreEqual(result, wf.GetLogic(rulesPath));
        }

        [TestCase(@"C:\", 0)]
        public void GetLogic_NoLogic_ReturnsZero(string rulesPath, int result)
        {
            WorkFlow wf = new WorkFlow();

            Assert.AreEqual(result, wf.GetLogic(rulesPath));
        }

        [TestCase(@"C:\Scripts", 1)]
        public void GetLogic_GoodPath_ReturnsOneOrMore(string rulesPath, int result)
        {
            WorkFlow wf = new WorkFlow();
            Assert.IsTrue(wf.GetLogic(rulesPath) >= result);
        }
    }
}
