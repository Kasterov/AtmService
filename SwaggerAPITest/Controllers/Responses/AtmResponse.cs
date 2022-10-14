using SwaggerAPITest.Controllers.Common;

namespace SwaggerAPITest.Controllers.Responses;

public sealed record AtmResponce(string Massage)
{
    public ApiEndpoint[] Links { get; init; } = Array.Empty<ApiEndpoint>();

    public AtmResponce(string message, ApiEndpoint[] links) : this(message) => Links = links;
}