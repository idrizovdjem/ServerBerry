namespace SecretsVault.Services.Authentication;

using System.Threading.Tasks;

using SecretsVault.ViewModels.Authenticate;

public interface IAuthenticationService
{
    Task<ApplicationAuthenticateViewModel> AuthenticateAsync(string secretKey);
}