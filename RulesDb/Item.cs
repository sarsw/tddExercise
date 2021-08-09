namespace RulesDb
{

    public class Item
    {
        public enum ThingType { Upgrade, Membership, Physical, Book };

        public string itemDescription;
        public ThingType itemType;
    }
}
