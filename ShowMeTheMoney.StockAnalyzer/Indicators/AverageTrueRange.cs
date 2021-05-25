using System;
using ShowMeTheMoney.StockAnalyzer.Models;

namespace ShowMeTheMoney.StockAnalyzer.Indicators
{
    internal static class AverageTrueRange
    {
        internal static double[] Atr(TimeSeries timeSeries, int number)
        {
            var temp = new double[number];
            temp[0] = 0;

            for (var i = 1; i <= number; i++)
            {
                var diff1 = Math.Abs(timeSeries.Close[^(i + 1)] - timeSeries.High[^i]);
                var diff2 = Math.Abs(timeSeries.Close[^(i + 1)] - timeSeries.Low[^i]);
                var diff3 = timeSeries.High[^i] - timeSeries.Low[^i];

                var max = diff1 > diff2 ? diff1 : diff2;
                temp[i - 1] = max > diff3 ? max : diff3;
            }

            return SimpleMovingAverage.Calculate(temp, number);
        }
    }
}