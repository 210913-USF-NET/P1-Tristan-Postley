using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Product
    {
        public Product()
        {
            Stores = new HashSet<Store>();
        }

        public int Id { get; set; }
        public string Item { get; set; }
        public double? Price { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}
