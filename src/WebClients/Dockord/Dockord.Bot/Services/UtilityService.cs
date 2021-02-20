using Dockord.Bot.Configuration;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace Dockord.Bot.Services
{
    /// <summary>
    /// Helper service for initializing DSharpPlus configuration values
    /// </summary>
    class SetupService : IUtilityService
    {
        private readonly IDockordConfig _config;
        private readonly IServiceProvider _services;

        public SetupService(IDockordConfig config, IServiceProvider services)
        {
            _config = config;
            _services = services;
        }

        public DiscordConfiguration GetDiscordConfg() =>
            new DiscordConfiguration
            {
                AlwaysCacheMembers = _config.BotSettings.AlwaysCacheMembers ?? default,
                AutoReconnect = true,
                Intents = DiscordIntents.AllUnprivileged,
                LoggerFactory = new LoggerFactory().AddSerilog(Log.Logger),
                MessageCacheSize = _config.BotSettings.MessageCacheSize ?? default,
                MinimumLogLevel = _config.GetMinimumLogLevel(),
                Token = _config.BotSettings.Token,
                TokenType = TokenType.Bot,
            };

        public CommandsNextConfiguration GetCommandsConfig() =>
            new CommandsNextConfiguration
            {
                StringPrefixes = new[] { _config.BotSettings.Prefix },
                EnableDms = true,
                DmHelp = true,
                Services = _services,
            };

        public InteractivityConfiguration GetInteractivityConfig() =>
            new InteractivityConfiguration
            {
                PollBehaviour = PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromMinutes(2),
            };
    }
}
