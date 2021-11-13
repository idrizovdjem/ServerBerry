namespace SecretsVault.Core;

using System;
using System.Threading.Tasks;

using Flurl.Http;

using SecretsVault.Core.Constants;
using SecretsVault.Core.Exceptions;
using SecretsVault.ViewModels.Secret;
using SecretsVault.ViewModels.Response;
using SecretsVault.ViewModels.Application;

public class VaultManager
{
    private string token;
    private string applicationId;

    public async Task<bool> SetupAsync(string secretKey)
    {
        if(string.IsNullOrWhiteSpace(secretKey) == true)
        {
            throw new EmptySecretKeyException();
        }

        AuthenticateResponseModel responseModel = await ApiEndpointConstants.AuthenticationEndpoint
                .PostJsonAsync(secretKey)
                .ReceiveJson<AuthenticateResponseModel>();

        if(responseModel.Successfull == false)
        {
            throw new InvalidSecretKeyException(secretKey);
        }

        this.applicationId = responseModel.ApplicationId;
        this.token = responseModel.Token;

        return responseModel.Successfull;
    }

    public async Task<string> GetSecretAsync(string key, string environment)
    {
        ValidateKeyAndEnvironment(key, environment);

        GetSecretResponseModel responseModel = await ApiEndpointConstants.GetSecretEndpoint
            .PostJsonAsync(new GetSecretValueInputModel()
            {
                Key = key,
                Environment = environment,
                ApplicationId = applicationId
            })
            .ReceiveJson<GetSecretResponseModel>();

        if(responseModel.Successfull == false)
        {
            throw new RequestFailedException(ApiEndpointConstants.GetSecretEndpoint, responseModel.ErrorMessage);
        }

        return responseModel.Value;
    }

    public async Task<bool> SecretExistsAsync(string key, string environment)
    {
        ValidateKeyAndEnvironment(key, environment);

        SecretExistsResponseModel responseModel = await ApiEndpointConstants.SecretExistsEndpoint
            .PostJsonAsync(new SecretExistsInputModel()
            {
                Key = key,
                Environment = environment,
                ApplicationId = applicationId
            })
            .ReceiveJson<SecretExistsResponseModel>();

        if(responseModel.Successfull == false)
        {
            throw new RequestFailedException(ApiEndpointConstants.SecretExistsEndpoint, responseModel.ErrorMessage);
        }

        return responseModel.Result;
    }

    public async Task CreateSecretAsync(string key, string environment, string value)
    {
        CreateSecretInputModel inputModel = new CreateSecretInputModel()
        {
            Key = key,
            Environment = environment,
            Value = value,
            ApplicationId = applicationId
        };

        CreateSecretResponseModel responseModel = await ApiEndpointConstants.CreateSecretEndpoint
            .PostJsonAsync(inputModel)
            .ReceiveJson<CreateSecretResponseModel>();

        if(responseModel.Successfull == false)
        {
            throw new RequestFailedException(ApiEndpointConstants.CreateSecretEndpoint, responseModel.ErrorMessage);
        }
    }

    public async Task<bool> DeleteSecretAsync(string key, string environment)
    {
        DeleteSecretWithKeyAndEnvironmentInputModel inputModel = new DeleteSecretWithKeyAndEnvironmentInputModel()
        {
            Key = key,
            Environment = environment,
            ApplicationId = applicationId
        };

        DeleteSecretResponseModel responseModel = await ApiEndpointConstants.DeleteSecretEndpoint
            .PostJsonAsync(inputModel)
            .ReceiveJson<DeleteSecretResponseModel>();

        if(responseModel.Successfull == false)
        {
            throw new RequestFailedException(ApiEndpointConstants.DeleteSecretEndpoint, responseModel.ErrorMessage);
        }

        return responseModel.Deleted;
    }

    public async Task CreateApplicationAsync(string applicationName)
    {
        if(string.IsNullOrWhiteSpace(applicationId) == true)
        {
            throw new ArgumentException("Application name cannot be null");
        }

        CreateApplicationWithUserIdInputModel inputModel = new CreateApplicationWithUserIdInputModel()
        {
            Name =  applicationName,
            UserId = token
        };

        CreateApplicationResponseModel responseModel = await ApiEndpointConstants.CreateApplicationEndpoint
            .PostJsonAsync(inputModel)
            .ReceiveJson<CreateApplicationResponseModel>();

        if(responseModel.Successfull == false)
        {
            throw new RequestFailedException(ApiEndpointConstants.CreateApplicationEndpoint, responseModel.ErrorMessage);
        }
    }

    private static void ValidateKeyAndEnvironment(string key, string environment)
    {
        if (string.IsNullOrWhiteSpace(key) == true)
        {
            throw new ArgumentException("Key parameter is empty or null");
        }

        if (string.IsNullOrWhiteSpace(environment) == true)
        {
            throw new ArgumentException("Environment parameter is empty or null");
        }
    }
}
