using System;
using System.Collections.Generic;
using Models;
using StoreBL;
using DL;
using Serilog;
using ConsoleTables;
using System.Linq;

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
            List<Order> allOrders = _bl.GetAllOrders();
            bool match = false;
            do
            {
                bool askingAboutCustomer = false;
                List<Order> customerOrders = new List<Order>();

                Console.Clear();
                Console.WriteLine("What is it?");
                Console.WriteLine("");

                Console.WriteLine("[0] Tell me about the customers.");
                Console.WriteLine("[1] Tell me about the stores?");
                Console.WriteLine("[2] Remind me, whats the Secret Formula?");
                Console.WriteLine("[3] How much money have I made?");
                Console.WriteLine("[x] Back to work!");

                input = Console.ReadLine();

                switch(input)
                {
                    case "0":
                        askingAboutCustomer = true;
                        Console.Clear();
                        Console.WriteLine("Which one?");
                        string customer = Console.ReadLine();

                        //Loop through orders to see if the requested customer exists
                        foreach(var item in allOrders)
                        {
                            if(customer.ToLower() == item.Customer.Name.ToLower()) 
                            {
                                match = true;
                                customerOrders.Add(item);
                            }
                        }

                        do
                        {
                            if(match)
                            {
                                Console.Clear();
                                Console.WriteLine("What about them?");
                                Console.WriteLine("");
                                
                                Console.WriteLine("[0] Tell me about their orders from the first time they came in.");
                                Console.WriteLine("[1] Tell me about their orders from the most recent time they came in.");
                                Console.WriteLine("[2] Tell me about the biggest orders they've made.");
                                Console.WriteLine("[3] Tell me about the smallest orders they've made.");
                                Console.WriteLine("[x] Nevermind.");

                                string sortType = Console.ReadLine();

                                switch(sortType)
                                {
                                    case "0":
                                        List<Order> ordersByEarliestDate = customerOrders.OrderBy(o => o.Date).ToList();
                                        var table = new ConsoleTable("Location","Order","Total","Date");
                                        foreach (var item in ordersByEarliestDate)
                                        {
                                            table.AddRow($"{item.Store.Location}",
                                                        $"{item.LineItem.Item.Item}",
                                                        $"{item.LineItem.Quantity * item.LineItem.Item.Price}",
                                                        $"{item.Date}" );
                                        }
                                        table.Write(Format.Minimal);
                                        Console.ReadKey();
                                        break;

                                    case "1":
                                        List<Order> ordersByLatestDate = customerOrders.OrderByDescending(o => o.Date).ToList();
                                        var table2 = new ConsoleTable("Location","Order","Total","Date");
                                        foreach (var item in ordersByLatestDate)
                                        {
                                            table2.AddRow($"{item.Store.Location}",
                                                        $"{item.LineItem.Item.Item}",
                                                        $"{item.LineItem.Quantity * item.LineItem.Item.Price}",
                                                        $"{item.Date}" );
                                        }
                                        table2.Write(Format.Minimal);
                                        Console.ReadKey();
                                        break;

                                    case "2":
                                        List<Order> ordersByLargestTotal = customerOrders.OrderByDescending(o => (o.LineItem.Quantity * o.LineItem.Item.Price)).ToList();
                                        var table3 = new ConsoleTable("Location","Order","Total","Date");
                                        foreach (var item in ordersByLargestTotal)
                                        {
                                            table3.AddRow($"{item.Store.Location}",
                                                        $"{item.LineItem.Item.Item}",
                                                        $"{item.LineItem.Quantity * item.LineItem.Item.Price}",
                                                        $"{item.Date}" );
                                        }
                                        table3.Write(Format.Minimal);
                                        Console.ReadKey();
                                        break;
                                        
                                    case "3":
                                        List<Order> ordersBySmallestTotal = customerOrders.OrderBy(o => (o.LineItem.Quantity * o.LineItem.Item.Price)).ToList();
                                        var table4 = new ConsoleTable("Location","Order","Total","Date");
                                        foreach (var item in ordersBySmallestTotal)
                                        {
                                            table4.AddRow($"{item.Store.Location}",
                                                        $"{item.LineItem.Item.Item}",
                                                        $"{item.LineItem.Quantity * item.LineItem.Item.Price}",
                                                        $"{item.Date}" );
                                        }
                                        table4.Write(Format.Minimal);
                                        Console.ReadKey();
                                        break;

                                    case "x":
                                        askingAboutCustomer = false;
                                        break;

                                    default:
                                        Console.WriteLine("What?");
                                        System.Threading.Thread.Sleep(1000);
                                        break;
                                }
                            }
                            else 
                            {
                                Console.WriteLine("They haven't ordered anything.");
                                System.Threading.Thread.Sleep(2000);
                            }
                        } while(askingAboutCustomer);
                        break;

                    case "1":
                        Console.Clear();
                        Console.WriteLine("Which one?");
                        Console.WriteLine("");

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
                        Console.WriteLine("[x] Nevermind");

                        string store = Console.ReadLine();

                        bool askingAboutStore = true;

                        switch(store)
                        {
                            case "0":
                            case "1":
                            case "2":
                                Store selectedStore = allStores[int.Parse(store)];
                                List<Order> storesOrders = new List<Order>();
                                foreach(var item in allOrders)
                                {
                                    if(selectedStore.Location.ToLower() == item.Store.Location.ToLower()) 
                                    {
                                        match = true;
                                        storesOrders.Add(item);
                                    }
                                }

                                storeMenu:
                                Console.Clear();
                                Console.WriteLine($"What do you want to know about the {selectedStore.Location} Krusty Krab?");
                                Console.WriteLine("");

                                Console.WriteLine("[0] Tell me about the orders.");
                                Console.WriteLine("[1] How many patties do we have there.");
                                Console.WriteLine("[2] Order more patties.");
                                Console.WriteLine("[x] Nevermind.");

                                string menuSelection = Console.ReadLine();

                                switch(menuSelection)
                                {
                                    case "0":
                                        do
                                        {
                                            Console.Clear();
                                            Console.WriteLine("How do you want them sorted?");
                                            Console.WriteLine("");

                                            Console.WriteLine("[0] By what made me the most money.");
                                            Console.WriteLine("[1] By what made me the least money.");
                                            Console.WriteLine("[2] By the earliest one.");
                                            Console.WriteLine("[3] By the most recent one.");
                                            Console.WriteLine("[x] Nevermind");

                                            string input2 = Console.ReadLine();

                                            switch(input2)
                                            {
                                                case "0":
                                                    List<Order> ordersByLargestTotal = storesOrders.OrderByDescending(o => o.LineItem.Quantity * o.LineItem.Item.Price).ToList();
                                                    var table = new ConsoleTable("Location","Customer","Total","Date");
                                                    foreach (var item in ordersByLargestTotal)
                                                    {
                                                        table.AddRow($"{item.Store.Location}",
                                                                    $"{item.Customer.Name}",
                                                                    $"{item.LineItem.Quantity * item.LineItem.Item.Price}",
                                                                    $"{item.Date}" );
                                                    }
                                                    table.Write(Format.Minimal);
                                                    Console.ReadKey();
                                                    break;
                                                case "1":
                                                    List<Order> ordersBySmallestTotal = storesOrders.OrderBy(o => o.LineItem.Quantity * o.LineItem.Item.Price).ToList();
                                                    var table2 = new ConsoleTable("Location","Customer","Total","Date");
                                                    foreach (var item in ordersBySmallestTotal)
                                                    {
                                                        table2.AddRow($"{item.Store.Location}",
                                                                    $"{item.Customer.Name}",
                                                                    $"{item.LineItem.Quantity * item.LineItem.Item.Price}",
                                                                    $"{item.Date}" );
                                                    }
                                                    table2.Write(Format.Minimal);
                                                    Console.ReadKey();
                                                    break;
                                                case "2":
                                                    List<Order> ordersByEarliestDate = storesOrders.OrderBy(o => o.Date).ToList();
                                                    var table3 = new ConsoleTable("Location","Customer","Total","Date");
                                                    foreach (var item in ordersByEarliestDate)
                                                    {
                                                        table3.AddRow($"{item.Store.Location}",
                                                                    $"{item.Customer.Name}",
                                                                    $"{item.LineItem.Quantity * item.LineItem.Item.Price}",
                                                                    $"{item.Date}" );
                                                    }
                                                    table3.Write(Format.Minimal);
                                                    Console.ReadKey();
                                                    break;
                                                case "3":
                                                    List<Order> ordersByLatestDate = storesOrders.OrderByDescending(o => o.Date).ToList();
                                                    var table4 = new ConsoleTable("Location","Customer","Total","Date");
                                                    foreach (var item in ordersByLatestDate)
                                                    {
                                                        table4.AddRow($"{item.Store.Location}",
                                                                    $"{item.Customer.Name}",
                                                                    $"{item.LineItem.Quantity * item.LineItem.Item.Price}",
                                                                    $"{item.Date}" );
                                                    }
                                                    table4.Write(Format.Minimal);
                                                    Console.ReadKey();
                                                    break;
                                                case "x":
                                                    askingAboutStore = false;
                                                    break;
                                                default:
                                                    Console.WriteLine("What?");
                                                    System.Threading.Thread.Sleep(1000);
                                                    break;

                                            }

                                        } while(askingAboutStore);
                                        break;
                                    case "1":
                                        //View Inventory
                                        List<Inventory> allInv = _bl.GetAllInventories();
                                        var invTable = new ConsoleTable("Product","Inventory");
                                        foreach (var item in allInv)
                                        {
                                            if(item.Store.Location == selectedStore.Location)
                                            {
                                                // Console.WriteLine(item.Product.Item + item.Amount);
                                                invTable.AddRow($"{item.Product.Item}", 
                                                                $"{item.Amount}");
                                            }
                                        }
                                        invTable.Write(Format.Minimal);
                                        Console.ReadKey();

                                        goto storeMenu;
                                        // break;
                                    case "2":
                                        //Add to Inventory
                                        Console.WriteLine("What kind?");
                                        Console.WriteLine("");
                                        Console.WriteLine("[0] Plain");
                                        Console.WriteLine("[1] Regular");
                                        Console.WriteLine("[2] Deluxe");

                                        int pattyType;
                                        if (!int.TryParse(Console.ReadLine(), out pattyType))
                                        {
                                            Console.WriteLine("What?");
                                            goto storeMenu;
                                        } 
                                        //Produce product id without querying db 
                                        int productId = pattyType + 1;


                                        Console.WriteLine("How many?");
                                        int numberToOrder = 0;
                                        if (!int.TryParse(Console.ReadLine(), out numberToOrder)) 
                                        {
                                            Console.WriteLine("What?");
                                            goto storeMenu;
                                        }
                                        
                                        _bl.UpdateInventory(new Order() 
                                        {
                                            StoreId = selectedStore.Id, 
                                            LineItem = new LineItem() 
                                            {
                                                Quantity = numberToOrder, 
                                                ProductId = productId
                                            }
                                        });
                                        break;
                                    case "x":
                                        break;
                                    default:
                                        Console.WriteLine("What?");
                                        System.Threading.Thread.Sleep(1000);
                                        break;
                                }

                                break;
                            case "x":
                                break;
                            default:
                                Console.WriteLine("What?");
                                System.Threading.Thread.Sleep(1000);
                                break;
                        }

                        break;

                    case "2":
                        Console.WriteLine("*Sighs*");
                        System.Threading.Thread.Sleep(1000);

                        Console.WriteLine("It's a secret.");
                        System.Threading.Thread.Sleep(2000);
                        break;

                    case "3":
                        float total = 0;
                        foreach(var item in allOrders)
                        {
                            total += (float)(item.LineItem.Quantity * item.LineItem.Item.Price);
                        }
                        Console.Clear();
                        Console.WriteLine(total);
                        Console.ReadKey();
                        break;

                    case "x":
                        exit = true;
                        //Closes app, does this interfere with Logs?
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("That wasn't an option.");
                        break;
                }
            } while (!exit);
        }
    }
}