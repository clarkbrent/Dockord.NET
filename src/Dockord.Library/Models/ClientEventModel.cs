namespace Dockord.Library.Models
{
    /// <summary>
    /// A model of properties that store the values of various Discord client events.
    /// </summary>
    /// <remarks>
    /// The order that <see cref="ClientEventModel"/>'s public properties are defined
    /// determines the order in which log template <see cref="string"/> parameters <br />
    /// are generated using <see cref="Extensions.DiscordEventExtensions.ToEventLogTuple"/>.
    /// </remarks>
    public class ClientEventModel : DiscordEventBase, IClientEventModel
    {
        public string? GuildName { get; set; }
        public ulong? GuildId { get; set; }
    }
}
