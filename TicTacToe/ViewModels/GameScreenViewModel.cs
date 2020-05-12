using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using TicTacToe.Infrastructure;
using TicTacToe.Infrastructure.Services;
using TicTacToe.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TicTacToe.ViewModels
{
    public class GameScreenViewModel : ViewModelBase
    {
        private IGameEngine _gameEngine;
        private readonly INavigationService _navigationService;

        private bool _isGameWon;
        private bool _isGameDraw;
        private int _gameWinner;

        public Command SelectCellCommand { get; set; }
        public Command PlayAgainCommand { get; set; }
        public Command MainMenuCommand { get; set; }

        public List<BoardCell> Board
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
                RaisePropertyChanged(nameof(BoardButtonsEnabled));
            }
        }

        public bool IsGameDraw
        {
            get => _isGameDraw;
            set
            {
                _isGameDraw = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(BoardButtonsEnabled));
            }
        }

        public bool BoardButtonsEnabled
        {
            get => (!_isGameWon && !_isGameDraw);
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

        public int CurrentTurnPlayerId
        {
            get { return _gameEngine.Players.Where(p => p.IsPlayerTurn == true).First().PlayerId; }
        }

        public GameScreenViewModel(INavigationService navigationService)
        {
            SelectCellCommand = new Command<string>(SelectCell);
            MainMenuCommand = new Command(OpenMainMenu);
            PlayAgainCommand = new Command(PlayAgain);

            _navigationService = navigationService;
            
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
            if (_gameEngine.Players.Where(m => m.IsPlayerTurn == true).FirstOrDefault().PlayerType == PlayerType.Ai)
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
        }

        private void OpenMainMenu()
        {
            _navigationService.GoBack();

            var settings = new EngineSettings() { Difficulty = _gameEngine.AiDifficulty, Player1AvatarId = 1, Player2AvatarId = 3 };
            settings.GameType = _gameEngine.GameType == GameType.Singleplayer ? GameType.Singleplayer : GameType.Multiplayer;
            IGameEngine newGameEngine = new GameEngine(settings);

            _gameEngine = newGameEngine;
            RaisePropertyChanged(nameof(Board));

            IsGameWon = false;
            IsGameDraw = false;
        }

        private void PlayAgain()
        {
            var settings = new EngineSettings() { Difficulty = _gameEngine.AiDifficulty, 
		        Player1AvatarId = _gameEngine.Players[0].PlayerAvatar, Player2AvatarId = _gameEngine.Players[1].PlayerAvatar };
            settings.GameType = _gameEngine.GameType == GameType.Singleplayer ? GameType.Singleplayer : GameType.Multiplayer;
            IGameEngine newGameEngine = new GameEngine(settings);

            _gameEngine = newGameEngine;
            RaisePropertyChanged(nameof(Board));

            IsGameWon = false;
            IsGameDraw = false;
        }
    }
}
