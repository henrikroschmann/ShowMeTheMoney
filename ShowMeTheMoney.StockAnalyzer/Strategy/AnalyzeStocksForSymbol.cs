using System;
using System.Linq;
using ShowMeTheMoney.CompanyBuilder.Models;
using ShowMeTheMoney.Core;
using ShowMeTheMoney.Core.Helpers;
using ShowMeTheMoney.StockAnalyzer.Models;

namespace ShowMeTheMoney.StockAnalyzer.Strategy
{
    internal class AnalyzeStocksForSymbol
    {
        internal static void Analyze(Company company, out Stock stockData)
        {
            stockData = null;
            var stockAnalysis = AnalyzeCompanyStock.Analyze(company);

            if (stockAnalysis != null &&
                stockAnalysis.DailyBuySignals.Any(x =>
                    x.DateTime.Date >= StockTime.PreviousWorkDay(DateTime.Today)) &&
                stockAnalysis.DailyCandles.BackingList.ToArray()[^1].Close >
                stockAnalysis.DailyCandles.BackingList.ToArray()[^2].Close)
            {
                Logger.WriteLine($"Found something that might be interesting {company.Name}");
                {
                    var ts = new TimeSeries
                    {
                        Close =
                            stockAnalysis.DailyCandles.BackingList.Select(x => (double) x.Close).Reverse()
                                .ToArray()[..5],
                        Time = stockAnalysis.DailyCandles.BackingList.Select(x => x.DateTime.DateTime).Reverse()
                            .ToArray()[..5],
                        High = stockAnalysis.DailyCandles.BackingList.Select(x => (double) x.Close).Reverse()
                            .ToArray()[..5],
                        Low = stockAnalysis.DailyCandles.BackingList.Select(x => (double) x.Close).Reverse()
                            .ToArray()[..5],
                        Open = stockAnalysis.DailyCandles.BackingList.Select(x => (double) x.Close).Reverse()
                            .ToArray()[..5],
                        Vol = stockAnalysis.DailyCandles.BackingList.Select(x => (double) x.Close).Reverse()
                            .ToArray()[..5]
                    };

                    stockData = new Stock
                    {
                        Symbol = company.Symbol,
                        Date = DateTime.Now,
                        TimeSeries = ts,
                        DailySuperTrend = stockAnalysis.DailySuperTrend.IsTrending,
                        DailyMacDSignalFound = stockAnalysis.DailyBuySignals.Count > 0 ? 1 : 0,
                        DailyTrend = stockAnalysis.DailyTrendSignals.Count > 0 ? 1 : 0
                    };
                }
            }
        }
    }
}