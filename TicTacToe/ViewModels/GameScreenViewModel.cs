using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TicTacToe.Models;
using TicTacToe.Services;
using Xamarin.Forms;

namespace TicTacToe.ViewModels
{
    public class GameScreenViewModel : IGameScreenViewModel, INotifyPropertyChanged
    {
        private IGameEngine _gameEngine;

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
                NotifyPropertyChanged("IsGameWon");
            }
        }

        public bool IsGameDraw
        {
            get => _isGameDraw;
            set
            {
                _isGameDraw = value;
                NotifyPropertyChanged("IsGameDraw");
            }
        }

        public bool BoardButtonsEnabled
        {
            get => _boardButtonsEnabled;
            set
            {
                _boardButtonsEnabled = value;
                NotifyPropertyChanged("BoardButtonsEnabled");
            }
        }
        
        public int GameWinner
        {
            get => _gameWinner;
            set
            {
                _gameWinner = value;
                NotifyPropertyChanged("GameWinner");
            }
        }

        public GameScreenViewModel(IGameEngine gameEngine)
        {
            _gameEngine = gameEngine;

            SelectCellCommand = new Command<string>(SelectCell);
            MainMenuCommand = new Command(OpenMainMenu);
            PlayAgainCommand = new Command(PlayAgain);

            BoardButtonsEnabled = true;
        }

        private void SelectCell(string cellId)
        {
            if (!int.TryParse(cellId, out var cell) || cellId == string.Empty)
                return;

            var tickResult = _gameEngine.TickPlayerTurn(cell);

            // If game is finished end.
            UpdateGameStatus(tickResult);
            if (tickResult.GameFinished)
                return;

            // If the games multiplayer, make AI players take their turns.
            while (_gameEngine.Players.Where(m => m.IsPlayerTurn == true).FirstOrDefault().PlayerType == PlayerType.Ai)
                UpdateGameStatus(_gameEngine.TickAIPlayerTurn());
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
        }

        private void PlayAgain()
        {
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
