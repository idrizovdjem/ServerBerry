namespace AppRunner.CommandExecutor.Commands;

using System;
using System.Threading.Tasks;

using AppRunner.Services.Application;

public class RemoveCommand : ICommand
{
    private readonly IApplicationsService applicationsService;

    public RemoveCommand(IApplicationsService applicationsService)
    {
        this.applicationsService = applicationsService;
    }

    public async Task ExecuteAsync(string command)
    {
        string[] commandParts = command.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        string appName = commandParts[1];
        bool isAppRemoved = await this.applicationsService.RemoveAsync(appName);

        string message = isAppRemoved ? "Application removed successfully" : "Application not found";
        Console.WriteLine(message);
    }

    public string GetDescription()
    {
        return "REMOVE\n\tRemove application with provided name\n\tREMOVE [Application Name]\n";
    }

    public bool IsMatch(string command)
    {
        if (command.ToLower().StartsWith("remove") == false)
        {
            return false;
        }

        string[] commandParts = command.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        string appName = commandParts[1];

        if (string.IsNullOrWhiteSpace(appName) == true)
        {
            throw new ArgumentException($"Application name cannot be empty");
        }

        return true;
    }
}
