using System.Collections.Generic;

namespace Models
{
    public class Order
    {
        public Customer Customer {get; set;}
        public int Quantity {get; set;}
        public Product Product {get; set;}

        public Store Store {get; set;}
    }
}