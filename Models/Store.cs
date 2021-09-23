using System.Collections.Generic;

namespace Models
{
    public class Store
    {
        // public Store() {}

        public string Location {get; set;}

        public List<Order> Orders {get; set;}
        public List<Inventory> Inventories {get; set;}
        public int Id {get; set;}


    }
}