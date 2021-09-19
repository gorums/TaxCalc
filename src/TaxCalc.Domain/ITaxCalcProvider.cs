using System.Threading;
using System.Threading.Tasks;
using TaxCalc.Domain.Models;

namespace TaxCalc.Domain
{
    /// <summary>
    /// This interface define an Tax provider
    /// </summary>
    public interface ITaxCalcProvider
    {
        /// <summary>
        /// Shows the sales tax rates for a given location.
        /// </summary>
        /// <param name="zip">Postal code for given location (5-Digit ZIP or ZIP+4).</param>
        /// <param name="optionalAddress">Optional parameter with [street, country, state and city]</param>
        /// <param name="cancellationToken">Notification that operations should be canceled</param>
        /// <returns></returns>
        Task<RateResult> GetTaskRateForLocationAsync(string zip, OptionalAddress optionalAddress, CancellationToken cancellationToken = default);

        /// <summary>
        /// Shows the sales tax that should be collected for a given order.
        /// </summary>
        /// <param name="order">An Order to calculate Tax</param>
        /// <param name="cancellationToken">Notification that operations should be canceled</param>
        /// <returns></returns>
        Task<TaxResult> CalculateTaxForAnOrderAsync(Order order, CancellationToken cancellationToken = default);
    }
}
