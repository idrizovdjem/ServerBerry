namespace AppRunner.CommandExecutor.Commands
{
    using System;
    using System.Threading.Tasks;

    using AppRunner.Data.Models;
    using AppRunner.Services.Application;

    public class ListCommand : ICommand
    {
        private readonly IApplicationsService applicationsService;

        public ListCommand(ApplicationsService applicationsService)
        {
            this.applicationsService = applicationsService;
        }

        public async Task ExecuteAsync(string command)
        {
            Application[] applications = await this.applicationsService.GetApplicationsAsync();
            if(applications.Length == 0)
            {
                Console.WriteLine("No applications registered");
                return;
            }

            foreach(Application application in applications)
            {
                Console.WriteLine($"{application.Name}:");
                Console.WriteLine($"\tPath: {application.Path}");
                Console.WriteLine($"\tType: {application.Type}");
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public bool IsMatch(string command)
        {
            return command.ToLower() == "list";
        }
        
        public string GetDescription()
        {
            return "LIST\n\tDisplay all registered applications\n";
        }
    }
}
