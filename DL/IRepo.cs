using System.Collections.Generic;
using Models;

namespace DL
{
    public interface IRepo
    {
        List<Store> GetAllStores();

        List<Customer> GetAllCustomers();
    }
}