﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Foundations.Optional;
using GapTraderCore.Candles;
using GapTraderCore.ViewModels;

namespace GapTraderCore.Interfaces
{
    public interface IMarket : IObservableMarket
    {
        Dictionary<FibonacciLevel, FibLevel> GapFibRetraceLevels { get; }

        Dictionary<FibonacciLevel, FibLevel> GapFibExtensionLevels { get; }

        List<DailyCandle> DailyCandles { get; set; }

        Dictionary<DateTime, List<BidAskCandle>> MinuteData { get; set; }

        List<Gap> UnfilledGaps { get; }

        DataDetails DataDetails { get; }

        void DeriveDailyFromMinute(Del counter);

        void ClearData();

        void CalculateStats(bool ukData, Optional<double> previousClose);

        bool IsUkData { get; }
    }
}