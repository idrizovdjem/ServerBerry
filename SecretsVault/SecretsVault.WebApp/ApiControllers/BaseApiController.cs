namespace SecretsVault.WebApp.ApiControllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
    }
}
