using System;
using System.Collections.Generic;

namespace Models
{
    public class Customer
    {
        public Customer() {}

        public Customer(string name) 
        {
            this.Name = name;
        }

        public Customer(string name, string password)
        {
            this.Name = name;
            this.Password = password;
        }

        public string Name {get; set;}
        public string Password {get; set;}
        public int Id {get; set;}


        // public List<Order> Orders {get; set;}

    }
}
