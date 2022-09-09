using Microsoft.AspNetCore.Mvc;
using SwaggerAPITest.Controllers.Requests;
using SwaggerAPITest.Controllers.Responses;
using SwaggerAPITest.Models;
using SwaggerAPITest.Services.Interfaces;
using static SwaggerAPITest.Models.CardBrand;

namespace SwaggerAPITest.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AtmController : Controller
{
    private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
    {
        new ("4444333322221111", "Troy Mcfarland", "edyDfd5A", CardBrands.Visa, 800),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
    };

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
