using System.Collections.Generic;
using Trady.Analysis;
using Trady.Core.Infrastructure;

namespace ShowMeTheMoney.StockAnalyzer.Models
{
    internal interface IStockAnalysis
    {
        AnalyzeContext DailyCandles { get; set; }
        IReadOnlyList<IIndexedOhlcv> DailyBuySignals { get; set; }
        IReadOnlyList<IIndexedOhlcv> DailyTrendSignals { get; set; }
        Trend DailySuperTrend { get; set; }
    }
}