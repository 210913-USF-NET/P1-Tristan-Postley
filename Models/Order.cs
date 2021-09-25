using System.Collections.Generic;

namespace Models
{
    public class Order
    {
        public Customer Customer {get; set;}
        public int CustomerId {get; set;}
        // public List<LineItem> LineItems {get; set;}
        public LineItem LineItem {get; set;}
        public Store Store {get; set;}
        public int StoreId {get; set;}

        public int Id {get; set;}
        public string Date {get; set;}
    
    }
}