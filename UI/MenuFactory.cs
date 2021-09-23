using System;
using DL;
using StoreBL;
using DL.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace UI
{
    public class MenuFactory
    {
        public static IMenu GetMenu(string menuString)
        {
            string connectionString = File.ReadAllText(@"../connectionString.txt");
            DbContextOptions<KrustyKrabDBContext> options = new DbContextOptionsBuilder<KrustyKrabDBContext>()
            .UseSqlServer(connectionString).Options;
            KrustyKrabDBContext context = new KrustyKrabDBContext(options);
            
            switch (menuString.ToLower())
            {
                case "main": 
                    return new MainMenu();
                case "location":
                    return new LocationMenu(new BL(new DBRepo(context)));
                case "name":
                    return new NameMenu(new BL(new DBRepo(context)));
                case "order":
                    return new OrderMenu(new BL(new DBRepo(context)));
                default:
                    return null;
            }
        }
    }
}