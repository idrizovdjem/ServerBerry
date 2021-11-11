namespace SecretsVault.Services.Core;

using System.Threading.Tasks;

using SecretsVault.ViewModels.Key;
using SecretsVault.ViewModels.Secret;

public interface ISecretsService
{
    Task<bool> IsKeyAvailableAsync(CheckKeyInputModel input);

    Task CreateAsync(CreateSecretInputModel input);

    Task<string> GetValueAsync(GetSecretValueInputModel input);

    Task<bool> DeleteAsync(string secretId, string userId);

    Task DeleteAllAsync(string applicationId);

    Task<EditSecretInputModel> GetForEditAsync(string secretId);

    Task EditAsync(EditSecretInputModel input);

    Task<bool> ExistsAsync(SecretExistsInputModel input);
}
