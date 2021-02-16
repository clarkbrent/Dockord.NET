using Dockord.Bot.Configuration;
using Microsoft.Extensions.Logging;

namespace Dockord.Bot.Services
{
    class LooperService : ILooperService
    {
        private readonly ILogger<LooperService> _logger;
        private readonly IDockordConfig _config;

        public LooperService(ILogger<LooperService> logger, IDockordConfig config)
        {
            _logger = logger;
            _config = config;
        }

        public void Run()
        {
            for (int i = 0; i < _config.LoopAmount; i++)
            {
                _logger.LogInformation($"Loop Number: {{{nameof(_config.LoopAmount)}}}", i);
            }
        }
    }
}
