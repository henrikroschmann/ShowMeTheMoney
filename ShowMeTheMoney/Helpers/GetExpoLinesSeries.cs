using System;
using System.Collections.Generic;
using System.Linq;
using ShowMeTheMoney.StockAnalyzer.Models;

namespace ShowMeTheMoney.Helpers
{
    /// <summary>
    ///     Return the sum of all closes per day divided by portfolio stocks.
    /// </summary>
    internal static class GetExpoLinesSeries
    {
        internal static void Series(List<Portfolio> portfolios, out double[] doubles)
        {
            try
            {
                doubles = new[]
                {
                    portfolios.Sum(x => x.TimeSeries.Close[^5]) / portfolios.Count,
                    portfolios.Sum(x => x.TimeSeries.Close[^4]) / portfolios.Count,
                    portfolios.Sum(x => x.TimeSeries.Close[^3]) / portfolios.Count,
                    portfolios.Sum(x => x.TimeSeries.Close[^2]) / portfolios.Count,
                    portfolios.Sum(x => x.TimeSeries.Close[^1]) / portfolios.Count
                };
            }
            catch (Exception)
            {
                doubles = new[]
                {
                    portfolios.Sum(x => x.TimeSeries.Close[0]) / portfolios.Count,
                    0,
                    0,
                    0,
                    0
                };
            }
        }
    }
}