using Microsoft.EntityFrameworkCore;
using SwaggerAPITest.Models;

namespace SwaggerAPITest.DataBase;

public class BankDB : DbContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Card> Cards => Set<Card>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=BankCards.db");
        }
    }
}
