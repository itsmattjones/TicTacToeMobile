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
        public int PlayerOneAvatar = 1;
        public int PlayerTwoAvatar = 2;

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
                PlayerOne = new NormalPlayerModel { PlayerId = 1, IsPlayerTurn = true, PlayerAvatar = PlayerOneAvatar },
                PlayerTwo = new NormalPlayerModel{ PlayerId = 2, IsPlayerTurn = false, PlayerAvatar = PlayerTwoAvatar },
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
                PlayerOne = new NormalPlayerModel { PlayerId = 1, IsPlayerTurn = true, PlayerAvatar = PlayerOneAvatar },
                PlayerTwo = new AiPlayerModel { PlayerId = 2, IsPlayerTurn = false, PlayerAvatar = PlayerTwoAvatar, Difficulty = AiDifficulty },
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
