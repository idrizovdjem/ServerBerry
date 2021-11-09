namespace SecretsVault.ViewModels.Application;

using SecretsVault.ViewModels.Secret;

public class ApplicationOverviewViewModel
{
    public string Id { get; set; }

    public string Name { get; set; }

    public SecretOverviewViewModel[] Secrets { get; set; }
}
