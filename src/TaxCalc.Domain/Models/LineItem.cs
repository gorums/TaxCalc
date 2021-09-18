using System.Text.Json.Serialization;

namespace TaxCalc.Domain.Models
{
    public class LineItem
    {
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unit_price")]
        public double UnitPrice { get; set; }

        [JsonPropertyName("product_tax_code")]
        public string ProductTaxCode { get; set; }
    }
}
