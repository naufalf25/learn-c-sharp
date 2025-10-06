using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using UnoGame.BackEnd.Enums;

namespace UnoGame.WPFApp.Helper
{
    internal class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                CardColor.Red => Brushes.Red,
                CardColor.Green => Brushes.LimeGreen,
                CardColor.Blue => Brushes.DodgerBlue,
                CardColor.Yellow => Brushes.Yellow,
                _ => Brushes.White,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
