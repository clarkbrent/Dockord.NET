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
        private readonly string _channelName;
        private readonly List<DiscordEmoji> _reactionEmojis;
        private DiscordMessage? _reactionMessage;

        public ReactionMessage(CommandContext ctx, string channelName)
        {
            _ctx = ctx;
            _channelName = channelName;
            _reactionEmojis = new ConfirmOrDenyEmojiModel(_ctx).ToDiscordEmojiList();
        }

        public ulong Id { get; private set; }

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
        public async Task Send()
        {
            DiscordChannel channel = await GuildChannelService.Find(_ctx, _channelName).ConfigureAwait(false);

            var embedBuilder = new DiscordEmbedBuilder()
                .WithTitle("Choose:")
                .WithDescription("Confirm or deny?")
                .AddField("Jump Link", $"[Original Message]({_ctx.Message.JumpLink})");

            _reactionMessage = await channel.SendMessageAsync(embed: embedBuilder).ConfigureAwait(false);
            Id = _reactionMessage.Id;

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
                        .WithDescription("Confirm or deny?")
                        .AddField("Jump Link", $"[Original Message]({_ctx.Message.JumpLink})");

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

            var interactivity = _ctx.Client.GetInteractivity();

            return await interactivity.WaitForReactionAsync(x =>
                !x.User.IsBot
                && x.Message.Id == _reactionMessage.Id
                && _reactionEmojis.Contains(x.Emoji),
                timeoutoverride: TimeSpan.FromMilliseconds(delay)).ConfigureAwait(false);
        }
    }
}
