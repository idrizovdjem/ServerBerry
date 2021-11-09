namespace AppRunner.CommandExecutor.Commands
{
    using System;
    using System.Threading.Tasks;
    
    using AppRunner.CommandExecutor.Services;
    using AppRunner.Services.Application;

    public class HelpCommand : ICommand
    {
        private readonly IApplicationsService applicationsService;

        public HelpCommand(IApplicationsService applicationsService)
        {
            this.applicationsService = applicationsService;
        }

        public  Task ExecuteAsync(string command)
        {
            ICommand[] allCommands = CommandsService.GetCommandPatterns(this.applicationsService);
            foreach(ICommand commandObject in allCommands)
            {
                Console.WriteLine(commandObject.GetDescription());
            }

            return Task.CompletedTask;
        }

        public string GetDescription()
        {
            return "HELP\n\tDisplay all commands with documentation\n";
        }

        public bool IsMatch(string command)
        {
            return command.ToLower() == "help";
        }
    }
}