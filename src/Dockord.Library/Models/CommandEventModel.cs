namespace Dockord.Library.Models
{
    /// <summary>
    /// A model of properties that store the values of various Discord command events.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The order that <see cref="CommandEventModel"/>'s public properties are defined
    /// determines the order in which log template <see cref="string"/> parameters <br />
    /// are generated using <see cref="Extensions.DiscordEventExtensions.ToEventLogTuple"/>.
    /// </para>
    /// <para>
    /// <see cref="CommandName"/> should not have a null or whitespace value.
    /// </para>
    /// </remarks>
    public class CommandEventModel : DiscordEventBase, ICommandEventModel
    {
        private string _commandName = "<unknown command>";

        public string CommandName
        {
            get => _commandName;
            set => _commandName = string.IsNullOrWhiteSpace(value)
                ? "<unknown command>"
                : value;
        }

        public string? CommandArgs { get; set; }
        public string? ChannelName { get; set; }
        public ulong? ChannelId { get; set; }
        public bool? IsDirectMessage { get; set; }
        public string? GuildName { get; set; }
        public ulong? GuildId { get; set; }
    }
}