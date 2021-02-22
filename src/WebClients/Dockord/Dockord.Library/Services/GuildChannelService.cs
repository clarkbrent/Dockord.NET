using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dockord.Library.Services
{
    public static class GuildChannelService
    {
        /// <summary>
        /// Find channel from <see cref="string"/> <paramref name="channelName"/> argument.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="channelName"></param>
        /// <param name="allowDirectMessages"></param>
        public static async Task<DiscordChannel> Find(CommandContext ctx, string channelName, bool allowDirectMessages = false)
        {
            if (allowDirectMessages && ctx.Channel.IsPrivate)
                throw new InvalidOperationException("This command cannot be sent in a direct message.");

            IReadOnlyList<DiscordChannel> channels = await ctx.Guild.GetChannelsAsync().ConfigureAwait(false)
                ?? throw new InvalidOperationException("No channels found on server.");

            return channels.FirstOrDefault(x => x.Name == channelName)
                ?? throw new InvalidOperationException($"Channel named '{channelName}' was not found.");
        }
    }
}