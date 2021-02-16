using Dockord.Bot.Configuration;
using Dockord.Bot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Dockord.Bot
{
    partial class Program
    {
        // Setup dependency injection
        private static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IDockordConfig, DockordConfig>();
                    services.AddSingleton<IBotService, BotService>();
                })
                .UseSerilog();
    }
}
