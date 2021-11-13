namespace AppRunner.Common.Exceptions;

using System;

public class MissingSecretKeyException : Exception
{
    public MissingSecretKeyException()
        : base("Secret key is required")
    { }
}
