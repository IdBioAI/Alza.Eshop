using Alza.Infrastructure.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alza.Infrastructure.Operations.Transient
{
    public interface IProductOperation
    {
        ProductResponse GetProducts(ProductRequest productRequest);
        ProductDto GetProductById(int productId);
        void UpdateProductDescriptionById(int productId, string description);
    }

    public class ProductOperation : IProductOperation
    {
        public ProductResponse GetProducts(ProductRequest productRequest)
        {
            return new ProductResponse();
        }

        public ProductDto GetProductById(int productId) 
        {
            return new ProductDto();
        }

        public void UpdateProductDescriptionById(int productId, string description)
        {
            
        }
    }
}
