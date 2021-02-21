namespace Dockord.Bot.Configuration
{
    public class BotSettingsOptions : BaseConfig
    {
        public bool? AlwaysCacheMembers { get; private protected set; }
        public int? MessageCacheSize { get; private protected set; }
        public string? Prefix { get; private protected set; }
        public string? Token { get; private protected set; }
    }
}
