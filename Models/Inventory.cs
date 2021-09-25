namespace Models
{
    public class Inventory
    {
        public int Amount {get; set;}
        public int Id {get; set;}
        public int StoreId {get; set;}
        public int ProductId {get; set;}
        public Store Store {get; set;}
        public Product Product {get; set;}

    }
}