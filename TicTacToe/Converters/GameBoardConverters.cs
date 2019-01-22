using System;
using System.Globalization;
using Xamarin.Forms;

namespace TicTacToe.Converters
{
    /// <summary>
    /// Converts the image shown on cells on the game board.
    /// </summary>
    public class BoardCellImageConverter : IValueConverter
    {
        private const string DefaultCell = "avatar_blank.png";
        private const string Nought = "avatar_0.png";
        private const string Cross = "avatar_x.png";
        private const string Hotdog = "avatar_hotdog.png";
        private const string Burger = "avatar_burger.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value)
            {
                case 0:
                    return DefaultCell;
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
