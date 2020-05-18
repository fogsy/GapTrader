﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace GapAnalyserWPF.Converters
{
    public class EmptyDoubleToStringConverter : MarkupConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || Math.Abs((double) value - default(double)) < 0)
            {
                return "";
            }

            return value.ToString();
        }

        public new object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType is null)
            {
                return null;
            }

            if (double.TryParse((string)value, out double d))
            {
                return d;
            }

            return default;
        }
    }
}
