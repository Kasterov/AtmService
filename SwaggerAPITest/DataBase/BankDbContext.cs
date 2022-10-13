using Microsoft.EntityFrameworkCore;
using SwaggerAPITest.Models;

namespace SwaggerAPITest.DataBase;

public class BankDbContext : DbContext
{
    public DbSet<Card> Cards => Set<Card>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=BankCards.db");
    }
}
