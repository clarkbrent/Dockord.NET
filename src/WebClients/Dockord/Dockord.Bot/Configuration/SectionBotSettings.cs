namespace Dockord.Bot.Configuration.Options
{
    public class SectionBotSettings
    {
        public bool? AlwaysCacheMembers { get; private protected set; }
        public int? ErrorMessageDeleteSecondsDelay { get; private protected set; }
        public int? MessageCacheSize { get; private protected set; }
        public string? Prefix { get; private protected set; }
        public string? Token { get; private protected set; }
    }
}
