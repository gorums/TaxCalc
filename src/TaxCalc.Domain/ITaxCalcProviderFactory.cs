namespace TaxCalc.Domain
{
    public interface ITaxCalcProviderFactory
    {
        ITaxCalcProvider GetTaxCalcProvider(string provideName);
    }
}
