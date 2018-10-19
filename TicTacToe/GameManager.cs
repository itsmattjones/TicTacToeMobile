using System.Collections.Generic;
using TicTacToe.Models;
using TicTacToe.Models.Player;
using TicTacToe.ViewModels;
using TicTacToe.Views;

namespace TicTacToe
{
    public class GameManager
    {
        public GameEngine GameEngine;
        public AiDifficulty AiDifficulty;

        /// <summary>
        /// Creates a new multiplayer game.
        /// </summary>
        public void CreateMultiplayerGame()
        {
            // Create Game Engine.
            GameEngine = new GameEngine();

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

        /// <summary>
        /// Creates a new singleplayer game.
        /// </summary>
        public void CreateSingleplayerGame()
        {
            // Initalize Game Engine.
            GameEngine = new GameEngine();

            // Setup model for the game.
            var gameModel = new GameModel
            {
                PlayerOne = new NormalPlayerModel { PlayerId = 1, IsPlayerTurn = true, PlayerAvatar = 1 },
                PlayerTwo = new AiPlayerModel { PlayerId = 2, IsPlayerTurn = false, PlayerAvatar = 2, Difficulty = AiDifficulty },
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
