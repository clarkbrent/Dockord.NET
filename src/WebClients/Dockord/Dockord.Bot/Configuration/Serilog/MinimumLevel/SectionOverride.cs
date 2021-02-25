namespace Dockord.Bot.Configuration.Serilog.MinimumLevel
{
#pragma warning disable RCS1170 // Ignore this error because we use a binder in DockordBotConfig to set any props that have a private setter
    public class SectionOverride
    {
        public string? Microsoft { get; private set; }
        public string? System { get; private set; }
    }
#pragma warning restore RCS1170 // Use read-only auto-implemented property.
}
