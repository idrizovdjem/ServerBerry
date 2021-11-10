namespace SecretsVault.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

using SecretsVault.ViewModels.Response;
using SecretsVault.Services.Authentication;
using SecretsVault.ViewModels.Authenticate;

[ApiController]
[Route("api/[controller]/[action]")]
public class ApplicationsController : ControllerBase
{
    private readonly IAuthenticationService authenticationService;

    public ApplicationsController(IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<AuthenticateResponseModel> Authenticate([FromBody] string secretKey)
    {
        if(string.IsNullOrWhiteSpace(secretKey) == true)
        {
            AuthenticateResponseModel badResponseModel = new AuthenticateResponseModel()
            {
                Successfull = false,
                ErrorMessage = "Missing secretKey",
                StatusCode = 400
            };

            return badResponseModel;
        }

        ApplicationAuthenticateViewModel authenticateViewModel = await this.authenticationService.AuthenticateAsync(secretKey);
        if(string.IsNullOrWhiteSpace(authenticateViewModel.Token) == true || string.IsNullOrWhiteSpace(authenticateViewModel.ApplicationId))
        {
            AuthenticateResponseModel badResponseModel = new AuthenticateResponseModel()
            {
                Successfull = false,
                ErrorMessage = "Invalid secretKey",
                StatusCode = 400
            };

            return badResponseModel;
        }

        AuthenticateResponseModel responseModel = new AuthenticateResponseModel()
        {
            Successfull = true,
            Token = authenticateViewModel.Token,
            ApplicationId = authenticateViewModel.ApplicationId,
            StatusCode = 200
        };

        return responseModel;
    }
}
