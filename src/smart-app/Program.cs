using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace smart_app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "SMART App";

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
