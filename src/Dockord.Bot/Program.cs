using Dockord.Bot.Factories;
using Dockord.Bot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Dockord.Bot
{
    internal static class Program
    {
        private static async Task Main()
        {
            string nameSpace = typeof(Program).Namespace!;
            string appName = nameSpace[(nameSpace.LastIndexOf('.', nameSpace.LastIndexOf('.') - 1) + 1)..];

            Log.Logger = LoggerFactory.Create();

            try
            {
                Log.Information($"Configuring app ({{{nameof(appName)}}})...", appName);

                IHost host = HostFactory.Create()
                    ?? throw new InvalidOperationException("An error occured while configuring the host.");

                IBotService bot = host.Services.GetService<IBotService>()
                    ?? throw new InvalidOperationException("Bot service could not be found.");

                await bot.RunAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"App ({{{nameof(appName)}}}) terminated unexpectedly!", appName);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
