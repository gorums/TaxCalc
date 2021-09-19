using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaxCalc.Business.Constans;
using TaxCalc.Business.Validators;
using TaxCalc.Domain;
using TaxCalc.Domain.Models;

namespace TaxCalc.Business
{
    /// <summary>
    /// This class implement <see cref="ITaxCalcBusiness"/>
    /// </summary>
    public class TaxCalcBusiness : ITaxCalcBusiness
    {
        private readonly ITaxCalcProviderFactory taxCalcProviderFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxCalcBusiness" /> class
        /// </summary>
        /// <param name="taxCalcProviderFactory"><see cref="ITaxCalcProviderFactory"/></param>
        public TaxCalcBusiness(ITaxCalcProviderFactory taxCalcProviderFactory)
        {
            this.taxCalcProviderFactory = taxCalcProviderFactory;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<Rate> GetTaskRateForLocationAsync(string zip, OptionalAddress optionalAddress, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(zip))
            {
                throw new ArgumentException($"The parameter {nameof(zip)} is required");
            }

            var provider = taxCalcProviderFactory.GetTaxCalcProvider(ProviderNames.TAX_JAR);

            var result = await provider.GetTaskRateForLocationAsync(zip, optionalAddress, cancellationToken);

            return result.Rate;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<Tax> CalculateTaxForAnOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            new OrderValidator().ValidateAndThrow(order);

            var provider = taxCalcProviderFactory.GetTaxCalcProvider(ProviderNames.TAX_JAR);

            var result = await provider.CalculateTaxForAnOrderAsync(order, cancellationToken);

            return result.Tax;
        }
    }
}
