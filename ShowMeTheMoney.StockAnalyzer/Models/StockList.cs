using ShowMeTheMoney.CompanyBuilder.Models;

namespace ShowMeTheMoney.StockAnalyzer.Models
{
    public class StockList : Company, IStockList
    {
        public Stock Stock { get; set; }
    }
}