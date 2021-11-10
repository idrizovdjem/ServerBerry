namespace SecretsVault.Core.Exceptions;

using System;

internal class InvalidSecretKeyException : Exception
{
    public InvalidSecretKeyException(string secretKey)
        : base($"Invalid secret key ({secretKey})")
    { }
}