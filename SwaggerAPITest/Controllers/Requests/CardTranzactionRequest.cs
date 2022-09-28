namespace SwaggerAPITest.Controllers.Requests;

public sealed record CardTranzactionRequest(
    string CardNumberSender,
    string CardNumberReceiver,
    decimal Amount);
