using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace Dockord.Bot.Modules.InteractivityCommands.ConfirmOrDeny
{
#pragma warning disable CA1822 // Cannot mark members as static because DSharpPlus injects command modules as a depedency
    public class ConfirmOrDeny : BaseCommandModule
    {
        [Command("confirmordeny")]
        [Description("Creates an interactive message with emojis to react to.")]
        [Aliases("cod")]
        public async Task Execute(CommandContext ctx,
            [Description("A question you would like anyone in the channel to either confirm or deny. " +
            "Sentences must be surrounded with double-quotes. ie; \"Is this real?\"")] string question,
            [Description("**(Optional)** Name of the channel that the question will be sent to, and answered in.")] string channelName = "",
            [Description("**(Optional)** Number of seconds to wait for an answer before the command times out.")] int delay = 60)
        {
            var message = new ReactionMessage(ctx);

            await message.Send(channelName, question).ConfigureAwait(false);
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
