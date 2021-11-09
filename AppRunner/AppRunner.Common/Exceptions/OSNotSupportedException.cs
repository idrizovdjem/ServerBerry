namespace AppRunner.Common.Exceptions
{
    using System;

    public class OSNotSupportedException : Exception
    {
        public OSNotSupportedException()
            : base("Your current OS is not supported")
        {
        }
    }
}
