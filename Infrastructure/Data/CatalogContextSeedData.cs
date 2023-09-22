using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities;
namespace Infrastructure.Data
{
    public class CatalogContextSeedData
    {
        public static async Task SeedAsync(CatalogContext catalogContext)
        {
			try
			{
                if (catalogContext.Database.IsSqlServer())
                {
                    catalogContext.Database.EnsureDeleted();
                    catalogContext.Database.Migrate();
                }

                if(!await catalogContext.BankUsers.AnyAsync())
                {
                    await catalogContext.BankUsers.AddRangeAsync(
                        new BankUserAccount("ej", 5000.50M) { AccountId = "202309211556581" },
                        new BankUserAccount("user2", 3214.55M) { AccountId = "202309211556582" }
                        );
                    await catalogContext.SaveChangesAsync();
                }
            }
			catch (Exception ex)
			{
                Console.WriteLine(ex.ToString());
				throw;
			}
        }
    }
}
