namespace SecretsVault.Core.Exceptions;

using System;

public class RequestFailedException : Exception
{
    public RequestFailedException(string endpoint, string errorMessage)
        : base($"Request to {endpoint} failed; ErrorMessage: {errorMessage}")
    {
    }
}
