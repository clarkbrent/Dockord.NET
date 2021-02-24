using Dockord.Bot.Services;
using Microsoft.Extensions.Configuration;
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

            IConfiguration config = DockordBotConfig.Create();
            Log.Logger = DockordBotLogger.Create(config);

            try
            {
                Log.Information($"Configuring {{{nameof(appName)}}}...", appName);

                IHost host = DockordBotHost.Create()
                    ?? throw new InvalidOperationException("An error occured while configuring the host.");

                IDockordBotService bot = host.Services.GetService<IDockordBotService>()
                    ?? throw new InvalidOperationException("Bot service could not be found.");

                await bot.RunAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"{{{nameof(appName)}}} terminated unexpectedly!", appName);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
