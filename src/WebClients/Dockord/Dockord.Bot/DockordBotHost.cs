using Dockord.Bot.Events;
using Dockord.Bot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Dockord.Bot
{
    internal static class DockordBotHost
    {
        /// <summary>
        /// Creates a project specific <see cref="IHost"/>.
        /// </summary>
        /// <remarks>
        /// Sets up dependency injection in project.
        /// </remarks>
        /// <returns><see cref="IHost"/></returns>
        public static IHost Create() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((services) =>
                {
                    services.AddSingleton<IDockordBotConfig>(DockordBotConfig.Get());
                    services.AddSingleton<IDiscordConfigService, DiscordConfigService>();
                    services.AddSingleton<IDockordBotService, DockordBotService>();
                    services.AddSingleton<IDiscordEventService, DiscordEventService>();

                    services.AddTransient<IClientEventHandler, ClientEventHandler>();
                    services.AddTransient<ICommandEventHandler, CommandEventHandler>();
                })
                .UseSerilog()
                .Build();
    }
}
