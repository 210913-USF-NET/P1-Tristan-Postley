using System.Collections.Generic;

namespace Models
{
    public class Store
    {
        public Store() {}

        public string Location {get; set;}
        public string Name {get; set;}


        public List<Product> Products {get; set;}

    }
}