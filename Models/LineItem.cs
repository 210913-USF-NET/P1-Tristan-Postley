using System.Collections.Generic;
namespace Models
{
    public class LineItem
    {
        public Product Item {get; set;}
        public int Quantity {get; set;}
        public int Id {get; set;}
        public int ProductId {get; set;}
        public int OrderId {get; set;}




    }
}