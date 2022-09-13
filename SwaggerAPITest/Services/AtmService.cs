using SwaggerAPITest.Models;
using SwaggerAPITest.Services.Interfaces;
using static SwaggerAPITest.Models.CardBrand;

namespace SwaggerAPITest.Services;

public class AtmService : IAtmService
{
    private readonly IBankService _bankService;

    public AtmService(IBankService bankService)
    {
        _bankService = bankService;
    }

    private decimal TotalAmount { get; } = 10_000;

    public bool IsCardExist(string cardNumber) => _bankService.IsCardExist(cardNumber);

    public bool VerifyPassword(string cardNumber, string password) => _bankService.VerifyPassword(cardNumber, password);

    private Card GetCard(string cardNumber) => _bankService.GetCard(cardNumber);

    public decimal GetCardBalance(string cardNumber) => _bankService.GetCardBalance(cardNumber);

    public void Withdraw(string cardNumber, decimal amount)
    {
        _bankService.Withdraw(cardNumber, amount);

        if (amount > TotalAmount)
        {
            throw new ArgumentOutOfRangeException("Amount for withdraw biger than current ATM balance!");
        }

        GetCard(cardNumber).Withdraw(amount);
    }
}
