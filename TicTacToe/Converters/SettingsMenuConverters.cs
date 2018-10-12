using System;
using System.Globalization;
using TicTacToe.Models;
using Xamarin.Forms;

namespace TicTacToe.Converters
{
    public class EasyButtonColourConverter : IValueConverter
    {
        public Color Selected = Color.FromHex("#286800");
        public Color NotSelected = Color.FromHex("#383838");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value)
            {
                case AIDifficulty.Easy:
                    return Selected;
                default:
                    return NotSelected;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MediumButtonColourConverter : IValueConverter
    {
        public Color Selected = Color.FromHex("#286800");
        public Color NotSelected = Color.FromHex("#383838");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case AIDifficulty.Medium:
                    return Selected;
                default:
                    return NotSelected;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class HardButtonColourConverter : IValueConverter
    {
        public Color Selected = Color.FromHex("#286800");
        public Color NotSelected = Color.FromHex("#383838");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case AIDifficulty.Hard:
                    return Selected;
                default:
                    return NotSelected;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
