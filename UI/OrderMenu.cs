using System;
using System.Collections.Generic;
using Models;
using StoreBL;
using Serilog;
using ConsoleTables;

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
            decimal total = 0;
            order = _bl.AddOrder(order);
            do
            {
                Console.Clear();
                Console.WriteLine("Let me guess, you want a Krabby Patty?");

                menu:
                List<Product> allProducts = _bl.GetAllProducts();
                if(allProducts == null || allProducts.Count == 0)
                {
                    Console.WriteLine("No Products");
                    return;
                }

                var table = new ConsoleTable("", "", "");
                for(int i = 0; i < allProducts.Count; i++)
                {
                    table.AddRow($"[{i}]", $"{allProducts[i].Item}", $"{allProducts[i].Price}");
                }
                table.Write(Format.Minimal);
                Console.WriteLine("[x] Exit");

                input = Console.ReadLine();


                switch (input)
                {
                    case "0":
                    case "1":
                    case "2":
                        Product orderedProd = allProducts[int.Parse(input)];

                        Console.WriteLine("How many?");
                        int.TryParse(Console.ReadLine(), out quantity);
                        if(quantity < 1) 
                        {
                            Console.Clear();
                            Console.WriteLine("*Blank stare*");
                            System.Threading.Thread.Sleep(1000);
                            break;
                        }
                        else
                        {
                            //Create LineItem
                            LineItem lineItem = new LineItem();
                            lineItem.Item = orderedProd;
                            lineItem.Quantity = quantity;
                            order.LineItem = lineItem;

                            //Add LineItem to DB
                            _bl.AddLineItem(order);

                            total += lineItem.Quantity * lineItem.Item.Price;

                            Console.Clear();
                            Console.WriteLine("Anything else?");
                            goto menu;

                        }
                        // break;
                    case "x":
                        Console.Clear();
                        if(total > 0) 
                        {
                            Console.WriteLine($"That'll be {total}");
                            Console.ReadKey();
                        }
                        else 
                        {
                            Console.WriteLine("Be that way.");
                            System.Threading.Thread.Sleep(2000);
                        }
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("That wasn't an option");
                        System.Threading.Thread.Sleep(2000);
                        break;
                }
            } while (!exit);
        }
    }
}