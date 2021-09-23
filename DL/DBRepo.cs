using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model = Models;
using Entity = DL.Entities;

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
        // public Model.Restaurant AddRestaurant(Model.Restaurant resto)
        // {
        //     throw new NotImplementedException();
        // }

        public List<Model.Store> GetAllStores()
        {
            return _context.Stores.Select(
                store => new Model.Store() {
                    Location = store.Location,
                    Id = store.Id
                }
            ).ToList();
        }

        public List<Model.Product> GetAllProducts()
        {
            return _context.Products.Select(
                product => new Model.Product() {
                    Item = product.Item,
                    Price = (decimal)product.Price
                }
            ).ToList();
        }

        // public Model.Restaurant UpdateRestaurant(Model.Restaurant restaurantToUpdate)
        // {
        //     throw new NotImplementedException();
        // }

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
                StoreId = orderToAdd.StoreId
            };
        }

        public List<Model.Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }
    }
} 