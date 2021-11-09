namespace SecretsVault.ViewModels.Application
{
    using System.ComponentModel.DataAnnotations;

    public class DeleteApplicationInputModel
    {
        [Required]
        public string DeletePhrase { get; set; }

        [Required]
        public string ApplicationId { get; set; }
    }
}
