namespace SecretsVault.Core.Exceptions;

using System;

internal class GetSecretRequestFailedException : Exception
{
    public GetSecretRequestFailedException(string message)
        : base(message)
    { }
}
