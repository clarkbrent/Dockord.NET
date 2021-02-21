using Dockord.Bot.Events;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace Dockord.Bot.Services
{
    /// <summary>
    /// Initializes event handlers for various Dockord, and DSharpPlus events.
    /// </summary>
    internal class EventService : IEventService
    {
        private readonly IClientEventHandler _clientEventHandler;
        private readonly ICommandEventHandler _commandEventHandler;

        public EventService(IClientEventHandler clientEventHandler, ICommandEventHandler commandEventHandler)
        {
            _clientEventHandler = clientEventHandler;
            _commandEventHandler = commandEventHandler;
        }

        public void SetupClientEventHandlers(DiscordClient client)
        {
            client.ClientErrored += _clientEventHandler.ClientError;
            client.Ready += _clientEventHandler.ClientReady;
            client.GuildAvailable += _clientEventHandler.GuildAvailable;
        }

        public void SetupCommandEventHandlers(CommandsNextExtension commands)
        {
            commands.CommandErrored += _commandEventHandler.CommandErrored;
            commands.CommandExecuted += _commandEventHandler.CommandExecuted;
        }
    }

    internal interface IEventService
    {
        void SetupClientEventHandlers(DiscordClient client);
        void SetupCommandEventHandlers(CommandsNextExtension commands);
    }
}
