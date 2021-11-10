namespace SecretsVault.ViewModels.Response;

public class AuthenticateResponseModel : BaseResponseModel
{
    public string Token { get; set; }

    public string ApplicationId { get; set; }
}