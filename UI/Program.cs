using System;
using Models;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my store!");

            StoreFront myStore = new StoreFront() 
            {
                Name = "My Store",
                Address = "123 West East Street, Los Angeles CA"
            };

            Console.WriteLine(myStore.ToString());
            myStore.Name = Console.ReadLine();
            Console.WriteLine(myStore.ToString());
        }
    }
}
