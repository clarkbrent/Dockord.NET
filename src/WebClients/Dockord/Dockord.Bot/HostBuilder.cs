using Dockord.Bot.Configuration;
using Dockord.Bot.Events;
using Dockord.Bot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Dockord.Bot
{
    partial class Program
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
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IDockordConfig, DockordConfig>();
                    services.AddSingleton<IUtilityService, SetupService>();
                    services.AddSingleton<IBotService, BotService>();
                    services.AddSingleton<IEventService, EventService>();

                    services.AddTransient<IClientEventHandler, ClientEventHandler>();
                    services.AddTransient<ICommandEventHandler, CommandEventHandler>();
                })
                .UseSerilog();
    }
}
