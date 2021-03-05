using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System.Collections.Generic;

namespace Dockord.Bot.Modules.InteractivityCommands.ConfirmOrDeny
{
    public class ConfirmOrDenyEmojiModel
    {
        public ConfirmOrDenyEmojiModel(CommandContext ctx)
        {
            Confirmed = DiscordEmoji.FromName(ctx.Client, ":white_check_mark:");
            Denied = DiscordEmoji.FromName(ctx.Client, ":x:");
        }

        public DiscordEmoji Confirmed { get; }
        public DiscordEmoji Denied { get; }

        /// <summary>
        /// Create a list of <see cref="DiscordEmoji"/>s that contains each of <see cref="ConfirmOrDenyEmojiModel"/>'s <see cref="DiscordEmoji"/> properties.
        /// </summary>
        public List<DiscordEmoji> ToDiscordEmojiList()
        {
            var emojiList = new List<DiscordEmoji>();

            foreach (var prop in GetType().GetProperties())
            {
                if (prop.GetValue(this) is DiscordEmoji emoji)
                    emojiList.Add(emoji);
            }

            return emojiList;
        }
    }
}
