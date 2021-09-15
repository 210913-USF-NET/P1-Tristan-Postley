using System;

namespace Models
{
    public class Customer
    {
        public Customer() {}

        public Customer(string name) 
        {
            this.Name = name;
        }

        public Customer(string name, int age) : this(name) 
        {
            this.Age = age;
        }

        public string Name {get; set;}
        public int Age {get; set;}
        public string City {get; set;}

        public List<Order> Orders {get; set;}

    }
}
