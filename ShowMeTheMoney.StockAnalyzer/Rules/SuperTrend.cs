using System.Collections.Generic;
using System.Linq;
using ShowMeTheMoney.StockAnalyzer.Indicators;
using ShowMeTheMoney.StockAnalyzer.Models;
using Trady.Core.Infrastructure;

namespace ShowMeTheMoney.StockAnalyzer.Rules
{
    internal static class SuperTrend
    {
        internal static Trend Calculate(IEnumerable<IOhlcv> stockCandleEnumerable)
        {
            return TrendFinder(stockCandleEnumerable);
        }

        internal static Trend TrendFinder(IEnumerable<IOhlcv> stockCandleEnumerable)
        {
            var candleEnumerable = stockCandleEnumerable.ToArray();
            var timeSeries = GetTimeSeries.Get(candleEnumerable);
            var highLow2 = HighLow.HighLow2(candleEnumerable);
            if (timeSeries.Close.Length <= 2) return new Trend();
            var atr = AverageTrueRange.Atr(timeSeries, timeSeries.Close.Length - 1);
            return GetTrend.IsTrending(atr, highLow2, timeSeries);
        }
    }
}