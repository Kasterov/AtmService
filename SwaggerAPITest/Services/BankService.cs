using SwaggerAPITest.DataBase;
using SwaggerAPITest.Models;
using SwaggerAPITest.Services.Interfaces;
using static SwaggerAPITest.Models.CardBrand;

namespace SwaggerAPITest.Services;

public class BankService : IBankService
{
    private static readonly IReadOnlyCollection<CardBrandLimit> WithdrawLimits = new List<CardBrandLimit>
    {
        new(CardBrands.Visa, 200),
        new(CardBrands.MasterCard, 300)
    };

    private readonly BankDbContext _dbContext;

    public BankService(BankDbContext context)
    {
        _dbContext = context;
    }

    public bool IsCardExist(string cardNumber) => _dbContext.Cards.Any(x => x.CardNumber == cardNumber);

    public bool VerifyPassword(string cardNumber, string password)
        => GetCard(cardNumber)
        .IsPasswordEqual(password);

    public Card GetCard(string cardNumber) => _dbContext.Cards.Single(x => x.CardNumber == cardNumber);

    public decimal GetCardBalance(string cardNumber)
        => GetCard(cardNumber)
        .GetBalance();

    private static decimal GetWithdrawLimit(CardBrands cardBrand)
    {
        return WithdrawLimits.First(x => x.CardBrand == cardBrand).Amount;
    }

    public void Withdraw(string cardNumber, decimal amount)
    {
        var card = GetCard(cardNumber);
        var limit = GetWithdrawLimit(card.CardBrand);

        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException("Amount for withdraw less than 0 or equal!");
        }

        if (amount > card.Balance)
        {
            throw new ArgumentOutOfRangeException("Amount for withdraw biger than current balance!");
        }

        if (amount > limit)
        {
            throw new InvalidOperationException($"One time {card.CardBrand} withdraw limit is {limit}");
        }

        card.Withdraw(amount);
        _dbContext.SaveChangesAsync();
    }

    public void AddAmount(string cardNumber, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException("Amount for add less than 0 or equal!");
        }

        var cardToAdd = GetCard(cardNumber);
        cardToAdd.AddAmount(amount);
        _dbContext.SaveChangesAsync();
    }
    public void Tranzaction(string cardNumberSender, string cardNumberReceiver, decimal amount)
    {
        Withdraw(cardNumberSender, amount);
        AddAmount(cardNumberReceiver, amount);
        _dbContext.SaveChangesAsync();
    }
};

