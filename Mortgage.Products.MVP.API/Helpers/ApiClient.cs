using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Mortgage_Products_MVP
{
    public class ApiClient
    {
        HttpClient _client = new HttpClient();
        ITestOutputHelper _output;

        public ApiClient(ITestOutputHelper output)
        {
            _output = output;
            var config = new ConfigurationBuilder().AddJsonFile("appconfig.json").Build();
            _client.BaseAddress = new Uri(config["APIBaseUrl"]);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> MakeApiPostRequest(string requestUri, JObject requestBody)
        {
            _output.WriteLine($"Making POST Call to '{_client.BaseAddress + requestUri}'\nWith Json Request Body:\n{requestBody.ToString()}");

            HttpResponseMessage response = await _client.PostAsync(
                requestUri,
                new StringContent(
                    JsonConvert.SerializeObject(requestBody),
                    Encoding.UTF8,
                    "application/json"));

            return response;
        }

        public async Task<HttpResponseMessage> MakeApiGetRequest(string requestUri)
        {
            _output.WriteLine($"Making GET Call to '{_client.BaseAddress + requestUri}'");

            HttpResponseMessage response = await _client.GetAsync(
                requestUri);

            return response;
        }

        public async Task<HttpResponseMessage> MakeApiPutRequest(string requestUri, string requestParameter, JObject requestBody)
        {
            _output.WriteLine($"Making PUT Call to '{_client.BaseAddress + requestUri}'\nWith Json Request Body:\n{requestBody.ToString()}");

            HttpResponseMessage response = await _client.PutAsync(
                requestUri + requestParameter,
                new StringContent(
                    JsonConvert.SerializeObject(requestBody),
                    Encoding.UTF8,
                    "application/json"));

            return response;
        }

        public async Task<HttpResponseMessage> MakeApiDeleteRequest(string requestUri)
        {
            _output.WriteLine($"Making DELETE Call to '{_client.BaseAddress + requestUri}'");

            HttpResponseMessage response = await _client.DeleteAsync(
                requestUri);

            return response;
        }
    }
}
