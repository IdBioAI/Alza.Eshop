using Alza.Infrastructure.Dto.Product;
using Alza.Infrastructure.Operations.Transient;
using Microsoft.AspNetCore.Mvc;

namespace Alza.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/products")]
    [ApiVersion("2")]
    public class ProductV2Controller(IProductV2Operation productV2Operation) : CommonController
    {

        private readonly IProductV2Operation productV2Operation = productV2Operation;

        /// <summary>
        /// List all available products with pagination support
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts(ProductRequest productRequest)
        {
            return HandleResponse<ProductResponse>(await productV2Operation.GetProducts(productRequest));
        }
    }
}
