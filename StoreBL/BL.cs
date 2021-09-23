using System;
using System.Collections.Generic;
using Models;
using DL;

namespace StoreBL
{
    public class BL : IBL
    {
        private IRepo _repo;

        public BL(IRepo repo)
        {
            _repo = repo;
        }

        public List<Store> GetAllStores()
        {
            return _repo.GetAllStores();
        }

        public List<Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }
        public Customer AddCustomer(Customer customer)
        {
            return _repo.AddCustomer(customer);
        }
        public List<Product> GetAllProducts()
        {
            return _repo.GetAllProducts();
        }
        public Order AddOrder(Order order)
        {
            return _repo.AddOrder(order);
        }
    }
}
