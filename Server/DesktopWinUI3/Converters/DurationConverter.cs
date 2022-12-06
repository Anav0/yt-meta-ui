using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWinUI3.Converters
{
    public sealed class DurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return "0h 0m 0s";

            var seconds = System.Convert.ToDouble(value);

            var h = Math.Round(seconds / 3600);
            var m = Math.Round(seconds % 3600 / 60);
            var s = seconds % 60;

            return $"{h}h {m}m {s}s";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
