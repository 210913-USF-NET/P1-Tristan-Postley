namespace Models
{
    public class Order
    {
        public List<LineItem> LineItems {get; set;}

        public decimal Total {get; set;}
    }
}