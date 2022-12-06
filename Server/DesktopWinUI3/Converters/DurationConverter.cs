using Microsoft.UI.Xaml.Data;
using System;

namespace DesktopWinUI3.Converters;

public sealed class DurationConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null) return "0h 0m 0s";

        double seconds = System.Convert.ToDouble(value);

        double h = Math.Round(seconds / 3600);
        double m = Math.Round(seconds % 3600 / 60);
        double s = seconds % 60;

        return $"{h}h {m}m {s}s";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
