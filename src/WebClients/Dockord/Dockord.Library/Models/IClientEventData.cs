namespace Dockord.Library.Models
{
    public interface IClientEventData
    {
        ulong? GuildId { get; set; }
        string? GuildName { get; set; }
        string? UserDiscriminator { get; set; }
        ulong? UserId { get; set; }
        string? Username { get; set; }
    }
}
