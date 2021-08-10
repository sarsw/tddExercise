using NUnit.Framework;
using RulesDb;
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

        [TestCase("book title", "book", "physical", 1)]
        [TestCase("membership application", "membership", "virtual", 2)]
        public void AddItem_Db_ReturnsOne(string description, string thingType, string subType, int orderNumber)
        {
            Db db = new Db();

            Order order = new() { OrderNumber = orderNumber };
            
            Item it = new() {  ItemDescription = description, ItemType = thingType, SubType = subType, Qty = 1};

            order.Basket[1]=it;

            db.Things[orderNumber] = order;

            Assert.IsTrue(db.Things[orderNumber].Basket[1].ItemDescription == description && db.Things[orderNumber].Basket[1].ItemType == thingType);
        }

        [TestCase("this is a test", "testItem", "testSub", 1, "testSub testItem")]
        [TestCase("book title", "book", "physical", 1, "Shipping Royalty Dept Commission Payment")]
        [TestCase("Learning to Ski", "video", "physical", 1, "Shipping Commission Payment First Aid")]
        [TestCase("membership application", "membership", "virtual", 2, "activate")]
        [TestCase("membership upgrade", "upgrade", "virtual", 2, "upgrade s@s.com")]
        public void CheckScript_ForType_ReturnsTrue(string description, string thingType, string subType, int orderNumber, string expected)
        {

            Order order = new() { OrderNumber = orderNumber, CustomerEmail = "s@s.com" };

            Item it = new() { ItemDescription = description, ItemType = thingType, SubType = subType, Qty = 1 };

            order.Basket[1] = it;

            WorkFlow wf = new WorkFlow();       // create the workflow engine
            wf.GetLogic("C:\\Scripts");

             Assert.IsTrue(expected == wf.Process(order));
        }

    }
}
