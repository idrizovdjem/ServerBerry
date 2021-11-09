namespace SecretsVault.ViewModels.Secret;

using System.ComponentModel.DataAnnotations;

public class DeleteSecretInputModel
{
    [Required]
    public string SecretId { get; set; }
}
