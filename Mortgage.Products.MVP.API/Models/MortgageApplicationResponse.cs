using Newtonsoft.Json;

namespace Mortgage_Products_MVP.Models
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
