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

    [HttpPost]
    public async Task<SecretExistsResponseModel> Exists(SecretExistsInputModel input)
    {
        bool secretExists = await this.secretsService.ExistsAsync(input);
        return new SecretExistsResponseModel()
        {
            Successfull = true,
            Result = secretExists,
            StatusCode = 200
        };
    }

    [HttpPost]
    public async Task<CreateSecretResponseModel> Create(CreateSecretInputModel input)
    {
        SecretExistsInputModel secretExistsModel = new SecretExistsInputModel()
        {
            ApplicationId = input.ApplicationId,
            Environment = input.Environment,
            Key = input.Key,
        };

        bool secretExists = await this.secretsService.ExistsAsync(secretExistsModel);
        if(secretExists == true)
        {
            return new CreateSecretResponseModel()
            {
                Successfull = false,
                StatusCode = 400,
                ErrorMessage = "Secret with this key and environment already exists",
                Created = false
            };
        }

        await this.secretsService.CreateAsync(input);

        return new CreateSecretResponseModel()
        {
            Successfull = true,
            Created = true,
            StatusCode = 200,
        };
    }

    [HttpPost]
    public async Task<DeleteSecretResponseModel> Delete(DeleteSecretWithKeyAndEnvironmentInputModel input)
    {
        bool deleted = await this.secretsService.DeleteAsync(input);
        return new DeleteSecretResponseModel()
        {
            Successfull = true,
            StatusCode = 200,
            Deleted = deleted
        };
    }
}
