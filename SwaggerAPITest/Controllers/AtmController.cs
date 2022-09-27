using Microsoft.AspNetCore.Mvc;
using SwaggerAPITest.Controllers.Requests;
using SwaggerAPITest.Controllers.Responses;
using SwaggerAPITest.Services.Interfaces;

namespace SwaggerAPITest.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AtmController : ControllerBase
{
    private readonly IAtmService _atmService;

    public AtmController(IAtmService atmService)
    {
        _atmService = atmService;
    }

    [HttpGet("cards/{cardNumber}/init")]
    public IActionResult Init([FromRoute] string cardNumber)
    {
        return _atmService.IsCardExist(cardNumber)
            ? Ok(new AtmResponse("Your card is in system!"))
            : NotFound(new AtmResponse("Your card is NOT in system!"));
    }

    [HttpPost("cards/authorize")]
    public IActionResult Authorize([FromBody] CardAuthorizeRequest request)
    {
        return _atmService.VerifyPassword(request.CardNumber, request.Password)
            ? Ok(new AtmResponse("Authorization is successful!"))
            : NotFound(new AtmResponse("Authorization is NOT successful!"));
    }

    [HttpPost("cards/withdraw")]
    public IActionResult Withdraw([FromBody] CardWithdrawRequest request)
    {
        _atmService.Withdraw(request.CardNumber,request.Amount);
        return Ok(new AtmResponse($"Withdraw successful! -{request.Amount}"));
    }

    [HttpGet("cards/{cardNumber}/getBalance")]
    public IActionResult GetBalance([FromRoute] string cardNumber)
    {
        return Ok(new AtmResponse($"GetBalance is successful! Balance:{_atmService.GetCardBalance(cardNumber)}"));
    }
}
