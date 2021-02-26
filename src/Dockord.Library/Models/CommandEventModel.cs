namespace Dockord.Library.Models
{
    /// <summary>
    /// A model of properties that store the values of various Discord command events.
    /// </summary>
    /// <remarks>
    /// The order that <see cref="CommandEventModel"/>'s public properties are defined
    /// determines the order in which log template <see cref="string"/> parameters <br />
    /// are generated using <see cref="Extensions.DiscordEventExtensions.ToEventLogTuple"/>.
    /// </remarks>
    public class CommandEventModel : DiscordEventBase, ICommandEventModel
    {
        private string _commandName = "";

        public string? CommandName
        {
            get => _commandName;
            set => _commandName = value ?? "<unknown command>";
        }

        public string? CommandArgs { get; set; }
        public string? ChannelName { get; set; }
        public ulong? ChannelId { get; set; }
        public bool? IsDirectMessage { get; set; }
        public string? GuildName { get; set; }
        public ulong? GuildId { get; set; }
    }
}