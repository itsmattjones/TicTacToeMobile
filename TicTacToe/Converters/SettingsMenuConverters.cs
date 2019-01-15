using System;
using System.Globalization;
using TicTacToe.Models.Player;
using Xamarin.Forms;

namespace TicTacToe.Converters
{
    /// <summary>
    /// Converts the colour of the easy difficulty button
    /// depending on whether it's selected or not.
    /// </summary>
    public class EasyButtonColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value)
            {
                case AiDifficulty.Easy:
                    return Color.FromHex("#604609");
                default:
                    return Color.FromHex("#ffba1c");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts the colour of the medium difficulty button
    /// depending on whether it's selected or not.
    /// </summary>
    public class MediumButtonColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case AiDifficulty.Medium:
                    return Color.FromHex("#604609");
                default:
                    return Color.FromHex("#ffba1c");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts the colour of the hard difficulty button
    /// depending on whether it's selected or not.
    /// </summary>
    public class HardButtonColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case AiDifficulty.Hard:
                    return Color.FromHex("#604609");
                default:
                    return Color.FromHex("#ffba1c");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
