namespace AppRunner.WebApi.Controllers;

using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using AppRunner.Services.Application;
using AppRunner.ViewModels.Application;

public class ApplicationsController : BaseController
{
    private readonly IApplicationsService applicationsService;

    public ApplicationsController(IApplicationsService applicationsService)
    {
        this.applicationsService = applicationsService;
    }

    [HttpGet]
    public async Task<ActionResult> OnGetAsync()
    {
        IEnumerable<ApplicationViewModel> applications = await this.applicationsService.GetApplicationsAsync();
        return Ok(applications);
    }
}
