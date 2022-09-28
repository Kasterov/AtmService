using Microsoft.AspNetCore.Mvc;
using static SwaggerAPITest.Models.CardBrand;

namespace SwaggerAPITest.Models;

public class Card
{
    public int Id { get; set; }
    public string CardNumber { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public CardBrands CardBrand { get; set; }

    public decimal Balance { get; set; }

    public Card() {}
    public Card(string cardNumber, string userName, string password, CardBrands cardBrands, decimal balance)
    {
        CardNumber = cardNumber;
        UserName = userName;
        Password = password;
        CardBrand = cardBrands;
        Balance = balance;  
    }
    public decimal Withdraw(decimal amount) => Balance -= amount;

    public decimal GetBalance() => Balance;

    public decimal AddAmount(decimal amount) => Balance += amount;

    public bool IsPasswordEqual(string password) => password == Password;
}

