namespace AppRunner.Services.Application
{
    using System.Threading.Tasks;

    using AppRunner.Data.Models;
    using AppRunner.Common.Enums;

    public interface IApplicationsService
    {
        Task<Application[]> GetApplicationsAsync();

        Task<bool> IsNameAvailableAsync(string name);

        Task AddAsync(string name, string path, AppType appType);

        Task<bool> RemoveAsync(string name);

       Task<bool> UpdateNameAsync(string oldName, string newName); 

       Task<Application> GetByNameAsync(string name);
    }
}
