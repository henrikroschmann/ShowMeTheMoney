namespace ShowMeTheMoney.CompanyBuilder.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Currency { get; set; }
        public string Isin { get; set; }
        public string Sector { get; set; }
        public string IcbCode { get; set; }
        public string FactSheet { get; set; }
        public PriceData PriceData { get; set; }
    }
}