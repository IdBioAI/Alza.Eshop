using Alza.DbContexts;
using Alza.DbContexts.Models;
using Microsoft.EntityFrameworkCore;

namespace Alza.Infrastructure.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(int page, int pageSize);
        Task<Product?> GetProductById(int productId);
    }

    public class ProductRepository(ApplicationDbContext applicationDbContext) : IProductRepository
    {
        private readonly ApplicationDbContext applicationDbContext = applicationDbContext;

        public async Task<List<Product>> GetProducts(int page, int pageSize)
        {
            return await applicationDbContext.Products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Product?> GetProductById(int productId)
        {
            return await applicationDbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }
    }
}
