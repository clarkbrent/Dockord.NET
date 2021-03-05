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
    public class DiscordConfigService : IDiscordConfigService
    {
        private readonly IConfigService _config;
        private readonly IServiceProvider _services;

        public DiscordConfigService(IConfigService config, IServiceProvider services)
        {
            _config = config;
            _services = services;
        }

        public DiscordConfiguration Client =>
            new DiscordConfiguration
            {
                AlwaysCacheMembers = _config.BotSettings.AlwaysCacheMembers ?? default,
                AutoReconnect = true,
                Intents = DiscordIntents.AllUnprivileged,
                LoggerFactory = new LoggerFactory().AddSerilog(),
                MessageCacheSize = _config.BotSettings.MessageCacheSize ?? default,
                MinimumLogLevel = _config.GetMinimumLogLevel(),
                Token = _config.BotSettings.Token,
                TokenType = TokenType.Bot,
            };

        public CommandsNextConfiguration CommandsNext =>
            new CommandsNextConfiguration
            {
                StringPrefixes = new[] { _config.BotSettings.Prefix },
                EnableDms = true,
                DmHelp = true,
                Services = _services,
            };

        public InteractivityConfiguration Interactivity =>
            new InteractivityConfiguration
            {
                PollBehaviour = PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromMinutes(2),
            };
    }
    public interface IDiscordConfigService
    {
        CommandsNextConfiguration CommandsNext { get; }
        DiscordConfiguration Client { get; }
        InteractivityConfiguration Interactivity { get; }
    }
}
