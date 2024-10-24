using Alza.Infrastructure.Dto.Product;
using Alza.Infrastructure.Operations.Transient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alza.Web.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController(ProductOperation productOperation) : ControllerBase
    {

        private readonly ProductOperation productOperation = productOperation;

        /// <summary>
        /// List all available products with pagination support
        /// </summary>
        [HttpPost]
        public ProductResponse GetProducts(ProductRequest productRequest)
        {
            return productOperation.GetProducts(productRequest);
        }

        /// <summary>
        /// Get one product by product Id
        /// </summary>
        [HttpGet("{productId}")]
        public ProductDto GetProductById(int productId)
        {
            return productOperation.GetProductById(productId);
        }

        /// <summary>
        /// Update product description only by product Id
        /// </summary>
        [HttpPatch("{productId}")]
        public void UpdateProductDescriptionById(int productId, string description)
        {
            productOperation.UpdateProductDescriptionById(productId, description);
        }

    }
}
