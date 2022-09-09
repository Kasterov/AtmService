using SwaggerAPITest.Models;
using SwaggerAPITest.Services.Interfaces;
using static SwaggerAPITest.Models.CardBrand;

namespace SwaggerAPITest.Services;

public class AtmService : IAtmService
{
    private decimal TotalAmount { get; } = 10_000;

    private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
    {
        new ("4444333322221111", "Troy Mcfarland", "edyDfd5A", CardBrands.Visa, 800),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
    };

    public bool IsCardExist(string cardNumber) => Cards.Any(x => x.CardNumber == cardNumber);

    public bool VerifyPassword(string cardNumber, string password) 
        => GetCard(cardNumber)
        .IsPasswordEqual(password);

    private Card GetCard(string cardNumber) => Cards.Single(x => x.CardNumber == cardNumber);

    public decimal GetCardBalance(string cardNumber) => GetCard(cardNumber).GetBalance();
    public void Withdraw(string cardNumber, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException("Amount for withdraw less than 0 or equal!");
        }
        if (amount > GetCard(cardNumber).Balance)
        {
            throw new ArgumentOutOfRangeException("Amount for withdraw biger than current balance!");
        }
        if (amount > TotalAmount)
        {
            throw new ArgumentOutOfRangeException("Amount for withdraw biger than current ATM balance!");
        }
        GetCard(cardNumber).Withdraw(amount);
    }

}
