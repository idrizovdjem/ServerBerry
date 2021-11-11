namespace SecretsVault.Services.Core;

using System.Threading.Tasks;

using SecretsVault.ViewModels.Key;
using SecretsVault.ViewModels.Secret;

public interface ISecretsService
{
    Task<bool> IsKeyAvailableAsync(CheckKeyInputModel input);

    Task CreateAsync(CreateSecretInputModel input);

    Task<string> GetValueAsync(GetSecretValueInputModel input);

    Task<bool> DeleteByIdAsync(string secretId, string userId);

    Task DeleteAllAsync(string applicationId);

    Task<bool> DeleteAsync(DeleteSecretWithKeyAndEnvironmentInputModel input);

    Task<EditSecretInputModel> GetForEditAsync(string secretId);

    Task EditAsync(EditSecretInputModel input);

    Task<bool> ExistsAsync(SecretExistsInputModel input);
}
