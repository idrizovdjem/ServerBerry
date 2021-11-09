namespace AppRunner.CommandExecutor
{
    using System;
    using System.Threading.Tasks;

    public class Program
    {
        private static string databaseType;
        private static string connectionString;

        public static async Task Main(string[] args)
        {
            Setup(args);
            Engine engine = new Engine(databaseType, connectionString);
            await engine.RunAsync();
        }

        private static void Setup(string[] args)
        {
            if(args.Length < 2)
            {
                throw new ArgumentException("Database type and connection string are required arguments");
            }

            string databaseTypeArg = args[0];
            string connectionStringArg = args[1];

            if(string.IsNullOrWhiteSpace(databaseTypeArg) == true)
            {
                throw new ArgumentException("Database type cannot be empty");
            }

            if(string.IsNullOrWhiteSpace(connectionStringArg) == true)
            {
                throw new ArgumentException("Connection string cannot be empty");
            }

            databaseType = databaseTypeArg;
            connectionString = connectionStringArg;
        }
    }
}
