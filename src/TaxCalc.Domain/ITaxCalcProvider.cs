using System.Threading;
using System.Threading.Tasks;
using TaxCalc.Domain.Models;

namespace TaxCalc.Domain
{
    public interface ITaxCalcProvider
    {
        Task<RateResult> GetTaskRateForLocationAsync(string zip, OptionalAddress optionalAddress, CancellationToken cancellationToken = default);

        Task<TaxResult> CalculateTaxForAnOrderAsync(Order order, CancellationToken cancellationToken = default);
    }
}
