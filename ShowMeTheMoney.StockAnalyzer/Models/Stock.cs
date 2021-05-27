using System;

namespace ShowMeTheMoney.StockAnalyzer.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public TimeSeries TimeSeries { get; set; }
        public int DailyBuySignalFound { get; set; }
        public int DailySuperTrend { get; set; }
        public int DailyMacDSignalFound { get; set; }
        public int DailyTrend { get; set; }
    }
}