﻿namespace SecretsVault.Core;

using System;
using System.Threading.Tasks;

using Flurl.Http;

using SecretsVault.Core.Constants;
using SecretsVault.Core.Exceptions;
using SecretsVault.ViewModels.Secret;
using SecretsVault.ViewModels.Response;

public class VaultManager
{
    private string authenticationToken;
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

        if(responseModel.Successfull == false && responseModel.StatusCode == 400)
        {
            throw new InvalidSecretKeyException(secretKey);
        }

        this.authenticationToken = responseModel.Token;
        this.applicationId = responseModel.ApplicationId;

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