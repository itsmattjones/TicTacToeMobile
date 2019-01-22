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

        #endregion

        #region Properties

        public Command ShowDifficultySelectionDialogCommand { get; set; }
        public Command ChangeDifficultyCommand { get; set; }
        public Command MainMenuCommand { get; set; }

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

        #endregion

        /// <summary>
        /// Initializes a new instance of the SettingsMenuViewModel class.
        /// </summary>
        public SettingsMenuViewModel()
        {
            ShowDifficultySelectionDialogCommand = new Command(ShowDifficultySelectionDialog);
            ChangeDifficultyCommand = new Command<string>(ChangeDifficulty);
            MainMenuCommand = new Command(ShowMainMenu);
            ChosenDifficulty = Program.GameManager.AiDifficulty;
            ShowDifficultySelection = false;
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
