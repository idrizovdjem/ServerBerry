namespace SecretsVault.ViewModels.Response;

public class BaseResponseModel
{
    public bool Successfull { get; set; }

    public string ErrorMessage { get; set; }

    public short StatusCode { get; set; }
}