namespace SecretsVault.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using SecretsVault.Data.Models;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if(dbContextOptionsBuilder.IsConfigured == false)
            {
                dbContextOptionsBuilder
                    .UseSqlServer("Server=DESKTOP-LH7CK7C\\SQLEXPRESS;Database=SecretsVault;Integrated Security=true");
            }
        }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Secret> Secrets { get; set; }
    }
}
