using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TaxCalc.Domain.Models
{
    public class RateResult
    {
        [JsonPropertyName("rate")]
        public Rate Rate { get; set; }
    }
}
