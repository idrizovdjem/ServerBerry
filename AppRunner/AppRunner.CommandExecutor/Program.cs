namespace AppRunner.CommandExecutor;

using System.Threading.Tasks;

using SecretsVault.Core;

using AppRunner.Common.Exceptions;
using AppRunner.CommandExecutor.Constants;

public class Program
{
    private static VaultManager vaultManager;
    private static string databaseType;
    private static string connectionString;

    public static async Task Main(string[] args)
    {
        vaultManager = new VaultManager();
        await vaultManager.SetupAsync(SetupConstants.VAULT_MANAGER_SECRET_KEY);

        await SetupAsync(args);
        Engine engine = new Engine(databaseType, connectionString);
        await engine.RunAsync();
    }

    private static async Task SetupAsync(string[] args)
    {
        if(args.Length == 0)
        {
            throw new MissingEnvironmentException();
        }

        string environment = args[0];

        databaseType = await vaultManager.GetSecretAsync("DatabaseType", environment);
        connectionString = await vaultManager.GetSecretAsync("ConnectionString", environment);
    }
}
