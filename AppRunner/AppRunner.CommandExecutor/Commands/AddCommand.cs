namespace AppRunner.CommandExecutor.Commands;

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using AppRunner.Common.Enums;
using AppRunner.Common.Exceptions;
using AppRunner.Services.Application;

public class AddCommand : ICommand
{
    private readonly ApplicationsService applicationsService;

    public AddCommand(ApplicationsService applicationsService)
    {
        this.applicationsService = applicationsService;
    }

    public async Task ExecuteAsync(string command)
    {
        string[] commandParts = command.Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);
        string name = commandParts[1];
        string type = commandParts[2];
        string path = commandParts[3];

        bool isNameAvailable = await this.applicationsService.IsNameAvailableAsync(name);
        if (isNameAvailable == false)
        {
            throw new ApplicationAlreadyRegisteredException(name);
        }

        bool isTypeParsed = Enum.TryParse(type, true, out AppType applicationType);
        if (isTypeParsed == false)
        {
            throw new NotSupportedApplicationTypeException(type);
        }

        await this.applicationsService.AddAsync(name, path, applicationType);
    }

    public bool IsMatch(string command)
    {
        if (command.ToLower().StartsWith("add") == false)
        {
            return false;
        }

        string[] commandParts = command.Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);
        if (commandParts.Length < 4)
        {
            return false;
        }

        string name = commandParts[1];
        string type = commandParts[2];
        string path = commandParts[3];

        ValidateArguments(name, type, path);
        return true;
    }

    public string GetDescription()
    {
        StringBuilder stringBuilder = new StringBuilder("ADD\n");
        stringBuilder.AppendLine("\tAdd an application with provided parameters");
        stringBuilder.AppendLine("\tADD [Application Name] [Application Type] [Path]");

        return stringBuilder.ToString();
    }

    private static void ValidateArguments(string name, string type, string path)
    {
        if (string.IsNullOrWhiteSpace(name) == true)
        {
            throw new ArgumentException("Application name cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(type) == true)
        {
            throw new ArgumentException("Application type cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(path) == true)
        {
            throw new ArgumentException("Path cannot be empty");
        }
    }
}
