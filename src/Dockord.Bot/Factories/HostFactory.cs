using Dockord.Bot.Events;
using Dockord.Bot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Dockord.Bot.Factories
{
    internal static class HostFactory
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
                    services.AddSingleton<IConfigService>(ConfigService.Get());
                    services.AddSingleton<IDiscordConfigService, DiscordConfigService>();
                    services.AddSingleton<IBotService, BotService>();
                    services.AddSingleton<IDiscordEventService, DiscordEventService>();

                    services.AddTransient<IClientEventHandler, ClientEventHandler>();
                    services.AddTransient<ICommandEventHandler, CommandEventHandler>();
                })
                .UseSerilog()
                .Build();
    }
}
