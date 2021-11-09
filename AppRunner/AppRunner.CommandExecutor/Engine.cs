namespace AppRunner.CommandExecutor;

using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DatabaseExtractorCore;

using AppRunner.Data;
using AppRunner.Services.Application;
using AppRunner.CommandExecutor.Commands;
using AppRunner.CommandExecutor.Services;

public class Engine
{
    private readonly ICommand[] commands;
    private readonly IApplicationsService applicationsService;

    public Engine(string databaseType, string connectionString)
    {
        DbContextOptionsBuilder optionsBuilder = DatabaseExtractor.GetOptionsBuilder(databaseType, connectionString);
        this.applicationsService = new ApplicationsService(new ApplicationDbContext(optionsBuilder.Options));
        this.commands = CommandsService.GetCommandPatterns(applicationsService);
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Write("> ");
            string command = Console.ReadLine().Trim();
            if (command == "exit")
            {
                break;
            }
            else if (command == string.Empty)
            {
                continue;
            }

            try
            {
                await ProcessCommand(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private async Task ProcessCommand(string command)
    {
        ICommand matchingCommand = null;
        foreach (ICommand commandPattern in this.commands)
        {
            if (commandPattern.IsMatch(command) == true)
            {
                matchingCommand = commandPattern;
                break;
            }
        }

        if (matchingCommand == null)
        {
            Console.WriteLine("Invalid command");
            return;
        }

        await matchingCommand.ExecuteAsync(command);
    }
}
