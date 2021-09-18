using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TaxCalc.Domain.Models
{
    public class ProviderResponseDetail
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("detail")]
        public string Detail { get; set; }
    }
}
