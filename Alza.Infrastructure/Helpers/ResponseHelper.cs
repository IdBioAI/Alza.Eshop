using Alza.Infrastructure.Dto;
using System.Net;

namespace Alza.Infrastructure.Helpers
{
    public static class ResponseHelper
    {
        public static CommonResponseStatus<T> CreateResponse<T>(T? responseData, HttpStatusCode statusCode, string errorMessage = "")
        => new()
        {
            StatusCode = statusCode,
            ErrorMessage = errorMessage,
            ResponseData = responseData
        };

        public static CommonResponseStatus CreateResponse(HttpStatusCode statusCode, string errorMessage = "")
        => new()
        {
            StatusCode = statusCode,
            ErrorMessage = errorMessage,
        };
    }
}
