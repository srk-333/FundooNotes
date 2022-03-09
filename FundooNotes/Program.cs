//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Saurav">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooNotes{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// built in class
    /// </summary>
    public class Program    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)        {            CreateHostBuilder(args).Build().Run();        }

        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns> host of application </returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>            Host.CreateDefaultBuilder(args)                .ConfigureWebHostDefaults(webBuilder =>                {                    webBuilder.UseStartup<Startup>();                });    }}