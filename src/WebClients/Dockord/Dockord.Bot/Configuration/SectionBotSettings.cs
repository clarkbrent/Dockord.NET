namespace Dockord.Bot.Configuration.Options
{
#pragma warning disable RCS1170 // Ignore this error because we use a binder in DockordBotConfig to set any props that have a private setter
    public class SectionBotSettings
    {
        public bool? AlwaysCacheMembers { get; private set; }
        public int? ErrorMessageDeleteSecondsDelay { get; private set; }
        public int? MessageCacheSize { get; private set; }
        public string? Prefix { get; private set; }
        public string? Token { get; private set; }
    }
#pragma warning restore RCS1170 // Use read-only auto-implemented property.
}
