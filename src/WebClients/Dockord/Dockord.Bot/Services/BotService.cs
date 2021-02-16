using Dockord.Bot.Configuration;
using Dockord.Bot.Events;
using DSharpPlus;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Dockord.Bot.Services
{
    class BotService : IBotService
    {
        private readonly IDockordConfig _config;
        private readonly ILogger<BotService> _logger;

        public BotService(IDockordConfig config, ILogger<BotService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public DiscordClient? Client { get; private set; }

        public async Task RunAsync()
        {
            SetupClient();
            if (Client == null)
                throw new NullReferenceException("Failed to setup Discord client.");

            await Client.ConnectAsync();
        }

        private void SetupClient()
        {
            _logger.LogInformation(DockordEvents.BotClientConfiguring, "Configuring Discord client.");

            var discordConfig = new DiscordConfiguration
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

            Client = new DiscordClient(discordConfig);

            Client.ClientErrored += BotEventHandler.ClientError;
            Client.Ready += BotEventHandler.ClientReady;
            Client.GuildAvailable += BotEventHandler.GuildAvailable;
        }
    }
}
