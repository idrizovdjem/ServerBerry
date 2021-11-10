namespace SecretsVault.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

using SecretsVault.Services.Core;
using SecretsVault.ViewModels.Secret;
using SecretsVault.ViewModels.Response;

[ApiController]
[Route("api/[controller]/[action]")]
public class SecretsController : ControllerBase
{
    private readonly ISecretsService secretsService;

    public SecretsController(ISecretsService secretsService)
    {
        this.secretsService = secretsService;
    }

    [HttpPost]
    public async Task<GetSecretResponseModel> GetValue(GetSecretValueInputModel input)
    {
        string secretValue = await this.secretsService.GetValueAsync(input);
        if(string.IsNullOrWhiteSpace(secretValue) == true)
        {
            return new GetSecretResponseModel()
            {
                StatusCode = 404,
                Successfull = false,
                ErrorMessage = "Can't find secret with key and environment"
            };
        }

        return new GetSecretResponseModel()
        {
            Successfull = true,
            StatusCode = 200,
            Value = secretValue
        };
    }
}
