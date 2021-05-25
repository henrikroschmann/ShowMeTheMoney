using System;
using Trady.Analysis;
using Trady.Analysis.Extension;
using Trady.Core.Infrastructure;

namespace ShowMeTheMoney.StockAnalyzer.Rules
{
    public static class SellRules
    {
        public static Predicate<IIndexedOhlcv> Get()
        {
            return Rule.Create(c => c.IsFullStoBearishCross(14, 3, 3))
                .Or(c => c.IsMacdBearishCross(12, 24))
                .Or(c => c.IsSmaBearishCross(10, 30));
        }
    }
}