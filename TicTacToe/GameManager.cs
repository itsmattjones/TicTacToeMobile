using System.Collections.Generic;
using TicTacToe.Models;
using TicTacToe.ViewModels;
using TicTacToe.Views;

namespace TicTacToe
{
    public class GameManager
    {
        public GameEngine gameEngine;

        public void CreateMultiplayerGame()
        {
            // Initalize Game Engine.
            gameEngine = new GameEngine();

            // Setup model for the game.
            var gameModel = new GameModel
            {
                PlayerOne = new NormalPlayerModel { PlayerId = 1, IsPlayerTurn = true, PlayerAvatar = 1 },
                PlayerTwo = new NormalPlayerModel{ PlayerId = 2, IsPlayerTurn = false, PlayerAvatar = 2 },
                Board = new List<CellModel>()
            };

            for (var i = 0; i < gameModel.BoardSize; i++)
                gameModel.Board.Add(new CellModel());

            // Setup view model for the game screen.
            var gameScreenViewModel = new GameScreenViewModel { GameModel = gameModel };

            Xamarin.Forms.Application.Current.MainPage = new GameScreen(gameScreenViewModel);
        }

        public void CreateSingleplayerGame()
        {
            // Initalize Game Engine.
            gameEngine = new GameEngine();

            // Setup model for the game.
            var gameModel = new GameModel
            {
                PlayerOne = new NormalPlayerModel { PlayerId = 1, IsPlayerTurn = true, PlayerAvatar = 1 },
                PlayerTwo = new AIPlayerModel { PlayerId = 2, IsPlayerTurn = false, PlayerAvatar = 2, Difficulty = Program.aiDifficulty },
                Board = new List<CellModel>()
            };

            for (var i = 0; i < gameModel.BoardSize; i++)
                gameModel.Board.Add(new CellModel());

            // Setup view model for the game screen.
            var gameScreenViewModel = new GameScreenViewModel { GameModel = gameModel };

            Xamarin.Forms.Application.Current.MainPage = new GameScreen(gameScreenViewModel);
        }
    }
}
