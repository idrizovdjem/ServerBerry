namespace SecretsVault.WebApp.ApiControllers
{
    using System.Threading.Tasks;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    using SecretsVault.Services.Core;
    using SecretsVault.ViewModels.Application;

    [Route("api/applications/[action]")]
    public class ApplicationsApiController : BaseApiController
    {
        private readonly IApplicationsService applicationsService;

        public ApplicationsApiController(IApplicationsService applicationsService)
        {
            this.applicationsService = applicationsService;
        }

        public async Task<bool> IsNameAvailable(CheckApplicationNameInputModel input)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await this.applicationsService.IsNameAvailableAsync(input.Name, userId);
        }
    }
}
