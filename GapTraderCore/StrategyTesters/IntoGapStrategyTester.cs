﻿using GapTraderCore.Candles;
using GapTraderCore.Interfaces;
using GapTraderCore.Strategies;

namespace GapTraderCore.StrategyTesters
{
    public sealed class IntoGapStrategyTester : GapFillStrategyTester
    {
        public IntoGapStrategyTester(ITradeLevelCalculator tradeLevelCalculator) : base(
            tradeLevelCalculator)
        {
            DefaultFibEntry = FibonacciLevel.OneHundredAndTwentySevenPointOne;
            DefaultFibTarget = FibonacciLevel.FivePointNine;
            FibLevelEntry = DefaultFibEntry;
            FibLevelTarget = DefaultFibTarget;
        }

        protected override void NewStrategy<TEntry, TTarget>(object entry, object target, string title = "")
        {
            Strategy = new IntoGapStrategy<TEntry, TTarget>(entry, Stop, target, Stats, MinimumGapSize, Trades, title);
        }

        protected override (double, double, double, double) CalculateTradeLevels(DailyCandle candle)
        {
            var gap = candle.Gap.GapPoints;
            var open = candle.Open;

            return GetTradeLevels(gap, open);
        }
    }
}
