//using Mortgage.Products.MVP.API.Models;
//using Newtonsoft.Json;
//using System;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;

//namespace Mortgage.Products.MVP.API
//{
//    /// <summary>
//    /// Class which implements all HTTP operations: GET, POST - for this exercise
//    /// </summary>
//    public static class HttpRestCalls
//    {
//        private static string _baseURL;
//        private static HttpClient _httpClient;


//        public static string BaseURL { get => _baseURL; }

//        private static HttpClient InitialiseHttpClient()
//        {
//            string assemblyVersion = "1.0.0";
//            string userAgent = $"MortgageProduct_MVP_API_{assemblyVersion}";

//            HttpClient httpClient = new HttpClient
//            {
//                BaseAddress = new Uri(BaseURL)
//            };

//            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

//            return httpClient;
//        }

//        private static async Task<HttpResponseMessage> ExecuteRequest(HttpRequestMessage request)
//        {
//            HttpResponseMessage response = await _httpClient.SendAsync(request);

//            return response;
//        }

//        private static async Task<ApiResponse<TResponse>> SendRequestAsync<TRequest, TResponse>(HttpMethod method, string requestUri, TRequest requestBody)
//        {
//            using (HttpRequestMessage request = new HttpRequestMessage(method, _httpClient.BaseAddress))
//            {
//                if (requestBody != null)
//                {
//                    request.Content = new StringContent(requestBody.ToString(), Encoding.UTF8, "application/json");
//                }

//                //log calling to Uri via method

//                using (HttpResponseMessage response = await ExecuteRequest(request))
//                {
//                    string responseText = await response.Content.ReadAsStringAsync();

//                    if (!response.IsSuccessStatusCode)
//                    {
//                        //log
//                    }

//                    TResponse responseContent = default(TResponse);

//                    responseContent = JsonConvert.DeserializeObject<TResponse>(responseText);

//                    return new ApiResponse<TResponse>()
//                    {
//                        StatusCode = response.StatusCode,
//                        Content = responseContent
//                    };
//                }
//            }
//        }

//        public static async Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest requestBody)
//        {
//            return await SendRequestAsync<TRequest, TResponse>(HttpMethod.Post, url, requestBody);
//        }

//        public static async Task<ApiResponse<TResponse>> GetAsync<TResponse>(string url)
//        {
//            return await SendRequestAsync<object, TResponse>(HttpMethod.Post, url, default);
//        }
//    }
//}
