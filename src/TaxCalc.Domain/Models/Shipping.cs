using System.Text.Json.Serialization;

namespace TaxCalc.Domain.Models
{
    public class Shipping
    {
        [JsonPropertyName("city_amount")]
        public double CityAmount { get; set; }

        [JsonPropertyName("city_tax_rate")]
        public double CityTaxRate { get; set; }

        [JsonPropertyName("city_taxable_amount")]
        public double CityTaxableAmount { get; set; }

        [JsonPropertyName("combined_tax_rate")]
        public double CombinedTaxRate { get; set; }

        [JsonPropertyName("county_amount")]
        public double CountyAmount { get; set; }

        [JsonPropertyName("county_tax_rate")]
        public double CountyTaxRate { get; set; }

        [JsonPropertyName("county_taxable_amount")]
        public double CountyTaxableAmount { get; set; }

        [JsonPropertyName("special_district_amount")]
        public double SpecialDistrictAmount { get; set; }

        [JsonPropertyName("special_tax_rate")]
        public double SpecialTaxRate { get; set; }

        [JsonPropertyName("special_taxable_amount")]
        public double SpecialTaxableAmount { get; set; }

        [JsonPropertyName("state_amount")]
        public double StateAmount { get; set; }

        [JsonPropertyName("state_sales_tax_rate")]
        public double StateSalesTaxRate { get; set; }

        [JsonPropertyName("state_taxable_amount")]
        public double StateTaxableAmount { get; set; }

        [JsonPropertyName("tax_collectable")]
        public double TaxCollectable { get; set; }

        [JsonPropertyName("taxable_amount")]
        public double TaxableAmount { get; set; }
    }
}
