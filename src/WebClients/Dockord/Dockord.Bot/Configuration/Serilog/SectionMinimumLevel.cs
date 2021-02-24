using Dockord.Bot.Configuration.Options;
using Dockord.Bot.Configuration.Serilog.MinimumLevel;

namespace Dockord.Bot.Configuration.Serilog
{
    public class SectionMinimumLevel
    {
        public string? Default { get; private protected set; }
        public SectionOverride? Override { get; private protected set; }
    }
}
