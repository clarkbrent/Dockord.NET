using Dockord.Bot.Events;
using Dockord.Bot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Dockord.Bot
{
    internal partial class Program
    {
        /// <summary>
        /// Creates a project specific <see cref="IHostBuilder"/>.
        /// </summary>
        /// <remarks>
        /// Adds dependency injection to project.
        /// </remarks>
        /// <returns><see cref="IHostBuilder"/></returns>
        private static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((services) =>
                {
                    services.AddSingleton<IDockordConfigService, DockordConfigService>();
                    services.AddSingleton<IDiscordConfigService, DiscordConfigService>();
                    services.AddSingleton<IBotService, BotService>();
                    services.AddSingleton<IEventService, EventService>();

                    services.AddTransient<IClientEventHandler, ClientEventHandler>();
                    services.AddTransient<ICommandEventHandler, CommandEventHandler>();
                })
                .UseSerilog();
    }
}
