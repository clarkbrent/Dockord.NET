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
        private static readonly string _namespace = typeof(Program).Namespace!;
        private static readonly string _appName = _namespace[(_namespace.LastIndexOf('.', _namespace.LastIndexOf('.') - 1) + 1)..];

        private static async Task Main()
        {
            IConfiguration config = CreateConfigBuilder().Build();
            Log.Logger = SetupLogger(config).CreateLogger();

            try
            {
                Log.Information($"Configuring app ({{{nameof(_appName)}}})...", _appName);
                IHost host = CreateHostBuilder().Build();

                ILooperService looperService = host.Services.GetService<ILooperService>();

                Log.Information($"Starting app ({{{nameof(_appName)}}})...", _appName);
                looperService.Run();

                await Task.Delay(-1); // Run app forever
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Program terminated unexpectedly ({{{nameof(_appName)}}})!", _appName);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
