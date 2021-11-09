namespace SecretsVault.WebApp.Controllers;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using SecretsVault.Services.Core;
using SecretsVault.ViewModels.Key;
using SecretsVault.ViewModels.Secret;

[Authorize]
public class SecretsController : Controller
{
    private readonly ISecretsService secretsService;
    public SecretsController(ISecretsService secretsService)
    {
        this.secretsService = secretsService;
    }

    [HttpGet]
    public IActionResult Create(string applicationId)
    {
        if (string.IsNullOrWhiteSpace(applicationId) == true)
        {
            return Redirect("/");
        }

        ViewData["ApplicationId"] = applicationId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSecretInputModel input)
    {
        if (ModelState.IsValid == false)
        {
            return Redirect("/");
        }

        CheckKeyInputModel checkKeyInputModel = new CheckKeyInputModel()
        {
            ApplicationId = input.ApplicationId,
            Environment = input.Environment,
            Key = input.Key
        };

        bool isKeyAvailable = await this.secretsService.IsKeyAvailableAsync(checkKeyInputModel);
        if (isKeyAvailable == false)
        {
            return Redirect("/");
        }

        await this.secretsService.CreateAsync(input);
        return Redirect($"/Applications/Overview?applicationId={input.ApplicationId}");
    }



    [HttpGet]
    public async Task<IActionResult> Edit(string secretId)
    {
        if (string.IsNullOrWhiteSpace(secretId) == true)
        {
            return Redirect("/");
        }

        EditSecretInputModel viewModel = await this.secretsService.GetForEditAsync(secretId);
        if (viewModel == null)
        {
            return Redirect("/");
        }

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditSecretInputModel input)
    {
        if (ModelState.IsValid == false)
        {
            if (string.IsNullOrWhiteSpace(input.Id) == true)
            {
                return Redirect("/");
            }

            return View(input);
        }

        await this.secretsService.EditAsync(input);
        return Redirect("/applications/overview?=applicationId=" + input.Id);
    }
}
