using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public class Dependencies
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            //var useOnlyInMemoryDatabase = false;
            //if (configuration["UseOnlyInMemoryDatabase"] != null)
            //{
            //    useOnlyInMemoryDatabase = bool.Parse(configuration["UseOnlyInMemoryDatabase"]);
            //}

            //if (useOnlyInMemoryDatabase)
            //{
            //    services.AddDbContext<CatalogContext>(c =>
            //       c.UseInMemoryDatabase("Catalog"));
            //}
            //else
            //{
                services.AddDbContext<CatalogContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("LocalConnection")));
            //}
        }
    }
}