using Dockord.Library.Extensions;
using Dockord.Library.Models;
using Dockord.Library.Events;
using DSharpPlus;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Dockord.Bot.Events
{
    public class ClientEventHandler : IClientEventHandler
    {
        public Task ClientReady(DiscordClient client, ReadyEventArgs e)
        {
            client.Logger.LogInformation(DockordEventId.BotClientReady,
                "Bot is now ready to process events.");

            return Task.CompletedTask;
        }

        public Task ClientErrored(DiscordClient client, ClientErrorEventArgs e)
        {
            var clientError = new DiscordEventDataModel
            {
                Username = client.CurrentUser?.Username,
                UserDiscriminator = client.CurrentUser?.Discriminator,
                UserId = client.CurrentUser?.Id,
            };

            (string message, object[] args) = clientError.ToEventLogTuple(message: "Discord client error occurred.");

            client.Logger.LogError(DockordEventId.BotClientError, e.Exception, message, args);

            return Task.CompletedTask;
        }

        public Task GuildAvailable(DiscordClient client, GuildCreateEventArgs e)
        {
            var guildAvailable = new DiscordEventDataModel
            {
                GuildName = e.Guild.Name,
                GuildId = e.Guild.Id,
            };

            (string message, object[] args) = guildAvailable.ToEventLogTuple(message: "Guild available.");

            client.Logger.LogInformation(DockordEventId.BotClientGuildAvailable, message, args);

            return Task.CompletedTask;
        }
    }
}
