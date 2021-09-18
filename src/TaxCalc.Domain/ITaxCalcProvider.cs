using System.Threading;
using System.Threading.Tasks;
using TaxCalc.Domain.Models;

namespace TaxCalc.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaxCalcProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zip"></param>
        /// <param name="optionalAddress"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RateResult> GetTaskRateForLocationAsync(string zip, OptionalAddress optionalAddress, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TaxResult> CalculateTaxForAnOrderAsync(Order order, CancellationToken cancellationToken = default);
    }
}
