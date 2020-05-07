using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using TicTacToe.Infrastructure;
using TicTacToe.Infrastructure.Enums;
using TicTacToe.Infrastructure.Services;
using TicTacToe.Models;
using Xamarin.Forms;

namespace TicTacToe.ViewModels
{
    public class GameScreenViewModel : ViewModelBase
    {
        private IGameEngine _gameEngine;
        private readonly INavigationService _navigationService;

        private bool _boardButtonsEnabled;
        private bool _isGameWon;
        private bool _isGameDraw;
        private int _gameWinner;

        public Command SelectCellCommand { get; set; }
        public Command PlayAgainCommand { get; set; }
        public Command MainMenuCommand { get; set; }

        public List<IBoardCell> Board
        {
            get => _gameEngine.Board;
        }

        public bool IsGameWon 
        {
            get =>  _isGameWon;
            set 
            {
                _isGameWon = value;
                RaisePropertyChanged();
            }
        }

        public bool IsGameDraw
        {
            get => _isGameDraw;
            set
            {
                _isGameDraw = value;
                RaisePropertyChanged();
            }
        }

        public bool BoardButtonsEnabled
        {
            get => _boardButtonsEnabled;
            set
            {
                _boardButtonsEnabled = value;
                RaisePropertyChanged();
            }
        }
        
        public int GameWinner
        {
            get => _gameWinner;
            set
            {
                _gameWinner = value;
                RaisePropertyChanged();
            }
        }

        public GameScreenViewModel(INavigationService navigationService)
        {
            SelectCellCommand = new Command<string>(SelectCell);
            MainMenuCommand = new Command(OpenMainMenu);
            PlayAgainCommand = new Command(PlayAgain);

            _navigationService = navigationService;
            BoardButtonsEnabled = true;
            
            Messenger.Default.Register<IGameEngine>(this, engine => _gameEngine = engine);
        }

        private void SelectCell(string cellId)
        {
            if (!int.TryParse(cellId, out var cell) || cellId == string.Empty)
                return;

            // Try to select the cell
            var tickResult = _gameEngine.TickPlayerTurn(cell);
            RaisePropertyChanged(nameof(Board));

            // If game is finished end.
            UpdateGameStatus(tickResult);
            if (tickResult.GameFinished)
                return;

            // If the games multiplayer, make AI players take their turns.
            while (_gameEngine.Players.Where(m => m.IsPlayerTurn == true).FirstOrDefault().PlayerType == PlayerType.Ai)
            {
                var result = _gameEngine.TickAIPlayerTurn();
                RaisePropertyChanged(nameof(Board));
                UpdateGameStatus(result);
	        }
        }

        private void UpdateGameStatus(EngineTickResult engineTick)
        {
            if (engineTick.GameFinished && engineTick.Results.GameHasWinner)
            {
                IsGameWon = true;
                GameWinner = engineTick.Results.Winner.PlayerId;
            }
            else if(engineTick.GameFinished && !engineTick.Results.GameHasWinner)
                IsGameDraw = true;

            BoardButtonsEnabled = !(IsGameWon || IsGameDraw);
        }

        private void OpenMainMenu()
        {
            _navigationService.GoBack();

            var newGameEngine = _gameEngine.GameType == GameType.Singleplayer ?
                new GameEngine(GameType.Singleplayer) : new GameEngine(GameType.Multiplayer);

            _gameEngine = newGameEngine;
            RaisePropertyChanged(nameof(Board));

            IsGameWon = false;
            IsGameDraw = false;
            BoardButtonsEnabled = true;
        }

        private void PlayAgain()
        {
            var newGameEngine = _gameEngine.GameType == GameType.Singleplayer ? 
		        new GameEngine(GameType.Singleplayer) : new GameEngine(GameType.Multiplayer);

            _gameEngine = newGameEngine;
            RaisePropertyChanged(nameof(Board));

            IsGameWon = false;
            IsGameDraw = false;
            BoardButtonsEnabled = true;
        }
    }
}
