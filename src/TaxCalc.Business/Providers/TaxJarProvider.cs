using RestSharp;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TaxCalc.Domain;
using TaxCalc.Domain.Models;

namespace TaxCalc.Business.Providers
{
    public class TaxJarProvider : ITaxCalcProvider
    {
        private readonly IRestClient client;
        private readonly TaxProviderOptions taxProviderOptions;

        public TaxJarProvider(TaxProviderOptions taxProviderOptions)
        {
            this.taxProviderOptions = taxProviderOptions;

            client = new RestClient(this.taxProviderOptions.Url);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", this.taxProviderOptions.Token));
        }

        public async Task<RateResult> GetTaskRateForLocationAsync(string zip, OptionalAddress optionalAddress, CancellationToken cancellationToken = default)
        {
            var request = new RestRequest($"/rates/{zip}");
            if (optionalAddress != null)
            {
                request.AddJsonBody(optionalAddress);
            }

            var response = await client.GetAsync<RateResult>(request, cancellationToken);

            return response;            
        }

        public async Task<TaxResult> CalculateTaxForAnOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            var request = new RestRequest("/taxes");
            request.AddJsonBody(JsonSerializer.Serialize(order, new JsonSerializerOptions
            {
                IgnoreNullValues = true
            }));

            var response = await client.PostAsync<TaxResult>(request, cancellationToken);

            return response;
        }       
    }
}
