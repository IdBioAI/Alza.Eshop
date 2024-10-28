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
                if (productRequest.Page <= 0 || (productRequest.PageSize.HasValue && productRequest.PageSize <= 0))
                {
                    return ResponseHelper.CreateResponse<ProductResponse>(null, HttpStatusCode.BadRequest, "Invalid values for Page and/or PageSize. Page must be greater than 0 and PageSize must be a positive number.");
                }

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
