using System;
using Models;

namespace UI
{
    public class NameMenu : IMenu
    {
        //TODO
        //How to pass parameters?
        // public void Start(string location)
        public void Start()
        {
            string input = "";

            Console.WriteLine($"Welcome to the {location} Krusty Krab. Gimme your name?");

            input = Console.ReadLine();

            //TODO
            //Search DB for input
            //if(match) 
            // cust = customer object
            // Console.WriteLine("And your password?");
            // password = Console.ReadLine();

            //else 
            // Console.WriteLine("Think of a password, so nobody impersonates you.");
            // password = Console.ReadLine();
            // Customer cust = new Customer(string input, string password)

            // MenuFactory.GetMenu("order").Start(cust);

        }
    }
}