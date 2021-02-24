using Dockord.Bot.Configuration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Dockord.Bot.Services
{
    /// <summary>
    /// Creates strongly typed configuration values from <see cref="IConfiguration"/>.
    /// </summary>
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _config;

        public ConfigurationService(IConfiguration config)
        {
            _config = config;

            _config.GetSection(nameof(BotSettings))
                   .Bind(BotSettings, x => x.BindNonPublicProperties = true);

            _config.GetSection(nameof(Serilog))
                   .Bind(Serilog, x => x.BindNonPublicProperties = true);
        }

        public SectionBotSettings BotSettings { get; } = new SectionBotSettings();
        public SectionSerilog Serilog { get; } = new SectionSerilog();

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

    public interface IConfigurationService
    {
        SectionBotSettings BotSettings { get; }
        SectionSerilog Serilog { get; }

        LogLevel GetMinimumLogLevel();
    }
}
