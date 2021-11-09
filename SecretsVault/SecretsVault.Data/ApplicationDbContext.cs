namespace SecretsVault.Data
{
    using System;

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
                string migrationConnectionString = Environment.GetEnvironmentVariable("MigrationConnectionString");
                dbContextOptionsBuilder.UseSqlServer(migrationConnectionString);
            }
        }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Secret> Secrets { get; set; }
    }
}
