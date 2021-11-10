namespace SecretsVault.Core.Exceptions;

using System;

internal class EmptySecretKeyException : Exception
{
    public EmptySecretKeyException()
        : base("Missing secret key")
    { }
}
