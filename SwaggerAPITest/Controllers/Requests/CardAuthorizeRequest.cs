namespace SwaggerAPITest.Controllers.Requests;

public sealed record CardAuthorizeRequest(
    string CardNumber,
    string Password);
