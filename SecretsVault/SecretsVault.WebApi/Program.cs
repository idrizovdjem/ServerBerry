using DatabaseExtractorCore;

using SecretsVault.Data;
using SecretsVault.Services.Core;
using SecretsVault.Services.Authentication;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(DatabaseExtractor.GetOptionsBuilderAction(builder.Configuration));

builder.Services.AddScoped<IApplicationsService, ApplicationsService>();
builder.Services.AddScoped<ISecretsService, SecretsService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();