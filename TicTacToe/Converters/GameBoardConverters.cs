using System;
using System.Globalization;
using TicTacToe.Models;
using Xamarin.Forms;

namespace TicTacToe.Converters
{
    /// <summary>
    /// Converts the player ID to the player name during the
    /// end game dialog sequence.
    /// </summary>
    public class EndGameDialogWinner : IValueConverter
    {
        private const string PlayerOneText = "PLAYER 1";
        private const string PlayerTwoText = "PLAYER 2";
        private const string UnknownPlayerText = "UNKNOWN";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case 1:
                    return PlayerOneText;
                case 2:
                    return PlayerTwoText;
                default:
                    return UnknownPlayerText;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CurrentPlayerTurnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Concat("Player ", value, "'s turn");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
