using Microsoft.Extensions.Options;
using System;
using TaxCalc.Business.Configurations;
using TaxCalc.Business.Constans;
using TaxCalc.Business.Providers;
using TaxCalc.Domain;

namespace TaxCalc.Business
{
    /// <summary>
    /// This class implement <see cref="ITaxCalcProviderFactory"/>
    /// </summary>
    public class TaxCalcProviderFactory : ITaxCalcProviderFactory
    {
        private readonly TaxProviderOptions taxProviderOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxCalcProviderFactory" /> class
        /// </summary>
        /// <param name="taxProviderOptions"><see cref="IOptions{TaxProviderOptions}"/></param>
        public TaxCalcProviderFactory(IOptions<TaxProviderOptions> taxProviderOptions)
        {
            this.taxProviderOptions = taxProviderOptions.Value;

            ThrowIfInvalidOptions(this.taxProviderOptions);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ITaxCalcProvider GetTaxCalcProviderImpl()
        {
            switch (taxProviderOptions.Name)
            {
                case ProviderNames.TAX_JAR:
                    return new TaxJarProvider(taxProviderOptions);
            }

            throw new ArgumentException($"The provider {taxProviderOptions.Name} does not exist");
        }

        /// <summary>
        /// Valid the provider options
        /// </summary>
        /// <param name="options">Provider options</param>
        private static void ThrowIfInvalidOptions(TaxProviderOptions options)
        {
            options = options ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrEmpty(options.Name))
            {
                throw new ArgumentException("The Provider Name can not be empty", nameof(TaxProviderOptions.Name));
            }

            if (string.IsNullOrEmpty(options.Url))
            {
                throw new ArgumentException("The URl can not be empty", nameof(TaxProviderOptions.Url));
            }

            if (!Uri.IsWellFormedUriString(options.Url,UriKind.Absolute))
            {
                throw new ArgumentException("The URl is invalid", nameof(TaxProviderOptions.Url));
            }

            if (string.IsNullOrEmpty(options.Token))
            {
                throw new ArgumentException("The Token can not be empty", nameof(TaxProviderOptions.Token));
            }

            if (options.Retry < 1 || options.Retry > 10)
            {
                throw new ArgumentException("The Retry value only can be between 1 and 10", nameof(TaxProviderOptions.Retry));
            }
        }
    }
}
