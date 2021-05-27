using System.Collections.Generic;
using System.Linq;
using ShowMeTheMoney.CompanyBuilder.Models;
using ShowMeTheMoney.StockAnalyzer.Models;

namespace ShowMeTheMoney.StockAnalyzer
{
    /// <summary>
    ///     Class used to retrieve a list of companies and their stocks
    /// </summary>
    public static class GetCompanyStock
    {
        public static List<StockList> Get()
        {
            var companies = Database.Database.Get<Company>("Companies");
            var stocks = Database.Database.Get<Stock>("AnalyzedStocks");

            return companies.Select(comp => new StockList
                {
                    Name = comp.Name,
                    Sector = comp.Sector,
                    Symbol = comp.Symbol,
                    FactSheet = comp.FactSheet,
                    Stock = stocks.Find(stock => stock.Symbol == comp.Symbol)
                })
                .ToList();
        }
    }
}