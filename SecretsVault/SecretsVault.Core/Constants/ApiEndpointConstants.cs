namespace SecretsVault.Core.Constants;

internal static class ApiEndpointConstants
{
    public const string AuthenticationEndpoint = "http://localhost:5000/api/applications/authenticate";

    public const string GetSecretEndpoint = "http://localhost:5000/api/secrets/getvalue";

    public const string SecretExistsEndpoint = "http://localhost:5000/api/secrets/exists";
}
