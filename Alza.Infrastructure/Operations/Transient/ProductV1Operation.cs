using Alza.DbContexts;
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
    public interface IProductV1Operation
    {
        Task<CommonResponseStatus<ProductResponse>> GetProducts();
        Task<CommonResponseStatus<ProductDto>> GetProductById(int productId);
        Task<CommonResponseStatus> UpdateProductDescriptionById(int productId, string description);
    }

    public class ProductV1Operation(IProductRepository productRepository, ApplicationDbContext applicationDbContext) : IProductV1Operation
    {
        private readonly IProductRepository productRepository = productRepository;
        private readonly ApplicationDbContext applicationDbContext = applicationDbContext;

        public async Task<CommonResponseStatus<ProductResponse>> GetProducts()
        {
            try
            {
                // check valid values

                var productModels = await productRepository.GetProducts();

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

        public async Task<CommonResponseStatus<ProductDto>> GetProductById(int productId)
        {
            try
            {
                var productModel = await productRepository.GetProductById(productId);

                return productModel == null ? ResponseHelper.CreateResponse<ProductDto>(null, HttpStatusCode.NotFound, $"Product Id {productId} not found")
                     : ResponseHelper.CreateResponse<ProductDto>(ProductMapper.MapToProductDto(productModel), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error($"GetProduct by Id error for id: {productId}", ex);
                return ResponseHelper.CreateResponse<ProductDto>(null, HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<CommonResponseStatus> UpdateProductDescriptionById(int productId, string description)
        {
            try
            {
                var productModel = await productRepository.GetProductById(productId);
                if (productModel == null)
                {
                    return ResponseHelper.CreateResponse(HttpStatusCode.NotFound, $"Product Id {productId} not found");
                }

                productModel.Description = description;

                await applicationDbContext.SaveChangesAsync();
                return ResponseHelper.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Log.Error($"update product description error for product id {productId}", ex);
                return ResponseHelper.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
