using System;
using System.Globalization;
using TicTacToe.Models;
using Xamarin.Forms;

namespace TicTacToe.Converters
{
    /// <summary>
    /// Converts the avatar id to the avatar image name.
    /// </summary>
    public class AvatarIdToImageConverter : IValueConverter
    {
        private const string DefaultCell = "avatar_blank.png";
        private const string Nought = "avatar_p1_0.png";
        private const string Cross = "avatar_p2_x.png";
        private const string Burger = "avatar_p1_burger.png";
        private const string Hotdog = "avatar_p2_hotdog.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case 1:
                    return Nought;
                case 2:
                    return Cross;
                case 3:
                    return Hotdog;
                case 4:
                    return Burger;
                default:
                    return DefaultCell;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}