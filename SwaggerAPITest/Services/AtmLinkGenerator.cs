using SwaggerAPITest.Controllers;
using SwaggerAPITest.Controllers.Common;
using SwaggerAPITest.Extensions;
using SwaggerAPITest.Services.Interfaces;

namespace SwaggerAPITest.Services;

public sealed class AtmLinkGenerator : IAtmLinkGenerator
{
    private readonly LinkGenerator _linkGenerator;

    public AtmLinkGenerator(LinkGenerator linkGenerator) => _linkGenerator = linkGenerator;

    public ApiEndpoint[] GetAssociatedEndpoints(HttpContext httpContext, string endpointName, object? values = null)
    {
        return endpointName switch
        {
            nameof(AtmController.Init) or 
            nameof(AtmController.GetBalance) or 
            nameof(AtmController.Withdraw) or
            nameof(AtmController.AddAmount) or
            nameof(AtmController.Tranzaction)
            => new[]
            {
                _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Post, nameof(AtmController.Authorize))
            },
            nameof(AtmController.Authorize) => new[]
            {
                _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Get, nameof(AtmController.GetBalance), values),
                _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Post, nameof(AtmController.Withdraw)),
                _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Post, nameof(AtmController.AddAmount)),
                _linkGenerator.GetAssociatedEndpoint(httpContext, HttpMethod.Post, nameof(AtmController.Tranzaction)),
            },
            _ => throw new ArgumentOutOfRangeException("Invalid data!")
        };
    }
}
