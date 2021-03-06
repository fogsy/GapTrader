﻿using System.Collections.Generic;
using GapTraderCore.Interfaces;

namespace GapTraderCore.Strategies
{
    internal class IntoGapStrategy<TEntry, TTarget> : Strategy<TEntry, TTarget>, IGapFillStrategy
    {
        public double MinimumGapSize { get; }

        public IntoGapStrategy(object entry, double stop, object target, StrategyStats stats, double minimumGapSize,
            List<ITrade> trades, string title) : base(entry, stop, target, stats, trades, title)
        {
            MinimumGapSize = minimumGapSize;
        }
    }
}