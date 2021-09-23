using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Product
    {
        public Product()
        {
            Inventories = new HashSet<Inventory>();
            LineItems = new HashSet<LineItem>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Item { get; set; }
        public double? Price { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
