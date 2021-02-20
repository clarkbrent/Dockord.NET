using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dockord.Bot.Configuration
{
    /// <summary>
    /// Creates strongly typed configuration values from <see cref="IConfiguration"/>.
    /// </summary>
    class DockordConfig : BaseConfig, IDockordConfig
    {
        private readonly IConfiguration _config;
        private readonly SerilogOptions _serilogOptions = new SerilogOptions();
        private readonly BotSettingsOptions _botSettings = new BotSettingsOptions();

        public DockordConfig(IConfiguration config)
        {
            _config = config;

            _config.GetSection(nameof(Serilog))
                   .Bind(_serilogOptions, x => x.BindNonPublicProperties = true);

            _config.GetSection(nameof(BotSettings))
                   .Bind(_botSettings, x => x.BindNonPublicProperties = true);
        }

        public SerilogOptions Serilog => _serilogOptions;
        public BotSettingsOptions BotSettings => _botSettings;

        public LogLevel GetMinimumLogLevel()
        {
            string? minimumLogLevel = Serilog.MinimumLevel?.Default;

            return (minimumLogLevel?.ToLower()) switch
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
}
