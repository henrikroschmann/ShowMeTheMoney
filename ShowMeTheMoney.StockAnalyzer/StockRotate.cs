using System;
using System.Collections.Generic;
using System.Linq;
using ShowMeTheMoney.StockAnalyzer.Models;
using YahooFinanceApi;

namespace ShowMeTheMoney.StockAnalyzer
{
    public static class StockRotate
    {
        public static void Rotate(List<Portfolio> portfolios)
        {
            Database.Database.ClearCollection("Portfolio");
            foreach (var portfolio in portfolios)
            {
                // You could query multiple symbols with multiple fields through the following steps:
                var history = Yahoo.GetHistoricalAsync(portfolio.Symbol.Trim().Replace(" ", "-") + ".ST",
                    portfolio.TimeSeries.Time[^1].Date, DateTime.Today);

                var time = new List<DateTime>();
                var close = new List<double>();

                time.AddRange(portfolio.TimeSeries.Time);
                time.AddRange(history.Result.Select(x => x.DateTime).Where(x => x.Date > portfolio.TimeSeries.Time[^1])
                    .ToList());

                close.AddRange(portfolio.TimeSeries.Close);
                close.AddRange(history.Result.Where(x => x.DateTime > portfolio.TimeSeries.Time[^1])
                    .Select(x => (double) x.Close)
                    .ToList());
                portfolio.TimeSeries.Time = time.ToArray();
                portfolio.TimeSeries.Close = close.ToArray();
                Database.Database.Create("Portfolio", portfolio);
            }
        }
    }
}