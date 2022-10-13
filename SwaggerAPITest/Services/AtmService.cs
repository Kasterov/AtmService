using SwaggerAPITest.Models.Events;
using SwaggerAPITest.Services.Interfaces;

namespace SwaggerAPITest.Services;

public class AtmService : IAtmService
{
    private readonly IBankService _bankService;

    private decimal TotalAmount { get; set; } = 10_000;
    public AtmService(IBankService bankService)
    {
        _bankService = bankService;
    }
    public decimal GetCardBalance(string cardNumber)
        {
            return _bankService.GetCardBalance(cardNumber);
        }
    public bool IsCardExist(string cardNumber) {

        if (_bankService.IsCardExist(cardNumber))
        {   
            return true;
        }
        throw new UnauthorizedAccessException("Pass identification and authorization!");
    }

    public bool VerifyPassword(string cardNumber, string cardPassword)
    {
        if ( _bankService.VerifyPassword(cardNumber, cardPassword))
        {
            return true;
        }

        throw new UnauthorizedAccessException("Pass identification and authorization!");
    }  

    public void Withdraw(string cardNumber, decimal amount)
    {
        _bankService.Withdraw(cardNumber, amount);

        TotalAmount -= amount;
    }

    public void AddAmount(string cardNumber, decimal amount)
    {
        _bankService.AddAmount(cardNumber, amount);

        TotalAmount += amount;
    }

    public void Tranzaction(string cardNumberSender, string cardNumberReceiver, decimal amount)
    {
        _bankService.Tranzaction(cardNumberSender, cardNumberReceiver, amount);
    }
}
    