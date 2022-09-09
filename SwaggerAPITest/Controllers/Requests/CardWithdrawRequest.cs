namespace SwaggerAPITest.Controllers.Requests;

public sealed record CardWithdrawRequest(
    string CardNumber,
    decimal Amount);
