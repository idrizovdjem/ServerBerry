namespace SecretsVault.ViewModels.Secret
{
    using System.ComponentModel.DataAnnotations;

    public class EditSecretInputModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Environment { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
