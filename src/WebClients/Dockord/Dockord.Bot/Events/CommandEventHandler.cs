using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Dockord.Bot.Events
{
    class CommandEventHandler : ICommandEventHandler
    {
        private string _commandName = "<unknown command>";

        public CommandEventHandler()
        {
        }

        public Task CommandExecuted(CommandsNextExtension sender, CommandExecutionEventArgs e)
        {
            var username = $"{e.Context.User?.Username ?? "<unknown username>"}#{e.Context.User?.Discriminator ?? "<unknown descriminator>"}";

            e.Context.Client.Logger.LogInformation(DockordEvents.DiscordCmdExec,
                "{UserName} ({UserId}) successfully executed {Command}.",
                username,
                e.Context.User?.Id ?? null,
                e.Command.QualifiedName);

            return Task.CompletedTask;
        }

        public async Task CommandErrored(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            _commandName = e.Command.QualifiedName ?? "<unknown command>";

            // Check if the error is from a result of lack of required permissions
            if (e.Exception is ChecksFailedException)
            {
                LogCommandError(e, "User lacked required permissions for command.");
                await SendCommandErrorMessage(e, "You do not have the required permissions to execute this command");
            }
            else
            {
                LogCommandError(e);
                await SendCommandErrorMessage(e, "There was an unspecified error while executing the command");
            }
        }

        private async Task SendCommandErrorMessage(CommandErrorEventArgs e, string message)
        {
            var emoji = DiscordEmoji.FromName(e.Context.Client, ":no_entry:");
            var embed = new DiscordEmbedBuilder
            {
                Title = "Access denied",
                Description = $"{emoji} {message}: `{_commandName}`",
                Color = DiscordColor.DarkRed
            };

            if (e.Context.User is DiscordMember user)
            {
                await user.SendMessageAsync(embed.Build());
            }
            else
            {
                await e.Context.RespondAsync(embed);
            }
        }

        private void LogCommandError(CommandErrorEventArgs e, string message = "")
        {
            var username = $"{e.Context.User?.Username ?? "<unknown username>"}#{e.Context.User?.Discriminator ?? "<unknown descriminator>"}";
            var channelIsDM = e.Context.Channel?.IsPrivate ?? false;
            var logStack = "{ExceptionType}: {ExceptionMessage} {ChannelId} {ChannelName} {ChannelIsDM} {GuildId} {GuildName}";

            var eventId = e.Exception is ChecksFailedException
                ? DockordEvents.BotCmdsAuthError
                : DockordEvents.BotCmdsError;

            e.Context.Client.Logger.LogError(eventId,
                e.Exception,
                $"{{UserName}} ({{UserId}}) tried executing {{Command}}. {message} {logStack}",
                username,
                e.Context.User?.Id ?? 0,
                _commandName,
                e.Exception.GetType(),
                e.Exception.Message ?? "<no message>",
                e.Context.Channel?.Id ?? 0,
                e.Context.Channel?.Name ?? "<unknown channel>",
                channelIsDM,
                e.Context.Guild?.Id ?? 0,
                e.Context.Guild?.Name ?? "<unknown guild>");
        }
    }
}
