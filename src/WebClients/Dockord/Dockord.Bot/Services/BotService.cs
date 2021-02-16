using Dockord.Bot.Configuration;
using Dockord.Bot.Events;
using Dockord.Bot.Modules;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
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
        private readonly IServiceProvider _services;
        private readonly IClientEventHandler _clientEventHandler;
        private readonly ICommandEventHandler _commandEventHandler;

        public BotService(IDockordConfig config, ILogger<BotService> logger, IServiceProvider services,
            IClientEventHandler clientEventHandler, ICommandEventHandler commandEventHandler)
        {
            _config = config;
            _logger = logger;
            _services = services;
            _clientEventHandler = clientEventHandler;
            _commandEventHandler = commandEventHandler;
        }

        public DiscordClient? Client { get; private set; }
        public CommandsNextExtension? Commands { get; private set; }

        public async Task RunAsync()
        {
            SetupClient();
            if (Client == null)
                throw new NullReferenceException("Failed to setup Discord client.");

            SetupCommands();
            if (Commands == null)
                throw new NullReferenceException("Failed to setup Discord commands.");

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

            Client.ClientErrored += _clientEventHandler.ClientError;
            Client.Ready += _clientEventHandler.ClientReady;
            Client.GuildAvailable += _clientEventHandler.GuildAvailable;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                PollBehaviour = PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromMinutes(2),
            });
        }

        private void SetupCommands()
        {
            var commandConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new[] { _config.BotSettings.Prefix },
                EnableDms = true,
                DmHelp = true,
                Services = _services,
            };

            Commands = Client.UseCommandsNext(commandConfig);

            Commands.CommandErrored += _commandEventHandler.CommandErrored;
            Commands.CommandExecuted += _commandEventHandler.CommandExecuted;

            Commands.RegisterCommands<BasicCommands>();
            Commands.RegisterCommands<InteractivityCommands>();
        }
    }
}
