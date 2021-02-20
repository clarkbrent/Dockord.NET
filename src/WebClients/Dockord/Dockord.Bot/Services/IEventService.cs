using DSharpPlus;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;

namespace Dockord.Bot.Services
{
    interface IEventService
    {
        void SetupClientEventHandlers(DiscordClient client);
        void SetupCommandEventHandlers(CommandsNextExtension commands);
    }
}