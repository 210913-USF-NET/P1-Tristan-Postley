using System;
using Models;
using StoreBL;
using System.Collections.Generic;
using Serilog;
namespace UI
{
    public class LocationMenu : IMenu
    {
        private IBL _bl;

        public LocationMenu(IBL bl)
        {
            _bl = bl;
        }
        public void Start(Order order)
        {
            bool exit = false;
            string input = "";
            do
            {
                Console.Clear();
                Console.WriteLine("You are hungry for a Krabby Patty. Where would you like to go?");
                
                List<Store> allStores = _bl.GetAllStores();
                if(allStores == null || allStores.Count == 0)
                {
                    Console.WriteLine("No Stores");
                    return;
                }
                for(int i = 0; i < allStores.Count; i++)
                {
                    Console.WriteLine($"[{i}] {allStores[i].Location}");
                }
                
                Console.WriteLine("[x] Exit");

                input = Console.ReadLine();

                switch (input)
                {
                    //Hardcoded, will only work for three options
                    case "0":
                    case "1":
                    case "2":  
                        order.Store = allStores[int.Parse(input)];
                        Log.Information($"Selected store at location: {order.Store.Location}");
                        MenuFactory.GetMenu("name").Start(order);
                        break;
                    case "x":
                        Console.WriteLine("Be that way.");
                        Log.Information("Exited from selecting location");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("That wasn't an option");
                        Log.Information("Tried to select invalid location");
                        break;
                }
            } while (!exit);
        }
    }
}