using System;
using Trady.Core.Infrastructure;

namespace ShowMeTheMoney.StockAnalyzer.Indicators
{
    internal static class HighLow
    {
        internal static double[] HighLow2(IOhlcv[] candleEnumerable)
        {
            var hl = new double[candleEnumerable.Length];
            for (var index = 0; index < candleEnumerable.Length; index++)
                hl[index] = (double) Math.Round((candleEnumerable[index].High + candleEnumerable[index].Low) / 2, 1);

            return hl;
        }
    }
}