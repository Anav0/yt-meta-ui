using Microsoft.UI.Xaml.Data;
using System;

namespace DesktopWinUI3.Converters;

public sealed class PriceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null) return "";

        return $"{Math.Round(System.Convert.ToDouble(value))}zł";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
