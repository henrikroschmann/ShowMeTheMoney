using ShowMeTheMoney.CompanyBuilder.Models;
using ShowMeTheMoney.Core;
using ShowMeTheMoney.StockAnalyzer.Strategy;

namespace ShowMeTheMoney.StockAnalyzer
{
    public static class GetStocks
    {
        public static void Get()
        {
            Database.Database.ClearCollection("AnalyzedStocks");
            Logger.WriteLine("Getting price data for stocks");
            foreach (var company in Database.Database.Get<Company>("Companies"))
            {
                AnalyzeStocksForSymbol.Analyze(company, out var stockData);
                if (stockData != null)
                    Database.Database.Create("AnalyzedStocks", stockData);
            }

            Logger.WriteLine("That is all for now.");
        }
    }
}