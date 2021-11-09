namespace AppRunner.WebApi.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;

    using AppRunner.Data.Models;
    using AppRunner.Services.Application;

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
            Application[] applications = await this.applicationsService.GetApplicationsAsync();
            return Ok(applications);
        }
    }
}
