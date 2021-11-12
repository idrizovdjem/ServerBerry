namespace SecretsVault.Core.Constants;

using SecretsVault.Common.Constants;

internal static class ApiEndpointConstants
{
    public const string AuthenticationEndpoint = $"{ApiConstants.Endpoint}/api/authentication";

    public const string GetSecretEndpoint = $"{ApiConstants.Endpoint}/api/secrets/getvalue";

    public const string SecretExistsEndpoint = $"{ApiConstants.Endpoint}/api/secrets/exists";

    public const string CreateSecretEndpoint = $"{ApiConstants.Endpoint}/api/secrets/create";

    public const string DeleteSecretEndpoint = $"{ApiConstants.Endpoint}/api/secrets/delete";

    public const string CreateApplicationEndpoint = $"{ApiConstants.Endpoint}/api/applications/create";
}
