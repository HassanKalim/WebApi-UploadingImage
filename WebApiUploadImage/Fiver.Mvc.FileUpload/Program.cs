﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Fiver.Mvc.FileUpload
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
        //test
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
