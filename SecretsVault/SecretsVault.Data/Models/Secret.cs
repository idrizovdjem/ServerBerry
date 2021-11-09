namespace SecretsVault.Data.Models;

using System;
using System.ComponentModel.DataAnnotations;

using SecretsVault.Data.BaseModels;

public class Secret : BaseEntity<string>
{
    public Secret()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    [Required]
    public string ApplicationId { get; set; }

    public virtual Application Application { get; set; }

    [Required]
    [MaxLength(250)]
    public string Environment { get; set; }

    [Required]
    [MaxLength(250)]
    public string Key { get; set; }

    [Required]
    public string Value { get; set; }
}
