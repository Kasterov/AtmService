using SwaggerAPITest.Models;
using static SwaggerAPITest.DataBase.BankDB;
using static SwaggerAPITest.Models.CardBrand;

namespace SwaggerAPITest.DataBase;

public class BankCreateCards
{
    public List<Card> ListCards;
    public BankCreateCards()
    {
        using (ApplicationContext db = new ApplicationContext()) {
            db.Cards.Add(new ("4444333322221111", "Troy Mcfarland", "edyDfd5A", CardBrands.Visa, 800));
            db.Cards.Add(new ("5200000000001005", "Levi Downs", "teEAxnqg", CardBrands.MasterCard, 400));
            ListCards = db.Cards.ToList();
            db.SaveChanges();
        }
    }
}
