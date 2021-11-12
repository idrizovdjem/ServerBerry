namespace SecretsVault.ViewModels.Application;

using System.ComponentModel.DataAnnotations;

public class CreateApplicationWithUserIdInputModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string UserId { get; set; }
}
