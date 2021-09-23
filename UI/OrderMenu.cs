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
            do
            {
                Console.Clear();
                Console.WriteLine("Let me guess, you want a Krabby Patty?");

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
                        Product orderedProd = new Product();
                        orderedProd.Item = allProducts[int.Parse(input)].Item;
                        orderedProd.Price = allProducts[int.Parse(input)].Price;
                
                        // order.Product = new Product();
                        // order.Product.Item = allProducts[int.Parse(input)].Item;

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
                            //Add order to DB
                            LineItem lineItem = new LineItem();
                            lineItem.Item = orderedProd;
                            lineItem.Quantity = quantity;
                            order.LineItems = new List<LineItem>();
                            order.LineItems.Add(lineItem);
                            _bl.AddOrder(order);

                        }
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