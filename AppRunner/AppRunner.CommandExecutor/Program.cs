namespace AppRunner.CommandExecutor;

using System;
using System.Threading.Tasks;

using SecretsVault.Core;

using AppRunner.Common.Exceptions;
using AppRunner.CommandExecutor.Constants;
using AppRunner.Common.Constants;

public class Program
{
    private static VaultManager vaultManager;
    private static string secretKey;
    private static string environment;

    public static async Task Main(string[] args)
    {
        ParseArguments(args);

        vaultManager = new VaultManager();
        await vaultManager.SetupAsync(secretKey);

        string databaseType = await vaultManager.GetSecretAsync(VaultConstants.DATABASE_TYPE_KEY, environment);
        string connectionString = await vaultManager.GetSecretAsync(VaultConstants.CONNECTION_STRING_KEY, environment);

        Engine engine = new Engine(databaseType, connectionString);
        await engine.RunAsync();
    }

    private static void ParseArguments(string[] args)
    {
        if(args.Length < 2)
        {
            throw new ArgumentException("Secret key and environment arguments are required");
        }

        secretKey = args[0];
        if(string.IsNullOrWhiteSpace(secretKey) == true)
        {
            throw new MissingSecretKeyException();
        }

        environment = args[1];
        if(string.IsNullOrWhiteSpace(environment) == true)
        {
            throw new MissingEnvironmentException();
        }
    }
}
