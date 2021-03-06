using Dockord.Bot.Modules;
using Dockord.Bot.Modules.InteractivityCommands.ConfirmOrDeny;
using Dockord.Library.Events;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dockord.Bot.Services
{
    public class BotService : IBotService
    {
        private readonly ILogger<BotService> _logger;
        private readonly IDiscordConfigService _discordConfig;
        private readonly IDiscordEventService _eventService;

        public BotService(ILogger<BotService> logger, IDiscordEventService eventService, IDiscordConfigService discordConfig)
        {
            _logger = logger;
            _eventService = eventService;
            _discordConfig = discordConfig;

            Client = SetClient();
            Commands = SetCommands();
        }

        public DiscordClient Client { get; }
        public CommandsNextExtension Commands { get; }

        public async Task RunAsync()
        {
            _logger.LogInformation(DockordEventId.BotClientStarting, "Starting bot...");

            await Client.ConnectAsync().ConfigureAwait(false);

            await Task.Delay(-1).ConfigureAwait(false); // Run bot forever
        }

        private DiscordClient SetClient()
        {
            _logger.LogInformation(DockordEventId.BotClientConfig, "Configuring bot client...");

            var client = new DiscordClient(_discordConfig.Client);

            _eventService.SetupClientEventHandlers(client);
            client.UseInteractivity(_discordConfig.Interactivity);

            return client;
        }

        private CommandsNextExtension SetCommands()
        {
            _logger.LogInformation(DockordEventId.BotCmdModuleConfig, "Configuring bot commands...");

            CommandsNextExtension commands = Client.UseCommandsNext(_discordConfig.CommandsNext)
                ?? throw new InvalidOperationException("Failed to configure bot commands.");

            _eventService.SetupCommandEventHandlers(commands);

            commands.RegisterCommands<BasicCommands>();
            commands.RegisterCommands<ConfirmOrDeny>();

            return commands;
        }
    }

    /// <summary>Initializes a DSharpPlus Discord client, and it's commands.</summary>
    public interface IBotService
    {
        DiscordClient Client { get; }
        CommandsNextExtension Commands { get; }

        Task RunAsync();
    }
}
