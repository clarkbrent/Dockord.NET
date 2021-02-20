using Dockord.Bot.Events;
using Dockord.Bot.Modules;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dockord.Bot.Services
{
    class BotService : IBotService
    {
        private readonly ILogger<BotService> _logger;
        private readonly IUtilityService _utilityService;
        private readonly IEventService _eventService;

        public BotService(ILogger<BotService> logger, IEventService eventService, IUtilityService utilityService)
        {
            _logger = logger;
            _eventService = eventService;
            _utilityService = utilityService;

            Client = SetClient();
            Commands = SetCommands();
        }

        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync()
        {
            _logger.LogInformation(DockordEvents.BotClientStarting, "Starting bot...");
            await Client.ConnectAsync();

            await Task.Delay(-1); // Run bot forever
        }

        private DiscordClient SetClient()
        {
            _logger.LogInformation(DockordEvents.BotClientConfig, "Configuring bot client...");

            var client = new DiscordClient(config: _utilityService.GetDiscordConfg())
                ?? throw new InvalidOperationException("Failed to configure bot client.");

            _eventService.SetupClientEventHandlers(client);

            client.UseInteractivity(configuration: _utilityService.GetInteractivityConfig());

            return client;
        }

        private CommandsNextExtension SetCommands()
        {
            _logger.LogInformation(DockordEvents.BotCmdsConfig, "Configuring bot commands...");

            CommandsNextExtension commands = Client.UseCommandsNext(cfg: _utilityService.GetCommandsConfig())
                ?? throw new InvalidOperationException("Failed to configure bot commands.");

            _eventService.SetupCommandEventHandlers(commands);

            commands.RegisterCommands<BasicCommands>();
            commands.RegisterCommands<InteractivityCommands>();

            return commands;
        }
    }
}
