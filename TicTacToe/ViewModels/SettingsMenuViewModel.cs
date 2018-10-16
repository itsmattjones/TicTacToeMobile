using System.ComponentModel;
using TicTacToe.Models;
using Xamarin.Forms;

namespace TicTacToe.ViewModels
{
    public class SettingsMenuViewModel : INotifyPropertyChanged
    {
        #region Fields

        private AIDifficulty _chosenDifficulty;

        #endregion

        #region Properties

        public Command ChangeDifficultyCommand { get; set; }
        public Command MainMenuCommand { get; set; }

        /// <summary>
        /// The chosen game difficulty.
        /// </summary>
        public AIDifficulty ChosenDifficulty
        {
            get { return _chosenDifficulty; }
            set
            {
                _chosenDifficulty = value;
                NotifyPropertyChanged("ChosenDifficulty");
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the SettingsMenuViewModel class.
        /// </summary>
        public SettingsMenuViewModel()
        {
            ChangeDifficultyCommand = new Command<string>(ChangeDifficulty);
            MainMenuCommand = new Command(ShowMainMenu);
            ChosenDifficulty = Program.aiDifficulty;
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
                    Program.aiDifficulty = AIDifficulty.Easy;
                    break;
                case "medium":
                    Program.aiDifficulty = AIDifficulty.Medium;
                    break;
                case "hard":
                    Program.aiDifficulty = AIDifficulty.Hard;
                    break;
            }

            ChosenDifficulty = Program.aiDifficulty;
        }

        /// <summary>
        /// Shows the main menu.
        /// </summary>
        private void ShowMainMenu()
        {
            Program.ReturnToMainMenu();
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
