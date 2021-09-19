namespace TaxCalc.Domain
{
    /// <summary>
    /// This interface define an Tax provider factory
    /// </summary>
    public interface ITaxCalcProviderFactory
    {
        /// <summary>
        /// Obtain the Tax provider implementation by provider name
        /// </summary>
        /// <returns>The Tax provider implementation by provider name</returns>
        ITaxCalcProvider GetTaxCalcProviderImpl();
    }
}
