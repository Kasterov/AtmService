﻿using SwaggerAPITest.Models;
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

    public IReadOnlyCollection<Card> cards = new List<Card> {
        new("4444333322221111", "Troy Mcfarland", "edyDfd5A", CardBrands.Visa, 800),
        new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
    };

    public bool IsCardExist(string cardNumber) => cards.Any(x => x.CardNumber == cardNumber);

    public bool VerifyPassword(string cardNumber, string password)
        => GetCard(cardNumber)
        .IsPasswordEqual(password);

    public Card GetCard(string cardNumber) => cards.Single(x => x.CardNumber == cardNumber);

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

        if (amount  > limit)
        {
            throw new InvalidOperationException($"One time {card.CardBrand} withdraw limit is {limit}");
        }

        card.Withdraw(amount);
    }

    public void AddAmount(string cardNumber, decimal amount)
    {
        var cardToAdd = GetCard(cardNumber);
        cardToAdd.AddAmount(amount);
    }
    public void Tranzaction(string cardNumberSender, string cardNumberReceiver, decimal amount)
    {
        Withdraw(cardNumberSender, amount);
        AddAmount(cardNumberReceiver, amount);
    }
};

