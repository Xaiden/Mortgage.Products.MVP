using Newtonsoft.Json;

namespace Mortgage.Products.MVP.API.Models
{
    public class MortgageApplicationResponse
    {
        public string Decision { get; set; }

        public string Status { get; set; }

        public string Id { get; set; }

        [JsonProperty(PropertyName = "Reason")]
        public MortgageApplicationResponseReasons[] Reasons { get; set; }
    }
}
