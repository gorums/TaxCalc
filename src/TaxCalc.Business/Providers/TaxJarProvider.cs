using Polly;
using RestSharp;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TaxCalc.Business.Configurations;
using TaxCalc.Domain;
using TaxCalc.Domain.Models;

namespace TaxCalc.Business.Providers
{
    /// <summary>
    /// This class implement <see cref="ITaxCalcProvider"/>
    /// </summary>
    public class TaxJarProvider : ITaxCalcProvider
    {
        private readonly IRestClient client;
        private readonly ITaxProviderOptions taxProviderOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxJarProvider" /> class
        /// </summary>
        /// <param name="taxProviderOptions"><see cref="ITaxProviderOptions"/></param>
        public TaxJarProvider(ITaxProviderOptions taxProviderOptions)
        {
            this.taxProviderOptions = taxProviderOptions;

            client = new RestClient(this.taxProviderOptions.Url);
            client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", this.taxProviderOptions.Token));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<RateResult> GetTaskRateForLocationAsync(string zip, OptionalAddress optionalAddress, CancellationToken cancellationToken = default)
        {
            var request = new RestRequest($"/rates/{zip}", Method.GET);

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

            return await this.ExecuteIfExWaitAndRetryAsync<RateResult>(() => client.ExecuteAsync(request, cancellationToken));
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<TaxResult> CalculateTaxForAnOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            var request = new RestRequest("/taxes", Method.POST);

            request.AddJsonBody
            (
                JsonSerializer.Serialize(order, new JsonSerializerOptions
                {
                    IgnoreNullValues = true
                })
            );

            return await this.ExecuteIfExWaitAndRetryAsync<TaxResult>(() => client.ExecuteAsync(request, cancellationToken));
        }

        /// <summary>
        /// Execute and retry if the API call is falling
        /// </summary>
        /// <typeparam name="T">The response type</typeparam>
        /// <param name="task">The API call</param>
        /// <returns>The response type</returns>
        /// <exception cref="TaxCalcProviderException">If the API continue falling</exception>
        private async Task<T> ExecuteIfExWaitAndRetryAsync<T>(Func<Task<IRestResponse>> task)
        {
            // Polly for wait and retry if any exception in the api call
            var response = await Policy
               .Handle<Exception>()
               .WaitAndRetryAsync
               (
                    taxProviderOptions.Retry, retryAttempt =>
                    {
                        var timeToWait = TimeSpan.FromMilliseconds(retryAttempt * taxProviderOptions.Wait);
                        Console.WriteLine($"Waiting {timeToWait.TotalSeconds} seconds");

                        return timeToWait;
                    }
               )
               .ExecuteAsync(async () =>
               {
                   var result = await task.Invoke();
                   if (!result.IsSuccessful)
                   {
                       throw new TaxCalcProviderException(result.Content);
                   }

                   return result;
               });

            return JsonSerializer.Deserialize<T>(response.Content);
        }
    }
}
