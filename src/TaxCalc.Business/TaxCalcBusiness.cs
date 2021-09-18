using System;
using System.Threading;
using System.Threading.Tasks;
using TaxCalc.Business.Constans;
using TaxCalc.Domain;
using TaxCalc.Domain.Models;

namespace TaxCalc.Business
{
    public class TaxCalcBusiness : ITaxCalcBusiness
    {
        private readonly ITaxCalcProviderFactory taxCalcProviderFactory;

        public TaxCalcBusiness(ITaxCalcProviderFactory taxCalcProviderFactory)
        {
            this.taxCalcProviderFactory = taxCalcProviderFactory;
        }

        public async Task<Tax> CalculateTaxForAnOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            var provider = taxCalcProviderFactory.GetTaxCalcProvider(ProviderNames.TAX_JAR);

            var result = await provider.CalculateTaxForAnOrderAsync(order, cancellationToken);

            return result.Tax;
        }

        public async Task<Rate> GetTaskRateForLocationAsync(string zip, OptionalAddress optionalAddress, CancellationToken cancellationToken = default)
        {
            var provider = taxCalcProviderFactory.GetTaxCalcProvider(ProviderNames.TAX_JAR);

            var result = await provider.GetTaskRateForLocationAsync(zip, optionalAddress, cancellationToken);

            return result.Rate;
        }
    }
}
