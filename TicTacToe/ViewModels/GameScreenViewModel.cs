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

        public Command SelectCellCommand { get; set; }
        public Command PlayAgainCommand { get; set; }
        public Command MainMenuCommand { get; set; }

        /// <summary>
        /// Gets or sets the game model.
        /// </summary>
        public GameModel GameModel
        {
            get { return _gameModel; }
            set
            {
                _gameModel = value;
                NotifyPropertyChanged("GameModel");
            }
        }

        /// <summary>
        /// Game won state.
        /// </summary>
        public bool IsGameWon 
        {
            get { return _isGameWon; }
            set 
            {
                _isGameWon = value;
                NotifyPropertyChanged("IsGameWon");
            }
        }

        /// <summary>
        /// Game draw state.
        /// </summary>
        public bool IsGameDraw
        {
            get { return _isGameDraw; }
            set
            {
                _isGameDraw = value;
                NotifyPropertyChanged("IsGameDraw");
            }
        }

        /// <summary>
        /// Whether the board buttons are enabled/disabled.
        /// </summary>
        public bool BoardButtonsEnabled
        {
            get { return _boardButtonsEnabled; }
            set
            {
                _boardButtonsEnabled = value;
                NotifyPropertyChanged("BoardButtonsEnabled");
            }
        }

        /// <summary>
        /// The PlayerID of the game winner.
        /// </summary>
        public int GameWinner
        {
            get { return _gameWinner; }
            set
            {
                _gameWinner = value;
                NotifyPropertyChanged("GameWinner");
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the GameScreenViewModel class.
        /// </summary>
        public GameScreenViewModel()
        {
            SelectCellCommand = new Command<string>(SelectCell);
            MainMenuCommand = new Command(OpenMainMenu);
            PlayAgainCommand = new Command(PlayAgain);
            BoardButtonsEnabled = true;
        }

        #region Helper Methods

        /// <summary>
        /// Selects the cell for a player.
        /// </summary>
        /// <param name="cellId">Cell identifier.</param>
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

        /// <summary>
        /// Selects the cell for a AI player.
        /// </summary>
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

        /// <summary>
        /// Opens the main menu.
        /// </summary>
        private void OpenMainMenu()
        {
            Program.ReturnToMainMenu();
        }

        /// <summary>
        /// Starts a new game, the same as the current.
        /// </summary>
        private void PlayAgain()
        {
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
