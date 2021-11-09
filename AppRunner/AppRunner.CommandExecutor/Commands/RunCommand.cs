namespace AppRunner.CommandExecutor.Commands
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using AppRunner.Core;
    using AppRunner.Data.Models;
    using AppRunner.Core.Loggers;
    using AppRunner.Common.Exceptions;
    using AppRunner.Services.Application;

    public class RunCommand : ICommand
    {
        private readonly IApplicationsService applicationsService;

        public RunCommand(IApplicationsService applicationsService)
        {
           this.applicationsService = applicationsService; 
        }

        public async Task ExecuteAsync(string command)
        {
            string[] runArguments = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Application[] applications = await this.GetApplicationsAsync(runArguments);
            RunApplications(applications);
        }

        public string GetDescription()
        {
            StringBuilder stringBuilder = new StringBuilder("RUN\n");
            stringBuilder.AppendLine("\tCreate app container instance and run applications");
            stringBuilder.AppendLine("\tRUN ALL");
            stringBuilder.AppendLine("\tRUN [App1] [App2] [App3] ...");

            return stringBuilder.ToString();
        }

        public bool IsMatch(string command)
        {
            if (command.ToLower().StartsWith("run") == false)
            {
                return false;
            }

            if (command.ToLower() == "run all")
            {
                return true;
            }

            string[] runArguments = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return runArguments.Length > 1;
        }

        private async Task<Application[]> GetApplicationsAsync(string[] runArguments)
        {
            if (runArguments[1].ToLower() == "all")
            {
                return await this.applicationsService.GetApplicationsAsync();
            }

            List<Application> applications = new List<Application>();
            string[] applicationNames = runArguments.Skip(1).ToArray();
            foreach (string name in applicationNames)
            {
                Application application = await this.applicationsService.GetByNameAsync(name);
                if (application == null)
                {
                    throw new ApplicationNotRegisteredException(name);
                }

                applications.Add(application);
            }

            return applications.ToArray();
        }

        private static void RunApplications(Application[] applications)
        {
            AppsContainer appContainer = new AppsContainer(applications)
            {
                Logger = new ConsoleLogger()
            };

            appContainer.StartApps();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
            }

            appContainer.StopApps();
        }
    }
}