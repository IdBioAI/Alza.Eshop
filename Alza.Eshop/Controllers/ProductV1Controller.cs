using Alza.Infrastructure.Dto.Product;
using Alza.Infrastructure.Operations.Transient;
using Microsoft.AspNetCore.Mvc;

namespace Alza.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/products")]
    [ApiVersion("1")]
    public class ProductV1Controller(IProductV1Operation productV1Operation) : CommonController
    {

        private readonly IProductV1Operation productV1Operation = productV1Operation;

        /// <summary>
        /// List all available products
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts()
        {
            return HandleResponse<ProductResponse>(await productV1Operation.GetProducts());
        }

        /// <summary>
        /// Get one product by product Id
        /// </summary>
        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductById(int productId)
        {
            return HandleResponse<ProductDto>(await productV1Operation.GetProductById(productId));
        }

        /// <summary>
        /// Update product description only by product Id
        /// </summary>
        [HttpPatch("{productId}/description")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProductDescriptionById(int productId, UpdateDescriptionDto updateDescription)
        {
            return HandleResponse(await productV1Operation.UpdateProductDescriptionById(productId, updateDescription.Description));
        }

    }
}
