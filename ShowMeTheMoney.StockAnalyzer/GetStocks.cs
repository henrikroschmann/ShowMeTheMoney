using ShowMeTheMoney.CompanyBuilder.Models;
using ShowMeTheMoney.Core;
using ShowMeTheMoney.Core.Helpers;
using ShowMeTheMoney.StockAnalyzer.Models;
using ShowMeTheMoney.StockAnalyzer.Strategy;
using System;
using System.Linq;

namespace ShowMeTheMoney.StockAnalyzer
{
    public static class GetStocks
    {
        public static void Get()
        {
            Database.Database.ClearCollection("AnalyzedStocks");
            Logger.WriteLine("Gettings price data for stocks");           
            foreach (var company in Database.Database.Get<Company>("Companies"))
            {
                try
                {
                    StockAnalysis stockAnalysis = AnalyzeCompanyStock.Analyze(company);
                    if (stockAnalysis.DailyBuySignals.Count(x => x.DateTime.Date >= StockTime.PreviousWorkDay(DateTime.Today)) <=0 &&
                        stockAnalysis.DailySuperTrend.IsTrending == 0)
                    {
                        continue;
                    }

                    Logger.WriteLine($"Found some gold: {company.Name}");
                    var ts = new TimeSeries
                    {
                        Close = stockAnalysis.DailyCandles.BackingList.Select(x => (double)x.Close).Reverse().ToArray()[0..5],
                        Time = stockAnalysis.DailyCandles.BackingList.Select(x => x.DateTime.DateTime).Reverse().ToArray()[0..5],
                        High = stockAnalysis.DailyCandles.BackingList.Select(x => (double)x.Close).Reverse().ToArray()[0..5],
                        Low = stockAnalysis.DailyCandles.BackingList.Select(x => (double)x.Close).Reverse().ToArray()[0..5],
                        Open = stockAnalysis.DailyCandles.BackingList.Select(x => (double)x.Close).Reverse().ToArray()[0..5],
                        Vol = stockAnalysis.DailyCandles.BackingList.Select(x => (double)x.Close).Reverse().ToArray()[0..5]
                    };

                    Database.Database.Create("AnalyzedStocks", new Stock
                    {
                        Symbol = company.Symbol,
                        Date = DateTime.Now,
                        TimeSeries = ts,
                        DailySuperTrend = stockAnalysis.DailySuperTrend.IsTrending,
                        DailyMacDSignalFound = stockAnalysis.DailyBuySignals.Count > 0 ? 1 : 0,
                        DailyTrend = stockAnalysis.DailyTrendSignals.Count > 0 ? 1 : 0
                    });

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
