using Alza.Infrastructure.Constants;
using Alza.Infrastructure.Dto;
using Alza.Infrastructure.Dto.Product;
using Alza.Infrastructure.Helpers;
using Alza.Infrastructure.Mappers;
using Alza.Infrastructure.Repository;
using Serilog;
using System.Net;

namespace Alza.Infrastructure.Operations.Transient
{
    public interface IProductV2Operation
    {
        Task<CommonResponseStatus<ProductResponse>> GetProducts(ProductRequest productRequest);
    }

    public class ProductV2Operation(IProductRepository productRepository) : IProductV2Operation
    {
        private readonly IProductRepository productRepository = productRepository;

        public async Task<CommonResponseStatus<ProductResponse>> GetProducts(ProductRequest productRequest)
        {
            try
            {
                // check valid values

                var productModels = await productRepository.GetProducts(productRequest.Page, productRequest.PageSize ?? SearchConstants.ProductPageSize);

                return ResponseHelper.CreateResponse<ProductResponse>(new ProductResponse()
                {
                    ProductList = productModels.Select(ProductMapper.MapToProductDto).ToList()
                }, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error("GetProducts error", ex);
                return ResponseHelper.CreateResponse<ProductResponse>(null, HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
