using Microsoft.AspNetCore.Mvc;
using static SwaggerAPITest.Models.CardBrand;

namespace SwaggerAPITest.Models;

public class Card
{
    public string CardNumber { get; }
    public string UserName { get; }
    public string Password { get; }
    public CardBrands Brand { get; }

    public decimal Balance { get; set; }

    public Card(string cardNumber, string userName, string password, CardBrands cardBrands, decimal balance)
    {
        CardNumber = cardNumber;
        UserName = userName;
        Password = password;
        Brand = cardBrands;
        Balance = balance;  
    }
    public decimal Withdraw(decimal amount) => Balance -= amount;

    public decimal GetBalance() => Balance;

    public bool IsPasswordEqual(string password) => password == Password;
}

