using System;
using Models;

namespace UI
{
    public class MainMenu : IMenu
    {
        public void Start(Order order)
        {
            Console.WriteLine("Main Menu");

            MenuFactory.GetMenu("location").Start(order);
        }
    }
}