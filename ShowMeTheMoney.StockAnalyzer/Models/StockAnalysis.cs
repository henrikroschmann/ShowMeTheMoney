using System.Collections.Generic;
using Trady.Analysis;
using Trady.Core.Infrastructure;

namespace ShowMeTheMoney.StockAnalyzer.Models
{
    internal class StockAnalysis : IStockAnalysis
    {
        public AnalyzeContext DailyCandles { get; set; }
        public IReadOnlyList<IIndexedOhlcv> DailyBuySignals { get; set; }
        public IReadOnlyList<IIndexedOhlcv> DailyTrendSignals { get; set; }
        public Trend DailySuperTrend { get; set; }
    }
}