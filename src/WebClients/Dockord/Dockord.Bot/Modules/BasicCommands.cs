using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace Dockord.Bot.Modules
{
    class BasicCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("Example ping command.")]
        [Aliases("pong")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            var emoji = DiscordEmoji.FromName(ctx.Client, ":ping_pong:");

            await ctx.RespondAsync($"{emoji} Pong! Ping: {ctx.Client.Ping}ms");
        }

        [Command("greet")]
        [Description("Says hi to specified user.")]
        [Aliases("sayhi", "say_hi")]
        public async Task Greet(CommandContext ctx, [Description("The user to say hi to.")] DiscordMember member)
        {
            await ctx.TriggerTypingAsync();

            var emoji = DiscordEmoji.FromName(ctx.Client, ":wave:");

            await ctx.RespondAsync($"{emoji} Hello, {member.Mention}!");
        }

        [Command("deletemessages")]
        [Description("Deletes the number of messages specified from the channel.")]
        [Aliases("dmsgs")]
        [RequireOwner]
        public async Task DeleteMessages(CommandContext ctx, [Description("The amount of messages to delete.")] int limit = 100)
        {
            var messages = await ctx.Channel.GetMessagesAsync(limit);

            await ctx.Channel.DeleteMessagesAsync(messages);
        }
    }
}
