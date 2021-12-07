namespace AppRunner.WebApi.Controllers;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using AppRunner.Common.Constants;
using AppRunner.Services.Application;
using AppRunner.ViewModels.Application;

public class ApplicationsController : BaseController
{
    private readonly IApplicationsService applicationsService;

    public ApplicationsController(IApplicationsService applicationsService)
    {
        this.applicationsService = applicationsService;
    }

    [HttpGet(ApplicationsEndpoints.BaseUrl)]
    public async Task<ActionResult> GetAll()
    {
        IEnumerable<ApplicationViewModel> applications = await this.applicationsService.GetApplicationsViewModelsAsync();
        return Ok(applications);
    }

    [HttpGet(ApplicationsEndpoints.CheckNameUrl)]
    public async Task<ActionResult> CheckName(string value)
    {
        bool isNameAvailable = await this.applicationsService.IsNameAvailableAsync(value);
        return Ok(isNameAvailable);
    }
}
