using Dockord.Bot.Configuration.Serilog;

namespace Dockord.Bot.Configuration.Options
{
#pragma warning disable RCS1170 // Ignore this error because we use a binder in DockordBotConfig to set any props that have a private setter
    public class SectionSerilog
    {
        public string? SeqUrl { get; private set; }
        public SectionMinimumLevel? MinimumLevel { get; private set; }
    }
#pragma warning restore RCS1170 // Use read-only auto-implemented property.
}
