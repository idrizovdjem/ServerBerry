namespace SecretsVault.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

using SecretsVault.ViewModels.Response;
using SecretsVault.ViewModels.Authenticate;
using SecretsVault.Services.Authentication;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    [HttpPost]
    public async Task<AuthenticateResponseModel> Authenticate([FromBody] string secretKey)
    {
        if (string.IsNullOrWhiteSpace(secretKey) == true)
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
        if (authenticateViewModel == null)
        {
            AuthenticateResponseModel notFoundResponseModel = new AuthenticateResponseModel()
            {
                StatusCode = 404,
                Successfull = false,
                ErrorMessage = "Application not found"
            };

            return notFoundResponseModel;
        }

        if (string.IsNullOrWhiteSpace(authenticateViewModel.ApplicationId))
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
            ApplicationId = authenticateViewModel.ApplicationId,
            Token = authenticateViewModel.Token,
            StatusCode = 200
        };

        return responseModel;
    }
}