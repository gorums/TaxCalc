using System.Text.Json.Serialization;

namespace TaxCalc.Domain.Models
{
    public class TaxResult
    {
        [JsonPropertyName("tax")]
        public Tax Tax { get; set; }
    }
}
