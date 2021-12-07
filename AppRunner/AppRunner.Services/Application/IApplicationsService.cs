namespace AppRunner.Services.Application;

using System.Threading.Tasks;
using System.Collections.Generic;

using AppRunner.Data.Models;
using AppRunner.Common.Enums;
using AppRunner.ViewModels.Application;

public interface IApplicationsService
{
    Task<Application[]> GetApplicationsAsync();

    Task<IEnumerable<ApplicationViewModel>> GetApplicationsViewModelsAsync();

    Task<bool> IsNameAvailableAsync(string name);

    Task AddAsync(string name, string path, AppType appType);

    Task<bool> RemoveAsync(string name);

    Task<bool> UpdateNameAsync(string oldName, string newName);

    Task<Application> GetByNameAsync(string name);
}
