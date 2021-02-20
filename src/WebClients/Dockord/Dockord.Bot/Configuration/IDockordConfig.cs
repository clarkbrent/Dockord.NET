using Microsoft.Extensions.Logging;

namespace Dockord.Bot.Configuration
{
    interface IDockordConfig
    {
        BotSettingsOptions BotSettings { get; }
        SerilogOptions Serilog { get; }

        LogLevel GetMinimumLogLevel();
    }
}