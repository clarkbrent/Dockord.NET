using Dockord.Bot.Events;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace Dockord.Bot.Services
{
    /// <summary>
    /// Initializes event handlers for various DSharpPlus events.
    /// </summary>
    public class DiscordEventService : IDiscordEventService
    {
        private readonly IClientEventHandler _clientEventHandler;
        private readonly ICommandEventHandler _commandEventHandler;

        public DiscordEventService(IClientEventHandler clientEventHandler, ICommandEventHandler commandEventHandler)
        {
            _clientEventHandler = clientEventHandler;
            _commandEventHandler = commandEventHandler;
        }

        public void SetupClientEventHandlers(DiscordClient client)
        {
            client.ClientErrored += _clientEventHandler.ClientErrored;
            client.Ready += _clientEventHandler.ClientReady;
            client.GuildAvailable += _clientEventHandler.GuildAvailable;
        }

        public void SetupCommandEventHandlers(CommandsNextExtension commands)
        {
            commands.CommandErrored += _commandEventHandler.CommandErrored;
            commands.CommandExecuted += _commandEventHandler.CommandExecuted;
        }
    }

    public interface IDiscordEventService
    {
        void SetupClientEventHandlers(DiscordClient client);
        void SetupCommandEventHandlers(CommandsNextExtension commands);
    }
}
