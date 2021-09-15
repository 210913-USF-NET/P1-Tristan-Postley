using System.Collections.Generic;

namespace Models
{
    public class Order
    {
        public int Id {get;}
        public List<LineItem> LineItems {get; set;}

        public decimal Total {get; set;}
    }
}