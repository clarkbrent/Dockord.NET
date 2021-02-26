namespace Dockord.Library.Models
{
    /// <summary>
    /// A base class of properties that store the values of various Discord events.
    /// </summary>
    /// <remarks>
    /// The order that <see cref="DiscordEventBase"/>'s public properties are defined
    /// determines the order in which log template <see cref="string"/> parameters <br />
    /// are generated using <see cref="Extensions.DiscordEventExtensions.ToEventLogTuple"/>.
    /// </remarks>
    public abstract class DiscordEventBase : IDiscordEvent
    {
        private string _username = "";

        public string? Username
        {
            get => _username;
            set => _username = value ?? "<unknown user>";
        }

        public string? UserDiscriminator { get; set; }
        public ulong? UserId { get; set; }
    }
}
