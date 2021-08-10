using System.Collections.Generic;

namespace RulesDb
{
    public class Order
    {
        public Dictionary<int, Item> Basket = new Dictionary<int, Item>();
        public string CustomerEmail;
        public int OrderNumber;
    }
}