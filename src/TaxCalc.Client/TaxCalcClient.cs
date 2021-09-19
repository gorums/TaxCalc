using RestSharp;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TaxCalc.Domain.Models;

namespace TaxCalc.Client
{
    /// <summary>
    /// This class implement <see cref="ITaxCalcClient"/>
    /// </summary>
    public class TaxCalcClient : ITaxCalcClient
    {
        private readonly IRestClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxCalcClient" /> class
        /// </summary>
        /// <param name="taxCalcBaseUrl">The base url where we have Tax Calc microservice running</param>
        public TaxCalcClient(string taxCalcBaseUrl)
        {
            client = new RestClient(taxCalcBaseUrl);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<RateResult> GetTaxRateForLocationAsync(string zip, OptionalAddress optionalAddress, CancellationToken cancellationToken = default)
        {

            if (string.IsNullOrEmpty(zip))
            {
                throw new ArgumentException("The parameter zip is required");
            }

            var request = new RestRequest($"api/TaxCalculators/GetTaxRateForLocation?zip={zip}");

            if (optionalAddress != null)
            {
                if (!string.IsNullOrEmpty(optionalAddress.Street))
                {
                    request.AddParameter("street", optionalAddress.Street);
                }

                if (!string.IsNullOrEmpty(optionalAddress.Country))
                {
                    request.AddParameter("country", optionalAddress.Country);
                }

                if (!string.IsNullOrEmpty(optionalAddress.State))
                {
                    request.AddParameter("state", optionalAddress.State);
                }

                if (!string.IsNullOrEmpty(optionalAddress.City))
                {
                    request.AddParameter("city", optionalAddress.City);
                }
            }

            return await client.GetAsync<RateResult>(request, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<TaxResult> CalculateTaxForAnOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            if (order == null)
            {
                throw new ArgumentException("Order is required");
            }

            var request = new RestRequest("api/TaxCalculators/CalculateTaxForAnOrder");

            request.AddJsonBody
            (
                JsonSerializer.Serialize(order, new JsonSerializerOptions
                {
                    IgnoreNullValues = true
                })
            );

            return await client.PostAsync<TaxResult>(request, cancellationToken);
        }
    }
}
