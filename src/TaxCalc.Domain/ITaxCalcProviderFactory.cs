namespace TaxCalc.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaxCalcProviderFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provideName"></param>
        /// <returns></returns>
        ITaxCalcProvider GetTaxCalcProvider(string provideName);
    }
}
