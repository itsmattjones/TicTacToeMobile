using System;
using Xunit;
using TicTacToe.Infrastructure;
using TicTacToe.Models;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace TicTacToe.UnitTests
{
    public class GameEngineTests
    {

        #region Player Tick Tests

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(8)]
        public void PlayerTickValidCell(int cell)
        {
            var gameEngine = new GameEngine(new EngineSettings() {
                GameType = GameType.Multiplayer,
                Player1AvatarId = 1,
                Player2AvatarId = 2
            });

            var tickResult = gameEngine.TickPlayerTurn(cell);
            Assert.True(tickResult.SuccessfulTick);
            Assert.Equal(CellState.TakenByPlayer1, gameEngine.Board[cell].State);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(-5)]
        [InlineData(51)]
        public void PlayerTickInvalidCell(int cell)
        {
            var gameEngine = new GameEngine(new EngineSettings()
            {
                GameType = GameType.Multiplayer,
                Player1AvatarId = 1,
                Player2AvatarId = 2
            });

            var tickResult = gameEngine.TickPlayerTurn(cell);
            Assert.False(tickResult.SuccessfulTick);
        }

        [Fact]
        public void PlayerTickUnavailableCell()
        {
            var gameEngine = new GameEngine(new EngineSettings()
            {
                GameType = GameType.Multiplayer,
                Player1AvatarId = 1,
                Player2AvatarId = 2
            });

            var tickResult = gameEngine.TickPlayerTurn(1);
            Assert.True(tickResult.SuccessfulTick);
            Assert.Equal(CellState.TakenByPlayer1, gameEngine.Board[1].State);

            tickResult = gameEngine.TickPlayerTurn(1);
            Assert.False(tickResult.SuccessfulTick);
            Assert.Equal(CellState.TakenByPlayer1, gameEngine.Board[1].State);
        }

        [Fact]
        public void PlayerTickDetectsWin()
        {
            var gameEngine = new GameEngine(new EngineSettings()
            {
                GameType = GameType.Multiplayer,
                Player1AvatarId = 1,
                Player2AvatarId = 2
            });

            var cellSelectOrder = new List<int>() { 0, 5, 1, 7 };
            foreach (int cell in cellSelectOrder)
            {
                var result = gameEngine.TickPlayerTurn(cell);
                Assert.True(result.SuccessfulTick);
	        }

            var tickResult = gameEngine.TickPlayerTurn(2);
            Assert.True(tickResult.SuccessfulTick);
            Assert.True(tickResult.GameFinished);
            Assert.True(tickResult.Results.GameHasWinner);
            Assert.Equal(gameEngine.Players[0], tickResult.Results.Winner);
        }

        [Fact]
        public void PlayerTickDetectsDraw()
        {
            var gameEngine = new GameEngine(new EngineSettings()
            {
                GameType = GameType.Multiplayer,
                Player1AvatarId = 1,
                Player2AvatarId = 2
            });

            var cellSelectOrder = new List<int>() { 0, 1, 2, 3, 4, 6, 5, 8 };
            foreach (int cell in cellSelectOrder)
            {
                var result = gameEngine.TickPlayerTurn(cell);
                Assert.True(result.SuccessfulTick);
            }

            var tickResult = gameEngine.TickPlayerTurn(7);
            Assert.True(tickResult.SuccessfulTick);
            Assert.True(tickResult.GameFinished);
            Assert.False(tickResult.Results.GameHasWinner);
        }

        #endregion

        #region AI Player Tick Tests

        [Fact]
        public void AITickNormalPlayer()
        {
            var gameEngine = new GameEngine(new EngineSettings()
            {
                GameType = GameType.Multiplayer,
                Player1AvatarId = 1,
                Player2AvatarId = 2
            });

            var exception = Assert.Throws<Exception>(() => {
                var result = gameEngine.TickAIPlayerTurn();
	        });

            Assert.Equal("Cannot tick AI player turn of a non-ai player", exception.Message);
        }

        [Fact]
        public void EasyAIPlayerTick()
        {
            var gameEngine = new GameEngine(new EngineSettings()
            {
                GameType = GameType.Singleplayer,
                Difficulty = AiDifficulty.Easy,
                Player1AvatarId = 1,
                Player2AvatarId = 2
            });

            Assert.True(gameEngine.TickPlayerTurn(0).SuccessfulTick);

            var tickResult = gameEngine.TickAIPlayerTurn();
            Assert.True(tickResult.SuccessfulTick);
            Assert.Equal(CellState.TakenByPlayer2, gameEngine.Board[1].State);
        }

        [Fact]
        public void MediumAIPlayerTickBlocks1Cell()
        {
            var gameEngine = new GameEngine(new EngineSettings()
            {
                GameType = GameType.Singleplayer,
                Difficulty = AiDifficulty.Medium,
                Player1AvatarId = 1,
                Player2AvatarId = 2
            });

            Assert.True(gameEngine.TickPlayerTurn(0).SuccessfulTick);
            Assert.True(gameEngine.TickAIPlayerTurn().SuccessfulTick);

            var expectedPlays = new List<int>() { 1, 2, 3, 4, 6, 8 };
            var expectedPlayOccured = false;
            foreach(var play in expectedPlays)
            {
		        if(gameEngine.Board[play].State == CellState.TakenByPlayer2)
                {
                    expectedPlayOccured = true;
                    break;
		        } 
	        }

            Assert.True(expectedPlayOccured);
        }

        [Fact]
        public void MediumAIPlayerTickBlocks2Cells()
        {
            var gameEngine = new GameEngine(new EngineSettings()
            {
                GameType = GameType.Singleplayer,
                Difficulty = AiDifficulty.Medium,
                Player1AvatarId = 1,
                Player2AvatarId = 2
            });

            Assert.True(gameEngine.TickPlayerTurn(4).SuccessfulTick);
            Assert.True(gameEngine.TickAIPlayerTurn().SuccessfulTick);

            var possibleNextPlays = new List<int>() { 1, 3, 5, 7 };
            var playTaken = -1;
            foreach (var play in possibleNextPlays)
            {
                if (gameEngine.Board[play].State != CellState.TakenByPlayer2)
                {
                    Assert.True(gameEngine.TickPlayerTurn(play).SuccessfulTick);
                    playTaken = play;
                    break;
                }
	        }
            
            Assert.True(gameEngine.TickAIPlayerTurn().SuccessfulTick);

            switch (playTaken)
            {
                case 1: Assert.Equal(CellState.TakenByPlayer2, gameEngine.Board[7].State); break;
                case 3: Assert.Equal(CellState.TakenByPlayer2, gameEngine.Board[5].State); break;
                case 5: Assert.Equal(CellState.TakenByPlayer2, gameEngine.Board[3].State); break;
                case 7: Assert.Equal(CellState.TakenByPlayer2, gameEngine.Board[1].State); break;
                default: Assert.False(true); break;
	        }
        }

        [Fact]
        public void HardAIPlayerTickTakesCentralCell()
        {
            var gameEngine = new GameEngine(new EngineSettings()
            {
                GameType = GameType.Singleplayer,
                Difficulty = AiDifficulty.Hard,
                Player1AvatarId = 1,
                Player2AvatarId = 2
            });

            Assert.True(gameEngine.TickPlayerTurn(1).SuccessfulTick);
            Assert.True(gameEngine.TickAIPlayerTurn().SuccessfulTick);
            Assert.Equal(CellState.TakenByPlayer2, gameEngine.Board[4].State);
        }

        [Fact]
        public void HardAIPlayerTickUsesMediumAIIfCantWin()
        {
            var gameEngine = new GameEngine(new EngineSettings()
            {
                GameType = GameType.Singleplayer,
                Difficulty = AiDifficulty.Hard,
                Player1AvatarId = 1,
                Player2AvatarId = 2
            });

            Assert.True(gameEngine.TickPlayerTurn(0).SuccessfulTick);
            Assert.True(gameEngine.TickAIPlayerTurn().SuccessfulTick);
            Assert.Equal(CellState.TakenByPlayer2, gameEngine.Board[4].State);
            Assert.True(gameEngine.TickPlayerTurn(1).SuccessfulTick);
            Assert.True(gameEngine.TickAIPlayerTurn().SuccessfulTick);
            Assert.Equal(CellState.TakenByPlayer2, gameEngine.Board[2].State);
        }

        [Fact]
        public void HardAIPlayerTickAttempsWin()
        {
            var gameEngine = new GameEngine(new EngineSettings()
            {
                GameType = GameType.Singleplayer,
                Difficulty = AiDifficulty.Hard,
                Player1AvatarId = 1,
                Player2AvatarId = 2
            });

            Assert.True(gameEngine.TickPlayerTurn(0).SuccessfulTick);
            Assert.True(gameEngine.TickAIPlayerTurn().SuccessfulTick);
            Assert.Equal(CellState.TakenByPlayer2, gameEngine.Board[4].State);
            Assert.True(gameEngine.TickPlayerTurn(2).SuccessfulTick);
            Assert.True(gameEngine.TickAIPlayerTurn().SuccessfulTick);
            Assert.Equal(CellState.TakenByPlayer2, gameEngine.Board[1].State);
            Assert.True(gameEngine.TickPlayerTurn(8).SuccessfulTick);
            Assert.True(gameEngine.TickAIPlayerTurn().SuccessfulTick);
            Assert.Equal(CellState.TakenByPlayer2, gameEngine.Board[7].State);
        }

        #endregion
    }
}
