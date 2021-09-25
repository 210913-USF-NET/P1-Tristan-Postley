using System.Collections.Generic;
using Models;

namespace StoreBL
{
    public interface IBL
    {
        List<Store> GetAllStores();
        List<Customer> GetAllCustomers();
        Customer AddCustomer(Customer customer);
        List<Product> GetAllProducts();
        Order AddOrder(Order order);
        LineItem AddLineItem(Order order);
        List<Order> GetAllOrders();
    }
}