using Dockord.Bot.Services;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace Dockord.Bot
{
    internal static class DockordBotLogger
    {
        /// <summary>
        /// Creates a project specific Serilog <see cref="LoggerConfiguration"/>.
        /// </summary>
        /// <returns><see cref="LoggerConfiguration"/></returns>
        public static Logger Create(IConfiguration config)
        {
            var dockordConfig = new ConfigurationService(config);

            var loggerConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId();

            if (string.IsNullOrWhiteSpace(dockordConfig.Serilog?.SeqUrl))
                loggerConfig.WriteTo.Console();
            else
                loggerConfig.WriteTo.Seq(dockordConfig.Serilog.SeqUrl);

            return loggerConfig.CreateLogger();
        }
    }
}
