using Dockord.Bot.Services;
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
        public static Logger Create()
        {
            DockordBotConfig config = DockordBotConfig.Get();

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

            return loggerConfig.CreateLogger();
        }
    }
}
