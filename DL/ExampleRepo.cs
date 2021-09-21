using System;
using System.Collections.Generic;
using Models;

namespace DL
{
    public class ExampleRepo : IRepo
    {
        public List<Store> GetAllStores() 
        {
            return new List<Store> ()
            {
                new Store()
                {
                    Name = "Store One",
                    Location = "Bikini Bottom"
                },
                new Store()
                {
                    Name = "Store two",
                    Location = "Tentacle Acres"
                },
                new Store()
                {
                    Name = "Store Three",
                    Location = "New Kelp City"
                }
            };
        }

        public List<Customer> GetAllCustomers() 
        {
            return new List<Customer> ()
            {
                new Customer()
                {
                    Name = "Patrick Star",
                    Password = "12345"
                },
                new Customer()
                {
                    Name = "Sandy Cheeks",
                    Password = "Texas"
                }
            };
        }

        public List<Product> GetAllProducts()
        {
            return new List<Product> ()
            {
                new Product()
                {
                    
                }
            };
        }
        
    }
}
