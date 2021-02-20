using DSharpPlus;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;

namespace Dockord.Bot.Services
{
    /// <summary>Initializes a DSharpPlus Discord client, and it's commands.</summary>
    interface IBotService
    {
        DiscordClient Client { get; }
        CommandsNextExtension Commands { get; }

        Task RunAsync();
    }
}