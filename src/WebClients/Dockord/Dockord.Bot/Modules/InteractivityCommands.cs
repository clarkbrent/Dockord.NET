using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dockord.Bot.Modules
{
    class InteractivityCommands : BaseCommandModule
    {
        [Command("respondreaction")]
        [Description("Creates an interactive emoji reaction dialog.")]
        public async Task RespondReaction(CommandContext ctx,
            [Description("Name of the channel that the reaction message will be sent to.")] string channelName,
            [Description("Number of seconds to wait for a reaction before the response times out.")] int delay)
        {
            IReadOnlyList<DiscordChannel> channels = await ctx.Guild.GetChannelsAsync();
            if (channels == null)
                throw new NullReferenceException("No channels found on server.");

            DiscordChannel channel = channels.Where(x => x.Name == channelName).FirstOrDefault();
            if (channel == null)
                throw new NullReferenceException($"Channel named '{channelName}' not found.");

            InteractivityExtension interactivity = ctx.Client.GetInteractivity();

            // Emojis for user(s) react to
            DiscordEmoji confirmEmoji = DiscordEmoji.FromName(ctx.Client, ":white_check_mark:");
            DiscordEmoji denyEmoji = DiscordEmoji.FromName(ctx.Client, ":x:");

            var reactionEmbed = new DiscordEmbedBuilder
            {
                Title = "Choose:",
                Description = "Confirm or deny?",
            };
            reactionEmbed.AddField("Jump Link", $"[Original Message]({ctx.Message.JumpLink})");

            DiscordMessage reactionMessage = await SendReactionMessage(channel, confirmEmoji, denyEmoji, reactionEmbed);

            var result = await interactivity.WaitForReactionAsync(x =>
                x.User.IsBot == false
                && (x.Emoji == confirmEmoji || x.Emoji == denyEmoji)
                && x.Channel.Id == channel.Id
                && x.Message.Id == reactionMessage.Id,
                TimeSpan.FromMilliseconds(delay));

            reactionEmbed.Timestamp = DateTime.UtcNow;

            if (result.TimedOut == false)
            {
                await HandleSuccessResult(ctx, confirmEmoji, reactionEmbed, reactionMessage, result);
            }
            else
            {
                await HandleTimeoutResult(ctx, denyEmoji, reactionEmbed, reactionMessage);
            }
        }

        private static async Task<DiscordMessage> SendReactionMessage(DiscordChannel channel, DiscordEmoji confirmEmoji,
            DiscordEmoji denyEmoji, DiscordEmbedBuilder reactionEmbed)
        {
            DiscordMessage reactionMessage = await channel.SendMessageAsync(embed: reactionEmbed);
            await reactionMessage.CreateReactionAsync(confirmEmoji);
            await reactionMessage.CreateReactionAsync(denyEmoji);

            return reactionMessage;
        }

        private static async Task HandleSuccessResult(CommandContext ctx, DiscordEmoji confirmEmoji,
            DiscordEmbedBuilder reactionEmbed, DiscordMessage reactionMessage,
            InteractivityResult<MessageReactionAddEventArgs> result)
        {
            bool isConfirmEmoji = result.Result.Emoji == confirmEmoji;

            var selectedReaction = isConfirmEmoji
                ? "Confirmed"
                : "Denied";

            reactionEmbed.Color = isConfirmEmoji
                ? DiscordColor.Green
                : DiscordColor.Red;

            string username = result.Result.User is DiscordMember member
                ? member.DisplayName
                : result.Result.Message.Author.Username;

            reactionEmbed.Footer = new DiscordEmbedBuilder.EmbedFooter
            {
                Text = $"{result.Result.Emoji} {selectedReaction} by {username}",
            };

            // Update original reaction message with selection choice
            await reactionMessage.DeleteAllReactionsAsync();
            await reactionMessage.ModifyAsync(embed: reactionEmbed.Build());

            reactionEmbed.Title = null;
            reactionEmbed.ClearFields();
            reactionEmbed.Description = $"{ctx.User.Mention}: [Command {selectedReaction}]({ctx.Message.JumpLink})";

            // Send result notification to channel that command was originally sent from
            await ctx.Message.RespondAsync(embed: reactionEmbed);
        }

        private async Task HandleTimeoutResult(CommandContext ctx, DiscordEmoji denyEmoji,
            DiscordEmbedBuilder reactionEmbed, DiscordMessage reactionMessage)
        {
            reactionEmbed.Color = DiscordColor.Red;
            reactionEmbed.Footer = new DiscordEmbedBuilder.EmbedFooter
            {
                Text = $"{denyEmoji} Request timed out",
            };

            // Update channel that reaction response was expected in
            await reactionMessage.DeleteAllReactionsAsync();
            await reactionMessage.ModifyAsync(embed: reactionEmbed.Build());

            reactionEmbed.Title = null;
            reactionEmbed.ClearFields();
            reactionEmbed.Description = $"{ctx.User.Mention}: [Command Timed Out]({ctx.Message.JumpLink})";

            // Send timeout notification to channel that command was originally sent from
            await ctx.RespondAsync(embed: reactionEmbed);

            throw new TimeoutException("Response timed out.");
        }
    }
}
