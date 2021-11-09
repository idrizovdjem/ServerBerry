namespace SecretsVault.WebApp.Controllers
{
    using System.Threading.Tasks;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using SecretsVault.ViewModels.Application;
    using SecretsVault.Services.Core;

    [Authorize]
    public class ApplicationsController : Controller
    {
        private readonly IApplicationsService applicationsService;

        public ApplicationsController(IApplicationsService applicationsService)
        {
            this.applicationsService = applicationsService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateApplicationInputModel input)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isNameAvailable = await this.applicationsService.IsNameAvailableAsync(input.Name, userId);

            if(isNameAvailable == false)
            {
                return View();
            }

            await this.applicationsService.CreateAsync(input, userId);
            return Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Overview(string applicationId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationOverviewViewModel viewModel = await this.applicationsService.GetByIdAsync(applicationId, userId);
            if(viewModel == null)
            {
                return Redirect("/");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string applicationId)
        {
            if (string.IsNullOrWhiteSpace(applicationId) == true)
            {
                return Redirect("/");
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DeletePhraseViewModel deletePhraseViewModel = await this.applicationsService.GetDeletePhraseAsync(applicationId, userId);

            if (deletePhraseViewModel == null)
            {
                return Redirect("/");
            }

            return View(deletePhraseViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteApplicationInputModel input)
        {
            if(ModelState.IsValid == false)
            {
                return Redirect("/");
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DeletePhraseViewModel deletePhraseViewModel = await this.applicationsService.GetDeletePhraseAsync(input.ApplicationId, userId);

            if($"{deletePhraseViewModel.CreatorEmail}/{deletePhraseViewModel.ApplicationName}" != input.DeletePhrase)
            {
                return Redirect("/");
            }

            await this.applicationsService.DeleteAsync(input.ApplicationId, userId);
            return Redirect("/");
        }
    }
}
