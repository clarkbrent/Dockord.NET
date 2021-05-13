using DSharpPlus.CommandsNext;
using System.Threading.Tasks;

namespace Dockord.Bot.Events
{
    public interface ICommandEventHandler
    {
        Task CommandExecuted(CommandsNextExtension sender, CommandExecutionEventArgs e);
        Task CommandErrored(CommandsNextExtension sender, CommandErrorEventArgs e);
    }
}