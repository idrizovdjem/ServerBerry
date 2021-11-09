namespace SecretsVault.WebApp.Controllers;

using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SecretsVault.Services.Core;
using SecretsVault.ViewModels.Application;

[Authorize]
public class HomeController : Controller
{
    private readonly IApplicationsService applicationsService;

    public HomeController(IApplicationsService applicationsService)
    {
        this.applicationsService = applicationsService;
    }

    public async Task<IActionResult> Index()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        ApplicationViewModel[] applications = await this.applicationsService.GetAllAsync(userId);
        return View(applications);
    }
}
