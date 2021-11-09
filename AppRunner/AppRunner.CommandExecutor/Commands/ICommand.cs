namespace AppRunner.CommandExecutor.Commands
{
    using System.Threading.Tasks;

    public interface ICommand
    {
        bool IsMatch(string command);

        Task ExecuteAsync(string command);

        string GetDescription();
    }
}
