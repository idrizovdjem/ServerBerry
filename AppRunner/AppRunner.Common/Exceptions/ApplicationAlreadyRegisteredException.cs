namespace AppRunner.Common.Exceptions;

using System;

public class ApplicationAlreadyRegisteredException : Exception
{
    public ApplicationAlreadyRegisteredException(string appName)
        : base($"Application with name ({appName}) is already registered")
    {
    }
}
