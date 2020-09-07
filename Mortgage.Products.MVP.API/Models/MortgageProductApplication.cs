using Newtonsoft.Json;

namespace Mortgage_Products_MVP.Models
{
    public class MortgageProductApplication
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "age")]
        public string Age { get; set; }

        [JsonProperty(PropertyName = "mortgageAmount")]
        public string MortgageAmount { get; set; }

        [JsonProperty(PropertyName = "propertyValue")]
        public string PropertyValue { get; set; }
    }
}
