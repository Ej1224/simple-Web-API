using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {

        }

        public DbSet<BankUserAccount> BankUsers { get; set; }
        public DbSet<BankTransaction> BankTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BankUserAccount>()
                .Property(user => user.AccountBalance)
                .HasColumnType("decimal(18,2)");

            builder.Entity<BankTransaction>()
                .Property(trans => trans.TransferAmount)
                .HasColumnType("decimal(18,2)");

            builder.Entity<BankUserAccount>()
                .Property(user => user.RowVersion).IsConcurrencyToken();

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
