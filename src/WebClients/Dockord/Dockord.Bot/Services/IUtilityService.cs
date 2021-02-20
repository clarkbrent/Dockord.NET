using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;

namespace Dockord.Bot.Services
{
    interface IUtilityService
    {
        CommandsNextConfiguration GetCommandsConfig();
        DiscordConfiguration GetDiscordConfg();
        InteractivityConfiguration GetInteractivityConfig();
    }
}