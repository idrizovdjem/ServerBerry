namespace SecretsVault.ViewModels.Secret;

using System.ComponentModel.DataAnnotations;

public class CreateSecretInputModel
{
    [Required]
    public string ApplicationId { get; set; }

    [Required]
    public string Key { get; set; }

    [Required]
    public string Environment { get; set; }

    [Required]
    public string Value { get; set; }
}
