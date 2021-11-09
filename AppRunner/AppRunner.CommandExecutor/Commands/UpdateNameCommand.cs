namespace AppRunner.CommandExecutor.Commands
{    
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using AppRunner.Services.Application;

    public class UpdateNameCommand : ICommand
    {
        private readonly IApplicationsService applicationsService;

        public UpdateNameCommand(IApplicationsService applicationsService)
        {
            this.applicationsService = applicationsService;
        }

        public async Task ExecuteAsync(string command)
        {
            string[] commandParts = command.Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);
            string oldName = commandParts[2];
            string newName = commandParts[3];

            if(oldName == newName)
            {
                Console.WriteLine("New name cannot be the same as the old name");
            }

            bool isNameUpdated = await this.applicationsService.UpdateNameAsync(oldName, newName);
            string message = isNameUpdated ? "Application name successfully updated" : "Application not found";
            Console.WriteLine(message);
        }

        public string GetDescription()
        {
            StringBuilder stringBuilder = new StringBuilder("UPDATE NAME\n");
            stringBuilder.AppendLine("\tSet application name to new value");
            stringBuilder.AppendLine("\tUPDATE NAME [Old Name] [New Name]");

            return stringBuilder.ToString();
        }

        public bool IsMatch(string command)
        {
            if(command.ToLower().StartsWith("update name") == false)
            {
                return false;
            }

            string[] commandParts = command.Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);
            string oldName = commandParts[2];
            string newName = commandParts[3];

            if(string.IsNullOrEmpty(oldName) == true)
            {
                throw new ArgumentException("Old application name cannot be empty");
            }

            if(string.IsNullOrEmpty(newName) == true)
            {
                throw new ArgumentException("New application name cannot be empty"); 
            }

            return true;
        }
    }
}