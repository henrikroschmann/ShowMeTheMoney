using ShowMeTheMoney.CompanyBuilder.Models;

namespace ShowMeTheMoney.StockAnalyzer.Models
{
    public class Portfolio : Company
    {
        public TimeSeries TimeSeries { get; set; }
    }
}