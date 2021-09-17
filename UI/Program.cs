using System;
using Serilog;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Welcome to my store!");

            // new MainMenu(new BL(new ExampleRepo())).Start();

             //Logger
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("../logs/logs.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        
            Log.Information("App starting...");

            new MainMenu().Start();

            Log.Information("App closing...");

            Log.CloseAndFlush();
        }
    }
}
