using Dockord.Library.Services;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dockord.Bot.Modules.InteractivityCommands.ConfirmOrDeny
{
    public class ReactionMessage
    {
        private readonly CommandContext _ctx;
        private readonly List<DiscordEmoji> _reactionEmojis;
        private DiscordMessage? _reactionMessage;

        public ReactionMessage(CommandContext ctx)
        {
            _ctx = ctx;
            _reactionEmojis = new ConfirmOrDenyEmojiModel(_ctx).ToDiscordEmojiList();
        }

        public DiscordChannel? Channel { get; private set; }
        public string Question { get; private set; } = "";

        /// <summary>
        /// Delete all reactions associated with instance of <see cref="ReactionMessage"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task DeleteAllReactions()
        {
            if (_reactionMessage == null)
                throw new InvalidOperationException("Could not find the reaction message.");

            await _reactionMessage.DeleteAllReactionsAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Send an interactivity message with reactions to select from.
        /// </summary>
        public async Task Send(string? channelName, string question)
        {
            Channel = string.IsNullOrWhiteSpace(channelName)
                ? _ctx.Channel
                : await GuildChannelService.Find(_ctx, channelName).ConfigureAwait(false);

            Question = question;

            var embedBuilder = new DiscordEmbedBuilder()
                .WithTitle("Choose:")
                .WithDescription(Question);

            _reactionMessage = await Channel.SendMessageAsync(embed: embedBuilder).ConfigureAwait(false);

            foreach (var emoji in _reactionEmojis) // Attach all reaction emojis to the message
            {
                await _reactionMessage.CreateReactionAsync(emoji).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Update reaction message.
        /// </summary>
        /// <param name="embedBuilder"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task Update(DiscordEmbedBuilder embedBuilder)
        {
            if (_reactionMessage == null)
                throw new InvalidOperationException("Could not find the reaction message.");

            embedBuilder.WithTitle("Choose:")
                        .WithDescription(Question);

            await _reactionMessage.ModifyAsync(embed: embedBuilder.Build()).ConfigureAwait(false);
        }

        /// <summary>
        /// Wait for interactivity to return a reaction result <see cref="DiscordMessage"/> <paramref name="reactionMessage"/>,
        /// and return result.
        /// </summary>
        /// <param name="delay"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<InteractivityResult<MessageReactionAddEventArgs>> WaitForResult(int delay)
        {
            if (_reactionMessage == null)
                throw new InvalidOperationException("Could not find the reaction message.");

            InteractivityExtension interactivity = _ctx.Client.GetInteractivity();

            return await interactivity.WaitForReactionAsync(x =>
                !x.User.IsBot
                && x.Message.Id == _reactionMessage.Id
                && _reactionEmojis.Contains(x.Emoji),
                timeoutoverride: TimeSpan.FromSeconds(delay)).ConfigureAwait(false);
        }
    }
}
