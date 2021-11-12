namespace SecretsVault.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

using SecretsVault.Services.Core;
using SecretsVault.ViewModels.Response;
using SecretsVault.ViewModels.Application;

[ApiController]
[Route("api/[controller]/[action]")]
public class ApplicationsController : ControllerBase
{
    private readonly IApplicationsService applicationsService;

    public ApplicationsController(IApplicationsService applicationsService)
    {
        this.applicationsService = applicationsService;
    }

    [HttpPost]
    public async Task<CreateApplicationResponseModel> Create(CreateApplicationWithUserIdInputModel input)
    {
        if(string.IsNullOrEmpty(input?.Name) == true)
        {
            return new CreateApplicationResponseModel()
            {
                Successfull = false,
                StatusCode = 400,
                ErrorMessage = "Application name is required",
                Created = false
            };
        }

        if(string.IsNullOrEmpty(input?.UserId) == true)
        {
            return new CreateApplicationResponseModel()
            {
                StatusCode = 400,
                Successfull = false,
                ErrorMessage = "User id or token is required",
                Created = false
            };
        }

        bool nameAvailable = await this.applicationsService.IsNameAvailableAsync(input.Name, input.UserId);
        if(nameAvailable == false)
        {
            return new CreateApplicationResponseModel()
            {
                StatusCode = 400,
                Successfull = false,
                ErrorMessage = "This application name is already in use",
                Created = false
            };
        }

        await this.applicationsService.CreateAsync(input);
        return new CreateApplicationResponseModel()
        {
            StatusCode = 201,
            Successfull = true,
            Created = true,
            ErrorMessage = String.Empty
        };
    }
}
