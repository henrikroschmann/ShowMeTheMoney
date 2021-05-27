using System;
using Trady.Analysis;
using Trady.Analysis.Extension;
using Trady.Core.Infrastructure;

namespace ShowMeTheMoney.StockAnalyzer.Rules
{
    public static class BuyRules
    {
        public static Predicate<IIndexedOhlcv> Get()
        {
            return Rule.Create(c => c.IsFullStoBullishCross(14, 3, 3))
                .And(c => c.IsMacdOscBullish())
                .And(c => c.IsSmaOscBullish(10, 30))
                .And(c => c.IsAccumDistBullish());
        }

        public static Predicate<IIndexedOhlcv> StillGood()
        {
            return Rule.Create(c => c.IsFastStoBullishCross(14, 3))
                .And(c => c.IsFastStoOscBullish(14, 3));
            //.And(c => c.IsFastStoOverbought(14, 3));
        }
    }
}