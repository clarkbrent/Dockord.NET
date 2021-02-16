using DSharpPlus.CommandsNext;
using System.Threading.Tasks;

namespace Dockord.Bot.Events
{
    interface ICommandEventHandler
    {
        Task CommandExecuted(CommandsNextExtension sender, CommandExecutionEventArgs e);
        Task CommandErrored(CommandsNextExtension sender, CommandErrorEventArgs e);
    }
}