using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinRT;

namespace DesktopWinUI3.Converters
{
    public sealed class StringToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is null) return null;

            var x = value.As<DateTime>().ToString("dd/MM/yyyy HH:mm");
            return x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is null) return null;

            var str = value.As<string>();

            if(string.IsNullOrEmpty(str)) return null;

            return DateTime.Parse(str);
        }
    }
}
