namespace AppRunner.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AppRunner.Common.Enums;

    public class Application
    {
        public Application()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        public AppType Type { get; set; }
    }
}
