using System;
using Xunit;
using TicTacToe.Converters;
using System.Globalization;

namespace TicTacToe.UnitTests
{
    public class GameBoardConvertersTests
    {
        [Fact]
        public void EndGameDialogWinTests()
        {
            var vConverter = new EndGameDialogWinner();

            Assert.Equal("PLAYER 1", vConverter.Convert(1, typeof(string), null, CultureInfo.CurrentCulture));
            Assert.Equal("PLAYER 2", vConverter.Convert(2, typeof(string), null, CultureInfo.CurrentCulture));
            Assert.Equal("UNKNOWN", vConverter.Convert(5, typeof(string), null, CultureInfo.CurrentCulture));

            Assert.Throws<NotImplementedException>(() => {
                vConverter.ConvertBack("PLAYER 1", typeof(int), null, CultureInfo.CurrentCulture);
            });
        }

        [Fact]
        public void CurrentPlayerTurnTests()
        {
            var vConverter = new CurrentPlayerTurnConverter();

            Assert.Equal("Player 1's turn", vConverter.Convert(1, typeof(string), null, CultureInfo.CurrentCulture));
            Assert.Equal("Player 2's turn", vConverter.Convert(2, typeof(string), null, CultureInfo.CurrentCulture));
            Assert.Equal("Player 5's turn", vConverter.Convert(5, typeof(string), null, CultureInfo.CurrentCulture));

            Assert.Throws<NotImplementedException>(() => {
                vConverter.ConvertBack("Player 1's turn", typeof(int), null, CultureInfo.CurrentCulture);
	        });
        }
    }
}
