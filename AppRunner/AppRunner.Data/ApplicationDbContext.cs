namespace AppRunner.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using AppRunner.Data.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .AddJsonFile("secrets.json")
                    .Build();

                string migrationConnectionString = configuration["MigrationConnectionString"];
                optionsBuilder.UseSqlServer(migrationConnectionString);
            }
        }

        public DbSet<Application> Applications { get; set; }
    }
}
