using System.ComponentModel;
using TicTacToe.Models;
using Xamarin.Forms;

namespace TicTacToe.ViewModels
{
    public class GameScreenViewModel : INotifyPropertyChanged
    {

        #region Fields

        private GameModel _gameModel;
        private bool _boardButtonsEnabled;
        private bool _isGameWon;
        private bool _isGameDraw;
        private int _gameWinner;

        #endregion

        #region Properties

        public GameModel GameModel
        {
            get { return _gameModel; }
            set
            {
                _gameModel = value;
                NotifyPropertyChanged("GameModel");
            }
        }

        public bool IsGameWon 
        {
            get { return _isGameWon; }
            set 
            {
                _isGameWon = value;
                NotifyPropertyChanged("IsGameWon");
            }
        }

        public bool IsGameDraw
        {
            get { return _isGameDraw; }
            set
            {
                _isGameDraw = value;
                NotifyPropertyChanged("IsGameDraw");
            }
        }

        public bool BoardButtonsEnabled
        {
            get { return _boardButtonsEnabled; }
            set
            {
                _boardButtonsEnabled = value;
                NotifyPropertyChanged("BoardButtonsEnabled");
            }
        }

        public int GameWinner
        {
            get { return _gameWinner; }
            set
            {
                _gameWinner = value;
                NotifyPropertyChanged("GameWinner");
            }
        }

        public Command SelectCellCommand { get; set; }
        public Command PlayAgainCommand { get; set; }
        public Command MainMenuCommand { get; set; }

        #endregion

        public GameScreenViewModel()
        {
            SelectCellCommand = new Command<string>(SelectCell);
            MainMenuCommand = new Command(OpenMainMenu);
            PlayAgainCommand = new Command(PlayAgain);
            BoardButtonsEnabled = true;
        }

        #region Helper Methods

        private void SelectCell(string cellId)
        {
            GameModel = Program.gameManager.gameEngine.TakePlayerTurn(GameModel, cellId, out bool isGameWon, out bool isGameDraw);

            IsGameWon = isGameWon;
            if (IsGameWon)
                GameWinner = GameModel.PlayerOne.IsPlayerTurn ? GameModel.PlayerOne.PlayerId : GameModel.PlayerTwo.PlayerId;

            if(!IsGameWon)
                IsGameDraw = isGameDraw;

            BoardButtonsEnabled = !(IsGameWon || IsGameDraw);

            if (IsGameWon || isGameDraw)
                return;

            // If next player is AI make it take its turn.
            if (Program.gameManager.gameEngine.GetTurnPlayer(GameModel).PlayerType == PlayerType.AI)
                SelectCellAI();
        }

        public void SelectCellAI()
        {
            GameModel = Program.gameManager.gameEngine.TakeAITurn(GameModel, out bool isGameWonAI, out bool isGameDrawAI);

            IsGameWon = isGameWonAI;
            if (IsGameWon)
                GameWinner = GameModel.PlayerOne.IsPlayerTurn ? GameModel.PlayerOne.PlayerId : GameModel.PlayerTwo.PlayerId;

            if (!IsGameWon)
                IsGameDraw = isGameDrawAI;

            BoardButtonsEnabled = !(IsGameWon || IsGameDraw);
        }

        private void OpenMainMenu()
        {
            Program.ReturnToMainMenu();
        }

        private void PlayAgain()
        {
            // If a AI player exists, the gamemode must be singelplayer.
            if(GameModel.PlayerOne.PlayerType == PlayerType.AI || GameModel.PlayerTwo.PlayerType == PlayerType.AI)
            {
                Program.gameManager.CreateSingleplayerGame();
            }
            else
            {
                Program.gameManager.CreateMultiplayerGame();
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
