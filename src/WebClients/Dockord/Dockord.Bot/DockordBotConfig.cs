using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Dockord.Bot
{
    internal static class DockordBotConfig
    {
        /// <summary>
        /// Creates a project specific <see cref="IConfigurationBuilder"/>.
        /// </summary>
        /// <returns><see cref="IConfigurationBuilder"/></returns>
        public static IConfiguration Create()
        {
            string currentEnvironment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
            var builder = new ConfigurationBuilder();

            return builder.SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{currentEnvironment}.json", optional: true)
                          .AddEnvironmentVariables()
                          .Build();
        }
    }
}