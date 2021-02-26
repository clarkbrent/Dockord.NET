namespace Dockord.Library.Models
{
    public interface IClientEventModel : IDiscordEvent
    {
        ulong? GuildId { get; set; }
        string? GuildName { get; set; }
    }
}