namespace AppRunner.Common.Exceptions;

using System;

public class ApplicationNotRegisteredException : Exception
{
    public ApplicationNotRegisteredException(string name)
        : base($"{name} is not registered as application")
    {
    }
}