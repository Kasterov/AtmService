using SwaggerAPITest.Models;

namespace SwaggerAPITest.Services.Interfaces;

public interface IBankService
{
    public bool IsCardExist(string cardNumber);

    public bool VerifyPassword(string cardNumber, string password);

    public Card GetCard(string cardNumber);

    public decimal GetCardBalance(string cardNumber);

    public void Withdraw(string cardNumber, decimal amount);

    public void AddAmount(string cardNumber, decimal amount);

    public void Tranzaction(string cardNumberSender, string cardNumberReceiver, decimal amount);
}
