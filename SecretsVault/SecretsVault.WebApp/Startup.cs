namespace SecretsVault.WebApp;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using DatabaseExtractorCore;

using SecretsVault.Data;
using SecretsVault.Data.Models;
using SecretsVault.Services.Core;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(DatabaseExtractor.GetOptionsBuilderAction(this.Configuration));

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddControllersWithViews();

        services.AddScoped<IApplicationsService, ApplicationsService>();
        services.AddScoped<ISecretsService, SecretsService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });
    }
}