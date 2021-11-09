namespace SecretsVault.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
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
                IConfiguration configuration = new ConfigurationBuilder()
                    .AddJsonFile("secrets.json")
                    .Build();

                string migrationConnectionString = configuration["MigrationsConnectionString"];
                dbContextOptionsBuilder.UseSqlServer(migrationConnectionString);
            }
        }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Secret> Secrets { get; set; }
    }
}
