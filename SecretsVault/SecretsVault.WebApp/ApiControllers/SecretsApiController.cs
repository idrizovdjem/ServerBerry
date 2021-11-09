namespace SecretsVault.WebApp.ApiControllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SecretsVault.Services.Core;
    using SecretsVault.ViewModels.Key;
    using SecretsVault.ViewModels.Secret;

    [Route("api/secrets/[action]")]
    public class SecretsApiController : BaseApiController
    {
        private readonly ISecretsService secretsService;

        public SecretsApiController(ISecretsService secretsService)
        {
            this.secretsService = secretsService;
        }

        [HttpPost]
        public async Task<bool> IsKeyAvailable(CheckKeyInputModel input)
        {
            return await this.secretsService.IsKeyAvailableAsync(input);
        }

        [HttpPost]
        public async Task<string> GetSecretValue(GetSecretValueInputModel input)
        {
            return await this.secretsService.GetValueAsync(input);
        }

        [HttpPost]
        public async Task<bool> DeleteSecret(DeleteSecretInputModel input)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await this.secretsService.DeleteAsync(input.SecretId, userId);
        }
    }
}
