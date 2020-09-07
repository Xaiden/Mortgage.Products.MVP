using Mortgage_Products_MVP.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Xunit.Abstractions;

namespace Mortgage_Products_MVP.Helpers
{
    public static class RequestHelper
    {
        public static ApiResponse<object> MakeApiPostRequest(ITestOutputHelper output, string uri, JObject jsonRequestBody)
        {
            ApiClient apiClient = new ApiClient(output);

            HttpResponseMessage response = apiClient.MakeApiPostRequest(uri, jsonRequestBody).Result;

            return new ApiResponse<object>
            {
                Content = response.Content.ReadAsStringAsync().Result,
                StatusCode = response.StatusCode
            };
        }

        public static ApiResponse<object> MakeApiGetRequest(ITestOutputHelper output, string uri)
        {
            ApiClient apiClient = new ApiClient(output);

            HttpResponseMessage response = apiClient.MakeApiGetRequest(uri).Result;

            return new ApiResponse<object>
            {
                Content = response.Content.ReadAsStringAsync().Result,
                StatusCode = response.StatusCode
            };
        }
    }
}
