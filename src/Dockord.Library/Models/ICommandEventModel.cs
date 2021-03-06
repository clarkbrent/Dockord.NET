namespace Dockord.Library.Models
{
    public interface ICommandEventModel
    {
        ulong? ChannelId { get; set; }
        string? ChannelName { get; set; }
        string? CommandArgs { get; set; }
        string CommandName { get; set; }
        ulong? GuildId { get; set; }
        string? GuildName { get; set; }
        bool? IsDirectMessage { get; set; }
    }
}