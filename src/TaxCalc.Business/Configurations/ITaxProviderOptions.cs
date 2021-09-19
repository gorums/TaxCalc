namespace TaxCalc.Business.Configurations
{
    public interface ITaxProviderOptions
    {
        int Retry { get; set; }
        string Token { get; set; }
        string Url { get; set; }
        int Wait { get; set; }
    }
}