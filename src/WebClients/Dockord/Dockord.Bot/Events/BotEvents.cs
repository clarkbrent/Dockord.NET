using Microsoft.Extensions.Logging;

namespace Dockord.Bot.Events
{
    public class DockordEvents
    {
        #region Client EventIds
        public static readonly EventId BotClientConfiguring = new EventId(9102, nameof(BotClientConfiguring));
        public static readonly EventId BotClientError = new EventId(9400, nameof(BotClientError));
        public static readonly EventId BotClientGuildAvailable = new EventId(9000, nameof(BotClientGuildAvailable));
        public static readonly EventId BotClientReady = new EventId(9200, nameof(BotClientReady));
        #endregion

    }
}
