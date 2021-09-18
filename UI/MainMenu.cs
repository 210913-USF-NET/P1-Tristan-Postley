using System;

namespace UI
{
    public class MainMenu : IMenu
    {
        public void Start()
        {
            Console.WriteLine("Main Menu");

            MenuFactory.GetMenu("location").Start();
        }
    }
}