using Dockord.Library.Exceptions;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using System;
using System.Threading.Tasks;

namespace Dockord.Bot.Modules.InteractivityCommands.ConfirmOrDeny
{
    public class ReactionResultHandler
    {
        private readonly CommandContext _ctx;

        public ReactionResultHandler(CommandContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Update the interactivity reaction message, and then send a new result
        /// message to the original channel that the command was executed from.
        /// </summary>
        /// <param name="reactionMessage"></param>
        /// <param name="reactionResult"></param>
        public async Task Result(ReactionMessage reactionMessage, InteractivityResult<MessageReactionAddEventArgs> reactionResult)
        {
            var embedBuilder = new DiscordEmbedBuilder()
                .WithTimestamp(DateTime.UtcNow)
                .WithColor(DiscordColor.Red);

            var reactionEmoji = new ConfirmOrDenyEmojiModel(_ctx);
            string resultText = "Denied";

            if (reactionResult.Result.Emoji == reactionEmoji.Confirmed)
            {
                embedBuilder.Color = DiscordColor.Green;
                resultText = "Confirmed";
            }

            string username = reactionResult.Result.User?.Username ?? "<unknown username>";
            string footerText = $"{reactionResult.Result.Emoji} {resultText} by {username}";
            embedBuilder.WithFooter(footerText);

            await reactionMessage.DeleteAllReactions().ConfigureAwait(false);
            await reactionMessage.Update(embedBuilder).ConfigureAwait(false);

            await SendResultResponse(embedBuilder, resultText).ConfigureAwait(false);
        }

        /// <summary>
        /// Update reaction message with a timeout notice, and then notify the user
        /// who executed the command in a direct message that the command timed out.
        /// </summary>
        /// <param name="reactionMessage"></param>
        public async Task TimeOut(ReactionMessage reactionMessage)
        {
            var errorEmoji = DiscordEmoji.FromName(_ctx.Client, ":no_entry:");
            var embedBuilder = new DiscordEmbedBuilder()
                .WithTimestamp(DateTime.UtcNow)
                .WithFooter($"{errorEmoji} Request timed out")
                .WithColor(DiscordColor.Red);

            await reactionMessage.DeleteAllReactions().ConfigureAwait(false);
            await reactionMessage.Update(embedBuilder).ConfigureAwait(false);

            throw new InteractivityTimedOutException("Command timed out waiting for either approval or denial.");
        }

        /// <summary>
        /// Sends the reaction message result to the channel that the command was
        /// originally executed from.
        /// </summary>
        /// <param name="embed"></param>
        /// <param name="resultText"></param>
        private async Task SendResultResponse(DiscordEmbedBuilder embed, string resultText)
        {
            embed.ClearFields()
                 .WithTitle(null)
                 .WithDescription($"{_ctx.User.Mention}: [Command {resultText.ToLower()}]({_ctx.Message.JumpLink})")
                 .Build();

            await _ctx.RespondAsync(embed).ConfigureAwait(false);
        }
    }
}
