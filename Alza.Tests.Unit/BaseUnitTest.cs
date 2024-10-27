using Alza.DbContexts;
using Alza.DbContexts.Models;
using Alza.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Alza.Tests.Unit
{

    public class BaseUnitTest
    {
        public readonly IConfiguration AlzaConfiguration;
        public readonly ServiceProvider ServiceProvider;

        private void LoadMockData() 
        {
            // load Product data
            var jsonFilePath = Path.Combine("Products.json");
            var json = File.ReadAllText(jsonFilePath);
            var mockProducts = JsonSerializer.Deserialize<List<Product>>(json);

            var context = ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.AddRange(mockProducts);
            context.SaveChanges();
        }

        public BaseUnitTest()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json");

            AlzaConfiguration = builder.Build();

            // dependency injection
            var services = new ServiceCollection();
            services.AddScoped<IProductRepository, ProductRepository>();

            // use mock data from file or real database 
            if (AlzaConfiguration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("TestDatabase"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(AlzaConfiguration.GetConnectionString("AlzaShopDbContext")));
            }

            ServiceProvider = services.BuildServiceProvider();
            if (AlzaConfiguration.GetValue<bool>("UseInMemoryDatabase")) LoadMockData();
        }
    }
}
