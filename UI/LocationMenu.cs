using System;
using Models;

namespace UI
{
    public class LocationMenu : IMenu
    {
        public void Start()
        {
            bool exit = false;
            string input = "";
            string selectedLocation = "";
            do
            {
                Console.WriteLine("You are hungry for a Krabby Patty. Where would you like to go?");
                Console.WriteLine("[0] Bikini Bottom");
                Console.WriteLine("[1] Tentacle Acres");
                Console.WriteLine("[2] New Kelp City");
                Console.WriteLine("[x] Exit");

                input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        selectedLocation = "Bikini Bottom";
                        //Select from Stores where location = selectedLocation
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

                MenuFactory.GetMenu("name").Start(selectedLocation);

            } while (!exit);
        }
    }
}