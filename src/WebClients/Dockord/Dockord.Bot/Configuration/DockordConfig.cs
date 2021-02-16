using Microsoft.Extensions.Configuration;

namespace Dockord.Bot.Configuration
{
    class DockordConfig : BaseConfig, IDockordConfig
    {
        private readonly IConfiguration _config;
        private readonly SerilogOptions _serilogOptions = new SerilogOptions();
        private readonly BotSettingsOptions _botSettings = new BotSettingsOptions();

        public DockordConfig(IConfiguration config)
        {
            _config = config;

            _config.GetSection(nameof(Serilog))
                   .Bind(_serilogOptions, x => x.BindNonPublicProperties = true);

            _config.GetSection(nameof(BotSettings))
                   .Bind(_botSettings, x => x.BindNonPublicProperties = true);
        }

        public SerilogOptions Serilog => _serilogOptions;
        public BotSettingsOptions BotSettings => _botSettings;
        public int? LoopAmount => int.Parse(_config[nameof(LoopAmount)]);
    }
}
