using System.Collections.Generic;
using Models;

namespace StoreBL
{
    public interface IBL
    {
        List<Store> GetAllStores();

        List<Customer> GetAllCustomers();
    }
}