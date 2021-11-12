namespace AppRunner.Common.Exceptions;

using System;

public class MissingEnvironmentException : Exception
{
    public MissingEnvironmentException()
        : base("Environment is required")
    { }
}
