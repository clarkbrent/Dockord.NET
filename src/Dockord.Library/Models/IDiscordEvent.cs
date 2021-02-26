namespace Dockord.Library.Models
{
    public interface IDiscordEvent
    {
        string? UserDiscriminator { get; set; }
        ulong? UserId { get; set; }
        string? Username { get; set; }
    }
}