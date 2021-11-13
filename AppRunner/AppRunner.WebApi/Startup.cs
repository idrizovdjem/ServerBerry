namespace AppRunner.WebApi;

using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SecretsVault.Core;

using DatabaseExtractorCore;

using AppRunner.Data;
using AppRunner.Common.Constants;
using AppRunner.Services.Application;

public class Startup
{
    private readonly VaultManager vaultManager;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        this.vaultManager = new VaultManager();
        this.vaultManager.SetupAsync(this.Configuration["SecretKey"]).Wait();
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        string environment = this.Configuration["Environment"];
        string databaseType = this.vaultManager.GetSecretAsync(VaultConstants.DATABASE_TYPE_KEY, environment).GetAwaiter().GetResult();
        string connectionString = this.vaultManager.GetSecretAsync(VaultConstants.CONNECTION_STRING_KEY, environment).GetAwaiter().GetResult();

        services.AddDbContext<ApplicationDbContext>(DatabaseExtractor.GetOptionsBuilderAction(databaseType, connectionString));

        services.AddScoped<IApplicationsService, ApplicationsService>();

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AppRunner.WebApi", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppRunner.WebApi v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
