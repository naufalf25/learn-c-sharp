using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UnoGame.WPFApp.Helper
{
    internal class MuteButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            var param = parameter?.ToString()?.Split('|');
            if (param == null || param.Length < 2) return value.ToString();
            return (bool)value ? param[0] : param[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}
