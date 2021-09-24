using System;
using System.Collections.Generic;
using Models;
using StoreBL;
using DL;
using Serilog;

namespace UI
{
    public class AdminMenu : IMenu
    {
        private IBL _bl;
        
        public AdminMenu(IBL bl)
        {
            _bl = bl;
        }
        public void Start(Order order)
        {
            string input = "";
            bool exit = false;
            do
            {

                Console.Clear();
                Console.WriteLine("What is it?");
                Console.WriteLine("");

                Console.WriteLine("[0] Tell me about the customers.");
                Console.WriteLine("[1] Where am I makin' the most money?");
                Console.WriteLine("[2] Remind me, whats the Secret Formula?");
                Console.WriteLine("[3] How much money did I make today?");
                Console.WriteLine("[x] Back to work!");

                input = Console.ReadLine();

                switch(input)
                {
                    case "0":
                        break;
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "x":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("That wasn't an option.");
                        break;
                }
            } while (!exit);
        }
    }
}