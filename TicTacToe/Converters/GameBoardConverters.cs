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
        readonly string p1 = "X";
        readonly string p2 = "O";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value)
            {
                case 0:
                    return string.Empty;
                case 1:
                    return p1;
                case 2:
                    return p2;
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
        readonly Color p1 = Color.FromHex("#33cc33");
        readonly Color p2 = Color.FromHex("#ff471a");
        readonly Color unknown = Color.FromHex("#ccffff");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case 0:
                    return unknown;
                case 1:
                    return p1;
                case 2:
                    return p2;
                default:
                    return unknown;
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
        readonly string PlayerOneText = "PLAYER 1";
        readonly string PlayerTwoText = "PLAYER 2";
        readonly string UnknownPlayerText = "UNKNOWN";

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
