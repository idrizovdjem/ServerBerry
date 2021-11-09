namespace SecretsVault.Data.BaseModels;

public interface IEntity<TKey>
{
    TKey Id { get; set; }
}
