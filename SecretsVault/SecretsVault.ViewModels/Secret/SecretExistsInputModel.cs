namespace SecretsVault.ViewModels.Secret;

public class SecretExistsInputModel
{
    public string Key { get; set; }

    public string Environment { get; set; }

    public string ApplicationId { get; set; }
}
