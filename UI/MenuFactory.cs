using System;
using DL;
using StoreBL;

namespace UI
{
    public class MenuFactory
    {
        public static IMenu GetMenu(string menuString)
        {
            switch (menuString.ToLower())
            {
                case "main": 
                    return new MainMenu();
                case "location":
                    return new LocationMenu(new BL(new ExampleRepo()));
                case "name":
                    return new NameMenu(new BL(new ExampleRepo()));
                case "order":
                    return new OrderMenu(new BL(new ExampleRepo()));
                default:
                    return null;
            }
        }
    }
}