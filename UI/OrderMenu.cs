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
            Log.Information("Starting Order for " + order.Customer.Name);

            bool exit = false;
            string input = "";
            int quantity = 0;
            decimal total = 0;
            
            order = _bl.AddOrder(order);
            Product orderedProd = new Product();
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
                        orderedProd = allProducts[int.Parse(input)];

                        Console.WriteLine("How many?");

                        int.TryParse(Console.ReadLine(), out quantity);
                        if(quantity < 1) 
                        {
                            Log.Information("User has input an invalid quantity");

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

                            _bl.UpdateInventory(new Order() 
                                {
                                    StoreId = order.StoreId, 
                                    LineItem = new LineItem() 
                                    {
                                        //Update inventory adds quantity to inventory so we add a negative value to remove some
                                        Quantity = (quantity * -1), 
                                        ProductId = orderedProd.Id
                                    }
                                });

                            Log.Information($"{order.Customer.Name} has ordered {quantity} {orderedProd.Item}");
                            

                            Console.Clear();
                            Console.WriteLine("Anything else?");
                            goto menu;

                        }
                        // break;
                    case "x":
                        Console.Clear();
                        if(total > 0) 
                        {
                            Log.Information($"{order.Customer.Name} has completed their order with a total of {total}");

                            Console.WriteLine($"That'll be {total}");
                            Console.ReadKey();
                        }
                        else 
                        {
                            Log.Information($"{order.Customer.Name} has exited without ordering");

                            Console.WriteLine("Be that way.");
                            System.Threading.Thread.Sleep(2000);
                        }
                        exit = true;
                        break;
                    default:
                        Log.Information($"{order.Customer.Name} has input an invalid order");

                        Console.WriteLine("That wasn't an option");
                        System.Threading.Thread.Sleep(2000);
                        break;
                }
            } while (!exit);
        }
    }
}