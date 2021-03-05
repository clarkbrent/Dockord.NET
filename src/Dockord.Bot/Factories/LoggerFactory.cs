using Dockord.Bot.Services;
using Serilog;
using System;

namespace Dockord.Bot.Factories
{
    internal static class LoggerFactory
    {
        public static ILogger Create()
        {
            IConfigService config = ConfigService.Get();

            var loggerConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(config.GetSerilogSection())
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId();

            if (string.IsNullOrWhiteSpace(config.Serilog?.SeqUrl))
                loggerConfig.WriteTo.Console();
            else
                loggerConfig.WriteTo.Seq(config.Serilog.SeqUrl);

            ILogger logger = loggerConfig.CreateLogger();

            if (logger == null)
                throw new InvalidOperationException("Error building logger.");

            return logger;
        }
    }
}
