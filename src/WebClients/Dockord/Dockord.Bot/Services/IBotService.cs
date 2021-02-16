using DSharpPlus;
using System.Threading.Tasks;

namespace Dockord.Bot.Services
{
    interface IBotService
    {
        DiscordClient? Client { get; }

        Task RunAsync();
    }
}