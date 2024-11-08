// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using BackendIntegrationTest_Ameerhamza;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BackendIntegrationTest_Ameerhamza
{
    /// <summary>
    /// Program Class
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main Function
        /// </summary>
        /// <param name="args">Args</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create Host Builder
        /// </summary>
        /// <param name="args">String</param>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
