using System.Collections.Generic;
using System.Linq;
using ShowMeTheMoney.StockAnalyzer.Models;

namespace ShowMeTheMoney.Helpers
{
    /// <summary>
    ///     Get gains or losses from portfolio
    /// </summary>
    internal static class GetEarnings
    {
        internal static string Gainers(List<Portfolio> portfolios)
        {
            return portfolios.Count(x => x.TimeSeries.Close[^1] >= x.TimeSeries.Close[0]).ToString();
        }

        internal static string Losses(List<Portfolio> portfolios)
        {
            return portfolios.Count(x => x.TimeSeries.Close[^1] <= x.TimeSeries.Close[0]).ToString();
        }
    }
}