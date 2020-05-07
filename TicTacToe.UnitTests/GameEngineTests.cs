using System;
using Xunit;
using TicTacToe.Services;
using TicTacToe.Models;
using System.Linq;

namespace TicTacToe.UnitTests
{
    public class GameEngineTests
    {
        #region Initialization Tests

        [Fact]
        public void SetupValidSingleplayerGameEngine()
        {
            var gameEngine = new GameEngine(GameType.Singleplayer, 9);

            Assert.Equal(9, gameEngine.Board.Count);
            Assert.Equal(2, gameEngine.Players.Count);
            Assert.Equal(PlayerType.Normal, gameEngine.Players[0].PlayerType);
            Assert.Equal(PlayerType.Ai, gameEngine.Players[1].PlayerType);
            Assert.Equal(GameType.Singleplayer, gameEngine.GameType);
        }

        [Fact]
        public void SetupValidMultiplayerGameEngine()
        {
            var gameEngine = new GameEngine(GameType.Multiplayer, 9);

            Assert.Equal(9, gameEngine.Board.Count);
            Assert.Equal(2, gameEngine.Players.Count);
            Assert.Equal(PlayerType.Normal, gameEngine.Players[0].PlayerType);
            Assert.Equal(PlayerType.Normal, gameEngine.Players[1].PlayerType);
            Assert.Equal(GameType.Multiplayer, gameEngine.GameType);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(-9)]
        [InlineData(17)]
        [InlineData(0)]
        public void SetupGameEngineInvalidBoardSize(int boardSize)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(
                delegate { var gameEngine = new GameEngine(GameType.Multiplayer, boardSize); });

            Assert.Equal("Invalid board size", exception.Message);
	    }

        #endregion

        #region Tick Real Player Turn Tests

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(8)]
        public void CanTickRealPlayerTurnValidCell(int chosenCell)
        {
            var gameEngine = new GameEngine(GameType.Multiplayer, 9);
            var tickResult = gameEngine.TickPlayerTurn(chosenCell);

            Assert.True(tickResult.SuccessfulTick);
            Assert.False(gameEngine.Players[0].IsPlayerTurn);
            Assert.True(gameEngine.Players[1].IsPlayerTurn);
            Assert.Equal(1, gameEngine.Players[0].TurnsTaken);
            Assert.Contains(gameEngine.Board, c => c.State == CellState.TakenByPlayer1);
	    }

        [Fact]
        public void CantTickRealPlayerTurnInvalidCell()
        { 
        }

        [Fact]
        public void CantTickRealPlayerCellNotAvailable()
        { 
        }

        [Fact]
        public void RealPlayerTickDetectsWin()
        { 
	    }

        [Fact]
        public void RealPlayerTickDetectsDraw()
        { 
        }

        #endregion

        #region Tick AI Player Turn Tests
        #endregion
    }
}
