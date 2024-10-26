using Alza.DbContexts.Models;
using Alza.Infrastructure.Dto.Product;

namespace Alza.Infrastructure.Mappers
{
    public class ProductMapper
    {
        public static ProductDto MapToProductDto(Product product)
        {
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                ImgUri = product.ImgUri,
                Price = product.Price,
                Description = product.Description
            };
        }
    }
}
