using Dockord.Bot.Services;
using Dockord.Library.Events;
using Dockord.Library.Exceptions;
using Dockord.Library.Extensions;
using Dockord.Library.Models;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dockord.Bot.Events
{
    public class CommandEventHandler : ICommandEventHandler
    {
        private readonly IConfigService _config;
        private string? _commandArgs;
        private string? _commandName;
        private bool? _isDirectMessage;

        public CommandEventHandler(IConfigService config)
        {
            _config = config;
        }

        public Task CommandExecuted(CommandsNextExtension sender, CommandExecutionEventArgs e)
        {
            _commandArgs = e.Context.RawArgumentString;
            _commandName = e.Command.QualifiedName;
            EventId eventId = DockordEventId.BotCmdExec;

            LogCommandEvent(e, eventId, eventMessage: "Command executed successfully.");

            return Task.CompletedTask;
        }

        public async Task CommandErrored(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            _commandArgs = e.Context.RawArgumentString;
            _commandName = e.Command?.Name;
            _isDirectMessage = e.Context.Channel?.IsPrivate;
            EventId eventId = DockordEventId.BotCmdError;
            string eventMessage = "Error executing command.";
            string title = "Error occurred";
            string description = "There was an unspecified error while executing the command.";
            string hint = "";

            if (e.Exception is CommandNotFoundException commandNotFoundEx)
            {
                _commandName = commandNotFoundEx.CommandName;
                title = "Not found";
                description = "Specified command was not found.";
            }
            else if (e.Exception is NotFoundException)
            {
                title = "Not found";
                description = "Unable to find an expected resource.";
                hint = "Was a required message or attachment deleted?";
            }
            else if (e.Exception is ChecksFailedException) // Check if the error is from a lack of required permissions
            {
                eventId = DockordEventId.BotCmdAuthError;
                eventMessage = "User lacked required permissions for command.";
                title = "Access denied";
                description = "You do not have the required permissions to execute this command.";
            }
            else if (e.Exception is InteractivityTimedOutException interactivityTimedOutEx)
            {
                title = "Timed out";
                description = interactivityTimedOutEx.Message;
            }
            else if (e.Exception is InvalidOperationException && !string.IsNullOrWhiteSpace(e.Exception.Message))
            {
                description = e.Exception.Message;
            }
            else if (e.Exception is ArgumentException)
            {
                description = "There was an error with the command's argument(s).";
                hint = "Did you forget to add a required argument? " +
                    $"See `{e.Context.Prefix}help {_commandName}` for a full list of the command's available arguments.";
            }

            LogCommandEvent(e, eventId, eventMessage);
            await SendErrorResponse(e, title, description, hint).ConfigureAwait(false);
        }

        private void LogCommandEvent(CommandEventArgs e, EventId eventId, string eventMessage = "")
        {
            var cmdEventModel = new CommandEventModel
            {
                CommandName = _commandName,
                CommandArgs = _commandArgs,
                Username = e.Context.User?.Username,
                UserDiscriminator = e.Context.User?.Discriminator,
                UserId = e.Context.User?.Id,
                ChannelName = e.Context.Channel?.Name,
                ChannelId = e.Context.Channel?.Id,
                IsDirectMessage = _isDirectMessage,
                GuildName = e.Context.Guild?.Name,
                GuildId = e.Context.Guild?.Id,
            };

            (string message, object[] args) = cmdEventModel.ToEventLogTuple(eventMessage);

            if (e is CommandErrorEventArgs commandError)
                e.Context.Client.Logger.LogError(eventId, commandError.Exception, message, args);
            else
                e.Context.Client.Logger.LogInformation(eventId, message, args);
        }

        private async Task SendErrorResponse(CommandErrorEventArgs e, string title, string description, string? hint)
        {
            int deleteSecondsDelay = _config.BotSettings.ErrorMessageDeleteSecondsDelay ?? 15;
            bool deleteMessage = true;
            var emoji = DiscordEmoji.FromName(e.Context.Client, ":no_entry:");
            var embed = new DiscordEmbedBuilder()
                .WithTitle(title)
                .WithDescription($"{emoji} {description}")
                .WithColor(DiscordColor.Red);

            if (string.IsNullOrWhiteSpace(_commandArgs))
                embed.AddField("Command:", $"`{_commandName}`");
            else
                embed.AddField("Command:", $"`{_commandName} {_commandArgs}`");

            if (!string.IsNullOrWhiteSpace(hint))
                embed.AddField("HINT:", hint);

            if (e.Exception is not InteractivityTimedOutException && _isDirectMessage == false)
            {
                embed.WithFooter("NOTE: The message in the jump link that caused the error will be " +
                    $"deleted from the channel automatically after {deleteSecondsDelay} seconds.");
            }
            else
            {
                deleteMessage = false;
            }

            if (_isDirectMessage == false && e.Context.Message?.JumpLink != null)
                embed.AddField("Jump Link", $"{e.Context.Message.JumpLink}");

            if (e.Context.User is DiscordMember user)
                await user.SendMessageAsync(embed).ConfigureAwait(false);
            else
                await e.Context.RespondAsync(embed).ConfigureAwait(false);

            if (deleteMessage)
                await DeleteContextMessage(e, deleteSecondsDelay).ConfigureAwait(false);
        }

        private static async Task DeleteContextMessage(CommandErrorEventArgs e, int deletionDelay = 15)
        {
            await Task.Delay(TimeSpan.FromSeconds(deletionDelay)).ConfigureAwait(false);
            await e.Context.Message.DeleteAsync().ConfigureAwait(false);
        }
    }
}
