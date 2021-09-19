using System;
using System.Text.Json;
using TaxCalc.Domain.Models;

namespace TaxCalc.Business.Providers
{
    public class TaxCalcProviderException : Exception
    {
        private ProviderResponseDetail providerResponseDetail;
        public string Detail => providerResponseDetail.Detail ?? string.Empty;

        public TaxCalcProviderException(string message) : base(message)
        {
            this.providerResponseDetail = JsonSerializer.Deserialize<ProviderResponseDetail>(message);
        }
    }
}
