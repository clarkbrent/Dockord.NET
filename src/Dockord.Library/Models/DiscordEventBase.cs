namespace Dockord.Library.Models
{
    /// <summary>
    /// A base class of properties that store the values of various Discord events.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The order that <see cref="DiscordEventBase"/>'s public properties are defined
    /// determines the order in which log template <see cref="string"/> parameters <br />
    /// are generated using <see cref="Extensions.DiscordEventExtensions.ToEventLogTuple"/>.
    /// </para>
    /// <para>
    /// <see cref="Username"/> should not have a null or whitespace value.
    /// </para>
    /// </remarks>
    public abstract class DiscordEventBase : IDiscordEvent
    {
        private string _username = "<unknown username>";

        public string Username
        {
            get => _username;
            set => _username = string.IsNullOrWhiteSpace(value)
                ? "<unknown username>"
                : value;
        }

        public string? UserDiscriminator { get; set; }
        public ulong? UserId { get; set; }
    }
}
