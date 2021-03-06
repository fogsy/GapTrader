﻿using Foundations;
using GapTraderCore.Interfaces;

namespace GapTraderCore.ViewModels
{
    public sealed class LoadingBarViewModel : BindableBase, ILoadable
    {
        public double Progress
        {
            get => _progress;
            set
            {
                PercentProgress = value / Maximum;
                SetProperty(ref _progress, value);
            }
        }

        public double PercentProgress
        {
            get => _percentProgress;
            set => SetProperty(ref _percentProgress, value);
        }

        public double Maximum
        {
            get => _maximum;
            set => SetProperty(ref _maximum, value);
        }

        private double _progress;
        private double _maximum = 100;
        private double _percentProgress;
    }
}
