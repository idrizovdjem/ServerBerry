namespace AppRunner.Common.Exceptions;

using System;

public class NotSupportedApplicationTypeException : Exception
{
    public NotSupportedApplicationTypeException(string type)
        : base($"{type} is not supported application type")
    {
    }
}
