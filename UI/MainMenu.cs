using System;
using StoreBL;

namespace UI
{
    public class MainMenu : IMenu
    {
        private IBL _bl;

        public MainMenu(IBL bl)
        {
            _bl = bl;
        }

        public void Start()
        {
            Console.WriteLine("Main Menu");
        }
    }
}