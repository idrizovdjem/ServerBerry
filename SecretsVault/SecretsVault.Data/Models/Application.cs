namespace SecretsVault.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SecretsVault.Data.BaseModels;

    public class Application : BaseEntity<string>
    {
        public Application()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Secrets = new List<Secret>();
        }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<Secret> Secrets { get; set; }
    }
}
