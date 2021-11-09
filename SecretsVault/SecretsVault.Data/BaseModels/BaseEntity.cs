namespace SecretsVault.Data.BaseModels
{
    using System.ComponentModel.DataAnnotations;

    public class BaseEntity<TKey> : IEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
