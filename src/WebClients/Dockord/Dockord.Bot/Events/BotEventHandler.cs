using DSharpPlus;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Dockord.Bot.Events
{
    class BotEventHandler
    {
        #region Client Event Handlers
        public static Task ClientReady(DiscordClient client, ReadyEventArgs e)
        {
            client.Logger.LogInformation(DockordEvents.BotClientReady,
                "Discord client is ready to process events.");
            return Task.CompletedTask;
        }

        public static Task ClientError(DiscordClient client, ClientErrorEventArgs e)
        {
            client.Logger.LogError(DockordEvents.BotClientError, e.Exception,
                "Exception occured on the Discord client.");
            return Task.CompletedTask;
        }

        public static Task GuildAvailable(DiscordClient client, GuildCreateEventArgs e)
        {
            client.Logger.LogInformation(DockordEvents.BotClientGuildAvailable,
                "Guild available: {GuildName} ({GuildId})",
                e.Guild.Name,
                e.Guild.Id);
            return Task.CompletedTask;
        }
        #endregion
    }
}
