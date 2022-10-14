namespace SwaggerAPITest.Controllers.Common;

public record ApiEndpoint(
        string Rel,
        string? Href,
        string Method);
