using Dockord.Library.Models;
using Xunit;

namespace Dockord.Library.Tests.Extensions
{
    public class ValidDiscordEventModels : TheoryData<IDiscordEvent>
    {
        private const ulong _channelId = 2345678901;
        private const string _channelName = "channel-name";
        private const string _commandName = "confirmordeny";
        private const ulong _guildId = 3456789012;
        private const string _guildName = "test-guild";
        private const string _userDiscriminator = "1234";
        private const string _username = "unittestuser";
        private const ulong _userId = 1234567890;

        public ValidDiscordEventModels()
        {
            IDiscordEvent validCmdEventModel = new CommandEventModel
            {
                CommandName = _commandName,
                CommandArgs = $"{_channelName} 90",
                Username = _username,
                UserDiscriminator = _userDiscriminator,
                UserId = _userId,
                ChannelName = _channelName,
                ChannelId = _channelId,
                IsDirectMessage = false,
                GuildName = _guildName,
                GuildId = _guildId,
            };
            Add(validCmdEventModel);

            IDiscordEvent directMessageCmdEventModel = new CommandEventModel
            {
                CommandName = "help",
                CommandArgs = _commandName,
                Username = _username,
                UserDiscriminator = _userDiscriminator,
                UserId = _userId,
                ChannelId = _channelId,
                IsDirectMessage = true,
            };
            Add(directMessageCmdEventModel);

            IDiscordEvent noUserData = new CommandEventModel
            {
                CommandName = _commandName,
                CommandArgs = $"{_channelName} 90",
                ChannelName = _channelName,
                ChannelId = _channelId,
                IsDirectMessage = false,
                GuildName = _guildName,
                GuildId = _guildId,
            };
            Add(noUserData);

            IDiscordEvent noCommandName = new CommandEventModel
            {
                Username = _username,
                UserDiscriminator = _userDiscriminator,
                UserId = _userId,
                ChannelId = _channelId,
                IsDirectMessage = true,
            };
            Add(noCommandName);

            IDiscordEvent nullUsername = new CommandEventModel
            {
                CommandName = _commandName,
                CommandArgs = $"{_channelName} 90",
                ChannelName = _channelName,
                ChannelId = _channelId,
                IsDirectMessage = false,
                GuildName = _guildName,
                GuildId = _guildId,
                Username = null!,
            };
            Add(nullUsername);

            IDiscordEvent nullCommandName = new CommandEventModel
            {
                CommandName = null!,
                Username = _username,
                UserDiscriminator = _userDiscriminator,
                UserId = _userId,
                ChannelId = _channelId,
                IsDirectMessage = true,
            };
            Add(nullCommandName);

            IDiscordEvent whitespaceUsername = new CommandEventModel
            {
                CommandName = _commandName,
                CommandArgs = $"{_channelName} 90",
                ChannelName = _channelName,
                ChannelId = _channelId,
                IsDirectMessage = false,
                GuildName = _guildName,
                GuildId = _guildId,
                Username = " ",
            };
            Add(whitespaceUsername);

            IDiscordEvent whitespaceCommandName = new CommandEventModel
            {
                CommandName = " ",
                Username = _username,
                UserDiscriminator = _userDiscriminator,
                UserId = _userId,
                ChannelId = _channelId,
                IsDirectMessage = true,
            };
            Add(whitespaceCommandName);

            IDiscordEvent validClientEventModel = new ClientEventModel
            {
                Username = _username,
                UserDiscriminator = _userDiscriminator,
                UserId = _userId,
            };
            Add(validClientEventModel);

            IDiscordEvent guildClientEventModel = new ClientEventModel
            {
                Username = _username,
                UserDiscriminator = _userDiscriminator,
                UserId = _userId,
                GuildName = _guildName,
                GuildId = _guildId,
            };
            Add(guildClientEventModel);
        }
    }
}
