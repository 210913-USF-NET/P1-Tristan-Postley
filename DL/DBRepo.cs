using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = Models;
using Entity = DL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DL
{
    public class DBRepo : IRepo
    {
        //this needs dbcontext
        //and these methods will get data from db and persist to db

        private Entity.KrustyKrabDBContext _context;
        public DBRepo(Entity.KrustyKrabDBContext context)
        {
            _context = context;
        }

        public List<Model.Store> GetAllStores()
        {
            return _context.Stores.Select(
                store => new Model.Store() {
                    Location = store.Location,
                    Id = store.Id
                }
            ).ToList();
        }
        public List<Model.Inventory> GetAllInventories()
        {
            var query = from i in _context.Inventories
                join s in _context.Stores on i.StoreId equals s.Id
                join p in _context.Products on i.ProductId equals p.Id
                select new Model.Inventory
                {
                    Id = i.Id,
                    Store = new Model.Store() 
                    {
                        Location = s.Location,
                    },
                    Product = new Model.Product()
                    {
                        Item = p.Item
                    },
                    Amount = (int)i.Amount
                };
            
            return query.ToList();
        }
        public Model.Inventory UpdateInventory(Model.Order order)
        {
            Entity.Inventory invToChange = _context.Inventories.First(i => i.StoreId == order.StoreId && i.ProductId == order.LineItem.ProductId);
            invToChange.Amount += order.LineItem.Quantity;

            // _context.Add(invToChange);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Model.Inventory();
        }

        public List<Model.Product> GetAllProducts()
        {
            return _context.Products.Select(
                product => new Model.Product() {
                    Id = product.Id,
                    Item = product.Item,
                    Price = (decimal)product.Price
                }
            ).ToList();
        }
        public List<Model.Customer> GetAllCustomers()
        {
            return _context.Customers.Select(
                customer => new Model.Customer() {
                    Id = customer.Id,
                    Name = customer.Name,
                    Password = customer.Password
                }
            ).ToList();
        }

        public Model.Customer AddCustomer(Model.Customer customer)
        {
            Entity.Customer custToAdd = new Entity.Customer()
            {
                Name = customer.Name,
                Password = customer.Password
            };

            //Adds the custToAdd obj to change tracker
            custToAdd = _context.Add(custToAdd).Entity;
            //Changes don't get executed until you call SaveChanges
            _context.SaveChanges();
            //Clears the changeTracker so it returns a clean slate
            _context.ChangeTracker.Clear();

            return new Model.Customer()
            {
                Id = custToAdd.Id,
                Name = custToAdd.Name,
                Password = custToAdd.Password
            };
        }

        public Model.Order AddOrder(Model.Order order)
        {
            Entity.Order orderToAdd = new Entity.Order()
            {
                CustomerId = order.Customer.Id,
                StoreId = order.Store.Id
            };

             //Adds the orderToAdd obj to change tracker
            orderToAdd = _context.Add(orderToAdd).Entity;
            //Changes don't get executed until you call SaveChanges
            _context.SaveChanges();
            //Clears the changeTracker so it returns a clean slate
            _context.ChangeTracker.Clear();

            return new Model.Order()
            {
                Id = orderToAdd.Id,
                CustomerId = (int)orderToAdd.CustomerId,
                StoreId = orderToAdd.StoreId,
                Customer = order.Customer,
                Store = order.Store
            };
        }
        public Model.LineItem AddLineItem(Model.Order order)
        {
            Entity.LineItem lineItemToAdd = new Entity.LineItem()
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

            return new Model.LineItem()
            {
                Id = lineItemToAdd.Id,
                OrderId = (int)lineItemToAdd.OrderId,
                ProductId = (int)lineItemToAdd.ProductId,
                Quantity = (int)lineItemToAdd.Quantity
            };
        }

        public List<Model.Order> GetAllOrders()
        {            

            var query = from o in _context.Orders
                join li in _context.LineItems on o.Id equals li.OrderId
                join cust in _context.Customers on o.CustomerId equals cust.Id
                join prod in _context.Products on li.ProductId equals prod.Id
                join s in _context.Stores on o.StoreId equals s.Id
                select new Model.Order
                {
                    Id = o.Id,
                    Customer = new Model.Customer 
                    {
                        Name = cust.Name
                    },
                    LineItem = new Model.LineItem 
                    {
                        Quantity = (int)li.Quantity, 
                        Item = new Model.Product 
                        {
                            Price = (decimal)prod.Price,
                            Item = prod.Item
                        }
                    },
                    Date = o.Time.ToString(),
                    Store = new Model.Store
                    {
                        Location = s.Location
                    }
                };

            return query.ToList();
        }
    }
} 