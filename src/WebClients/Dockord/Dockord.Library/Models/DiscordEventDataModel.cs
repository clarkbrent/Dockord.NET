namespace Dockord.Library.Models
{
    /// <summary>
    /// A model of properties that store the values of various Discord events.
    /// </summary>
    /// <remarks>
    /// The order that <see cref="DiscordEventDataModel"/>'s public properties are defined
    /// determines the order in which log template <see cref="string"/> parameters  <br />
    /// are generated using <see cref="Extensions.DiscordEventDataExtensions.ToEventLogTuple"/>.
    /// </remarks>
    public class DiscordEventDataModel : IDiscordEventDataModel
    {
        private string _commandName = "";
        private string _username = "";

        public string? CommandName
        {
            get => _commandName;
            set => _commandName = value ?? "<unknown command>";
        }

        public string? CommandArgs { get; set; }

        public string? Username
        {
            get => _username;
            set => _username = value ?? "<unknown user>";
        }

        public string? UserDiscriminator { get; set; }

        public ulong? UserId { get; set; }

        public string? ChannelName { get; set; }

        public ulong? ChannelId { get; set; }

        public bool? IsDirectMessage { get; set; }

        public string? GuildName { get; set; }

        public ulong? GuildId { get; set; }
    }
}