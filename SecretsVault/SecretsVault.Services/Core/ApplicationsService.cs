namespace SecretsVault.Services.Core;

using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SecretsVault.Data;
using SecretsVault.Data.Models;
using SecretsVault.ViewModels.Application;
using SecretsVault.ViewModels.Secret;

public class ApplicationsService : IApplicationsService
{
    private readonly ApplicationDbContext context;
    private readonly ISecretsService secretsService;

    public ApplicationsService(ApplicationDbContext context, ISecretsService secretsService)
    {
        this.context = context;
        this.secretsService = secretsService;
    }

    public async Task CreateAsync(CreateApplicationInputModel input, string userId)
    {
        Application application = new Application()
        {
            Name = input.Name,
            CreatorId = userId,
            SecretKey = Guid.NewGuid().ToString()
        };

        await this.context.Applications.AddAsync(application);
        await this.context.SaveChangesAsync();
    }

    public async Task CreateAsync(CreateApplicationWithUserIdInputModel input)
    {
        Application application = new Application()
        {
            Name = input.Name,
            CreatorId = input.UserId,
            SecretKey = Guid.NewGuid().ToString()
        };

        await this.context.Applications.AddAsync(application);
        await this.context.SaveChangesAsync();
    }

    public async Task<ApplicationViewModel[]> GetAllAsync(string userId)
    {
        return await this.context.Applications
            .Where(a => a.CreatorId == userId)
            .Select(a => new ApplicationViewModel()
            {
                Id = a.Id,
                Name = a.Name
            })
            .ToArrayAsync();
    }

    public async Task<ApplicationOverviewViewModel> GetByIdAsync(string id, string userId)
    {
        return await this.context.Applications
            .Where(a => a.Id == id && a.CreatorId == userId)
            .Select(a => new ApplicationOverviewViewModel()
            {
                Id = a.Id,
                Name = a.Name,
                Secrets = a.Secrets
                    .Select(s => new SecretOverviewViewModel()
                    {
                        Id = s.Id,
                        Environment = s.Environment,
                        Key = s.Key
                    })
                    .ToArray()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> IsNameAvailableAsync(string name, string userId)
    {
        Application application = await this.context.Applications
            .FirstOrDefaultAsync(a => a.Name == name && a.CreatorId == userId);

        return application == null;
    }

    public async Task<bool> RemoveAsync(string applicationId, string userId)
    {
        Application application = await this.context.Applications
            .FirstOrDefaultAsync(a => a.Id == applicationId && a.CreatorId == userId);

        if (application == null)
        {
            return false;
        }

        this.context.Applications.Remove(application);
        await this.context.SaveChangesAsync();

        return true;
    }

    public async Task<DeletePhraseViewModel> GetDeletePhraseAsync(string id, string userId)
    {
        return await this.context.Applications
            .Where(a => a.Id == id && a.CreatorId == userId)
            .Select(a => new DeletePhraseViewModel()
            {
                CreatorEmail = a.Creator.Email,
                ApplicationName = a.Name,
                ApplicationId = a.Id
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteAsync(string id, string userId)
    {
        Application application = await this.context.Applications
            .Where(a => a.Id == id && a.CreatorId == userId)
            .FirstOrDefaultAsync();

        if (application == null)
        {
            return false;
        }

        await this.secretsService.DeleteAllAsync(id);

        this.context.Applications.Remove(application);
        await this.context.SaveChangesAsync();
        return true;
    }

    public async Task<string> GetSecretKeyAsync(string applicationId, string userId)
    {
        string secretKey = await this.context.Applications
            .Where(a => a.Id == applicationId && a.CreatorId == userId)
            .Select(a => a.SecretKey)
            .FirstOrDefaultAsync();

        return secretKey ?? string.Empty;
    }
}
