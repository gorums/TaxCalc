using System.Text.Json.Serialization;

namespace TaxCalc.Domain.Models
{
    public class Jurisdictions
    {
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("county")]
        public string County { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }
    }
}
