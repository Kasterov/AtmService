namespace SwaggerAPITest.Controllers.Requests;

public sealed record CardAddAmountRequest(
    string CardNumber,
    decimal Amount);
