using SwaggerAPITest.Models;
using static SwaggerAPITest.Models.CardBrand;

namespace SwaggerAPITest.DataBase;

public class BankDbCards
{
    public static void Init(BankDbContext db)
    {
        db.Database.EnsureCreated();

        if (!db.Cards.Any())
        {
            Card[] cards = {
                new("4444333322221111", "Troy Mcfarland", "edyDfd5A", CardBrands.Visa, 800),
                new("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400)
            };

            foreach (Card card in cards)
            {
                db.Cards.Add(card);
            }
            db.SaveChanges();
        }
    }
}
