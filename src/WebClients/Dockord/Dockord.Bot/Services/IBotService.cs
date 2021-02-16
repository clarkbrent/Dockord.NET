using DSharpPlus;
using System.Threading.Tasks;

namespace Dockord.Bot.Services
{
    interface IBotService
    {
        DiscordClient? Client { get; }
        CommandsNextExtension? Commands { get; }

        Task RunAsync();
    }
}