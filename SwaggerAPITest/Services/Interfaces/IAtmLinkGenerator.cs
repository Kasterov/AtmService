using SwaggerAPITest.Controllers.Common;

namespace SwaggerAPITest.Services.Interfaces;

public interface IAtmLinkGenerator
{
    public ApiEndpoint[] GetAssociatedEndpoints(HttpContext httpContext, string endpointName, object? values = null);
}
