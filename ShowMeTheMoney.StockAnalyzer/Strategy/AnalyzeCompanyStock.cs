using ShowMeTheMoney.CompanyBuilder.Models;
using ShowMeTheMoney.StockAnalyzer.Models;
using ShowMeTheMoney.StockAnalyzer.Rules;
using System;
using Trady.Analysis;
using Trady.Importer.Yahoo;

namespace ShowMeTheMoney.StockAnalyzer.Strategy
{
    internal static class AnalyzeCompanyStock
    {
        internal static StockAnalysis Analyze(Company company)
        {
            var importer = new YahooFinanceImporter();
            var dailyStocks = importer.ImportAsync(company.Symbol.Trim().Replace(" ", "-") + ".ST", DateTime.Today.AddDays(-100));
            var dailyCandles = new AnalyzeContext(dailyStocks.Result);

            var dailyBuySignals = new SimpleRuleExecutor(dailyCandles, BuyRules.Get()).Execute();
            var dailyTrendSignals = new SimpleRuleExecutor(dailyCandles, BuyRules.StillGood()).Execute();
            var dailySuperTrend = SuperTrend.Calculate(dailyCandles.BackingList);
            return new StockAnalysis
            {
                DailyCandles = dailyCandles,
                DailyBuySignals = dailyBuySignals,
                DailyTrendSignals = dailyTrendSignals,
                DailySuperTrend = dailySuperTrend
            };
        }
    }
}