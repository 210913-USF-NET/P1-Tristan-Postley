using System;
using System.Collections.Generic;
using Models;
using StoreBL;
using DL;
using Serilog;

namespace UI
{
    public class NameMenu : IMenu
    {
        private IBL _bl;
        
        public NameMenu(IBL bl)
        {
            _bl = bl;
        }
        public void Start(Order order)
        {
            Log.Information("Order Location: " + order.Store.Location);

            begin:
            string name = "";
            bool match = false;
            List<Customer> matchedCustomers = new List<Customer>();

            Console.Clear();
            Console.WriteLine($"Welcome to the {order.Store.Location} Krusty Krab. Gimme your name?");
            
            name = Console.ReadLine();
            //Start admin menu if Mr. Krabs
            if(name.Equals("Krabs")) MenuFactory.GetMenu("admin").Start(new Order());
            
            //Search DB for input
            List<Customer> allCustomers = _bl.GetAllCustomers();

            foreach(Customer cust in allCustomers)
            {
                if(cust.Name.ToLower() == name.ToLower())
                {
                    Log.Information("Found Customer: " + cust.Name);
                    match = true;
                    matchedCustomers.Add(cust);
                }
            }
            if(match)
            {
                Console.WriteLine("And your password?");
                string password = Console.ReadLine();

                //Will only check the password against the first customer with that name
                foreach(Customer cust in matchedCustomers)
                {
                    if(cust.Password == password)
                    {
                        Log.Information("Found Customer: " + cust.Name);
                        order.Customer = cust;
                        MenuFactory.GetMenu("order").Start(order);
                    }
                    else
                    {
                        Console.WriteLine($"That was the wrong password. Are you really {cust.Name}?");
                        System.Threading.Thread.Sleep(3000);
                        goto begin;
                    }
                }

            }
            else 
            {
                Console.WriteLine("Think of a password, so nobody impersonates you.");
                string password = Console.ReadLine();
                order.Customer = new Customer(name, password);
                order.Customer = _bl.AddCustomer(order.Customer);
                Log.Information("Created new Customer: " + order.Customer.Name + " " + order.Customer.Password);
                MenuFactory.GetMenu("order").Start(order);
            }
        }
    }
}