using ShowMeTheMoney.CompanyBuilder.Models;

namespace ShowMeTheMoney.StockAnalyzer.Models
{
    public interface IStockList
    {
        Stock Stock { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        string Symbol { get; set; }
        string Currency { get; set; }
        string Isin { get; set; }
        string Sector { get; set; }
        string IcbCode { get; set; }
        string FactSheet { get; set; }
        PriceData PriceData { get; set; }
    }
}