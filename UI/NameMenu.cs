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
            Log.Information("Order Location: " + order.Product.Location);

            begin:
            string name = "";
            bool match = false;
            List<Customer> matchedCustomers = new List<Customer>();

            
            Console.WriteLine($"Welcome to the {order.Product.Location} Krusty Krab. Gimme your name?");
            
            name = Console.ReadLine();
            //TODO
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
                        Console.WriteLine("That was the wrong password, so who are you really?");
                        goto begin;
                    }
                }

            }
            else 
            {
                Console.WriteLine("Think of a password, so nobody impersonates you.");
                string password = Console.ReadLine();
                order.Customer = new Customer(name, password);
                Log.Information("Created new Customer: " + order.Customer.Name + " " + order.Customer.Password);
                MenuFactory.GetMenu("order").Start(order);
            }
        }
    }
}