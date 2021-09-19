namespace TaxCalc.Business.Configurations
{
    public class TaxProviderOptions : ITaxProviderOptions
    {
        public string Url { get; set; }
        public string Token { get; set; }
        public int Wait { get; set; }
        public int Retry { get; set; }
    }
}
