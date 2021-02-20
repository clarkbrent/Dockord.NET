using Dockord.Bot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Dockord.Bot
{
    partial class Program
    {
        private static async Task Main()
        {
            string nameSpace = typeof(Program).Namespace!;
            string appName = nameSpace[(nameSpace.LastIndexOf('.', nameSpace.LastIndexOf('.') - 1) + 1)..];

            IConfiguration config = CreateConfigBuilder().Build();
            Log.Logger = SetupLogger(config).CreateLogger();

            try
            {
                Log.Information($"Configuring app ({{{nameof(appName)}}})...", appName);
                IHost host = CreateHostBuilder().Build()
                    ?? throw new InvalidOperationException("An error occured while configuring the host.");

                IBotService bot = host.Services.GetService<IBotService>()
                    ?? throw new InvalidOperationException("Bot service could not be found.");

                await bot.RunAsync();
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
