namespace ShowMeTheMoney.CompanyBuilder.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyTicker { get; set; }
        public string CompanySector { get; set; }
        public string CompanyMorningStar { get; set; }
        public PriceData PriceData { get; set; }
    }
}