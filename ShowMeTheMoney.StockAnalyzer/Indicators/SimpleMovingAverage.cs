namespace ShowMeTheMoney.StockAnalyzer.Indicators
{
    internal static class SimpleMovingAverage
    {
        /// <summary>
        ///     Takes the closing price as array and periods to determine speed
        ///     RMA = ((RMA(t-1) * (n-1)) + Xt) / n
        ///     fastSma = 5
        ///     SlowSMA = 34
        /// </summary>
        /// <param name="price"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        internal static double[] Calculate(double[] price, int period)
        {
            var sma = new double[price.Length];

            double sum = 0;

            for (var i = 0; i < period; i++)
            {
                sum += price[i];
                sma[i] = sum / (i + 1);
            }

            for (var i = period; i < price.Length; i++)
            {
                sum = 0;
                for (var j = i; j > i - period; j--) sum += price[j];

                sma[i] = sum / period;
            }

            return sma;
        }
    }
}