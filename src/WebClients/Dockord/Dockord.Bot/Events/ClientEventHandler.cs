using DSharpPlus;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Dockord.Bot.Events
{
    class ClientEventHandler : IClientEventHandler
    {
        public ClientEventHandler()
        {
        }

        public Task ClientReady(DiscordClient client, ReadyEventArgs e)
        {
            client.Logger.LogInformation(DockordEvents.BotClientReady,
                "Bot is now ready to process events.");

            return Task.CompletedTask;
        }

        public Task ClientError(DiscordClient client, ClientErrorEventArgs e)
        {
            var username = $"{client.CurrentUser?.Username ?? "<unknown username>"}#{client.CurrentUser?.Discriminator ?? "<unknown descriminator>"}";

            client.Logger.LogError(DockordEvents.BotClientError,
                e.Exception,
                "Exception occured on the Discord client. {UserName}",
                username);

            return Task.CompletedTask;
        }

        public Task GuildAvailable(DiscordClient client, GuildCreateEventArgs e)
        {
            client.Logger.LogInformation(DockordEvents.BotClientGuildAvailable,
                "Guild available: {GuildName} ({GuildId})",
                e.Guild.Name,
                e.Guild.Id);

            return Task.CompletedTask;
        }

    }
}
