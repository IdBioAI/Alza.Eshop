using System.Net;

namespace Alza.Infrastructure.Dto
{
    public class CommonResponseStatus
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string ErrorMessage { get; set; }
    }

    public class CommonResponseStatus<T> : CommonResponseStatus
    {
        public T? ResponseData { get; set; }
    }
}
