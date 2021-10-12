using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
//using Entity = DL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DL
{
    public class DBRepo : IRepo
    {
        //this needs dbcontext
        //and these methods will get data from db and persist to db

        private KKDBContext _context;
        public DBRepo(KKDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets list of stores from DB
        /// </summary>
        /// <returns>A list of store objects</returns>
        public List<Store> GetAllStores()
        {
            return _context.Stores.Select(
                store => new Store() {
                    Location = store.Location,
                    Id = store.Id
                }
            ).ToList();
        }
        
        /// <summary>
        /// Gets list of Inventories from DB
        /// </summary>
        /// <returns>A list of inventory objects</returns>
        public List<Inventory> GetAllInventories()
        {
            var query = from i in _context.Inventories
                join s in _context.Stores on i.StoreId equals s.Id
                join p in _context.Products on i.ProductId equals p.Id
                select new Inventory
                {
                    Id = i.Id,
                    Store = new Store() 
                    {
                        Location = s.Location,
                    },
                    Product = new Product()
                    {
                        Item = p.Item
                    },
                    Amount = (int)i.Amount,
                    StoreId = s.Id,
                    ProductId = p.Id
                };
            
            return query.ToList();
        }
        
        /// <summary>
        /// Takes a StoreId and a ProductId and adds a Quantity to the matched inventory
        /// </summary>
        /// <returns>An empty inventory object</returns>
        public Inventory UpdateInventory(Order order)
        {
            Inventory invToChange = _context.Inventories.First(i => i.StoreId == order.StoreId && i.ProductId == order.LineItem.ProductId);
            invToChange.Amount += order.LineItem.Quantity;

            // _context.Add(invToChange);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Inventory();
        }

        /// <summary>
        /// Gets list of products from DB
        /// </summary>
        /// <returns>A list of product objects</returns>
        public List<Product> GetAllProducts()
        {
            return _context.Products.Select(
                product => new Product() {
                    Id = product.Id,
                    Item = product.Item,
                    Price = (decimal)product.Price
                }
            ).ToList();
        }

        /// <summary>
        /// Gets list of customers from DB
        /// </summary>
        /// <returns>A list of customer objects</returns>
        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.Select(
                customer => new Customer() {
                    Id = customer.Id,
                    Name = customer.Name,
                    Password = customer.Password
                }
            ).ToList();
        }

        /// <summary>
        /// Takes a name and password and creates a new record for the customer in the DB
        /// </summary>
        /// <returns>A customer object with the Id it was assigned</returns>
        public Customer AddCustomer(Customer customer)
        {
            //Customer custToAdd = new Customer()
            //{
            //    Name = customer.Name,
            //    Password = customer.Password
            //};

            //Adds the custToAdd obj to change tracker
            customer = _context.Add(customer).Entity;
            //Changes don't get executed until you call SaveChanges
            _context.SaveChanges();
            //Clears the changeTracker so it returns a clean slate
            _context.ChangeTracker.Clear();

            return customer;

            //return new Customer()
            //{
            //    Id = custToAdd.Id,
            //    Name = custToAdd.Name,
            //    Password = custToAdd.Password
            //};
        }

        /// <summary>
        /// Takes a customer Id and a Store Id and creates a new record for the order in the DB
        /// </summary>
        /// <returns>An order object with Id, CustomerId, and StoreId</returns>
        public Order AddOrder(Order order)
        {
            Order orderToAdd = new Order()
            {
                CustomerId = order.Customer.Id,
                StoreId = order.Store.Id,
                Date = order.Date != null? order.Date.ToString() : null 
            };

             //Adds the orderToAdd obj to change tracker
            orderToAdd = _context.Add(orderToAdd).Entity;
            //Changes don't get executed until you call SaveChanges
            _context.SaveChanges();
            //Clears the changeTracker so it returns a clean slate
            _context.ChangeTracker.Clear();

            return new Order()
            {
                Id = orderToAdd.Id,
                CustomerId = (int)orderToAdd.CustomerId,
                StoreId = orderToAdd.StoreId,
                Customer = order.Customer,
                Store = order.Store
            };
        }

        /// <summary>
        /// Takes an order Id, product Id, and Quantity and creates a new record for the LineItem
        /// </summary>
        /// <returns>A LineItem object with Id</returns>
        public LineItem AddLineItem(Order order)
        {
            LineItem lineItemToAdd = new LineItem()
            {
                OrderId = order.Id,
                ProductId = order.LineItem.Item.Id,
                Quantity = order.LineItem.Quantity
            };

            //Adds the orderToAdd obj to change tracker
            lineItemToAdd = _context.Add(lineItemToAdd).Entity;
            //Changes don't get executed until you call SaveChanges
            _context.SaveChanges();
            //Clears the changeTracker so it returns a clean slate
            _context.ChangeTracker.Clear();

            return new LineItem()
            {
                Id = lineItemToAdd.Id,
                OrderId = (int)lineItemToAdd.OrderId,
                ProductId = (int)lineItemToAdd.ProductId,
                Quantity = (int)lineItemToAdd.Quantity
            };
        }

        /// <summary>
        /// Gets list of orders from DB with Ids info from other tables inserted by FK Ids
        /// </summary>
        /// <returns>A list of orders with all relevant info in human readable format</returns>
        public List<Order> GetAllOrders()
        {
            return _context.Orders.Select(
                order => new Order()
                {
                    Id = order.Id,
                    Customer = order.Customer,
                    LineItem = order.LineItem,
                    Date = order.Date,
                    Store = order.Store
                }
            ).ToList();

            //var query = from o in _context.Orders
            //    join li in _context.LineItems on o.Id equals li.OrderId
            //    join cust in _context.Customers on o.CustomerId equals cust.Id
            //    join prod in _context.Products on li.ProductId equals prod.Id
            //    join s in _context.Stores on o.StoreId equals s.Id
            //    select new Order
            //    {
            //        Id = o.Id,
            //        Customer = new Customer 
            //        {
            //            Name = cust.Name
            //        },
            //        LineItem = new LineItem 
            //        {
            //            Quantity = (int)li.Quantity, 
            //            Item = new Product 
            //            {
            //                Price = (decimal)prod.Price,
            //                Item = prod.Item
            //            }
            //        },
            //        Date = o.Date.ToString(),
            //        Store = new Store
            //        {
            //            Location = s.Location
            //        }
            //    };

            //return query.ToList();
        }
    }
} 