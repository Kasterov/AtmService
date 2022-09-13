using SwaggerAPITest.Models;
using SwaggerAPITest.Services.Interfaces;
using static SwaggerAPITest.Models.CardBrand;

namespace SwaggerAPITest.Services;

public class BankService : IBankService
{
    private decimal TotalAmountVisa { get; } = 200;
    private decimal TotalAmountMasterCard { get; } = 300;

    private static readonly IReadOnlyCollection<Card> Cards = new List<Card>
    {
        new ("4444333322221111", "Troy Mcfarland", "edyDfd5A", CardBrands.Visa, 800),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
    };

    public bool IsCardExist(string cardNumber) => Cards.Any(x => x.CardNumber == cardNumber);

    public bool VerifyPassword(string cardNumber, string password)
        => GetCard(cardNumber)
        .IsPasswordEqual(password);

    public Card GetCard(string cardNumber) => Cards.Single(x => x.CardNumber == cardNumber);

    public decimal GetCardBalance(string cardNumber)
        => GetCard(cardNumber)
        .GetBalance();

    public void Withdraw(string cardNumber, decimal amount)
    {
        var card = GetCard(cardNumber);

        if (amount <= 0)
        {
             throw new ArgumentOutOfRangeException("Amount for withdraw less than 0 or equal!");
        }

        if (amount > card.Balance)
        {
            throw new ArgumentOutOfRangeException("Amount for withdraw biger than current balance!");
        }

        switch (card.Brand)
        {
            case CardBrands.Visa:
                if (amount >= TotalAmountVisa)
                {
                    throw new InvalidOperationException($"One time {card.Brand} withdraw limit is {TotalAmountVisa}");
                }
                break;
            case CardBrands.MasterCard:
                if (amount >= TotalAmountMasterCard)
                {
                    throw new InvalidOperationException($"One time {card.Brand} withdraw limit is {TotalAmountMasterCard}");
                }
                break;
            default:
                break;
        }
    }
};

