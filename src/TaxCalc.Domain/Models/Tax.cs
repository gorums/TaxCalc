using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TaxCalc.Domain.Models
{
    public class Tax
    {
        [JsonPropertyName("amount_to_collect")]
        public double AmountToCollect { get; set; }

        [JsonPropertyName("breakdown")]
        public Breakdown Breakdown { get; set; }

        [JsonPropertyName("freight_taxable")]
        public bool FreightTaxable { get; set; }

        [JsonPropertyName("has_nexus")]
        public bool HasNexus { get; set; }

        [JsonPropertyName("jurisdictions")]
        public Jurisdictions Jurisdictions { get; set; }

        [JsonPropertyName("order_total_amount")]
        public double OrderTotalAmount { get; set; }

        [JsonPropertyName("rate")]
        public double Rate { get; set; }

        [JsonPropertyName("shipping")]
        public double Shipping { get; set; }

        [JsonPropertyName("tax_source")]
        public string TaxSource { get; set; }

        [JsonPropertyName("taxable_amount")]
        public double TaxableAmount { get; set; }
    }
}
