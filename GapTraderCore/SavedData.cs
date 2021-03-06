﻿using System;
using System.IO;
using System.Reflection;
using GapTraderCore.Interfaces;
using static System.IO.Directory;

namespace GapTraderCore
{
    [Serializable]
    public sealed class SavedData
    {
        public string MinuteDataFilePath { get; set; }

        public string DailyDataFilePath { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime SavedDate { get; set; }

        public string Name { get; set; }

        public bool IsUkData { get; set; }

        public double PreviousDailyClose { get; set; }

        public SavedData(string name, IMarket market)
        {
            Name = name;
            StartDate = market.DataDetails.StartDate;
            EndDate = market.DataDetails.EndDate;
            SavedDate = DateTime.Now;
            IsUkData = market.IsUkData;

            PreviousDailyClose = market.DailyCandles[0].Open + market.DailyCandles[0].Gap.GapPoints;

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            path += "\\Saved Data";

            CreateDirectory(path);

            MinuteDataFilePath = $"{path}\\{Name}_minute_bid_data.csv";
            DailyDataFilePath = $"{path}\\{Name}_daily_data.csv";

            CsvServices.WriteMinuteCsv(market.MinuteData, MinuteDataFilePath);
            CsvServices.WriteDailyDataCsv(market.DailyCandles, DailyDataFilePath);
        }
    }
}
