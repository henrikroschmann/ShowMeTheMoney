using ShowMeTheMoney.StockAnalyzer.Models;

namespace ShowMeTheMoney.StockAnalyzer.Indicators
{
    internal static class GetTrend
    {
        internal static Trend IsTrending(double[] atr, double[] hl, TimeSeries result)
        {
            var up1 = hl[^2] - 3.0 * atr[1];
            //var isUp = result.Close[^2] > up1 ? Math.Max(up, up1) : up;

            var dn1 = hl[^2] + 3.0 * atr[1];
            //var isDown = result.Close[^2] < dn1 ? Math.Min(dn, dn1) : dn;

            return new Trend
            {
                IsTrending = result.Close[^1] > dn1 ? 1 : result.Close[^1] < up1 ? -1 : 0,
                Time = result.Time[^1]
            };
        }
    }
}