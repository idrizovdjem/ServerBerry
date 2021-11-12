namespace SecretsVault.Services.Core;

using System.Threading.Tasks;

using SecretsVault.ViewModels.Application;

public interface IApplicationsService
{
    Task<bool> IsNameAvailableAsync(string name, string userId);

    Task CreateAsync(CreateApplicationInputModel input, string userId);

    Task CreateAsync(CreateApplicationWithUserIdInputModel input);

    Task<bool> RemoveAsync(string applicationId, string userId);

    Task<ApplicationViewModel[]> GetAllAsync(string userId);

    Task<ApplicationOverviewViewModel> GetByIdAsync(string id, string userId);

    Task<DeletePhraseViewModel> GetDeletePhraseAsync(string id, string userId);

    Task<bool> DeleteAsync(string applicationId, string userId);

    Task<string> GetSecretKeyAsync(string applicationId, string userId);
}
