using System.Collections.Generic;
using Models;

namespace DL
{
    public interface IRepo
    {
        List<Store> GetAllStores();
        List<Customer> GetAllCustomers();
        Customer AddCustomer(Customer customer);
        List<Product> GetAllProducts();
        List<Order> GetAllOrders();
        Order AddOrder(Order order);

    }
}