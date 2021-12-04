namespace ExpenseTracker.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using ExpenseTracker.Data.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext()
    { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    { }

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
}