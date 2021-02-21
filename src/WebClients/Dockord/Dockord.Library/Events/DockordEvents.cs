﻿using Microsoft.Extensions.Logging;

namespace Dockord.Bot.Events
{
    /// <summary>Static Dockord <see cref="EventId"/> library.</summary>
    /// <remarks>
    /// <para>
    /// <see cref="EventId.Id"/> should represent a subset of events followed by an equivalent HTTP status code similar to the operation the event is referencing. <br />
    /// i.e.; "1400" correlates to "1" representing the "BotClient" subset of events, 
    /// and "400" representing an error status code.
    /// </para>
    /// </remarks>
    public static class DockordEvents
    {
        #region BotClient

        public static EventId BotClientConfig { get; } = new EventId(1102, nameof(BotClientConfig));
        public static EventId BotClientStarting { get; } = new EventId(1102, nameof(BotClientStarting));
        public static EventId BotClientGuildAvailable { get; } = new EventId(1200, nameof(BotClientGuildAvailable));
        public static EventId BotClientReady { get; } = new EventId(1201, nameof(BotClientReady));
        public static EventId BotClientError { get; } = new EventId(1400, nameof(BotClientError));

        #endregion

        #region BotCommands

        public static EventId BotCmdsConfig { get; } = new EventId(2102, nameof(BotCmdsConfig));
        public static EventId BotCmdsExec { get; } = new EventId(2200, nameof(BotCmdsExec));
        public static EventId BotCmdsError { get; } = new EventId(2400, nameof(BotCmdsError));
        public static EventId BotCmdsAuthError { get; } = new EventId(2401, nameof(BotCmdsAuthError));

        #endregion
    }
}