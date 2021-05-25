using System.Collections.Generic;
using Trady.Analysis;

namespace ShowMeTheMoney.StockAnalyzer.Models
{
    internal class StockAnalysis
    {
        public AnalyzeContext DailyCandles { get; set; }
        public IReadOnlyList<Trady.Core.Infrastructure.IIndexedOhlcv> DailyBuySignals { get; set; }
        public IReadOnlyList<Trady.Core.Infrastructure.IIndexedOhlcv> DailyTrendSignals { get; set; }
        public Trend DailySuperTrend { get; set; }
    }
}
