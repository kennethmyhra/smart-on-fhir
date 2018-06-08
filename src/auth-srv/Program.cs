using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace EHR.AuthorizationServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "EHR Authorization Server";
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
