using Microsoft.Extensions.Logging;

namespace Dockord.Bot.Events
{
    public static class DockordEvents
    {
        #region Client

        public static EventId BotClientConfiguring { get; } = new EventId(9102, nameof(BotClientConfiguring));
        public static EventId BotClientError { get; } = new EventId(9400, nameof(BotClientError));
        public static EventId BotClientGuildAvailable { get; } = new EventId(9000, nameof(BotClientGuildAvailable));
        public static EventId BotClientReady { get; } = new EventId(9200, nameof(BotClientReady));

        #endregion

        #region Commands

        public static EventId DiscordCmdError { get; } = new EventId(8400, nameof(DiscordCmdError));
        public static EventId DiscordCmdExec { get; } = new EventId(8200, nameof(DiscordCmdExec));
        public static EventId DiscordCmdAuthError { get; } = new EventId(8401, nameof(DiscordCmdAuthError));

        #endregion
    }
}
