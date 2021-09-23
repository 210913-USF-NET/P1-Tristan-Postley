using System;
using Models;
using StoreBL;
using System.Collections.Generic;
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
                        // order.Store = new Store();
                        order.Store = allStores[int.Parse(input)];
                        MenuFactory.GetMenu("name").Start(order);
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