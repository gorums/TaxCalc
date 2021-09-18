using System.Threading;
using System.Threading.Tasks;
using TaxCalc.Domain.Models;

namespace TaxCalc.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaxCalcBusiness
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zip"></param>
        /// <param name="OptionalAddress"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Rate> GetTaskRateForLocationAsync(string zip, OptionalAddress OptionalAddress, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Tax> CalculateTaxForAnOrderAsync(Order order, CancellationToken cancellationToken = default);
    }
}
