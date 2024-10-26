using Alza.Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Alza.Web.Controllers
{
    public class CommonController : ControllerBase
    {
        protected IActionResult HandleResponse<T>(CommonResponseStatus<T> responseStatus)
        {
            return responseStatus.StatusCode switch
            {
                HttpStatusCode.OK => Ok(responseStatus.ResponseData),
                _ => StatusCode((int)responseStatus.StatusCode, responseStatus.ErrorMessage)
            };
        }

        protected IActionResult HandleResponse(CommonResponseStatus responseStatus)
        {
            return responseStatus.StatusCode switch
            {
                HttpStatusCode.OK => Ok(),
                _ => StatusCode((int)responseStatus.StatusCode, responseStatus.ErrorMessage)
            };
        }
    }
}
