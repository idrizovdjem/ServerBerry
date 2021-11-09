namespace AppRunner.Data
{
    using Microsoft.EntityFrameworkCore;

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
                optionsBuilder.UseSqlServer("Server=DESKTOP-LH7CK7C\\SQLEXPRESS;Database=AppRunnerCore;Integrated Security=true;");
            }
        }

        public DbSet<Application> Applications { get; set; }
    }
}
