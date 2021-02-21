namespace Dockord.Library.Models
{
    public interface IDiscordEventDataModel
    {
        ulong? ChannelId { get; set; }
        string? ChannelName { get; set; }
        string? CommandArgs { get; set; }
        string? CommandName { get; set; }
        ulong? GuildId { get; set; }
        string? GuildName { get; set; }
        bool? IsDirectMessage { get; set; }
        string? UserDiscriminator { get; set; }
        ulong? UserId { get; set; }
        string? Username { get; set; }
    }
}