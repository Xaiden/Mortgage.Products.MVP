using System.Net;

namespace Mortgage.Products.MVP.API.Models
{
    public class ApiResponse<TResult>
    {
        public HttpStatusCode StatusCode { get; set; }
        public TResult Content { get; set; }
        public object Contentresponse { get; internal set; }
    }
}
