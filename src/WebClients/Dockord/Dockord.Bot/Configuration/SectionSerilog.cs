using Dockord.Bot.Configuration.Serilog;

namespace Dockord.Bot.Configuration.Options
{
    public class SectionSerilog
    {
        public string? SeqUrl { get; private protected set; }
        public SectionMinimumLevel? MinimumLevel { get; private protected set; }
    }
}
