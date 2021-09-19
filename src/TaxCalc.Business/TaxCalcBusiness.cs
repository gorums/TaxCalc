using FluentValidation;
using System;
using System.Linq;
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
        public async Task<Rate> GetTaxRateForLocationAsync(string zip, OptionalAddress optionalAddress = default, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(zip))
            {
                throw new ArgumentException($"The parameter {nameof(zip)} is required");
            }

            var provider = taxCalcProviderFactory.GetTaxCalcProviderImpl();

            var result = await provider.GetTaxRateForLocationAsync(zip, optionalAddress, cancellationToken);

            return result.Rate;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<Tax> CalculateTaxForAnOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            var validation = new OrderValidator().Validate(order);
            if (!validation.IsValid)
            {
                throw new ArgumentException(String.Join(", ", validation.Errors.Select(er => er.ErrorMessage)));
            }

            var provider = taxCalcProviderFactory.GetTaxCalcProviderImpl();

            var result = await provider.CalculateTaxForAnOrderAsync(order, cancellationToken);

            return result.Tax;
        }
    }
}
