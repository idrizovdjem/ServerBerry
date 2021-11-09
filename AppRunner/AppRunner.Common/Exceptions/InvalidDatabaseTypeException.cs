namespace AppRunner.Common.Exceptions
{
    using System;

    public class InvalidDatabaseTypeException : Exception
    {
        public InvalidDatabaseTypeException(string databaseType)
            : base(databaseType + " is invalid database type")
        {
        }
    }
}
