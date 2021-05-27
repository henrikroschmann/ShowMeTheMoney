using System;
using System.Linq;
using ShowMeTheMoney.StockAnalyzer.Models;
using Trady.Core.Infrastructure;

namespace ShowMeTheMoney.StockAnalyzer.Indicators
{
    internal static class GetTimeSeries
    {
        internal static TimeSeries Get(IOhlcv[] candleEnumerable)
        {
            return new()
            {
                High = candleEnumerable.Select(item => (double) item.High).ToArray(),
                Low = candleEnumerable.Select(item => (double) item.Low).ToArray(),
                Close = candleEnumerable.Select(item => (double) item.Close).ToArray(),
                Time = candleEnumerable.Select(item => DateTime.Parse(item.DateTime.ToString())).ToArray()
            };
        }
    }
}