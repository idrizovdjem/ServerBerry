namespace SecretsVault.ViewModels.Application
{
    using System.ComponentModel.DataAnnotations;

    public class CreateApplicationInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
