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
                    return new LocationMenu();
                case "name":
                    return new NameMenu();
                case "order":
                    return new OrderMenu();
                default:
                    return null;
            }
        }
    }
}