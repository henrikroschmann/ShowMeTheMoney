using System;

namespace ShowMeTheMoney.StockAnalyzer.Models
{
    public class TimeSeries
    {
        /// <summary>
        ///     Gets the open price series.
        /// </summary>
        public double[] Open { get; set; }

        /// <summary>
        ///     Gets the highest price series.
        /// </summary>
        public double[] High { get; set; }

        /// <summary>
        ///     Gets the lowest price series.
        /// </summary>
        public double[] Low { get; set; }

        /// <summary>
        ///     Gets the close price series.
        /// </summary>
        public double[] Close { get; set; }

        /// <summary>
        ///     Gets the volume series.
        /// </summary>
        public double[] Vol { get; set; }

        /// <summary>
        ///     Gets the date and time series.
        /// </summary>
        public DateTime[] Time { get; set; }
    }
}