using DSharpPlus;
using DSharpPlus.EventArgs;
using System.Threading.Tasks;

namespace Dockord.Bot.Events
{
    interface IClientEventHandler
    {
        Task ClientError(DiscordClient client, ClientErrorEventArgs e);
        Task ClientReady(DiscordClient client, ReadyEventArgs e);
        Task GuildAvailable(DiscordClient client, GuildCreateEventArgs e);
    }
}