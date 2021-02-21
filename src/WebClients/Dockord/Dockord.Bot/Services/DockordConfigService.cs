using Dockord.Bot.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dockord.Bot.Services
{
    /// <summary>
    /// Creates strongly typed configuration values from <see cref="IConfiguration"/>.
    /// </summary>
    internal class DockordConfigService : BaseConfig, IDockordConfigService
    {
        private readonly IConfiguration _config;

        public DockordConfigService(IConfiguration config)
        {
            _config = config;

            _config.GetSection(nameof(BotSettings))
                   .Bind(BotSettings, x => x.BindNonPublicProperties = true);

            _config.GetSection(nameof(Serilog))
                   .Bind(Serilog, x => x.BindNonPublicProperties = true);
        }

        public BotSettingsOptions BotSettings { get; } = new BotSettingsOptions();
        public SerilogOptions Serilog { get; } = new SerilogOptions();

        public LogLevel GetMinimumLogLevel()
        {
            return (Serilog.MinimumLevel?.Default?.ToLower()) switch
            {
                "error" => LogLevel.Error,
                "warn" => LogLevel.Warning,
                "debug" => LogLevel.Debug,
                "information" => LogLevel.Information,
                "critical" => LogLevel.Critical,
                "trace" => LogLevel.Trace,
                _ => LogLevel.Error, // Default case
            };
        }
    }

    internal interface IDockordConfigService
    {
        BotSettingsOptions BotSettings { get; }
        SerilogOptions Serilog { get; }

        LogLevel GetMinimumLogLevel();
    }
}
