namespace AppRunner.CommandExecutor.Commands;

using System;
using System.Threading.Tasks;

public class ClearCommand : ICommand
{
    public Task ExecuteAsync(string command)
    {
        Console.Clear();
        return Task.CompletedTask;
    }

    public string GetDescription()
    {
        return "CLEAR\n\tClear the screen\n";
    }

    public bool IsMatch(string command)
    {
        return command.ToLower() == "clear";
    }
}
