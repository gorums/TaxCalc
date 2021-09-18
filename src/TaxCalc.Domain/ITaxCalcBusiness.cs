using System;
using System.Threading;
using System.Threading.Tasks;
using TaxCalc.Domain.Models;

namespace TaxCalc.Domain
{
    public interface ITaxCalcBusiness
    {
        Task<Rate> GetTaskRateForLocationAsync(string zip, OptionalAddress OptionalAddress, CancellationToken cancellationToken = default);

        Task<Tax> CalculateTaxForAnOrderAsync(Order order, CancellationToken cancellationToken = default);
    }
}
