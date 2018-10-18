using System;
using System.Globalization;
using Xamarin.Forms;

namespace TicTacToe.Converters
{
    /// <summary>
    /// Converts the text shown on cells on the game board.
    /// </summary>
    public class BoardCellTextConverter : IValueConverter
    {
        private const string P1 = "X";
        private const string P2 = "O";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value)
            {
                case 0:
                    return string.Empty;
                case 1:
                    return P1;
                case 2:
                    return P2;
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts the colour of the cells on the game board.
    /// </summary>
    public class BoardCellColourConverter : IValueConverter
    {
        private readonly Color _p1 = Color.FromHex("#33cc33");
        private readonly Color _p2 = Color.FromHex("#ff471a");
        private readonly Color _unknown = Color.FromHex("#ccffff");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case 0:
                    return _unknown;
                case 1:
                    return _p1;
                case 2:
                    return _p2;
                default:
                    return _unknown;
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
}
