using System;
using Models;
using StoreBL;
using Serilog;

namespace UI
{
    public class OrderMenu : IMenu
    {
        private IBL _bl;
        
        public OrderMenu(IBL bl)
        {
            _bl = bl;
        }
        public void Start(Order order)
        {
            Log.Information("Taking Order for " + order.Customer.Name);

            bool exit = false;
            string input = "";
            int quantity = 0;
            string selectedLocation = "";
            order.Product = new Product();
            do
            {
                Console.WriteLine("Let me guess, you want a Krabby Patty?");
                Console.WriteLine("[0] Plain Krabby Patty");
                Console.WriteLine("[1] Krabby Patty");
                Console.WriteLine("[2] Deluxe Krabby Patty");
                Console.WriteLine("[x] Exit");

                input = Console.ReadLine();

                Console.WriteLine("How many?");

                quantity = int.Parse(Console.ReadLine());

                if(quantity < 1) Console.WriteLine("Then why are you here?");

                switch (input)
                {
                    case "0":
                        selectedLocation = "Bikini Bottom";
                        break;
                    case "1":
                        selectedLocation = "Tentacle Acres";
                        break;
                    case "2":  
                        selectedLocation = "New Kelp City";
                        break;
                    case "x":
                        Console.WriteLine("Be that way.");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("That wasn't an option");
                        break;
                }


            } while (!exit);
        }
    }
}