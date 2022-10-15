using Microsoft.AspNetCore.Mvc;
using SwaggerAPITest.Controllers.Requests;
using SwaggerAPITest.Controllers.Responses;
using SwaggerAPITest.Services.Interfaces;

namespace SwaggerAPITest.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AtmController : ControllerBase
{
    private readonly IAtmLinkGenerator _atmLinkGenerator;
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService, IAtmLinkGenerator atmLinkGenerator)
    {
        _atmLinkGenerator = atmLinkGenerator;
        _atmService = atmService;
    }

    [HttpGet("cards/{cardNumber}/init", Name = nameof(Init))]
    public IActionResult Init([FromRoute] string cardNumber)
    {
        var links = _atmLinkGenerator.GetAssociatedEndpoints(HttpContext, nameof(Init));

        return _atmService.IsCardExist(cardNumber)
            ? Ok(new AtmResponce("Your card is in system!", links))
            : NotFound(new AtmResponce("Your card is NOT in system!"));
    }

    [HttpPost("cards/authorize", Name = nameof(Authorize))]
    public IActionResult Authorize([FromBody] CardAuthorizeRequest request)
    {
        var links = _atmLinkGenerator.GetAssociatedEndpoints(HttpContext, nameof(Authorize), new { request.CardNumber });

        return _atmService.VerifyPassword(request.CardNumber, request.Password)
            ? Ok(new AtmResponce("Authorization is successful!", links))
            : NotFound(new AtmResponce("Authorization is NOT successful!"));
    }

    [HttpPost("cards/addAmount", Name = nameof(AddAmount))]
    public IActionResult AddAmount([FromBody] CardAddAmountRequest request)
    {
        var links = _atmLinkGenerator.GetAssociatedEndpoints(HttpContext, nameof(AddAmount));

        _atmService.AddAmount(request.CardNumber, request.Amount);
        return Ok(new AtmResponce($"AddAmount is successful! +{request.Amount}", links));
    }

    [HttpPost("cards/withdraw", Name = nameof(Withdraw))]
    public IActionResult Withdraw([FromBody] CardWithdrawRequest request)
    {
        var links = _atmLinkGenerator.GetAssociatedEndpoints(HttpContext, nameof(Withdraw));

        _atmService.Withdraw(request.CardNumber,request.Amount);
        return Ok(new AtmResponce($"Withdraw successful! -{request.Amount}", links));
    }

    [HttpGet("cards/{cardNumber}/getBalance", Name = nameof(GetBalance))]
    public IActionResult GetBalance([FromRoute] string cardNumber)
    {
        var links = _atmLinkGenerator.GetAssociatedEndpoints(HttpContext, nameof(GetBalance));

        return Ok(new AtmResponce($"GetBalance is successful! Balance:{_atmService.GetCardBalance(cardNumber)}", links));
    }

    [HttpPost("cards/tranzaction", Name = nameof(Tranzaction))]
    public IActionResult Tranzaction([FromBody] CardTranzactionRequest request)
    {
        var links = _atmLinkGenerator.GetAssociatedEndpoints(HttpContext, nameof(Tranzaction));

        _atmService.Tranzaction(request.CardNumberSender, request.CardNumberReceiver, request.Amount);
        return Ok(new AtmResponce($"Tranzaction is successful!Amount sent: {request.Amount} to card: {request.CardNumberReceiver}", links));
    }
}
