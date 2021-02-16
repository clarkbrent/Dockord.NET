namespace Dockord.Bot.Configuration
{
    class SerilogOptions : BaseConfig
    {
        public string? SeqUrl { get; private protected set; }
        public MinimumLevelOptions? MinimumLevel { get; private protected set; }
    }

    class MinimumLevelOptions : BaseConfig
    {
        public string? Default { get; private protected set; }
        public OverrideOptions? Override { get; private protected set; }
    }

    class OverrideOptions : BaseConfig
    {
        public string? Microsoft { get; private protected set; }
        public string? System { get; private protected set; }
    }
}
