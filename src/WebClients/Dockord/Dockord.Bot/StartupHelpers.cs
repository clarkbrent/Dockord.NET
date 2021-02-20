using Dockord.Bot.Configuration;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace Dockord.Bot
{
    partial class Program
    {
        /// <summary>
        /// Creates a project specific <see cref="IConfigurationBuilder"/>.
        /// </summary>
        /// <returns><see cref="IConfigurationBuilder"/></returns>
        private static IConfigurationBuilder CreateConfigBuilder()
        {
            string currentEnvironment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
            var builder = new ConfigurationBuilder();

            return builder.SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{currentEnvironment}.json", optional: true)
                          .AddEnvironmentVariables();
        }

        /// <summary>
        /// Creates a project specific Serilog <see cref="LoggerConfiguration"/>.
        /// </summary>
        /// <returns><see cref="LoggerConfiguration"/></returns>
        private static LoggerConfiguration SetupLogger(IConfiguration config)
        {
            var dockordConfig = new DockordConfig(config);

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId();

            return string.IsNullOrWhiteSpace(dockordConfig.Serilog?.SeqUrl)
                ? logger.WriteTo.Console()
                : logger.WriteTo.Seq(dockordConfig.Serilog.SeqUrl);
        }
    }
}