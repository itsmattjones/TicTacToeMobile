using System.ComponentModel;
using TicTacToe.Models.Player;
using Xamarin.Forms;

namespace TicTacToe.ViewModels
{
    public class SettingsMenuViewModel : INotifyPropertyChanged
    {
        #region Fields

        private AiDifficulty _chosenDifficulty;
        private bool _showDifficultySelection;
        private bool _showPlayerOneAvatarSelection;
        private bool _showPlayerTwoAvatarSelection;
        public int _playerOneAvatar;
        public int _playerTwoAvatar;

        #endregion

        #region Properties

        public Command ShowDifficultySelectionDialogCommand { get; set; }
        public Command ShowP1AvatarSelectionDialogCommand { get; set; }
        public Command ShowP2AvatarSelectionDialogCommand { get; set; }
        public Command ChangeDifficultyCommand { get; set; }
        public Command MainMenuCommand { get; set; }
        public Command ChangePlayerOneAvatarCommand { get; set; }
        public Command ChangePlayerTwoAvatarCommand { get; set; }

        /// <summary>
        /// The chosen game difficulty.
        /// </summary>
        public AiDifficulty ChosenDifficulty
        {
            get { return _chosenDifficulty; }
            set
            {
                _chosenDifficulty = value;
                NotifyPropertyChanged("ChosenDifficulty");
            }
        }

        /// <summary>
        /// Whether to show difficulty selection dialog.
        /// </summary>
        public bool ShowDifficultySelection
        {
            get { return _showDifficultySelection; }
            set
            {
                _showDifficultySelection = value;
                NotifyPropertyChanged("ShowDifficultySelection");
            }
        }

        /// <summary>
        /// Whether to show player one avatar selection.
        /// </summary>
        public bool ShowPlayerOneAvatarSelection
        {
            get { return _showPlayerOneAvatarSelection; }
            set
            {
                _showPlayerOneAvatarSelection = value;
                NotifyPropertyChanged("ShowPlayerOneAvatarSelection");
            }
        }

        /// <summary>
        /// Whether to show player two avatar selection.
        /// </summary>
        public bool ShowPlayerTwoAvatarSelection
        {
            get { return _showPlayerTwoAvatarSelection; }
            set
            {
                _showPlayerTwoAvatarSelection = value;
                NotifyPropertyChanged("ShowPlayerTwoAvatarSelection");
            }
        }

        /// <summary>
        /// Avatar id for player one.
        /// </summary>
        public int PlayerOneAvatar
        {
            get { return _playerOneAvatar; }
            set
            {
                _playerOneAvatar = value;
                NotifyPropertyChanged("PlayerOneAvatar");
            }
        }

        /// <summary>
        /// Avatar id for player two.
        /// </summary>
        public int PlayerTwoAvatar
        {
            get { return _playerTwoAvatar; }
            set
            {
                _playerTwoAvatar = value;
                NotifyPropertyChanged("PlayerTwoAvatar");
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the SettingsMenuViewModel class.
        /// </summary>
        public SettingsMenuViewModel()
        {
            ShowDifficultySelectionDialogCommand = new Command(ShowDifficultySelectionDialog);
            ChangeDifficultyCommand = new Command<string>(ChangeDifficulty);
            MainMenuCommand = new Command(ShowMainMenu);
            ChangePlayerOneAvatarCommand = new Command<string>(ChangePlayerOneAvatar);
            ChangePlayerTwoAvatarCommand = new Command<string>(ChangePlayerTwoAvatar);
            ShowP1AvatarSelectionDialogCommand = new Command(ShowPlayerOneAvatarSelectionDialog);
            ShowP2AvatarSelectionDialogCommand = new Command(ShowPlayerTwoAvatarSelectionDialog);
            ChosenDifficulty = Program.GameManager.AiDifficulty;
            PlayerOneAvatar = Program.GameManager.PlayerOneAvatar;
            PlayerTwoAvatar = Program.GameManager.PlayerTwoAvatar;
            ShowDifficultySelection = false;
            ShowPlayerOneAvatarSelection = false;
            ShowPlayerTwoAvatarSelection = false;

        }

        #region Helper Methods

        /// <summary>
        /// Changes the difficulty.
        /// </summary>
        /// <param name="difficultyType">Difficulty.</param>
        private void ChangeDifficulty(string difficultyType)
        {
            if (string.IsNullOrEmpty(difficultyType))
                return;

            switch(difficultyType)
            {
                case "easy":
                    Program.GameManager.AiDifficulty = AiDifficulty.Easy;
                    break;
                case "medium":
                    Program.GameManager.AiDifficulty = AiDifficulty.Medium;
                    break;
                case "hard":
                    Program.GameManager.AiDifficulty = AiDifficulty.Hard;
                    break;
            }

            ChosenDifficulty = Program.GameManager.AiDifficulty;
            ShowDifficultySelection = false;
        }

        /// <summary>
        /// Shows the main menu.
        /// </summary>
        private void ShowMainMenu()
        {
            Program.ReturnToMainMenu();
        }

        /// <summary>
        /// Shows the difficulty selection dialog.
        /// </summary>
        private void ShowDifficultySelectionDialog()
        {
            ShowDifficultySelection = true;
        }

        /// <summary>
        /// Shows the player one avatar selection dialog.
        /// </summary>
        private void ShowPlayerOneAvatarSelectionDialog()
        {
            ShowPlayerOneAvatarSelection = true;
        }

        /// <summary>
        /// Shows the player two avatar selection dialog.
        /// </summary>
        private void ShowPlayerTwoAvatarSelectionDialog()
        {
            ShowPlayerTwoAvatarSelection = true;
        }

        /// <summary>
        /// Changes player ones avatar to the given avatar.
        /// </summary>
        private void ChangePlayerOneAvatar(string avatar)
        {
            PlayerOneAvatar = int.Parse(avatar);
            Program.GameManager.PlayerOneAvatar = PlayerOneAvatar;
            ShowPlayerOneAvatarSelection = false;
        }

        /// <summary>
        /// Changes player twos avatar to the given avatar.
        /// </summary>
        private void ChangePlayerTwoAvatar(string avatar)
        {
            PlayerTwoAvatar = int.Parse(avatar);
            Program.GameManager.PlayerTwoAvatar = PlayerTwoAvatar;
            ShowPlayerTwoAvatarSelection = false;
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
