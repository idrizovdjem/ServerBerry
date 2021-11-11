namespace SecretsVault.Core.Constants;

internal static class ApiEndpointConstants
{
    public const string AuthenticationEndpoint = "http://localhost:5000/api/applications/authenticate";

    public const string GetSecretEndpoint = "http://localhost:5000/api/secrets/getvalue";

    public const string SecretExistsEndpoint = "http://localhost:5000/api/secrets/exists";

    public const string CreateSecretEndpoint = "http://localhost:5000/api/secrets/create";

    public const string DeleteSecretEndpoint = "http://localhost:5000/api/secrets/delete";
}
