using Dockord.Bot.Configuration.Serilog.MinimumLevel;

namespace Dockord.Bot.Configuration.Serilog
{
#pragma warning disable RCS1170 // Ignore this error because we use a binder in DockordBotConfig to set any props that have a private setter
    public class SectionMinimumLevel
    {
        public string? Default { get; private set; }
        public SectionOverride? Override { get; private set; }
    }
#pragma warning restore RCS1170 // Use read-only auto-implemented property.
}
