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

        [TestCase("book title", Item.ThingType.Book, 1)]
        [TestCase("membership", Item.ThingType.Membership, 2)]
        public void AddItem_Db_ReturnsOne(string description, Item.ThingType thingType, int orderNumber)
        {
            Db db = new Db();

            Item it = new Item() { itemType = thingType, itemDescription = description };
            db.Things.Add(orderNumber, it);

            Assert.IsTrue(db.Things[orderNumber].itemDescription == description && db.Things[orderNumber].itemType == thingType);
        }

        [TestCase("book title", Item.ThingType.Book, 1)]
        [TestCase("membership", Item.ThingType.Membership, 2)]
        public void CheckScript_ForType_ReturnsOne(string description, Item.ThingType thingType, int orderNumber)
        {
            Db db = new Db();

            Item it = new Item() { itemType = thingType, itemDescription = description };
            db.Things.Add(orderNumber, it);

            WorkFlow wf = new WorkFlow();       // create the workflow engine
            wf.GetLogic("C:\\Scripts");

            foreach (var item in db.Things)     // there'll be only 1 item
            {
                Assert.IsTrue(1 == wf.Process(item.Value));
            }
        }

    }
}
