using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace Dockord.Bot.Modules.InteractivityCommands.ConfirmOrDeny
{
#pragma warning disable CA1822 // Cannot mark members as static because DSharpPlus injects command modules as a depedency
    public class ConfirmOrDeny : BaseCommandModule
    {
        [Command("confirmordeny")]
        [Description("Creates an interactive confirm or deny emoji reaction message.")]
        [Aliases("cod")]
        public async Task Execute(CommandContext ctx,
            [Description("Name of the channel that the reaction message will be sent to.")] string channelName,
            [Description("Number of seconds to wait for a reaction before the response times out.")] int delay)
        {
            var message = new ReactionMessage(ctx, channelName);

            await message.Send().ConfigureAwait(false);

            var result = await message.WaitForResult(delay).ConfigureAwait(false);
            var handle = new ReactionResultHandler(ctx);

            if (result.TimedOut)
                await handle.TimeOut(message).ConfigureAwait(false);
            else
                await handle.Result(message, result).ConfigureAwait(false);
        }
    }
#pragma warning restore CA1822 // Mark members as static
}
