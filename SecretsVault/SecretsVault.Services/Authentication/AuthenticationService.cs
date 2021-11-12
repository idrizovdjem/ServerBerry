namespace SecretsVault.Services.Authentication;

using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SecretsVault.Data;
using SecretsVault.ViewModels.Authenticate;

public class AuthenticationService : IAuthenticationService
{
    private readonly ApplicationDbContext context;

    public AuthenticationService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<ApplicationAuthenticateViewModel> AuthenticateAsync(string secretKey)
    {
        return await this.context.Applications
            .Where(a => a.SecretKey == secretKey)
            .Select(a => new ApplicationAuthenticateViewModel()
            {
                ApplicationId = a.Id,
                Token = a.CreatorId
            })
            .FirstOrDefaultAsync();
    }
}