namespace AppRunner.Common.Constants;

public static class ApplicationsEndpoints
{
    public const string BaseUrl = "/api/applications";

    public const string CheckNameUrl = $"{BaseUrl}/name/{{value}}";
}
