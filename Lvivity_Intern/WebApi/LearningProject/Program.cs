﻿using LearningProject.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System; 
using System.Threading.Tasks;
 

namespace LearningProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
             CreateWebHostBuilder(args).Build().Run();             
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
