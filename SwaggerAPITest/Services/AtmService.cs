using SwaggerAPITest.Models;
using SwaggerAPITest.Services.Interfaces;
using static SwaggerAPITest.Models.CardBrand;

namespace SwaggerAPITest.Services;

public class AtmService : IAtmService
{
    private readonly IBankService _bankService;
    private decimal TotalAmount { get; set; } = 10_000;
    public AtmService(IBankService bankService)
    {
        _bankService = bankService;
    }

    public bool IsCardExist(string cardNumber) => 
        _bankService.IsCardExist(cardNumber);

    public bool VerifyPassword(string cardNumber, string password) => 
        _bankService.VerifyPassword(cardNumber, password);

    public decimal GetCardBalance(string cardNumber) => 
        _bankService.GetCardBalance(cardNumber);

    public void Withdraw(string cardNumber, decimal amount)
    {      

        if (amount > TotalAmount)
        {
            throw new ArgumentOutOfRangeException("Amount for withdraw biger than current ATM balance!");
        }
        
        _bankService.Withdraw(cardNumber, amount);

        TotalAmount -= amount;
    }
}
