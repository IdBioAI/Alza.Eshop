using Alza.Infrastructure.Dto.Product;
using Alza.Infrastructure.Operations.Transient;
using Microsoft.AspNetCore.Mvc;

namespace Alza.Web.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController(IProductOperation productOperation) : CommonController
    {

        private readonly IProductOperation productOperation = productOperation;

        /// <summary>
        /// List all available products with pagination support
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> GetProducts(ProductRequest productRequest)
        {
            return HandleResponse<ProductResponse>(await productOperation.GetProducts(productRequest));
        }

        /// <summary>
        /// Get one product by product Id
        /// </summary>
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            return HandleResponse<ProductDto>(await productOperation.GetProductById(productId));
        }

        /// <summary>
        /// Update product description only by product Id
        /// </summary>
        [HttpPatch("{productId}")]
        public async Task<IActionResult> UpdateProductDescriptionById(int productId, string description)
        {
            return HandleResponse(await productOperation.UpdateProductDescriptionById(productId, description));
        }

    }
}
