using SwaggerAPITest.Models;
using static SwaggerAPITest.DataBase.BankDbContext;
using static SwaggerAPITest.Models.CardBrand;

namespace SwaggerAPITest.DataBase;

public class BankDbCards
{
    public static void Init(BankDbContext db)
    {

        if (!db.Cards.Any())
        {
            db.Cards.Add(new("4444333322221111", "Troy Mcfarland", "edyDfd5A", CardBrands.Visa, 800));
            db.Cards.Add(new("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400));
            db.SaveChanges();
        }
    }
}
