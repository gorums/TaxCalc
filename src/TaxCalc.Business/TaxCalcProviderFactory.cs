using Microsoft.Extensions.Options;
using System;
using TaxCalc.Business.Constans;
using TaxCalc.Business.Providers;
using TaxCalc.Domain;
using TaxCalc.Domain.Models;

namespace TaxCalc.Business
{
    public class TaxCalcProviderFactory : ITaxCalcProviderFactory
    {
        private readonly TaxProviderOptions taxProviderOptions;

        public TaxCalcProviderFactory(IOptions<TaxProviderOptions> taxProviderOptions)
        {
            this.taxProviderOptions = taxProviderOptions.Value;

            ThrowIfInvalidOptions(this.taxProviderOptions);
        }

        public ITaxCalcProvider GetTaxCalcProvider(string provideName)
        {
            switch(provideName)
            {
                case ProviderNames.TAX_JAR:
                    return new TaxJarProvider(taxProviderOptions);
            }

            throw new ArgumentException($"The provider {provideName} does not exist");
        }

        private static void ThrowIfInvalidOptions(TaxProviderOptions options)
        {
            options = options ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrEmpty(options.Url))
            {
                throw new ArgumentException("The URl can not be empty", nameof(TaxProviderOptions.Url));
            }

            if (string.IsNullOrEmpty(options.Token))
            {
                throw new ArgumentException("The Token can not be empty", nameof(TaxProviderOptions.Token));
            }

            if (options.Retry != null && (options.Retry < 1 || options.Retry > 10))
            {
                throw new ArgumentException("The Retry value only can be between 1 and 10", nameof(TaxProviderOptions.Retry));
            }
        }
    }
}
