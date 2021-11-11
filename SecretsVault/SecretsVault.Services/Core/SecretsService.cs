namespace SecretsVault.Services.Core;

using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SecretsVault.Data;
using SecretsVault.Data.Models;
using SecretsVault.ViewModels.Key;
using SecretsVault.ViewModels.Secret;

public class SecretsService : ISecretsService
{
    private readonly ApplicationDbContext context;

    public SecretsService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task CreateAsync(CreateSecretInputModel input)
    {
        Secret secret = new Secret()
        {
            Key = input.Key,
            ApplicationId = input.ApplicationId,
            Environment = input.Environment,
            Value = input.Value
        };

        await this.context.Secrets.AddAsync(secret);
        await this.context.SaveChangesAsync();
    }

    public async Task<bool> IsKeyAvailableAsync(CheckKeyInputModel input)
    {
        Secret secret = await this.context.Secrets
            .Where(a => a.ApplicationId == input.ApplicationId && a.Environment == input.Environment && a.Key == input.Key)
            .FirstOrDefaultAsync();

        return secret == null;
    }

    public async Task<string> GetValueAsync(GetSecretValueInputModel input)
    {
        string value = await this.context.Secrets
            .Where(s => s.ApplicationId == input.ApplicationId && s.Environment == input.Environment && s.Key == input.Key)
            .Select(s => s.Value)
            .FirstOrDefaultAsync();

        return value ?? string.Empty;
    }

    public async Task<bool> DeleteAsync(string secretId, string userId)
    {
        Secret secret = await this.context.Secrets
            .Where(s => s.Id == secretId && s.Application.CreatorId == userId)
            .FirstOrDefaultAsync();

        if (secret == null)
        {
            return false;
        }

        this.context.Secrets.Remove(secret);
        await this.context.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAllAsync(string applicationId)
    {
        Secret[] secrets = await this.context.Secrets
            .Where(s => s.ApplicationId == applicationId)
            .ToArrayAsync();

        this.context.Secrets.RemoveRange(secrets);
        await this.context.SaveChangesAsync();
    }

    public async Task<EditSecretInputModel> GetForEditAsync(string secretId)
    {
        return await this.context.Secrets
            .Where(s => s.Id == secretId)
            .Select(s => new EditSecretInputModel()
            {
                Id = s.Id,
                Key = s.Key,
                Environment = s.Environment,
                Value = s.Value
            })
            .FirstOrDefaultAsync();
    }

    public async Task EditAsync(EditSecretInputModel input)
    {
        Secret secret = await this.context.Secrets
            .Where(s => s.Id == input.Id)
            .FirstOrDefaultAsync();

        if (secret == null)
        {
            return;
        }

        secret.Key = input.Key;
        secret.Environment = input.Environment;
        secret.Value = input.Value;

        this.context.Update(secret);
        await this.context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(SecretExistsInputModel input)
    {
        return await this.context.Secrets
            .AnyAsync(s => s.Key == input.Key && s.Environment == input.Environment && s.ApplicationId == input.ApplicationId);
    }
}
